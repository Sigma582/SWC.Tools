using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using SWC.Tools.Common.AuthData;
using SWC.Tools.Common.Enums;
using SWC.Tools.Common.Layout;
using SWC.Tools.Common.MVVM;
using SWC.Tools.Common.Networking;
using SWC.Tools.Common.Networking.Json.CommandArgs;
using SWC.Tools.Common.Networking.Json.Entities;

namespace SWC.Tools.LayoutManager.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private const string LAST_PLAYER_ID_KEY = "lastPlayerId";
        private const string LAST_PLAYER_SECRET_KEY = "lastPlayerSecret";

        private readonly ActionCommand _saveLayoutCommand;
        private readonly ActionCommand _loadLayoutCommand;
        private readonly ActionCommand _browseCommand;
        private readonly ActionCommand _loginCommand;
        private string _playerprefsPath;
        private string _playerName;
        private MessageManager _messageManager;
        private Player _player;
        private bool _canAdjustTimestamp;
        private string _playerId;
        private string _playerSecret;

        public string PlayerprefsPath
        {
            get { return _playerprefsPath; }
            set
            {
                _playerprefsPath = value;
                OnPropertyChanged();
            }
        }

        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveLayoutCommand
        {
            get { return _saveLayoutCommand; }
        }

        public ICommand LoadLayoutCommand
        {
            get { return _loadLayoutCommand; }
        }

        public ICommand BrowseCommand
        {
            get { return _browseCommand; }
        }

        public ICommand LoginCommand
        {
            get { return _loginCommand; }
        }

        public Player Player
        {
            get { return _player; }
            set
            {
                _player = value;
                OnPropertyChanged();
            }
        }

        public bool OmitTimestamp
        {
            get { return _messageManager.SkipTimestamp; }
            set
            {
                _messageManager.SkipTimestamp = value;
                OnPropertyChanged();
            }
        }

        public int TimestampAdj
        {
            get { return _messageManager.TimestampAdj; }
            set
            {
                _messageManager.TimestampAdj = value;
                OnPropertyChanged();
            }
        }

        public bool CanAdjustTimestamp
        {
            get { return _canAdjustTimestamp; }
            set
            {
                _canAdjustTimestamp = value;
                OnPropertyChanged();
            }
        }

        public string PlayerId
        {
            get { return _playerId; }
            set
            {
                _playerId = value;
                OnPropertyChanged();
            }
        }

        public string PlayerSecret
        {
            get { return _playerSecret; }
            set
            {
                _playerSecret = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler LoginSuccessful;

        public MainViewModel()
        {
            _saveLayoutCommand = new ActionCommand(SaveCommandHandler);
            _loadLayoutCommand = new ActionCommand(LoadCommandHandler);
            _browseCommand = new ActionCommand(LoginByFile, true);
            _loginCommand = new ActionCommand(LoginByPlayerId, true);

            ReadConfig();
        }

        private void ReadConfig()
        {
            PlayerId = ConfigurationManager.AppSettings[LAST_PLAYER_ID_KEY];
            PlayerSecret = ConfigurationManager.AppSettings[LAST_PLAYER_SECRET_KEY];
        }

        private void LoginByFile(object arg)
        {
            try
            {
                var dialog = new OpenFileDialog
                {
                    CheckFileExists = true,
                    CheckPathExists = true,
                    DefaultExt = ".dat",
                    Filter = "playerprefs files (.dat) | *.dat",
                    Title = "Select playerprefs.dat file associated with your base",
                    Multiselect = false
                };
                var res = dialog.ShowDialog();
                if (res != true)
                {
                    return;
                }
                PlayerprefsPath = dialog.FileName;
                var authData = AuthDataProvider.Get(dialog.FileName);
                ThreadPool.QueueUserWorkItem(Login, authData);
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        private void LoginByPlayerId(object arg)
        {
            var authData = new AuthData(PlayerId, PlayerSecret);
            SaveToConfig(LAST_PLAYER_ID_KEY, PlayerId);
            SaveToConfig(LAST_PLAYER_SECRET_KEY, PlayerSecret);
            ThreadPool.QueueUserWorkItem(Login, authData);
        }

        private void Login(object arg)
        {
            try
            {
                var authData = (AuthData) arg;
                _messageManager = new MessageManager(GetServerUrl(), authData.PlayerId, authData.PlayerSecret);
                _messageManager.Refresh();
                Player = _messageManager.GetLoginData();

                if (Player != null)
                {
                    EnableCommands();
                    OnLoginSuccessful();
                }
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        private void SaveCommandHandler(object arg)
        {
            if (Player == null)
            {
                return;
            }
            var dialog = new SaveFileDialog
            {
                CheckPathExists = true,
                DefaultExt = ".json",
                Filter = "Layout files (*.json) | *.json",
                AddExtension = true,
                Title = "Select file to save layout",
            };
            var res = dialog.ShowDialog();
            if (res != true)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(Save, new Tuple<string, BaseType>(dialog.FileName, (BaseType) arg));
        }

        private void Save(object arg)
        {
            try
            {
                DisableCommands();

                var parameter = (Tuple<string, BaseType>) arg;

                _messageManager.Refresh();
                var buildings = parameter.Item2 == BaseType.War
                    ? _messageManager.GetWarParticipant().WarMap.Buildings
                    : Player.PlayerModel.Map.Buildings;

                var lm = new LayoutSaver();
                lm.Save(buildings, parameter.Item1);
                MessageBox.Show("Layout saved");
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
            finally
            {
                EnableCommands();
            }
        }

        private void LoadCommandHandler(object arg)
        {
            if (Player == null)
            {
                return;
            }
            var dialog = new OpenFileDialog
            {
                CheckPathExists = true,
                DefaultExt = ".json",
                Filter = "Layout files (*.json) | *.json",
                AddExtension = true,
                Title = "Select file with saved layout",
            };
            var res = dialog.ShowDialog();
            if (res != true)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(Load, new Tuple<string, BaseType>(dialog.FileName, (BaseType) arg));
        }

        private void Load(object arg)
        {
            try
            {
                DisableCommands();

                var parameter = (Tuple<string, BaseType>) arg;

                var lm = new LayoutLoader();
                var layout = lm.Load(parameter.Item1).OrderBy(b => b.X).ThenBy(b => -b.Z).ToList();
                if (parameter.Item2 == BaseType.War)
                {
                    _messageManager.UpdateWarLayout(layout.ToDictionary(b => b.Key, b => new Position(b.X, b.Z)));
                }
                else
                {
                    _messageManager.UpldateLayout(layout.ToDictionary(b => b.Key, b => new Position(b.X, b.Z)));
                    foreach (var b1 in layout.Where(b => b.Type == Building.BARRACKS || b.Type == Building.FACTORY).OrderByDescending(b => b.Z).ThenBy(b => b.X))
                    {
                        _messageManager.UpldateLayout(new [] { b1 }.ToDictionary(b => b.Key, b => new Position(b.X, b.Z)));
                    }
                }
                MessageBox.Show("Layout loaded");
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
            finally
            {
                EnableCommands();
            }
        }

        private void DisableCommands()
        {
            Application.Current.Dispatcher.Invoke(() => CanAdjustTimestamp = _saveLayoutCommand.Enabled = _loadLayoutCommand.Enabled = false);
        }

        private void EnableCommands()
        {
            Application.Current.Dispatcher.Invoke(() => CanAdjustTimestamp = _saveLayoutCommand.Enabled = _loadLayoutCommand.Enabled = true);
        }

        private void OnLoginSuccessful()
        {
            var e = LoginSuccessful;
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (e != null)
                {
                    e.Invoke(this, new EventArgs());
                }
            });
        }
    }
}