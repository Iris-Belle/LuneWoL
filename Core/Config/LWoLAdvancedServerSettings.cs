namespace LuneWoL.Core.Config;

[BackgroundColor(5, 30, 50, 255)]
public class LWoLAdvancedServerSettings : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    public override bool NeedsReload(ModConfig pendingConfig) => pendingConfig is not LWoLAdvancedServerSettings newConfig
        ? base.NeedsReload(pendingConfig)
        : !OreDensityCfg.Equals(newConfig.OreDensityCfg);

    [SeparatePage]
    public class DarkWatersDented
    {
        [BackgroundColor(155, 170, 205, 255), Range(0f, 1f), Increment(0.05f)]
        public float DarkWaterIntensity { get; set; }

        public DarkWatersDented()
        {
            DarkWaterIntensity = 0.6f;
        }
    }

    [SeparatePage]
    public class DarkerNightsDented
    {
        [BackgroundColor(120, 135, 180, 255), Range(1, int.MaxValue)]
        public int NightFadeDuration { get; set; }

        [BackgroundColor(120, 135, 180, 255), Range(0f, 1f), Increment(0.05f)]
        public float MinBrightness { get; set; }

        [BackgroundColor(120, 135, 180, 255)]
        public MoonPhasesDented MoonPhases { get; set; } = new();
        public class MoonPhasesDented
        {
            [BackgroundColor(120, 135, 180, 255), Range(0f, 1f), Increment(0.05f)]
            public float FullMoonMult { get; set; }

            [BackgroundColor(120, 135, 180, 255), Range(0f, 1f), Increment(0.05f)]
            public float WaningGibbousMult { get; set; }

            [BackgroundColor(120, 135, 180, 255), Range(0f, 1f), Increment(0.05f)]
            public float ThirdQuarterMult { get; set; }

            [BackgroundColor(120, 135, 180, 255), Range(0f, 1f), Increment(0.05f)]
            public float WaningCrescentMult { get; set; }

            [BackgroundColor(120, 135, 180, 255), Range(0f, 1f), Increment(0.05f)]
            public float NewMoonMult { get; set; }

            [BackgroundColor(120, 135, 180, 255), Range(0f, 1f), Increment(0.05f)]
            public float WaxingCrescentMult { get; set; }

            [BackgroundColor(120, 135, 180, 255), Range(0f, 1f), Increment(0.05f)]
            public float FirstQuarterMult { get; set; }

            [BackgroundColor(120, 135, 180, 255), Range(0f, 1f), Increment(0.05f)]
            public float WaxingGibbousMult { get; set; }

            public MoonPhasesDented()
            {
                FullMoonMult = 1f;
                WaningGibbousMult = 0.8f;
                ThirdQuarterMult = 0.6f;
                WaningCrescentMult = 0.4f;
                NewMoonMult = 0.35f;
                WaxingCrescentMult = 0.4f;
                FirstQuarterMult = 0.6f;
                WaxingGibbousMult = 0.8f;
            }
        }
        public DarkerNightsDented()
        {
            NightFadeDuration = 60;
            MinBrightness = 0.35f;
        }
    }

    [SeparatePage]
    public class OreDensityDented
    {
        [BackgroundColor(80, 100, 150, 255)]
        public bool DynamiteVein;

        [BackgroundColor(80, 100, 150, 255), Slider, Range(0, 100)]
        public int PrehardmodeOreDensityPercent;

        [BackgroundColor(80, 100, 150, 255), Slider, Range(0, 100)]
        public int PrehardmodeOreAmountPercent;

        [BackgroundColor(80, 100, 150, 255), Slider, Range(0, 100), ReloadRequired]
        public int HardmodeOreDensityPercent;

        [BackgroundColor(80, 100, 150, 255), Slider, Range(0, 100), ReloadRequired]
        public int HardmodeOreAmountPercent;

        [BackgroundColor(80, 100, 150, 255), Slider, Range(0, 100)]
        public int GemStoneDensityPercent;

        [BackgroundColor(80, 100, 150, 255), Slider, Range(0, 100)]
        public int GemStoneAmountPercent;

        [BackgroundColor(80, 100, 150, 255), Slider, Range(0, 100)]
        public int SiltDensityPercent;

        [BackgroundColor(80, 100, 150, 255), Slider, Range(0, 100)]
        public int SiltAmountPercent;

        [BackgroundColor(80, 100, 150, 255), Slider, Range(0, 100)]
        public int SlushDensityPercent;

        [BackgroundColor(80, 100, 150, 255), Slider, Range(0, 100)]
        public int SlushAmountPercent;

        public OreDensityDented()
        {
            DynamiteVein = true;
            PrehardmodeOreDensityPercent = 75;
            PrehardmodeOreAmountPercent = 75;
            HardmodeOreDensityPercent = 75;
            HardmodeOreAmountPercent = 75;
            GemStoneDensityPercent = 75;
            GemStoneAmountPercent = 75;
            SiltDensityPercent = 75;
            SiltAmountPercent = 75;
            SlushDensityPercent = 75;
            SlushAmountPercent = 75;
        }

        public override bool Equals(object obj) => obj is OreDensityDented other &&
               HardmodeOreDensityPercent == other.HardmodeOreDensityPercent &&
               HardmodeOreAmountPercent == other.HardmodeOreAmountPercent;

        public override int GetHashCode() => HashCode.Combine(HardmodeOreDensityPercent, HardmodeOreAmountPercent);
    }

    [SeparatePage]
    public class ServerDepthPressureDented
    {
        private bool _applyCalamityPreset;
        private bool _applyVanillaPreset;

        [Header("Presets")]
        [BackgroundColor(40, 70, 125, 255)]
        public bool ApplyCalamityPreset
        {
            get => _applyCalamityPreset;
            set
            {
                _applyCalamityPreset = value;
                if (!value) return;

                BaseStepSize = 16;
                BreathLossPerTile = 0.005f;
                TickReductionPerTile = 0.002f;
                LifeLossPerTile = 0.1f;
                LifeLossTileInterval = 4;
                LifeLossPerInterval = 0.15f;
                BaseMaxDepth = 256;
                BaseBreathAmount = 1f;
                BaseTickRate = 32f;
                BaseLifelossRate = 2f;
                DepthDarknessIntensity = 0.6f;

                StairValues.StairGills = 8;
                StairValues.StairMerman = 20;
                StairValues.StairDivingHelm = 24;
                StairValues.StairDivingGear = 48;
                StairValues.StairJellyfishDivingGear = 64;
                StairValues.StairArcticDivingGear = 128;
                StairValues.StairAbyssalDivingGear = 192;
                StairValues.StairAbyssalDivingSuit = 256;

                BreathValues.BreathlossGills = 0.9f;
                BreathValues.BreathlossMerman = 0.8f;
                BreathValues.BreathlossDivingHelm = 0.6f;
                BreathValues.BreathlossDivingGear = 0.5f;
                BreathValues.BreathlossJellyfishDivingGear = 0.45f;
                BreathValues.BreathlossArcticDivingGear = 0.4f;
                BreathValues.BreathlossAbyssalDivingGear = 0.35f;
                BreathValues.BreathlossAbyssalDivingSuit = 0.30f;

                TickValues.TickRateGills = 0.1f;
                TickValues.TickRateMerman = 0.2f;
                TickValues.TickRateDivingHelm = 0.35f;
                TickValues.TickRateDivingGear = 0.45f;
                TickValues.TickRateJellyfishDivingGear = 0.5f;
                TickValues.TickRateArcticDivingGear = 0.55f;
                TickValues.TickRateAbyssalDivingGear = 0.6f;
                TickValues.TickRateAbyssalDivingSuit = 0.65f;

                MaxDepthValues.MaxDepthGills = 0.1f;
                MaxDepthValues.MaxDepthMerman = 0.2f;
                MaxDepthValues.MaxDepthDivingHelm = 0.4f;
                MaxDepthValues.MaxDepthDivingGear = 0.5f;
                MaxDepthValues.MaxDepthJellyfishDivingGear = 0.55f;
                MaxDepthValues.MaxDepthArcticDivingGear = 0.6f;
                MaxDepthValues.MaxDepthAbyssalDivingGear = 0.65f;
                MaxDepthValues.MaxDepthAbyssalDivingSuit = 0.7f;

                LifeResistValues.LifelossResistGills = 0;
                LifeResistValues.LifelossResistMerman = 0;
                LifeResistValues.LifelossResistDivingHelm = 1;
                LifeResistValues.LifelossResistDivingGear = 2;
                LifeResistValues.LifelossResistJellyfishDivingGear = 3;
                LifeResistValues.LifelossResistArcticDivingGear = 4;
                LifeResistValues.LifelossResistAbyssalDivingGear = 5;
                LifeResistValues.LifelossResistAbyssalDivingSuit = 6;

                _applyCalamityPreset = false;
            }
        }
        [BackgroundColor(40, 70, 125, 255)]
        public bool ApplyVanillaPreset
        {
            get => _applyVanillaPreset;
            set
            {
                _applyVanillaPreset = value;
                if (!value) return;

                BaseStepSize = 16;
                BreathLossPerTile = 0.1f;
                TickReductionPerTile = 0.02f;
                LifeLossPerTile = 0.1f;
                LifeLossTileInterval = 4;
                LifeLossPerInterval = 0.15f;
                BaseMaxDepth = 64;
                BaseBreathAmount = 1f;
                BaseTickRate = 32f;
                BaseLifelossRate = 2f;
                DepthDarknessIntensity = 0.6f;

                StairValues.StairGills = 8;
                StairValues.StairMerman = 12;
                StairValues.StairDivingHelm = 16;
                StairValues.StairDivingGear = 32;
                StairValues.StairJellyfishDivingGear = 64;
                StairValues.StairArcticDivingGear = 96;

                BreathValues.BreathlossGills = 0.9f;
                BreathValues.BreathlossMerman = 0.8f;
                BreathValues.BreathlossDivingHelm = 0.7f;
                BreathValues.BreathlossDivingGear = 0.6f;
                BreathValues.BreathlossJellyfishDivingGear = 0.5f;
                BreathValues.BreathlossArcticDivingGear = 0.4f;

                TickValues.TickRateGills = 0.1f;
                TickValues.TickRateMerman = 0.2f;
                TickValues.TickRateDivingHelm = 0.3f;
                TickValues.TickRateDivingGear = 0.45f;
                TickValues.TickRateJellyfishDivingGear = 0.6f;
                TickValues.TickRateArcticDivingGear = 0.7f;

                MaxDepthValues.MaxDepthGills = 0.15f;
                MaxDepthValues.MaxDepthMerman = 0.25f;
                MaxDepthValues.MaxDepthDivingHelm = 0.35f;
                MaxDepthValues.MaxDepthDivingGear = 0.45f;
                MaxDepthValues.MaxDepthJellyfishDivingGear = 0.5f;
                MaxDepthValues.MaxDepthArcticDivingGear = 0.55f;

                LifeResistValues.LifelossResistGills = 1;
                LifeResistValues.LifelossResistMerman = 2;
                LifeResistValues.LifelossResistDivingHelm = 3;
                LifeResistValues.LifelossResistDivingGear = 4;
                LifeResistValues.LifelossResistJellyfishDivingGear = 5;
                LifeResistValues.LifelossResistArcticDivingGear = 6;

                _applyVanillaPreset = false;
            }
        }

        [Header("Tweaks")]

        [BackgroundColor(40, 70, 125, 255), Range(0, 512)]
        public int BaseStepSize { get; set; }

        [BackgroundColor(40, 70, 125, 255), Range(0f, 0.2f), Increment(0.001f)]
        public float BreathLossPerTile { get; set; }

        [BackgroundColor(40, 70, 125, 255), Range(0f, 0.2f), Increment(0.001f)]
        public float TickReductionPerTile { get; set; }

        [BackgroundColor(40, 70, 125, 255), Range(0f, 0.2f), Increment(0.001f)]
        public float LifeLossPerTile { get; set; }

        [BackgroundColor(40, 70, 125, 255), Range(0, 512)]
        public int LifeLossTileInterval { get; set; }

        [BackgroundColor(40, 70, 125, 255), Range(0f, 0.2f), Increment(0.001f)]
        public float LifeLossPerInterval { get; set; }

        [BackgroundColor(40, 70, 125, 255), Range(0, 2000)]
        public int BaseMaxDepth { get; set; }

        [BackgroundColor(40, 70, 125, 255), Range(0f, 4f), Increment(0.1f)]
        public float BaseBreathAmount { get; set; }

        [BackgroundColor(40, 70, 125, 255), Range(0f, 128f), Increment(1f)]
        public float BaseTickRate { get; set; }

        [BackgroundColor(40, 70, 125, 255), Range(0f, 64f), Increment(1f)]
        public float BaseLifelossRate { get; set; }

        [BackgroundColor(40, 70, 125, 255), Range(0f, 2f), Increment(0.05f)]
        public float DepthDarknessIntensity { get; set; }

        [BackgroundColor(40, 70, 125, 255)]
        public StairValuesDented StairValues { get; set; } = new();
        public class StairValuesDented
        {
            [BackgroundColor(40, 70, 125, 255), Range(0, 512)]
            public int StairGills { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0, 512)]
            public int StairMerman { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0, 512)]
            public int StairDivingHelm { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0, 512)]
            public int StairDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0, 512)]
            public int StairJellyfishDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0, 512)]
            public int StairArcticDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0, 512)]
            public int StairAbyssalDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0, 512)]
            public int StairAbyssalDivingSuit { get; set; }
            public StairValuesDented()
            {
                StairGills = 8;
                StairMerman = 12;
                StairDivingHelm = 16;
                StairDivingGear = 32;
                StairJellyfishDivingGear = 64;
                StairArcticDivingGear = 96;
                StairAbyssalDivingGear = 192;
                StairAbyssalDivingSuit = 256;

            }
        }

        [BackgroundColor(40, 70, 125, 255)]
        public BreathValuesDented BreathValues { get; set; } = new();
        public class BreathValuesDented
        {
            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float BreathlossGills { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float BreathlossMerman { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float BreathlossDivingHelm { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float BreathlossDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float BreathlossJellyfishDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float BreathlossArcticDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float BreathlossAbyssalDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float BreathlossAbyssalDivingSuit { get; set; }
            public BreathValuesDented()
            {
                BreathlossGills = 0.9f;
                BreathlossMerman = 0.8f;
                BreathlossDivingHelm = 0.7f;
                BreathlossDivingGear = 0.6f;
                BreathlossJellyfishDivingGear = 0.5f;
                BreathlossArcticDivingGear = 0.4f;
                BreathlossAbyssalDivingGear = 0.35f;
                BreathlossAbyssalDivingSuit = 0.30f;
            }
        }

        [BackgroundColor(40, 70, 125, 255)]
        public TickValuesDented TickValues { get; set; } = new();
        public class TickValuesDented
        {
            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float TickRateGills { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float TickRateMerman { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float TickRateDivingHelm { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float TickRateDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float TickRateJellyfishDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float TickRateArcticDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float TickRateAbyssalDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float TickRateAbyssalDivingSuit { get; set; }
            public TickValuesDented()
            {
                TickRateGills = 0.1f;
                TickRateMerman = 0.2f;
                TickRateDivingHelm = 0.3f;
                TickRateDivingGear = 0.45f;
                TickRateJellyfishDivingGear = 0.6f;
                TickRateArcticDivingGear = 0.7f;
                TickRateAbyssalDivingGear = 0.6f;
                TickRateAbyssalDivingSuit = 0.65f;
            }
        }

        [BackgroundColor(40, 70, 125, 255)]
        public MaxDepthDented MaxDepthValues { get; set; } = new();
        public class MaxDepthDented
        {
            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float MaxDepthGills { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float MaxDepthMerman { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float MaxDepthDivingHelm { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float MaxDepthDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float MaxDepthJellyfishDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float MaxDepthArcticDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float MaxDepthAbyssalDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Range(0f, 1f), Increment(0.01f)]
            public float MaxDepthAbyssalDivingSuit { get; set; }
            public MaxDepthDented()
            {
                MaxDepthGills = 0.15f;
                MaxDepthMerman = 0.25f;
                MaxDepthDivingHelm = 0.35f;
                MaxDepthDivingGear = 0.45f;
                MaxDepthJellyfishDivingGear = 0.5f;
                MaxDepthArcticDivingGear = 0.55f;
                MaxDepthAbyssalDivingGear = 0.65f;
                MaxDepthAbyssalDivingSuit = 0.7f;
            }
        }

        [BackgroundColor(40, 70, 125, 255)]
        public LifeResistDented LifeResistValues { get; set; } = new();
        public class LifeResistDented
        {
            [BackgroundColor(40, 70, 125, 255), Slider, Range(0, 12)]
            public int LifelossResistGills { get; set; }

            [BackgroundColor(40, 70, 125, 255), Slider, Range(0, 12)]
            public int LifelossResistMerman { get; set; }

            [BackgroundColor(40, 70, 125, 255), Slider, Range(0, 12)]
            public int LifelossResistDivingHelm { get; set; }

            [BackgroundColor(40, 70, 125, 255), Slider, Range(0, 12)]
            public int LifelossResistDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Slider, Range(0, 12)]
            public int LifelossResistJellyfishDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Slider, Range(0, 12)]
            public int LifelossResistArcticDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Slider, Range(0, 12)]
            public int LifelossResistAbyssalDivingGear { get; set; }

            [BackgroundColor(40, 70, 125, 255), Slider, Range(0, 12)]
            public int LifelossResistAbyssalDivingSuit { get; set; }
            public LifeResistDented()
            {
                LifelossResistGills = 1;
                LifelossResistMerman = 2;
                LifelossResistDivingHelm = 3;
                LifelossResistDivingGear = 4;
                LifelossResistJellyfishDivingGear = 5;
                LifelossResistArcticDivingGear = 6;
                LifelossResistAbyssalDivingGear = 5;
                LifelossResistAbyssalDivingSuit = 6;
            }
        }

        public ServerDepthPressureDented()
        {
            BaseStepSize = 16;
            BreathLossPerTile = 0.1f;
            TickReductionPerTile = 0.02f;
            LifeLossPerTile = 0.1f;
            LifeLossTileInterval = 4;
            LifeLossPerInterval = 0.15f;
            BaseMaxDepth = 64;
            BaseBreathAmount = 1f;
            BaseTickRate = 32f;
            BaseLifelossRate = 2f;
            DepthDarknessIntensity = 0.6f;
        }
    }

    [SeparatePage]
    public class WaterPoisionDented
    {
        [BackgroundColor(20, 55, 110, 255)]
        public bool JunglePoison { get; set; }

        [BackgroundColor(20, 55, 110, 255)]
        public bool HallowConfusion { get; set; }

        [BackgroundColor(20, 55, 110, 255)]
        public bool CrimsonIchor { get; set; }

        [BackgroundColor(20, 55, 110, 255)]
        public bool CorruptFlames { get; set; }

        public WaterPoisionDented()
        {
            JunglePoison = true;
            HallowConfusion = true;
            CrimsonIchor = true;
            CorruptFlames = true;
        }
    }


    [BackgroundColor(155, 170, 205, 200)]
    public DarkWatersDented DarkWaters = new();

    [BackgroundColor(120, 135, 180, 200)]
    public DarkerNightsDented DarkerNights = new();

    [BackgroundColor(80, 100, 150, 200)]
    public OreDensityDented OreDensityCfg = new();

    [BackgroundColor(40, 70, 125, 200)]
    public ServerDepthPressureDented ServerDepthPressure = new();

    [BackgroundColor(20, 55, 110, 200)]
    public WaterPoisionDented WaterPoision = new();

    public override void OnLoaded() => LuneWoL.LWoLAdvancedServerSettings = this;
}