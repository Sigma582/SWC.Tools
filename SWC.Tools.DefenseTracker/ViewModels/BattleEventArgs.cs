using System;
using SWC.Tools.Common.Networking.Json.Entities;

namespace SWC.Tools.DefenseTracker.ViewModels
{
    public class BattleEventArgs : EventArgs
    {
        public Battle Battle { get; }
        public int ScUnitsCountRemaining { get; }

        public BattleEventArgs(Battle battle, int scUnitsCountRemaining)
        {
            Battle = battle;
            ScUnitsCountRemaining = scUnitsCountRemaining;
        }
    }
}