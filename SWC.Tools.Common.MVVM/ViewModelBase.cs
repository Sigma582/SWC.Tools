using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using SWC.Tools.Common.Enums;
using SWC.Tools.Common.MVVM.Annotations;

namespace SWC.Tools.Common.MVVM
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected ActionCommand _serverSelectCommand;
        private const string WINDOWS_URL_KEY = "windowsServerUrl";
        private const string ANDROID_URL_KEY = "androidServerUrl";


        protected void Debug(string message)
        {
            File.AppendAllLines("debug.log", new[] {string.Format("{0:yyyy-MM-dd HH:mm:ss} | DEBUG: {1}", DateTime.Now, message)});
        }

        protected void OnError(Exception error)
        {
            Log(error);
            MessageBox.Show(error.ToString());
        }

        protected void Log(Exception error)
        {
            File.AppendAllLines("error.log", new[] {string.Format("{0:yyyy-MM-dd HH:mm:ss} | ERROR: {1}", DateTime.Now, error)});
        }

        protected static void SaveToConfig(string key, string value)
        {
            var c = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            c.AppSettings.Settings.Remove(key);
            c.AppSettings.Settings.Add(key, value);
            c.Save();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected string GetServerUrl()
        {
            var key = ANDROID_URL_KEY;
            return ConfigurationManager.AppSettings[key];
        }
    }
}
