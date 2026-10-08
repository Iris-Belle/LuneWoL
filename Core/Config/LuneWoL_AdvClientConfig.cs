namespace LuneWoL.Core.Config;

[BackgroundColor(35, 80, 130, 200)]
public class LuneWoL_AdvClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [SeparatePage]
    public class ClientDepthPressurePage
    {
        [Header("Performance")]

        [Range(0, 16), Slider]
        public int UpdateIntervalTicks { get; set; }

        [Range(0, 120)]
        public int MurkyWaterUpdateTicks { get; set; }

        [Range(-1, 16384)]
        public int TileScanLimit { get; set; }

        [Header("Debug")]
        
        public bool ShowDebugMarker { get; set; }

        public bool ShowScannedDebug { get; set; }

        public bool DebugText { get; set; }

        public ShowDebugMarkerColourPage ShowDebugMarkerColour { get; set; } = new();
        public class ShowDebugMarkerColourPage
        {
            [Range(0, 255)]
            public int MarkerR { get; set; }

            [Range(0, 255)]
            public int MarkerG { get; set; }

            [Range(0, 255)]
            public int MarkerB { get; set; }

            [Range(0, 255)]
            public int MarkerA { get; set; }

            public ShowDebugMarkerColourPage()
            {
                MarkerR = 255;
                MarkerG = 255;
                MarkerB = 255;
                MarkerA = 255;
            }
        }

        public ShowScannedDebugColourPage ShowScannedDebugColour { get; set; } = new();
        public class ShowScannedDebugColourPage
        {
            [Range(0, 255)]
            public int ScanR { get; set; }

            [Range(0, 255)]
            public int ScanG { get; set; }

            [Range(0, 255)]
            public int ScanB { get; set; }

            [Range(0, 255)]
            public int ScanA { get; set; }

            public ShowScannedDebugColourPage()
            {
                ScanR = 128;
                ScanG = 128;
                ScanB = 255;
                ScanA = 16;
            }
        }

        public ClientDepthPressurePage()
        {
            UpdateIntervalTicks = 1;
            TileScanLimit = 512;
            ShowDebugMarker = false;
            ShowScannedDebug = false;
            DebugText = false;
        }
    }

    [ColourPalette, BackgroundColor(180, 215, 255, 200)]
    public ClientDepthPressurePage ClientDepthPressure = new();

    public override void OnLoaded() => AdvClientConfig = this;
}