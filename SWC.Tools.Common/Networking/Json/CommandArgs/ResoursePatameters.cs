namespace SWC.Tools.Common.Networking.Json.CommandArgs
{
    public class ResoursePatameters
    {
        public ResoursePatameters(int credits, int materials, int contraband, int crystals)
        {
            Credits = credits;
            Materials = materials;
            Contraband = contraband;
            Crystals = crystals;
        }

        public int Credits { get; set; }

        public int Materials { get; set; }

        public int Contraband { get; set; }

        public int Crystals { get; set; }

    }
}