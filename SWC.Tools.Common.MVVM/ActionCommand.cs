using System;
using System.Windows;
using System.Windows.Input;

namespace SWC.Tools.Common.MVVM
{
    public class ActionCommand : ICommand
    {
        private readonly Action<object> _action;
        private bool _canExecute;

        public ActionCommand(Action<object> action, bool enabled = false)
        {
            _action = action;
            _canExecute = enabled;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public bool Enabled
        {
            set
            {
                _canExecute = value;
                if (CanExecuteChanged != null)
                {
                    Application.Current.Dispatcher.Invoke(() => CanExecuteChanged.Invoke(this, new EventArgs()));
                }
            }
            get { return _canExecute; }
        }

        public event EventHandler CanExecuteChanged;
    }
}