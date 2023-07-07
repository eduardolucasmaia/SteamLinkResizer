using System.Diagnostics;

namespace SteamLink
{
    public static class FindWindowUtil
    {
        public static bool GetProcessesByName(string processesName, string? windowTitle = null)
        {
            var processClient = Process.GetProcessesByName(processesName);
            if (processClient != null && processClient.Length > 0)
            {
                if (string.IsNullOrEmpty(windowTitle))
                {
                    return true;
                }

                if (processClient.Where(x => x.MainWindowTitle.ToLower().Contains(windowTitle.ToLower())).Any())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ProcessIsFirstWindow(string processesName, string? windowTitle = null)
        {
            var processClient = Process.GetProcessesByName(processesName);
            if (processClient != null && processClient.Length > 0)
            {
                if (string.IsNullOrEmpty(windowTitle))
                {
                    return true;
                }

                var processForegroundWindow = processClient.Where(x => x.MainWindowHandle == User32.GetForegroundWindow()).FirstOrDefault();

                if (processForegroundWindow != null && processForegroundWindow.MainWindowTitle.ToLower().Contains(windowTitle.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
