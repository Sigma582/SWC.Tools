using System;
using SWC.Tools.Common.Networking.Json.Entities;

namespace SWC.Tools.DefenseTracker.ViewModels
{
    public class BattleEventArgs : EventArgs
    {
        public Battle Battle { get; set; }
        public int ScUnitsCountRemaining { get; set; }

        public BattleEventArgs(Battle battle, int scUnitsCountRemaining)
        {
            Battle = battle;
            ScUnitsCountRemaining = scUnitsCountRemaining;
        }
    }
}