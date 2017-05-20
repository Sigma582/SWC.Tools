using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Mantin.Controls.Wpf.Notification;
using Microsoft.Win32;
using SWC.Tools.Common.AuthData;
using SWC.Tools.Common.Enums;
using SWC.Tools.Common.Layout;
using SWC.Tools.Common.MVVM;
using SWC.Tools.Common.Networking;
using SWC.Tools.Common.Networking.Json.CommandArgs;
using SWC.Tools.Common.Networking.Json.Entities;

namespace SWC.Tools.DefenseTracker.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private const string TARGET_PLAYER_ID = "targetPlayerId";
        private const string LISTENER_PLAYER_ID = "listenerPlayerId";
        private const string LISTENER_PLAYER_SECRET = "listenerPlayerSecret";
        private const string SELECTED_SERVER = "selectedServer";
        private const string NOTIFY_ALWAYS = "notifyAlways";
        private const string NOTIFY_ON_PROTECTION = "notifyOnProtection";
        private const string NOTIFY_ON_SC = "notifyOnSc";
        private const string SC_UNITS_NOTIFICATION_COUNT = "scUnitsNotificationCount";

        private const int TIMER_PERIOD = 60 * 1000;

        private MessageManager _messageManager;
        private string _targetPlayerId;
        private Timer _timer;
        private List<Battle> _battleLogs;
        private int? _scUnitsCount;
        private int _scUnitsNotificationCount;
        private bool _notifyAlways;
        private bool _notifyOnSc;
        private bool _notifyOnProtection;
        private int? _protectedUntil;

        public ActionCommand StartCommand { get; }
        public ActionCommand StopCommand { get; }

        public string TargetPlayerId
        {
            get { return _targetPlayerId; }
            set
            {
                _targetPlayerId = value;
                OnPropertyChanged();
            }
        }

        public List<Battle> BattleLogs
        {
            get { return _battleLogs; }
            private set
            {
                _battleLogs = value;
                OnPropertyChanged();
            }
        }

        public int? ScUnitsCount
        {
            get { return _scUnitsCount; }
            private set
            {
                _scUnitsCount = value;
                OnPropertyChanged();
            }
        }

        public int? ProtectedUntil
        {
            get { return _protectedUntil; }
            set
            {
                _protectedUntil = value;
                OnPropertyChanged();
            }
        }

        public int ScUnitsNotificationCount
        {
            get { return _scUnitsNotificationCount; }
            set
            {
                _scUnitsNotificationCount = value;
                OnPropertyChanged();
            }
        }

        public bool NotifyOnProtection
        {
            get { return _notifyOnProtection; }
            set
            {
                _notifyOnProtection = value;
                OnPropertyChanged();
            }
        }

        public bool NotifyOnSc
        {
            get { return _notifyOnSc; }
            set
            {
                _notifyOnSc = value;
                OnPropertyChanged();
            }
        }

        public bool NotifyAlways
        {
            get { return _notifyAlways; }
            set
            {
                _notifyAlways = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler<BattleEventArgs> BattleOccured;

        public MainViewModel()
        {
            StartCommand = new ActionCommand(Start, true);
            StopCommand = new ActionCommand(Stop);

            ReadConfig();
        }

        private void Start(object o)
        {
            StartCommand.Enabled = false;

            ThreadPool.QueueUserWorkItem((obj) =>
            {
                var listenerPlayerId = ConfigurationManager.AppSettings[LISTENER_PLAYER_ID];
                var listenerPlayerSecret = ConfigurationManager.AppSettings[LISTENER_PLAYER_SECRET];
                _messageManager = new MessageManager(GetServerUrl(), listenerPlayerId, listenerPlayerSecret);
                _messageManager.Init();
                if (string.IsNullOrEmpty(listenerPlayerId))
                {
                    SaveToConfig(LISTENER_PLAYER_ID, _messageManager.PlayerId);
                    SaveToConfig(LISTENER_PLAYER_SECRET, _messageManager.PlayerSecret);
                }

                SaveToConfig(SELECTED_SERVER, SelectedServer.ToString());
                SaveToConfig(NOTIFY_ALWAYS, NotifyAlways.ToString());
                SaveToConfig(NOTIFY_ON_PROTECTION, NotifyOnProtection.ToString());
                SaveToConfig(NOTIFY_ON_SC, NotifyOnSc.ToString());
                SaveToConfig(SC_UNITS_NOTIFICATION_COUNT, ScUnitsNotificationCount.ToString());
                SaveToConfig(TARGET_PLAYER_ID, TargetPlayerId);

                var player = _messageManager.VisitNeighbor(TargetPlayerId);
                ScUnitsCount = player.PlayerModel.DonatedTroops.SelectMany(kvp => kvp.Value).Sum(kvp => kvp.Value);
                BattleLogs = player.PlayerModel.BattleLogs.Where(b => b.Defender.PlayerId == TargetPlayerId).ToList();

                _timer = new Timer(Refresh, TargetPlayerId, 1, TIMER_PERIOD);

                StopCommand.Enabled = true;
            });
        }

        private void Stop(object o)
        {
            StopCommand.Enabled = false;
            _timer?.Dispose();
            StartCommand.Enabled = true;
        }

        private void Refresh(object state)
        {
            try
            {
                var scNotification = false;
                var protectionNotification = false;
                var newBattle = false;

                var playerId = (string) state;

                var player = _messageManager.VisitNeighbor(playerId);

                var lastDefenseDateOld = BattleLogs.Where(b => b.Defender.PlayerId == TargetPlayerId)
                    .Max(b => b.AttackDate);
                var lastDefenseDateNew = player.PlayerModel.BattleLogs.Where(b => b.Defender.PlayerId == TargetPlayerId)
                    .Max(b => b.AttackDate);

                var newScUnitsCount = player.PlayerModel.DonatedTroops.SelectMany(kvp => kvp.Value)
                    .Sum(kvp => kvp.Value);

                if (lastDefenseDateOld < lastDefenseDateNew)
                {
                    newBattle = true;
                }

                if (newScUnitsCount < ScUnitsNotificationCount)
                {
                    scNotification = true;
                }

                if (player.PlayerModel.ProtectedUntil != null)
                {
                    protectionNotification = true;
                }

                BattleLogs = player.PlayerModel.BattleLogs.Where(b => b.Defender.PlayerId == TargetPlayerId).ToList();
                ScUnitsCount = newScUnitsCount;
                ProtectedUntil = player.PlayerModel.ProtectedUntil;

                if (newBattle && (NotifyAlways || scNotification && NotifyOnSc || protectionNotification && NotifyOnProtection))
                {
                    Debug("Battle occurred");
                    OnBattleOccured(player);
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
            finally
            {
                _timer = new Timer(Refresh, TargetPlayerId, TIMER_PERIOD, 0);
            }
        }

        private void ReadConfig()
        {
            bool notifyAlways;
            bool notifyOnProtection;
            bool notifyOnSc;
            int scUnitsNotificationCount;
            Server selectedServer;

            bool.TryParse(ConfigurationManager.AppSettings[NOTIFY_ALWAYS], out notifyAlways);
            bool.TryParse(ConfigurationManager.AppSettings[NOTIFY_ON_PROTECTION], out notifyOnProtection);
            bool.TryParse(ConfigurationManager.AppSettings[NOTIFY_ON_SC], out notifyOnSc);
            int.TryParse(ConfigurationManager.AppSettings[SC_UNITS_NOTIFICATION_COUNT], out scUnitsNotificationCount);
            Enum.TryParse(ConfigurationManager.AppSettings[SELECTED_SERVER], out selectedServer);
            TargetPlayerId = ConfigurationManager.AppSettings[TARGET_PLAYER_ID];

            NotifyAlways = notifyAlways;
            NotifyOnProtection = notifyOnProtection;
            NotifyOnSc = notifyOnSc;
            ScUnitsNotificationCount = scUnitsNotificationCount;
            SelectedServer = selectedServer;
        }

        private void OnBattleOccured(Player player)
        {
            var lastDefense = player.PlayerModel.BattleLogs.Last(b => b.Defender.PlayerId == player.PlayerId);
            var scUnitsCount = player.PlayerModel.DonatedTroops.SelectMany(kvp => kvp.Value).Sum(kvp => kvp.Value);

            BattleOccured?.Invoke(this, new BattleEventArgs(lastDefense, scUnitsCount));
        }
    }
}