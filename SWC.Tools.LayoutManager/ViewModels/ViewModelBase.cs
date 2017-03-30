using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using SWC.Tools.LayoutManager.Annotations;

namespace SWC.Tools.LayoutManager.ViewModels
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Log(string error)
        {
            File.AppendAllLines("error.log", new[] {error});
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
    }
}
