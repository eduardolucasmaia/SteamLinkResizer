using SteamLink.Properties;
using System.Globalization;

namespace SteamLink
{
    public partial class SteamLink : Form
    {

        CancellationTokenSource cts;

        /// <summary>
        /// Encapsulates the display settings. Initialized by the constructor of the form.
        /// </summary>
        private readonly DisplaySettings _originalSettings;
        private DisplaySettings _currentSettings;
        private DisplaySettings? _selectedSettings;
        private bool _steamBigPicture = false;

        /// <summary>
        /// Initializes a new instance of MainForm.
        /// </summary>
        public SteamLink()
        {
            InitializeComponent();
            _originalSettings = DisplayManager.GetCurrentSettings();
            _currentSettings = DisplayManager.GetCurrentSettings();
            this.Icon = Resources.steam_link_resize;
        }

        private void SteamLink_Load(object sender, EventArgs e)
        {
            ListAllModes();
            InitializeValues();
        }

        private void InitializeValues()
        {
            tbTitleName.Text = Settings.Default.Value_TitleName;
            tbProcess.Text = Settings.Default.Value_ProcessName;
            if (Settings.Default.Value_Selected_Resolution != -1)
                modesListView.Items[Settings.Default.Value_Selected_Resolution].Selected = true;
            btnSave.Enabled = false;
        }

        protected void ThreadStartSteamBigPicture()
        {
            try
            {
                Task.Factory.StartNew(async () =>
                await LoopSteamBigPictureFirstWindow(cts.Token), cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        protected async Task LoopSteamBigPictureFirstWindow(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    _steamBigPicture = FindWindowUtil.GetProcessesByName(tbProcess.Text, tbTitleName.Text);

                    if (_steamBigPicture && _selectedSettings != null && !_currentSettings.Equals(_selectedSettings))
                    {
                        DisplayManager.SetDisplaySettings(_selectedSettings.Value);
                        _currentSettings = _selectedSettings.Value;
                        await Task.Delay(1000, cancellationToken);
                    }
                    else if (!_steamBigPicture && !_currentSettings.Equals(_originalSettings))
                    {
                        DisplayManager.SetDisplaySettings(_originalSettings);
                        _currentSettings = _originalSettings;
                        await Task.Delay(1000, cancellationToken);
                    }

                }
                catch (Exception ex)
                {
                    _steamBigPicture = false;
                    if (!_currentSettings.Equals(DisplayManager.GetCurrentSettings()))
                    {
                        DisplayManager.SetDisplaySettings(_originalSettings);
                        _currentSettings = _originalSettings;
                        await Task.Delay(1000, cancellationToken);
                    }
                }
                finally
                {
                    try
                    {
                        await Task.Delay(500, cancellationToken);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Loads all supported display modes and lists them in the modes list view.
        /// </summary>
        protected void ListAllModes()
        {
            try
            {
                this.modesListView.BeginUpdate();
                this.modesListView.Items.Clear();

                IEnumerator<DisplaySettings> enumerator = DisplayManager.GetModesEnumerator();

                DisplaySettings set;
                ListViewItem itm;

                while (enumerator.MoveNext())
                {
                    set = enumerator.Current;
                    itm = new ListViewItem(set.Index.ToString(CultureInfo.InvariantCulture));
                    itm.SubItems.Add(set.Width.ToString(CultureInfo.InvariantCulture));
                    itm.SubItems.Add(set.Height.ToString(CultureInfo.InvariantCulture));
                    itm.SubItems.Add(((int)set.Orientation * 90).ToString(CultureInfo.InvariantCulture));
                    itm.SubItems.Add(set.BitCount.ToString(CultureInfo.InvariantCulture));
                    itm.SubItems.Add(set.Frequency.ToString(CultureInfo.InvariantCulture));
                    itm.Tag = set;

                    if (this.modesListView.Items.Count > 0 && this.modesListView.Items[this.modesListView.Items.Count - 1].Tag.Equals(set))
                        continue;

                    this.modesListView.Items.Add(itm);
                }

                this.modesListView.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void modesListView_DoubleClick(object sender, EventArgs e)
        {
            if (this.modesListView.SelectedItems.Count == 0) return;

            DisplaySettings set = (DisplaySettings)this.modesListView.SelectedItems[0].Tag;

            if (MessageBox.Show(this, string.Format(CultureInfo.CurrentCulture,
                Properties.Settings.Default.Msg_Disp_Change, set),
                Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, (MessageBoxOptions)0) == DialogResult.Yes)
            {
                DisplayManager.SetDisplaySettings(set);

                //GetCurrentSettings();
                ListAllModes();
            }
        }

        private void SteamLink_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_currentSettings.Equals(_originalSettings))
            {
                DialogResult result = MessageBox.Show(this, string.Format(CultureInfo.CurrentCulture,
                   Properties.Settings.Default.Msg_Disp_Change_Original, _originalSettings),
                   Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);

                if (result == DialogResult.Cancel)
                    e.Cancel = true;
                else if (result == DialogResult.Yes)
                    DisplayManager.SetDisplaySettings(_originalSettings);
            }
        }

        private void modesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender != null && sender is ListView && ((ListView)sender).SelectedItems.Count > 0)
            {
                _selectedSettings = (DisplaySettings)((ListView)sender).SelectedItems[0].Tag;
                btnSave.Enabled = true;
            }
        }

        private void tbProcess_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void tbTitleName_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Value_ProcessName = tbProcess.Text;
            Properties.Settings.Default.Value_TitleName = tbTitleName.Text;
            if (modesListView.SelectedItems != null && modesListView.SelectedItems.Count > 0)
            {
                Properties.Settings.Default.Value_Selected_Resolution = modesListView.SelectedItems[0].Index;
            }
            else
            {
                Properties.Settings.Default.Value_Selected_Resolution = -1;
            }

            Properties.Settings.Default.Save();
            btnSave.Enabled = false;
        }

        private void ckActive_CheckedChanged(object sender, EventArgs e)
        {
            if (ckActive.Checked)
            {
                cts = new CancellationTokenSource();
                ThreadStartSteamBigPicture();
            }
            else
            {
                cts.Cancel();
                DisplayManager.SetDisplaySettings(_originalSettings);
            }
        }
    }
}