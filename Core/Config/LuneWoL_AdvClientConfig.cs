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

        [Range(-1, 16384)]
        public int TileScanLimit { get; set; }

        [Header("Debug")]
        
        public bool ShowSurfaceDebug { get; set; }
        
        public bool DrawScannedTiles { get; set; }
        
        public bool DebugText { get; set; }

        public ClientDepthPressurePage()
        {
            UpdateIntervalTicks = 2;
            TileScanLimit = 1024;
            ShowSurfaceDebug = false;
            DrawScannedTiles = false;
            DebugText = false;
        }
    }

    [ColourPalette, BackgroundColor(180, 215, 255, 200)]
    public ClientDepthPressurePage ClientDepthPressure = new();

    public override void OnLoaded() => AdvClientConfig = this;
}