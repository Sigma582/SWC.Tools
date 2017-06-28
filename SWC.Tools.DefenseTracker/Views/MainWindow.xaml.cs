using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Mantin.Controls.Wpf.Notification;
using SWC.Tools.DefenseTracker.ViewModels;

namespace SWC.Tools.DefenseTracker.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            DataContext = _viewModel = new MainViewModel();
            InitializeComponent();

            var ni = new System.Windows.Forms.NotifyIcon
            {
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                Visible = true,
                Text = Title
            };
            ni.DoubleClick +=
                delegate
                {
                    Show();
                    WindowState = WindowState.Normal;
                };

            _viewModel.BattleOccured += ViewModelOnBattleOccured;
        }

        private void ViewModelOnBattleOccured(object sender, BattleEventArgs e)
        {

            var notification = string.Format("Result:            {0}\n", e.Battle.Stars > 0 ? "DEFEAT" : "VICTORY") +
                               string.Format("Damage %:    {0}\n", e.Battle.BaseDamagePercent) +
                               string.Format("Stars scored:  {0}\n", e.Battle.Stars) +
                               string.Format("SC units left:  {0}", e.ScUnitsCountRemaining);

            Application.Current.Dispatcher.Invoke(() =>
            {
                var toast = new ToastPopUp("Defensive battle completed!", notification,
                    NotificationType.Information)
                {
                    Background = new LinearGradientBrush(Color.FromRgb(193, 237, 230), Color.FromRgb(128, 148, 255), 90),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    FontColor = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32)),

                };
                toast.MouseDoubleClick += delegate
                {
                    Show();
                    WindowState = WindowState.Normal;
                };
                toast.Show();
            });
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
            }

            base.OnStateChanged(e);
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int i;
            e.Handled = !int.TryParse(e.Text, out i);
        }
    }
}
