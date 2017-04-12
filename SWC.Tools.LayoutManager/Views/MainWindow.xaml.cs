using System;
using System.Windows;
using SWC.Tools.LayoutManager.ViewModels;

namespace SWC.Tools.LayoutManager.Views
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
            _viewModel.LoginSuccessful += OnLoginSuccessful;
            InitializeComponent();
        }

        private void OnLoginSuccessful(object sender, EventArgs eventArgs)
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            ManagementPanel.Visibility = Visibility.Visible;
        }
    }
}
