namespace LuneWoL.Core.Config;

[BackgroundColor(10, 75, 105, 255)]
public class LWoLAdvancedClientSettings : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [SeparatePage]
    public class ClientDepthPressureDented
    {
        [Header("Performance")]

        [BackgroundColor(35, 115, 145, 255), Range(0, 16), Slider, SliderColor(35, 115, 145, 255)]
        public int UpdateIntervalTicks { get; set; }

        [BackgroundColor(35, 115, 145, 255), Range(-1, 16384)]
        public int TileScanLimit { get; set; }

        [Header("Debug")]
        [BackgroundColor(35, 115, 145, 255)]
        public bool ShowSurfaceDebug { get; set; }

        [BackgroundColor(35, 115, 145, 255)]
        public bool DrawScannedTiles { get; set; }

        [BackgroundColor(35, 115, 145, 255)]
        public bool DebugText { get; set; }

        public ClientDepthPressureDented()
        {
            UpdateIntervalTicks = 2;
            TileScanLimit = 1024;
            ShowSurfaceDebug = false;
            DrawScannedTiles = false;
            DebugText = false;
        }
    }

    [BackgroundColor(10, 75, 105, 200)]
    public ClientDepthPressureDented ClientDepthPressure = new();

    public override void OnLoaded() => LuneWoL.LWoLAdvancedClientSettings = this;
}