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
using System.Collections.Generic;
using SWC.Tools.Common.Networking.Exception;
using SWC.Tools.Common;
using System.Text;
using SWC.Tools.Common.Util;

namespace SWC.Tools.LayoutManager.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private const string LAST_PLAYER_ID_KEY = "lastPlayerId";
        private const string LAST_PLAYER_SECRET_KEY = "lastPlayerSecret";

        private readonly ActionCommand _saveLayoutCommand;
        private readonly ActionCommand _loadLayoutCommand;
        private readonly ActionCommand _loginCommand;
        private readonly ActionCommand _gameroomCredentialsCommand;
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
        public ICommand LoginCommand
        {
            get { return _loginCommand; }
        }

        public ICommand GameroomCredentialsCommand
        {
            get { return _gameroomCredentialsCommand; }
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
            _loginCommand = new ActionCommand(LoginByPlayerId, true);
            _gameroomCredentialsCommand = new ActionCommand(GetGameroomCredentials, true);

            ReadConfig();
        }

        private void ReadConfig()
        {
            PlayerId = ConfigurationManager.AppSettings[LAST_PLAYER_ID_KEY];
            PlayerSecret = ConfigurationManager.AppSettings[LAST_PLAYER_SECRET_KEY];
        }

        private void LoginByPlayerId(object arg)
        {
            var authData = new AuthData(PlayerId, PlayerSecret);
            SaveToConfig(LAST_PLAYER_ID_KEY, PlayerId);
            SaveToConfig(LAST_PLAYER_SECRET_KEY, PlayerSecret);
            ThreadPool.QueueUserWorkItem(Login, authData);
        }

        private void GetGameroomCredentials(object arg)
        {
            var reg = Registry.CurrentUser.OpenSubKey(@"Software\The Walt Disney Company\Commander");

            if (reg == null)
            {
                MessageBox.Show("Can't get login data from FB Gameroom.");
                return;
            }

            var playerIdBytes = reg.GetValue(reg.GetValueNames().FirstOrDefault(name => name.StartsWith("prefPlayerId")));
            var playerSecretBytes = reg.GetValue(reg.GetValueNames().FirstOrDefault(name => name.StartsWith("prefPlayerSecret")));

            if (playerIdBytes == null || playerSecretBytes == null)
            {
                MessageBox.Show("Can't get login data from FB Gameroom.");
                return;
            }

            var playerId = Encoding.UTF8.GetString((byte[])playerIdBytes);
            var playerSecret = Encoding.UTF8.GetString((byte[])playerSecretBytes);

            PlayerId = playerId.Substring(0, playerId.Length - 1);
            PlayerSecret = playerSecret.Substring(0, playerSecret.Length - 1);
        }

        private void Login(object arg)
        {
            try
            {
                _loginCommand.Enabled = false;
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
            List<Building> unmappedBuildings = null;

            try
            {
                DisableCommands();

                var parameter = (Tuple<string, BaseType>) arg;

                var lm = new LayoutLoader();
                var savedLayout = lm.Load(parameter.Item1); //.OrderBy(b => b.X).ThenBy(b => -b.Z)
                var currentLayout = _messageManager.GetOwnBase();

                var match = MapUtil.Match(savedLayout, currentLayout);
                unmappedBuildings = match.Where(t => t.Item1 == null).Select(t => t.Item2).ToList();

                var newLayout = match.Select(tuple => 
                {
                    if (tuple.Item1 != null && tuple.Item2 != null)
                    {
                        tuple.Item2.X = tuple.Item1.X;
                        tuple.Item2.Z = tuple.Item1.Z;
                    }
                    return tuple.Item2;
                });

                if (parameter.Item2 == BaseType.War)
                {
                    _messageManager.UpdateWarLayout(newLayout.ToDictionary(b => b.Key, b => new Position(b.X, b.Z)));
                }
                else
                {
                    _messageManager.UpldateLayout(newLayout.ToDictionary(b => b.Key, b => new Position(b.X, b.Z)));
                    foreach (var b1 in newLayout.Where(b => b.Type == Building.BARRACKS || b.Type == Building.FACTORY).OrderByDescending(b => b.Z).ThenBy(b => b.X))
                    {
                        _messageManager.UpldateLayout(new [] { b1 }.ToDictionary(b => b.Key, b => new Position(b.X, b.Z)));
                    }
                }
                MessageBox.Show("Layout loaded");
            }
            catch (StatusException ex)
            {
                if (ex.StatusCode == ServerConstants.BUILDING_OVERLAP_WITH_ANOTHER)
                {
                    ProcessStatus1010(unmappedBuildings);
                }
                else
                {
                    OnError(ex);
                }
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

        private void ProcessStatus1010(List<Building> unmappedBuildings)
        {
            const int buildingsToList = 10;

            var message = new StringBuilder("Can't load layout because one or more buildings overlap with other building(s) and/or junk.");
            if (unmappedBuildings.Count > 0)
            {
                message.AppendLine("\nThe following buildings have no matching structures in the layout file you are loading and can't be moved:");

                for (int i = 0; i < Math.Min(unmappedBuildings.Count, buildingsToList); i++)
                {
                    var b = unmappedBuildings[i];
                    message.AppendFormat("    * {0} level {1}, current location: ({2};{3})\n", b.Type, b.Level, b.X, b.Z);
                }

                if(unmappedBuildings.Count > buildingsToList)
                {
                    message.AppendFormat("    ... and {0} more.\n", unmappedBuildings.Count - buildingsToList);
                }
            }
            var junkCount = _messageManager.GetOwnBase().Count(b => b.IsJunk);
            if (junkCount > 0)
            {
                message.AppendFormat("\nThere {1} {0} junk element{2} in your base.\n", junkCount, junkCount == 1 ? "is" : "are", junkCount == 1 ? "" : "s");
            }
            MessageBox.Show(message.ToString());
        }

        private void DisableCommands()
        {
            Application.Current.Dispatcher.Invoke(() => _saveLayoutCommand.Enabled = _loadLayoutCommand.Enabled = _gameroomCredentialsCommand.Enabled = false);
        }

        private void EnableCommands()
        {
            Application.Current.Dispatcher.Invoke(() => _saveLayoutCommand.Enabled = _loadLayoutCommand.Enabled = _gameroomCredentialsCommand.Enabled = true);
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