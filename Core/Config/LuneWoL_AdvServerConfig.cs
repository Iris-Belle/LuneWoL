namespace LuneWoL.Core.Config;

[BackgroundColor(35, 80, 130, 200)]
public class LuneWoL_AdvServerConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    public override bool NeedsReload(ModConfig pendingConfig)
    {
        if (pendingConfig is not LuneWoL_AdvServerConfig cfg)
            return base.NeedsReload(pendingConfig);

        return Adv_Environment.OreScarcity.NeedsReload(cfg.Adv_Environment.OreScarcity) || !Adv_Items.ReforgeNerf.Equals(cfg.Adv_Items.ReforgeNerf);
    }

    #region page

    [SeparatePage]
    public class Adv_EnvironmentPage
    {
        [SeparatePage]
        public class DarkerNightsPage
        {
            [Range(1, int.MaxValue)]
            public int NightFadeDuration { get; set; }

            [Range(0f, 1f), Increment(0.05f), RoundNumber(2)]
            public float MinBrightness { get; set; }
            
            public MoonPhasesPage MoonPhases { get; set; } = new();
            public class MoonPhasesPage
            {
                [Range(0f, 1f), Increment(0.05f), RoundNumber(2)]
                public float FullMoonMult { get; set; }

                [Range(0f, 1f), Increment(0.05f), RoundNumber(2)]
                public float WaningGibbousMult { get; set; }

                [Range(0f, 1f), Increment(0.05f), RoundNumber(2)]
                public float ThirdQuarterMult { get; set; }

                [Range(0f, 1f), Increment(0.05f), RoundNumber(2)]
                public float WaningCrescentMult { get; set; }

                [Range(0f, 1f), Increment(0.05f), RoundNumber(2)]
                public float NewMoonMult { get; set; }

                [Range(0f, 1f), Increment(0.05f), RoundNumber(2)]
                public float WaxingCrescentMult { get; set; }

                [Range(0f, 1f), Increment(0.05f), RoundNumber(2)]
                public float FirstQuarterMult { get; set; }

                [Range(0f, 1f), Increment(0.05f), RoundNumber(2)]
                public float WaxingGibbousMult { get; set; }

                public MoonPhasesPage()
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
            public DarkerNightsPage()
            {
                NightFadeDuration = 60;
                MinBrightness = 0.35f;
            }
        }

        [SeparatePage]
        public class MurkyWaterPage
        {
            [Range(0f, 1f), Increment(0.05f), RoundNumber(2)]
            public float DarkWaterIntensity { get; set; }

            public MurkyWaterPage()
            {
                DarkWaterIntensity = 0.6f;
            }
        }

        [SeparatePage]
        public class OreDropChancePage
        {
            [Slider, Range(0, 100)]
            public int SiltDropChance;

            [Slider, Range(0, 100)]
            public int SlushDropChance;

            [Slider, Range(0, 100)]
            public int DesertFossilDropChance;



            [Slider, Range(0, 100)]
            public int CopperDropChance;

            [Slider, Range(0, 100)]
            public int IronDropChance;

            [Slider, Range(0, 100)]
            public int SilverDropChance;

            [Slider, Range(0, 100)]
            public int GoldDropChance;

            [Slider, Range(0, 100)]
            public int DemoniteDropChance;



            [Slider, Range(0, 100)]
            public int TinDropChance;

            [Slider, Range(0, 100)]
            public int LeadDropChance;

            [Slider, Range(0, 100)]
            public int TungstenDropChance;

            [Slider, Range(0, 100)]
            public int PlatinumDropChance;

            [Slider, Range(0, 100)]
            public int CrimtaneDropChance;



            [Slider, Range(0, 100)]
            public int MeteoriteDropChance;

            [Slider, Range(0, 100)]
            public int ObsidianDropChance;

            [Slider, Range(0, 100)]
            public int HellstoneDropChance;



            [Slider, Range(0, 100)]
            public int CobaltDropChance;

            [Slider, Range(0, 100)]
            public int MythrilDropChance;

            [Slider, Range(0, 100)]
            public int TitaniumDropChance;



            [Slider, Range(0, 100)]
            public int PalladiumDropChance;

            [Slider, Range(0, 100)]
            public int OrichalcumDropChance;

            [Slider, Range(0, 100)]
            public int AdamantiteDropChance;



            [Slider, Range(0, 100)]
            public int ChlorophyteDropChance;

            [Slider, Range(0, 100)]
            public int LunarOreDropChance;

            public class CalamityModPage
            {
                [Slider, Range(0, 100)]
                public int SeaPrismDropChance;



                [Slider, Range(0, 100)]
                public int AerialiteOreDisenchantedDropChance;

                [Slider, Range(0, 100)]
                public int AerialiteOreDropChance;



                [Slider, Range(0, 100)]
                public int InfernalSueviteDropChance;

                [Slider, Range(0, 100)]
                public int CryonicOreDropChance;

                [Slider, Range(0, 100)]
                public int HallowedOreDropChance;



                [Slider, Range(0, 100)]
                public int PerennialOreDropChance;

                [Slider, Range(0, 100)]
                public int ScoriaOreDropChance;

                [Slider, Range(0, 100)]
                public int AstralOreDropChance;



                [Slider, Range(0, 100)]
                public int ExodiumOreDropChance;

                [Slider, Range(0, 100)]
                public int UelibloomOreDropChance;

                [Slider, Range(0, 100)]
                public int AuricOreDropChance;
                public CalamityModPage()
                {
                    SeaPrismDropChance = 75;
                    AerialiteOreDisenchantedDropChance = 75;
                    AerialiteOreDropChance = 75;

                    InfernalSueviteDropChance = 75;
                    CryonicOreDropChance = 75;
                    HallowedOreDropChance = 75;

                    PerennialOreDropChance = 75;
                    ScoriaOreDropChance = 75;
                    AstralOreDropChance = 75;

                    ExodiumOreDropChance = 75;
                    UelibloomOreDropChance = 75;
                    AuricOreDropChance = 75;
                }
            }

            public class ThoriumModPage
            {
                [Slider, Range(0, 100)]
                public int SynthGoldDropChance;

                [Slider, Range(0, 100)]
                public int SynthPlatinumDropChance;



                [Slider, Range(0, 100)]
                public int SmoothCoalDropChance;

                [Slider, Range(0, 100)]
                public int LifeQuartzDropChance;

                [Slider, Range(0, 100)]
                public int ThoriumOreDropChance;

                [Slider, Range(0, 100)]
                public int AquaiteDropChance;



                [Slider, Range(0, 100)]
                public int LodeStoneDropChance;

                [Slider, Range(0, 100)]
                public int ValadiumChunkDropChance;

                [Slider, Range(0, 100)]
                public int IllumiteChunkDropChance;

                public ThoriumModPage()
                {
                    SynthGoldDropChance = 75;
                    SynthPlatinumDropChance = 75;

                    SmoothCoalDropChance = 75;
                    LifeQuartzDropChance = 75;
                    ThoriumOreDropChance = 75;
                    AquaiteDropChance = 75;

                    LodeStoneDropChance = 75;
                    ValadiumChunkDropChance = 75;
                    IllumiteChunkDropChance = 75;
                }
            }

            public class SpiritModPage
            {
                [Slider, Range(0, 100)]
                public int BismiteCrystalOreDropChance;

                [Slider, Range(0, 100)]
                public int FloranOreTileDropChance;

                [Slider, Range(0, 100)]
                public int MarbleOreDropChance;

                [Slider, Range(0, 100)]
                public int GraniteOreDropChance;

                [Slider, Range(0, 100)]
                public int GlowstoneDropChance;

                [Slider, Range(0, 100)]
                public int CryoliteOreTileDropChance;



                [Slider, Range(0, 100)]
                public int SpiritOreTileDropChance;

                public SpiritModPage()
                {
                    BismiteCrystalOreDropChance = 75;
                    FloranOreTileDropChance = 75;
                    MarbleOreDropChance = 75;
                    GraniteOreDropChance = 75;
                    GlowstoneDropChance = 75;
                    CryoliteOreTileDropChance = 75;

                    SpiritOreTileDropChance = 75;
                }
            }

            public OreDropChancePage()
            {
                SiltDropChance = 75;
                SlushDropChance = 75;
                DesertFossilDropChance = 75;

                CopperDropChance = 75;
                IronDropChance = 75;
                SilverDropChance = 75;
                GoldDropChance = 75;
                DemoniteDropChance = 75;

                TinDropChance = 75;
                LeadDropChance = 75;
                TungstenDropChance = 75;
                PlatinumDropChance = 75;
                CrimtaneDropChance = 75;

                MeteoriteDropChance = 75;
                ObsidianDropChance = 75;
                HellstoneDropChance = 75;

                CobaltDropChance = 75;
                MythrilDropChance = 75;
                TitaniumDropChance = 75;

                PalladiumDropChance = 75;
                OrichalcumDropChance = 75;
                AdamantiteDropChance = 75;

                ChlorophyteDropChance = 75;

                LunarOreDropChance = 75;
            }

            public CalamityModPage CalamityMod = new();

            public ThoriumModPage ThoriumMod = new();

            public SpiritModPage SpiritMod = new();
        }

        [SeparatePage]
        public class OreScarcityPage
        {
            public bool DynamiteVein;

            [Slider, Range(0, 100)]
            public int PrehardmodeOreScarcityPercent;

            [Slider, Range(0, 100)]
            public int PrehardmodeOreAmountPercent;

            [Slider, Range(0, 100), ReloadRequired]
            public int HardmodeOreScarcityPercent;

            [Slider, Range(0, 100), ReloadRequired]
            public int HardmodeOreAmountPercent;

            [Slider, Range(0, 100)]
            public int GemStoneDensityPercent;

            [Slider, Range(0, 100)]
            public int GemStoneAmountPercent;

            [Slider, Range(0, 100)]
            public int SiltDensityPercent;

            [Slider, Range(0, 100)]
            public int SiltAmountPercent;

            [Slider, Range(0, 100)]
            public int SlushDensityPercent;

            [Slider, Range(0, 100)]
            public int SlushAmountPercent;

            public OreScarcityPage()
            {
                DynamiteVein = true;
                PrehardmodeOreScarcityPercent = 75;
                PrehardmodeOreAmountPercent = 75;
                HardmodeOreScarcityPercent = 75;
                HardmodeOreAmountPercent = 75;
                GemStoneDensityPercent = 75;
                GemStoneAmountPercent = 75;
                SiltDensityPercent = 75;
                SiltAmountPercent = 75;
                SlushDensityPercent = 75;
                SlushAmountPercent = 75;
            }

            public bool NeedsReload(OreScarcityPage other)
            {
                return
                    HardmodeOreScarcityPercent != other.HardmodeOreScarcityPercent ||
                    HardmodeOreAmountPercent != other.HardmodeOreAmountPercent;
            }
        }

        [SeparatePage]
        public class ServerDepthPressurePage
        {
            private bool _applyCalamityPreset;
            private bool _applyVanillaPreset;

            [Header("Presets")]
            
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

            [Range(0, 512)]
            public int BaseStepSize { get; set; }

            [Range(0f, 0.2f), Increment(0.001f), RoundNumber(3)]
            public float BreathLossPerTile { get; set; }

            [Range(0f, 0.2f), Increment(0.001f), RoundNumber(3)]
            public float TickReductionPerTile { get; set; }

            [Range(0f, 0.2f), Increment(0.001f), RoundNumber(3)]
            public float LifeLossPerTile { get; set; }

            [Range(0, 512)]
            public int LifeLossTileInterval { get; set; }

            [Range(0f, 0.2f), Increment(0.001f), RoundNumber(3)]
            public float LifeLossPerInterval { get; set; }

            [Range(0, 2000)]
            public int BaseMaxDepth { get; set; }

            [Range(0f, 4f), Increment(0.1f), RoundNumber(1)]
            public float BaseBreathAmount { get; set; }

            [Range(0f, 128f), Increment(1f), RoundNumber(1)]
            public float BaseTickRate { get; set; }

            [Range(0f, 64f), Increment(1f), RoundNumber(1)]
            public float BaseLifelossRate { get; set; }

            [Range(0f, 2f), Increment(0.05f), RoundNumber(2)]
            public float DepthDarknessIntensity { get; set; }

            
            public StairValuesPage StairValues { get; set; } = new();
            public class StairValuesPage
            {
                [Range(0, 512)]
                public int StairGills { get; set; }

                [Range(0, 512)]
                public int StairMerman { get; set; }

                [Range(0, 512)]
                public int StairDivingHelm { get; set; }

                [Range(0, 512)]
                public int StairDivingGear { get; set; }

                [Range(0, 512)]
                public int StairJellyfishDivingGear { get; set; }

                [Range(0, 512)]
                public int StairArcticDivingGear { get; set; }

                [Range(0, 512)]
                public int StairAbyssalDivingGear { get; set; }

                [Range(0, 512)]
                public int StairAbyssalDivingSuit { get; set; }
                public StairValuesPage()
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

            
            public BreathValuesPage BreathValues { get; set; } = new();
            public class BreathValuesPage
            {
                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float BreathlossGills { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float BreathlossMerman { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float BreathlossDivingHelm { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float BreathlossDivingGear { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float BreathlossJellyfishDivingGear { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float BreathlossArcticDivingGear { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float BreathlossAbyssalDivingGear { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float BreathlossAbyssalDivingSuit { get; set; }
                public BreathValuesPage()
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

            
            public TickValuesPage TickValues { get; set; } = new();
            public class TickValuesPage
            {
                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float TickRateGills { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float TickRateMerman { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float TickRateDivingHelm { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float TickRateDivingGear { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float TickRateJellyfishDivingGear { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float TickRateArcticDivingGear { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float TickRateAbyssalDivingGear { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float TickRateAbyssalDivingSuit { get; set; }
                public TickValuesPage()
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

            
            public MaxDepthPage MaxDepthValues { get; set; } = new();
            public class MaxDepthPage
            {
                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float MaxDepthGills { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float MaxDepthMerman { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float MaxDepthDivingHelm { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float MaxDepthDivingGear { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float MaxDepthJellyfishDivingGear { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float MaxDepthArcticDivingGear { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float MaxDepthAbyssalDivingGear { get; set; }

                [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
                public float MaxDepthAbyssalDivingSuit { get; set; }
                public MaxDepthPage()
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

            
            public LifeResistPage LifeResistValues { get; set; } = new();
            public class LifeResistPage
            {
                [Slider, Range(0, 12)]
                public int LifelossResistGills { get; set; }

                [Slider, Range(0, 12)]
                public int LifelossResistMerman { get; set; }

                [Slider, Range(0, 12)]
                public int LifelossResistDivingHelm { get; set; }

                [Slider, Range(0, 12)]
                public int LifelossResistDivingGear { get; set; }

                [Slider, Range(0, 12)]
                public int LifelossResistJellyfishDivingGear { get; set; }

                [Slider, Range(0, 12)]
                public int LifelossResistArcticDivingGear { get; set; }

                [Slider, Range(0, 12)]
                public int LifelossResistAbyssalDivingGear { get; set; }

                [Slider, Range(0, 12)]
                public int LifelossResistAbyssalDivingSuit { get; set; }
                public LifeResistPage()
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

            public ServerDepthPressurePage()
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
        public class SpaceVacuumPage
        {
            [Range(0, int.MaxValue)]
            public int BaseDoTRate { get; set; }

            [Range(0, int.MaxValue)]
            public int FishBowlReduction { get; set; }

            [Header("SpiritMod")]

            [Range(0, int.MaxValue)]
            public int AstroHelmReduction { get; set; }

            [Range(0, int.MaxValue)]
            public int AstraliteVisorReduction { get; set; }

            public SpaceVacuumPage()
            {
                BaseDoTRate = 50;
                FishBowlReduction = 10;
                AstroHelmReduction = 10;
                AstraliteVisorReduction = 15;
            }
        }

        [SeparatePage]
        public class ViscousWaterPage
        {
            [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
            public float WaterVelocityX { get; set; }

            [Range(0f, 1f), Increment(0.01f), RoundNumber(2)]
            public float WaterVelocityY { get; set; }

            public ViscousWaterPage()
            {
                WaterVelocityX = 1f;
                WaterVelocityY = 1f;
            }
        }

        [SeparatePage]
        public class WaterPoisionPage
        {
            public bool JunglePoison { get; set; }

            public bool HallowConfusion { get; set; }
            
            public bool CrimsonIchor { get; set; }
            
            public bool CorruptFlames { get; set; }

            public WaterPoisionPage()
            {
                JunglePoison = true;
                HallowConfusion = true;
                CrimsonIchor = true;
                CorruptFlames = true;
            }
        }

        [SeparatePage]
        public class WeatherEffectsPage
        {
            public bool SandStormEffect { get; set; }
            
            public bool BlizzardEffect { get; set; }

            public WeatherEffectsPage()
            {
                SandStormEffect = true;
                BlizzardEffect = true;
            }
        }

        #region new()
        public DarkerNightsPage DarkerNights = new();

        public MurkyWaterPage MurkyWater = new();

        public OreDropChancePage OreDropChance = new();

        public OreScarcityPage OreScarcity = new();

        public ServerDepthPressurePage ServerDepthPressure = new();

        public SpaceVacuumPage SpaceVacuum = new();

        public ViscousWaterPage ViscousWater = new();

        public WaterPoisionPage WaterPoision = new();

        public WeatherEffectsPage WeatherEffects = new();

        #endregion
    }

    [SeparatePage]
    public class Adv_ItemsPage
    {
        [SeparatePage]
        public class ReforgeNerfPage
        {
            [SeparatePage]
            public class WeaponReforgeNerf
            {
                
                public class _LargePage
                {
                    [Range(0f, 1.12f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Size { get; set; }

                    public _LargePage()
                    {
                        Size = 1.12f;
                    }
                    public override bool Equals(object obj) => obj is _LargePage other && Size == other.Size;
                    public override int GetHashCode() => HashCode.Combine(Size);
                }

                
                public class _MassivePage
                {
                    [Range(0f, 1.18f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Size { get; set; }

                    public _MassivePage()
                    {
                        Size = 1.18f;
                    }
                    public override bool Equals(object obj) => obj is _MassivePage other && Size == other.Size;
                    public override int GetHashCode() => HashCode.Combine(Size);
                }

                
                public class _DangerousPage
                {
                    [Range(0f, 1.05f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0, 2), ReloadRequired]
                    public int Crt { get; set; }

                    [Range(0f, 1.05f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Size { get; set; }

                    public _DangerousPage()
                    {
                        Dmg = 1.05f;
                        Crt = 2;
                        Size = 1.05f;
                    }
                    public override bool Equals(object obj) => obj is _DangerousPage other && Dmg == other.Dmg && Crt == other.Crt && Size == other.Size;
                    public override int GetHashCode() => HashCode.Combine(Dmg, Crt, Size);
                }

                
                public class _SavagePage
                {
                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Size { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    public _SavagePage()
                    {
                        Dmg = 1.1f;
                        Size = 1.1f;
                        Kb = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _SavagePage other && Dmg == other.Dmg && Size == other.Size && Kb == other.Kb;
                    public override int GetHashCode() => HashCode.Combine(Dmg, Size, Kb);
                }

                
                public class _SharpPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _SharpPage()
                    {
                        Dmg = 1.15f;
                    }

                    public override bool Equals(object obj) => obj is _SharpPage other && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Dmg);
                }

                
                public class _PointyPage
                {
                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _PointyPage()
                    {
                        Dmg = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _PointyPage other && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Dmg);
                }

                
                public class _LegendaryPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0, 5), ReloadRequired]
                    public int Crt { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Size { get; set; }

                    public _LegendaryPage()
                    {
                        Kb = 1.15f;
                        Dmg = 1.15f;
                        Crt = 5;
                        Spd = 0.9f;
                        Size = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _LegendaryPage other && Kb == other.Kb && Dmg == other.Dmg && Crt == other.Crt && Spd == other.Spd && Size == other.Size;
                    public override int GetHashCode() => HashCode.Combine(Kb, Dmg, Crt, Spd, Size);
                }

                
                public class _TinyPage
                {
                    [Range(0f, 0.82f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Size { get; set; }

                    public _TinyPage()
                    {
                        Size = 0.82f;
                    }

                    public override bool Equals(object obj) => obj is _TinyPage other && Size == other.Size;
                    public override int GetHashCode() => HashCode.Combine(Size);
                }

                
                public class _TerriblePage
                {
                    [Range(0f, 0.85f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 0.85f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0f, 0.87f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Size { get; set; }

                    public _TerriblePage()
                    {
                        Kb = 0.85f;
                        Dmg = 0.85f;
                        Size = 0.87f;
                    }

                    public override bool Equals(object obj) => obj is _TerriblePage other && Kb == other.Kb && Dmg == other.Dmg && Size == other.Size;
                    public override int GetHashCode() => HashCode.Combine(Kb, Dmg, Size);
                }

                
                public class _SmallPage
                {
                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Size { get; set; }

                    public _SmallPage()
                    {
                        Size = 0.9f;
                    }

                    public override bool Equals(object obj) => obj is _SmallPage other && Size == other.Size;
                    public override int GetHashCode() => HashCode.Combine(Size);
                }

                
                public class _DullPage
                {
                    [Range(0f, 0.85f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _DullPage()
                    {
                        Dmg = 0.85f;
                    }

                    public override bool Equals(object obj) => obj is _DullPage other && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Dmg);
                }

                
                public class _UnhappyPage
                {
                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Size { get; set; }

                    public _UnhappyPage()
                    {
                        Spd = 1.1f;
                        Kb = 0.9f;
                        Size = 0.9f;
                    }

                    public override bool Equals(object obj) => obj is _UnhappyPage other && Spd == other.Spd && Kb == other.Kb && Size == other.Size;
                    public override int GetHashCode() => HashCode.Combine(Spd, Kb, Size);
                }

                
                public class _BulkyPage
                {
                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 1.05f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Size { get; set; }

                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    public _BulkyPage()
                    {
                        Kb = 1.1f;
                        Dmg = 1.05f;
                        Size = 1.1f;
                        Spd = 1.15f;
                    }

                    public override bool Equals(object obj) => obj is _BulkyPage other && Kb == other.Kb && Dmg == other.Dmg && Size == other.Size && Spd == other.Spd;
                    public override int GetHashCode() => HashCode.Combine(Kb, Dmg, Size, Spd);
                }

                
                public class _ShamefulPage
                {
                    [Range(0f, 0.8f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Size { get; set; }

                    public _ShamefulPage()
                    {
                        Kb = 0.8f;
                        Dmg = 0.9f;
                        Size = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _ShamefulPage other && Kb == other.Kb && Dmg == other.Dmg && Size == other.Size;
                    public override int GetHashCode() => HashCode.Combine(Kb, Dmg, Size);
                }

                
                public class _HeavyPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    public _HeavyPage()
                    {
                        Kb = 1.15f;
                        Spd = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _HeavyPage other && Kb == other.Kb && Spd == other.Spd;
                    public override int GetHashCode() => HashCode.Combine(Kb, Spd);
                }

                
                public class _LightPage
                {
                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 0.85f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    public _LightPage()
                    {
                        Kb = 0.9f;
                        Spd = 0.85f;
                    }

                    public override bool Equals(object obj) => obj is _LightPage other && Kb == other.Kb && Spd == other.Spd;
                    public override int GetHashCode() => HashCode.Combine(Kb, Spd);
                }

                
                public class _SightedPage
                {
                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0, 3), ReloadRequired]
                    public int Crt { get; set; }

                    public _SightedPage()
                    {
                        Dmg = 1.1f;
                        Crt = 3;
                    }

                    public override bool Equals(object obj) => obj is _SightedPage other && Dmg == other.Dmg && Crt == other.Crt;
                    public override int GetHashCode() => HashCode.Combine(Dmg, Crt);
                }

                
                public class _RapidPage
                {
                    [Range(0f, 0.85f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Shtspd { get; set; }

                    public _RapidPage()
                    {
                        Spd = 0.85f;
                        Shtspd = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _RapidPage other && Spd == other.Spd && Shtspd == other.Shtspd;
                    public override int GetHashCode() => HashCode.Combine(Spd, Shtspd);
                }

                
                public class _HastyPage
                {
                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Shtspd { get; set; }

                    public _HastyPage()
                    {
                        Spd = 0.9f;
                        Shtspd = 1.15f;
                    }

                    public override bool Equals(object obj) => obj is _HastyPage other && Spd == other.Spd && Shtspd == other.Shtspd;
                    public override int GetHashCode() => HashCode.Combine(Spd, Shtspd);
                }

                
                public class _IntimidatingPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 1.05f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Shtspd { get; set; }

                    public _IntimidatingPage()
                    {
                        Kb = 1.15f;
                        Shtspd = 1.05f;
                    }

                    public override bool Equals(object obj) => obj is _IntimidatingPage other && Kb == other.Kb && Shtspd == other.Shtspd;
                    public override int GetHashCode() => HashCode.Combine(Kb, Shtspd);
                }

                
                public class _DeadlyPage
                {
                    [Range(0f, 1.05f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 1.05f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Shtspd { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0f, 0.95f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0, 2), ReloadRequired]
                    public int Crt { get; set; }

                    public _DeadlyPage()
                    {
                        Kb = 1.05f;
                        Shtspd = 1.05f;
                        Dmg = 1.1f;
                        Spd = 0.95f;
                        Crt = 2;
                    }

                    public override bool Equals(object obj) => obj is _DeadlyPage other && Kb == other.Kb && Shtspd == other.Shtspd && Dmg == other.Dmg && Spd == other.Spd && Crt == other.Crt;
                    public override int GetHashCode() => HashCode.Combine(Kb, Shtspd, Dmg, Spd, Crt);
                }

                
                public class _StaunchPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _StaunchPage()
                    {
                        Kb = 1.15f;
                        Dmg = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _StaunchPage other && Kb == other.Kb && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Kb, Dmg);
                }

                
                public class _UnrealPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0, 5), ReloadRequired]
                    public int Crt { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Shtspd { get; set; }

                    public _UnrealPage()
                    {
                        Kb = 1.15f;
                        Dmg = 1.15f;
                        Crt = 5;
                        Spd = 0.9f;
                        Shtspd = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _UnrealPage other && Kb == other.Kb && Dmg == other.Dmg && Crt == other.Crt && Spd == other.Spd && Shtspd == other.Shtspd;
                    public override int GetHashCode() => HashCode.Combine(Kb, Dmg, Crt, Spd, Shtspd);
                }

                
                public class _AwfulPage
                {
                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Shtspd { get; set; }

                    [Range(0f, 0.85f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _AwfulPage()
                    {
                        Kb = 0.9f;
                        Shtspd = 0.9f;
                        Dmg = 0.85f;
                    }

                    public override bool Equals(object obj) => obj is _AwfulPage other && Kb == other.Kb && Shtspd == other.Shtspd && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Kb, Shtspd, Dmg);
                }

                
                public class _LethargicPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Shtspd { get; set; }

                    public _LethargicPage()
                    {
                        Spd = 1.15f;
                        Shtspd = 0.9f;
                    }

                    public override bool Equals(object obj) => obj is _LethargicPage other && Spd == other.Spd && Shtspd == other.Shtspd;
                    public override int GetHashCode() => HashCode.Combine(Spd, Shtspd);
                }

                
                public class _AwkwardPage
                {
                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0f, 0.8f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    public _AwkwardPage()
                    {
                        Spd = 1.1f;
                        Kb = 0.8f;
                    }

                    public override bool Equals(object obj) => obj is _AwkwardPage other && Spd == other.Spd && Kb == other.Kb;
                    public override int GetHashCode() => HashCode.Combine(Spd, Kb);
                }

                
                public class _PowerfulPage
                {
                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0, 1), ReloadRequired]
                    public int Crt { get; set; }

                    public _PowerfulPage()
                    {
                        Spd = 1.1f;
                        Dmg = 1.15f;
                        Crt = 1;
                    }

                    public override bool Equals(object obj) => obj is _PowerfulPage other && Spd == other.Spd && Dmg == other.Dmg && Crt == other.Crt;
                    public override int GetHashCode() => HashCode.Combine(Spd, Dmg, Crt);
                }

                
                public class _FrenzyingPage
                {
                    [Range(0f, 0.85f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0f, 0.85f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _FrenzyingPage()
                    {
                        Spd = 0.85f;
                        Dmg = 0.85f;
                    }

                    public override bool Equals(object obj) => obj is _FrenzyingPage other && Spd == other.Spd && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Spd, Dmg);
                }

                
                public class _MysticPage
                {
                    [Range(0f, 0.85f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Mcst { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _MysticPage()
                    {
                        Mcst = 0.85f;
                        Dmg = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _MysticPage other && Mcst == other.Mcst && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Mcst, Dmg);
                }

                
                public class _AdeptPage
                {
                    [Range(0f, 0.85f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Mcst { get; set; }

                    public _AdeptPage()
                    {
                        Mcst = 0.85f;
                    }

                    public override bool Equals(object obj) => obj is _AdeptPage other && Mcst == other.Mcst;
                    public override int GetHashCode() => HashCode.Combine(Mcst);
                }

                
                public class _MasterfulPage
                {
                    [Range(0f, 0.85f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Mcst { get; set; }

                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0f, 1.05f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    public _MasterfulPage()
                    {
                        Mcst = 0.85f;
                        Dmg = 1.15f;
                        Kb = 1.05f;
                    }

                    public override bool Equals(object obj) => obj is _MasterfulPage other && Mcst == other.Mcst && Dmg == other.Dmg && Kb == other.Kb;
                    public override int GetHashCode() => HashCode.Combine(Mcst, Dmg, Kb);
                }

                
                public class _MythicalPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0, 5), ReloadRequired]
                    public int Crt { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Mcst { get; set; }

                    public _MythicalPage()
                    {
                        Kb = 1.15f;
                        Dmg = 1.15f;
                        Crt = 5;
                        Spd = 0.9f;
                        Mcst = 0.9f;
                    }

                    public override bool Equals(object obj) => obj is _MythicalPage other && Kb == other.Kb && Dmg == other.Dmg && Crt == other.Crt && Spd == other.Spd && Mcst == other.Mcst;
                    public override int GetHashCode() => HashCode.Combine(Kb, Dmg, Crt, Spd, Mcst);
                }

                
                public class _IneptPage
                {
                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Mcst { get; set; }

                    public _IneptPage()
                    {
                        Mcst = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _IneptPage other && Mcst == other.Mcst;
                    public override int GetHashCode() => HashCode.Combine(Mcst);
                }

                
                public class _IgnorantPage
                {
                    [Range(0f, 1.2f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Mcst { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _IgnorantPage()
                    {
                        Mcst = 1.2f;
                        Dmg = 0.9f;
                    }

                    public override bool Equals(object obj) => obj is _IgnorantPage other && Mcst == other.Mcst && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Mcst, Dmg);
                }

                
                public class _DerangedPage
                {
                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _DerangedPage()
                    {
                        Kb = 0.9f;
                        Dmg = 0.9f;
                    }

                    public override bool Equals(object obj) => obj is _DerangedPage other && Kb == other.Kb && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Kb, Dmg);
                }

                
                public class _IntensePage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Mcst { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _IntensePage()
                    {
                        Mcst = 1.15f;
                        Dmg = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _IntensePage other && Mcst == other.Mcst && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Mcst, Dmg);
                }

                
                public class _TabooPage
                {
                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Mcst { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    public _TabooPage()
                    {
                        Mcst = 1.1f;
                        Kb = 1.1f;
                        Spd = 0.9f;
                    }

                    public override bool Equals(object obj) => obj is _TabooPage other && Mcst == other.Mcst && Kb == other.Kb && Spd == other.Spd;
                    public override int GetHashCode() => HashCode.Combine(Mcst, Kb, Spd);
                }

                
                public class _CelestialPage
                {
                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Mcst { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _CelestialPage()
                    {
                        Mcst = 0.9f;
                        Kb = 1.1f;
                        Spd = 1.1f;
                        Dmg = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _CelestialPage other && Mcst == other.Mcst && Kb == other.Kb && Spd == other.Spd && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Mcst, Kb, Spd, Dmg);
                }

                
                public class _FuriousPage
                {
                    [Range(0f, 1.2f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Mcst { get; set; }

                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    public _FuriousPage()
                    {
                        Mcst = 1.2f;
                        Dmg = 1.15f;
                        Kb = 1.15f;
                    }

                    public override bool Equals(object obj) => obj is _FuriousPage other && Mcst == other.Mcst && Dmg == other.Dmg && Kb == other.Kb;
                    public override int GetHashCode() => HashCode.Combine(Mcst, Dmg, Kb);
                }

                
                public class _ManicPage
                {
                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Mcst { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    public _ManicPage()
                    {
                        Mcst = 0.9f;
                        Dmg = 0.9f;
                        Spd = 0.9f;
                    }

                    public override bool Equals(object obj) => obj is _ManicPage other && Mcst == other.Mcst && Dmg == other.Dmg && Spd == other.Spd;
                    public override int GetHashCode() => HashCode.Combine(Mcst, Dmg, Spd);
                }

                
                public class _Legendary2Page
                {
                    [Range(0f, 1.17f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 1.17f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0, 8), ReloadRequired]
                    public int Crt { get; set; }

                    public _Legendary2Page()
                    {
                        Kb = 1.17f;
                        Dmg = 1.17f;
                        Crt = 8;
                    }

                    public override bool Equals(object obj) => obj is _Legendary2Page other && Kb == other.Kb && Dmg == other.Dmg && Crt == other.Crt;
                    public override int GetHashCode() => HashCode.Combine(Kb, Dmg, Crt);
                }

                
                public class _KeenPage
                {
                    [Range(0, 3), ReloadRequired]
                    public int Crt { get; set; }

                    public _KeenPage()
                    {
                        Crt = 3;
                    }

                    public override bool Equals(object obj) => obj is _KeenPage other && Crt == other.Crt;
                    public override int GetHashCode() => HashCode.Combine(Crt);
                }

                
                public class _SuperiorPage
                {
                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0, 3), ReloadRequired]
                    public int Crt { get; set; }

                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    public _SuperiorPage()
                    {
                        Dmg = 1.1f;
                        Crt = 3;
                        Kb = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _SuperiorPage other && Dmg == other.Dmg && Crt == other.Crt && Kb == other.Kb;
                    public override int GetHashCode() => HashCode.Combine(Dmg, Crt, Kb);
                }

                
                public class _ForcefulPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    public _ForcefulPage()
                    {
                        Kb = 1.15f;
                    }

                    public override bool Equals(object obj) => obj is _ForcefulPage other && Kb == other.Kb;
                    public override int GetHashCode() => HashCode.Combine(Kb);
                }

                
                public class _HurtfulPage
                {
                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _HurtfulPage()
                    {
                        Dmg = 1.1f;
                    }

                    public override bool Equals(object obj) => obj is _HurtfulPage other && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Dmg);
                }

                
                public class _StrongPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    public _StrongPage()
                    {
                        Kb = 1.15f;
                    }

                    public override bool Equals(object obj) => obj is _StrongPage other && Kb == other.Kb;
                    public override int GetHashCode() => HashCode.Combine(Kb);
                }

                
                public class _UnpleasantPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 1.05f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _UnpleasantPage()
                    {
                        Kb = 1.15f;
                        Dmg = 1.05f;
                    }

                    public override bool Equals(object obj) => obj is _UnpleasantPage other && Kb == other.Kb && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Kb, Dmg);
                }

                
                public class _GodlyPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0, 5), ReloadRequired]
                    public int Crt { get; set; }

                    public _GodlyPage()
                    {
                        Kb = 1.15f;
                        Dmg = 1.15f;
                        Crt = 5;
                    }

                    public override bool Equals(object obj) => obj is _GodlyPage other && Kb == other.Kb && Dmg == other.Dmg && Crt == other.Crt;
                    public override int GetHashCode() => HashCode.Combine(Kb, Dmg, Crt);
                }

                
                public class _DemonicPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0, 5), ReloadRequired]
                    public int Crt { get; set; }

                    public _DemonicPage()
                    {
                        Dmg = 1.15f;
                        Crt = 5;
                    }

                    public override bool Equals(object obj) => obj is _DemonicPage other && Dmg == other.Dmg && Crt == other.Crt;
                    public override int GetHashCode() => HashCode.Combine(Dmg, Crt);
                }

                
                public class _ZealousPage
                {
                    [Range(0, 5), ReloadRequired]
                    public int Crt { get; set; }

                    public _ZealousPage()
                    {
                        Crt = 5;
                    }

                    public override bool Equals(object obj) => obj is _ZealousPage other && Crt == other.Crt;
                    public override int GetHashCode() => HashCode.Combine(Crt);
                }

                
                public class _BrokenPage
                {
                    [Range(0f, 0.7f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0f, 0.8f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    public _BrokenPage()
                    {
                        Dmg = 0.7f;
                        Kb = 0.8f;
                    }

                    public override bool Equals(object obj) => obj is _BrokenPage other && Dmg == other.Dmg && Kb == other.Kb;
                    public override int GetHashCode() => HashCode.Combine(Dmg, Kb);
                }

                
                public class _DamagedPage
                {
                    [Range(0f, 0.85f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _DamagedPage()
                    {
                        Dmg = 0.85f;
                    }

                    public override bool Equals(object obj) => obj is _DamagedPage other && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Dmg);
                }

                
                public class _WeakPage
                {
                    [Range(0f, 0.8f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    public _WeakPage()
                    {
                        Kb = 0.8f;
                    }

                    public override bool Equals(object obj) => obj is _WeakPage other && Kb == other.Kb;
                    public override int GetHashCode() => HashCode.Combine(Kb);
                }

                
                public class _ShoddyPage
                {
                    [Range(0f, 0.85f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _ShoddyPage()
                    {
                        Kb = 0.85f;
                        Dmg = 0.9f;
                    }

                    public override bool Equals(object obj) => obj is _ShoddyPage other && Kb == other.Kb && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Kb, Dmg);
                }

                
                public class _RuthlessPage
                {
                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 1.18f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _RuthlessPage()
                    {
                        Kb = 0.9f;
                        Dmg = 1.18f;
                    }

                    public override bool Equals(object obj) => obj is _RuthlessPage other && Kb == other.Kb && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Kb, Dmg);
                }

                
                public class _QuickPage
                {
                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    public _QuickPage()
                    {
                        Spd = 0.9f;
                    }

                    public override bool Equals(object obj) => obj is _QuickPage other && Spd == other.Spd;
                    public override int GetHashCode() => HashCode.Combine(Spd);
                }

                
                public class _Deadly2Page
                {
                    [Range(0f, 1.1f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    public _Deadly2Page()
                    {
                        Dmg = 1.1f;
                        Spd = 0.9f;
                    }

                    public override bool Equals(object obj) => obj is _Deadly2Page other && Dmg == other.Dmg && Spd == other.Spd;
                    public override int GetHashCode() => HashCode.Combine(Dmg, Spd);
                }

                
                public class _AgilePage
                {
                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0, 3), ReloadRequired]
                    public int Crt { get; set; }

                    public _AgilePage()
                    {
                        Spd = 0.9f;
                        Crt = 3;
                    }

                    public override bool Equals(object obj) => obj is _AgilePage other && Spd == other.Spd && Crt == other.Crt;
                    public override int GetHashCode() => HashCode.Combine(Spd, Crt);
                }

                
                public class _NimblePage
                {
                    [Range(0f, 0.95f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    public _NimblePage()
                    {
                        Spd = 0.95f;
                    }

                    public override bool Equals(object obj) => obj is _NimblePage other && Spd == other.Spd;
                    public override int GetHashCode() => HashCode.Combine(Spd);
                }

                
                public class _MurderousPage
                {
                    [Range(0, 3), ReloadRequired]
                    public int Crt { get; set; }

                    [Range(0f, 0.94f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0f, 1.07f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    public _MurderousPage()
                    {
                        Crt = 3;
                        Spd = 0.94f;
                        Dmg = 1.07f;
                    }

                    public override bool Equals(object obj) => obj is _MurderousPage other && Crt == other.Crt && Spd == other.Spd && Dmg == other.Dmg;
                    public override int GetHashCode() => HashCode.Combine(Crt, Spd, Dmg);
                }

                
                public class _SlowPage
                {
                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    public _SlowPage()
                    {
                        Spd = 1.15f;
                    }

                    public override bool Equals(object obj) => obj is _SlowPage other && Spd == other.Spd;
                    public override int GetHashCode() => HashCode.Combine(Spd);
                }

                
                public class _SluggishPage
                {
                    [Range(0f, 1.2f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    public _SluggishPage()
                    {
                        Spd = 1.2f;
                    }

                    public override bool Equals(object obj) => obj is _SluggishPage other && Spd == other.Spd;
                    public override int GetHashCode() => HashCode.Combine(Spd);
                }

                
                public class _LazyPage
                {
                    [Range(0f, 1.08f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    public _LazyPage()
                    {
                        Spd = 1.08f;
                    }

                    public override bool Equals(object obj) => obj is _LazyPage other && Spd == other.Spd;
                    public override int GetHashCode() => HashCode.Combine(Spd);
                }

                
                public class _AnnoyingPage
                {
                    [Range(0f, 0.8f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0f, 1.15f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    public _AnnoyingPage()
                    {
                        Dmg = 0.8f;
                        Spd = 1.15f;
                    }


                    public override bool Equals(object obj) => obj is _AnnoyingPage other && Dmg == other.Dmg && Spd == other.Spd;
                    public override int GetHashCode() => HashCode.Combine(Dmg, Spd);
                }

                
                public class _NastyPage
                {
                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Kb { get; set; }

                    [Range(0f, 0.9f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Spd { get; set; }

                    [Range(0f, 1.05f), Increment(0.01f), RoundNumber(2), ReloadRequired]
                    public float Dmg { get; set; }

                    [Range(0, 2), ReloadRequired]
                    public int Crt { get; set; }

                    public _NastyPage()
                    {
                        Kb = 0.9f;
                        Spd = 0.9f;
                        Dmg = 1.05f;
                        Crt = 2;
                    }

                    public override bool Equals(object obj) => obj is _NastyPage other && Kb == other.Kb && Spd == other.Spd && Dmg == other.Dmg && Crt == other.Crt;
                    public override int GetHashCode() => HashCode.Combine(Kb, Spd, Dmg, Crt);
                }
                public _LargePage Large { get; set; } = new();
                public _MassivePage Massive { get; set; } = new();
                public _DangerousPage Dangerous { get; set; } = new();
                public _SavagePage Savage { get; set; } = new();
                public _SharpPage Sharp { get; set; } = new();
                public _PointyPage Pointy { get; set; } = new();
                public _LegendaryPage Legendary { get; set; } = new();
                public _TinyPage Tiny { get; set; } = new();
                public _TerriblePage Terrible { get; set; } = new();
                public _SmallPage Small { get; set; } = new();
                public _DullPage Dull { get; set; } = new();
                public _UnhappyPage Unhappy { get; set; } = new();
                public _BulkyPage Bulky { get; set; } = new();
                public _ShamefulPage Shameful { get; set; } = new();
                public _HeavyPage Heavy { get; set; } = new();
                public _LightPage Light { get; set; } = new();
                public _SightedPage Sighted { get; set; } = new();
                public _RapidPage Rapid { get; set; } = new();
                public _HastyPage Hasty { get; set; } = new();
                public _IntimidatingPage Intimidating { get; set; } = new();
                public _DeadlyPage Deadly { get; set; } = new();
                public _StaunchPage Staunch { get; set; } = new();
                public _UnrealPage Unreal { get; set; } = new();
                public _AwfulPage Awful { get; set; } = new();
                public _LethargicPage Lethargic { get; set; } = new();
                public _AwkwardPage Awkward { get; set; } = new();
                public _PowerfulPage Powerful { get; set; } = new();
                public _FrenzyingPage Frenzying { get; set; } = new();
                public _MysticPage Mystic { get; set; } = new();
                public _AdeptPage Adept { get; set; } = new();
                public _MasterfulPage Masterful { get; set; } = new();
                public _MythicalPage Mythical { get; set; } = new();
                public _IneptPage Inept { get; set; } = new();
                public _IgnorantPage Ignorant { get; set; } = new();
                public _DerangedPage Deranged { get; set; } = new();
                public _IntensePage Intense { get; set; } = new();
                public _TabooPage Taboo { get; set; } = new();
                public _CelestialPage Celestial { get; set; } = new();
                public _FuriousPage Furious { get; set; } = new();
                public _ManicPage Manic { get; set; } = new();
                public _Legendary2Page Legendary2 { get; set; } = new();
                public _KeenPage Keen { get; set; } = new();
                public _SuperiorPage Superior { get; set; } = new();
                public _ForcefulPage Forceful { get; set; } = new();
                public _HurtfulPage Hurtful { get; set; } = new();
                public _StrongPage Strong { get; set; } = new();
                public _UnpleasantPage Unpleasant { get; set; } = new();
                public _GodlyPage Godly { get; set; } = new();
                public _DemonicPage Demonic { get; set; } = new();
                public _ZealousPage Zealous { get; set; } = new();
                public _BrokenPage Broken { get; set; } = new();
                public _DamagedPage Damaged { get; set; } = new();
                public _WeakPage Weak { get; set; } = new();
                public _ShoddyPage Shoddy { get; set; } = new();
                public _RuthlessPage Ruthless { get; set; } = new();
                public _QuickPage Quick { get; set; } = new();
                public _Deadly2Page Deadly2 { get; set; } = new();
                public _AgilePage Agile { get; set; } = new();
                public _NimblePage Nimble { get; set; } = new();
                public _MurderousPage Murderous { get; set; } = new();
                public _SlowPage Slow { get; set; } = new();
                public _SluggishPage Sluggish { get; set; } = new();
                public _LazyPage Lazy { get; set; } = new();
                public _AnnoyingPage Annoying { get; set; } = new();
                public _NastyPage Nasty { get; set; } = new();

                public override bool Equals(object obj) => obj is WeaponReforgeNerf other &&
                    Large.Equals(other.Large) &&
                    Massive.Equals(other.Massive) &&
                    Dangerous.Equals(other.Dangerous) &&
                    Savage.Equals(other.Savage) &&
                    Sharp.Equals(other.Sharp) &&
                    Pointy.Equals(other.Pointy) &&
                    Legendary.Equals(other.Legendary) &&
                    Tiny.Equals(other.Tiny) &&
                    Terrible.Equals(other.Terrible) &&
                    Small.Equals(other.Small) &&
                    Dull.Equals(other.Dull) &&
                    Unhappy.Equals(other.Unhappy) &&
                    Bulky.Equals(other.Bulky) &&
                    Shameful.Equals(other.Shameful) &&
                    Heavy.Equals(other.Heavy) &&
                    Light.Equals(other.Light) &&
                    Sighted.Equals(other.Sighted) &&
                    Rapid.Equals(other.Rapid) &&
                    Hasty.Equals(other.Hasty) &&
                    Intimidating.Equals(other.Intimidating) &&
                    Deadly.Equals(other.Deadly) &&
                    Staunch.Equals(other.Staunch) &&
                    Unreal.Equals(other.Unreal) &&
                    Awful.Equals(other.Awful) &&
                    Lethargic.Equals(other.Lethargic) &&
                    Awkward.Equals(other.Awkward) &&
                    Powerful.Equals(other.Powerful) &&
                    Frenzying.Equals(other.Frenzying) &&
                    Mystic.Equals(other.Mystic) &&
                    Adept.Equals(other.Adept) &&
                    Masterful.Equals(other.Masterful) &&
                    Mythical.Equals(other.Mythical) &&
                    Inept.Equals(other.Inept) &&
                    Ignorant.Equals(other.Ignorant) &&
                    Deranged.Equals(other.Deranged) &&
                    Intense.Equals(other.Intense) &&
                    Taboo.Equals(other.Taboo) &&
                    Celestial.Equals(other.Celestial) &&
                    Furious.Equals(other.Furious) &&
                    Manic.Equals(other.Manic) &&
                    Legendary2.Equals(other.Legendary2) &&
                    Keen.Equals(other.Keen) &&
                    Superior.Equals(other.Superior) &&
                    Forceful.Equals(other.Forceful) &&
                    Hurtful.Equals(other.Hurtful) &&
                    Strong.Equals(other.Strong) &&
                    Unpleasant.Equals(other.Unpleasant) &&
                    Godly.Equals(other.Godly) &&
                    Demonic.Equals(other.Demonic) &&
                    Zealous.Equals(other.Zealous) &&
                    Broken.Equals(other.Broken) &&
                    Damaged.Equals(other.Damaged) &&
                    Weak.Equals(other.Weak) &&
                    Shoddy.Equals(other.Shoddy) &&
                    Ruthless.Equals(other.Ruthless) &&
                    Quick.Equals(other.Quick) &&
                    Deadly2.Equals(other.Deadly2) &&
                    Agile.Equals(other.Agile) &&
                    Nimble.Equals(other.Nimble) &&
                    Murderous.Equals(other.Murderous) &&
                    Slow.Equals(other.Slow) &&
                    Sluggish.Equals(other.Sluggish) &&
                    Lazy.Equals(other.Lazy) &&
                    Annoying.Equals(other.Annoying) &&
                    Nasty.Equals(other.Nasty);

                public override int GetHashCode()
                {
                    var hash = new HashCode();
                    hash.Add(Large); hash.Add(Massive); hash.Add(Dangerous); hash.Add(Savage); hash.Add(Sharp);
                    hash.Add(Pointy); hash.Add(Legendary); hash.Add(Tiny); hash.Add(Terrible); hash.Add(Small);
                    hash.Add(Dull); hash.Add(Unhappy); hash.Add(Bulky); hash.Add(Shameful); hash.Add(Heavy);
                    hash.Add(Light); hash.Add(Sighted); hash.Add(Rapid); hash.Add(Hasty); hash.Add(Intimidating);
                    hash.Add(Deadly); hash.Add(Staunch); hash.Add(Unreal); hash.Add(Awful); hash.Add(Lethargic);
                    hash.Add(Awkward); hash.Add(Powerful); hash.Add(Frenzying); hash.Add(Mystic); hash.Add(Adept);
                    hash.Add(Masterful); hash.Add(Mythical); hash.Add(Inept); hash.Add(Ignorant); hash.Add(Deranged);
                    hash.Add(Intense); hash.Add(Taboo); hash.Add(Celestial); hash.Add(Furious); hash.Add(Manic);
                    hash.Add(Legendary2); hash.Add(Keen); hash.Add(Superior); hash.Add(Forceful); hash.Add(Hurtful);
                    hash.Add(Strong); hash.Add(Unpleasant); hash.Add(Godly); hash.Add(Demonic); hash.Add(Zealous);
                    hash.Add(Broken); hash.Add(Damaged); hash.Add(Weak); hash.Add(Shoddy); hash.Add(Ruthless);
                    hash.Add(Quick); hash.Add(Deadly2); hash.Add(Agile); hash.Add(Nimble); hash.Add(Murderous);
                    hash.Add(Slow); hash.Add(Sluggish); hash.Add(Lazy); hash.Add(Annoying); hash.Add(Nasty);
                    return hash.ToHashCode();
                }
            }

            [SeparatePage]
            public class AccessoryReforgeNerfPage
            {
                //defense
                public class _HardPage
                {
                    [Range(0, 1), Slider, ReloadRequired]
                    public int Defense { get; set; }

                    public _HardPage()
                    {
                        Defense = 1;
                    }

                    public override bool Equals(object obj) => obj is _HardPage other && Defense == other.Defense;
                    public override int GetHashCode() => HashCode.Combine(Defense);
                }
                
                public class _GuardingPage
                {
                    [Range(0, 2), Slider, ReloadRequired]
                    public int Defense { get; set; }

                    public _GuardingPage()
                    {
                        Defense = 2;
                    }

                    public override bool Equals(object obj) => obj is _GuardingPage other && Defense == other.Defense;
                    public override int GetHashCode() => HashCode.Combine(Defense);
                }
                
                public class _ArmoredPage
                {
                    [Range(0, 3), Slider, ReloadRequired]
                    public int Defense { get; set; }

                    public _ArmoredPage()
                    {
                        Defense = 3;
                    }

                    public override bool Equals(object obj) => obj is _ArmoredPage other && Defense == other.Defense;
                    public override int GetHashCode() => HashCode.Combine(Defense);
                }
                
                public class _WardingPage
                {
                    [Range(0, 4), Slider, ReloadRequired]
                    public int Defense { get; set; }

                    public _WardingPage()
                    {
                        Defense = 4;
                    }

                    public override bool Equals(object obj) => obj is _WardingPage other && Defense == other.Defense;
                    public override int GetHashCode() => HashCode.Combine(Defense);
                }
                //mana
                
                public class _ArcanePage
                {
                    [Range(0, 20), Slider, ReloadRequired]
                    public int MaxMana { get; set; }

                    public _ArcanePage()
                    {
                        MaxMana = 20;
                    }

                    public override bool Equals(object obj) => obj is _ArcanePage other && MaxMana == other.MaxMana;
                    public override int GetHashCode() => HashCode.Combine(MaxMana);
                }
                //crit
                
                public class _PrecisePage
                {
                    [Range(0f, 2f), Increment(1f), ReloadRequired]
                    public float Crit { get; set; }

                    public _PrecisePage()
                    {
                        Crit = 2f;
                    }

                    public override bool Equals(object obj) => obj is _PrecisePage other && Crit == other.Crit;
                    public override int GetHashCode() => HashCode.Combine(Crit);
                }
                
                public class _LuckyPage
                {
                    [Range(0f, 4f), Increment(1f), ReloadRequired]
                    public float Crit { get; set; }

                    public _LuckyPage()
                    {
                        Crit = 4f;
                    }

                    public override bool Equals(object obj) => obj is _LuckyPage other && Crit == other.Crit;
                    public override int GetHashCode() => HashCode.Combine(Crit);
                }
                //damage
                
                public class _JaggedPage
                {
                    [Range(0f, 0.01f), Increment(0.005f), RoundNumber(3), ReloadRequired]
                    public float Damage { get; set; }

                    public _JaggedPage()
                    {
                        Damage = 0.01f;
                    }

                    public override bool Equals(object obj) => obj is _JaggedPage other && Damage == other.Damage;
                    public override int GetHashCode() => HashCode.Combine(Damage);
                }
                
                public class _SpikedPage
                {
                    [Range(0f, 0.02f), Increment(0.005f), RoundNumber(3), ReloadRequired]
                    public float Damage { get; set; }

                    public _SpikedPage()
                    {
                        Damage = 0.02f;
                    }

                    public override bool Equals(object obj) => obj is _SpikedPage other && Damage == other.Damage;
                    public override int GetHashCode() => HashCode.Combine(Damage);
                }
                
                public class _AngryPage
                {
                    [Range(0f, 0.03f), Increment(0.005f), RoundNumber(3), ReloadRequired]
                    public float Damage { get; set; }

                    public _AngryPage()
                    {
                        Damage = 0.03f;
                    }

                    public override bool Equals(object obj) => obj is _AngryPage other && Damage == other.Damage;
                    public override int GetHashCode() => HashCode.Combine(Damage);
                }
                
                public class _MenacingPage
                {
                    [Range(0f, 0.04f), Increment(0.005f), RoundNumber(3), ReloadRequired]
                    public float Damage { get; set; }

                    public _MenacingPage()
                    {
                        Damage = 0.04f;
                    }

                    public override bool Equals(object obj) => obj is _MenacingPage other && Damage == other.Damage;
                    public override int GetHashCode() => HashCode.Combine(Damage);
                }
                

                //movement
                public class _BriskPage
                {
                    [Range(0f, 0.01f), Increment(0.005f), RoundNumber(3), ReloadRequired]
                    public float MovementSpeed { get; set; }

                    public _BriskPage()
                    {
                        MovementSpeed = 0.01f;
                    }

                    public override bool Equals(object obj) => obj is _BriskPage other && MovementSpeed == other.MovementSpeed;
                    public override int GetHashCode() => HashCode.Combine(MovementSpeed);
                }
                
                public class _FleetingPage
                {
                    [Range(0f, 0.02f), Increment(0.005f), RoundNumber(3), ReloadRequired]
                    public float MovementSpeed { get; set; }

                    public _FleetingPage()
                    {
                        MovementSpeed = 0.02f;
                    }

                    public override bool Equals(object obj) => obj is _FleetingPage other && MovementSpeed == other.MovementSpeed;
                    public override int GetHashCode() => HashCode.Combine(MovementSpeed);
                }
                
                public class _Hasty2Page
                {
                    [Range(0f, 0.03f), Increment(0.005f), RoundNumber(3), ReloadRequired]
                    public float MovementSpeed { get; set; }

                    public _Hasty2Page()
                    {
                        MovementSpeed = 0.03f;
                    }

                    public override bool Equals(object obj) => obj is _Hasty2Page other && MovementSpeed == other.MovementSpeed;
                    public override int GetHashCode() => HashCode.Combine(MovementSpeed);
                }
                
                public class _Quick2Page
                {
                    [Range(0f, 0.04f), Increment(0.005f), RoundNumber(3), ReloadRequired]
                    public float MovementSpeed { get; set; }

                    public _Quick2Page()
                    {
                        MovementSpeed = 0.04f;
                    }

                    public override bool Equals(object obj) => obj is _Quick2Page other && MovementSpeed == other.MovementSpeed;
                    public override int GetHashCode() => HashCode.Combine(MovementSpeed);
                }
                //melee
                
                public class _WildPage
                {
                    [Range(0f, 0.01f), Increment(0.005f), RoundNumber(3), ReloadRequired]
                    public float MeleeSpeed { get; set; }

                    public _WildPage()
                    {
                        MeleeSpeed = 0.01f;
                    }

                    public override bool Equals(object obj) => obj is _WildPage other && MeleeSpeed == other.MeleeSpeed;
                    public override int GetHashCode() => HashCode.Combine(MeleeSpeed);
                }
                
                public class _RashPage
                {
                    [Range(0f, 0.02f), Increment(0.005f), RoundNumber(3), ReloadRequired]
                    public float MeleeSpeed { get; set; }

                    public _RashPage()
                    {
                        MeleeSpeed = 0.02f;
                    }

                    public override bool Equals(object obj) => obj is _RashPage other && MeleeSpeed == other.MeleeSpeed;
                    public override int GetHashCode() => HashCode.Combine(MeleeSpeed);
                }
                
                public class _IntrepidPage
                {
                    [Range(0f, 0.03f), Increment(0.005f), RoundNumber(3), ReloadRequired]
                    public float MeleeSpeed { get; set; }

                    public _IntrepidPage()
                    {
                        MeleeSpeed = 0.03f;
                    }

                    public override bool Equals(object obj) => obj is _IntrepidPage other && MeleeSpeed == other.MeleeSpeed;
                    public override int GetHashCode() => HashCode.Combine(MeleeSpeed);
                }
                
                public class _ViolentPage
                {
                    [Range(0f, 0.04f), Increment(0.005f), RoundNumber(3), ReloadRequired]
                    public float MeleeSpeed { get; set; }

                    public _ViolentPage()
                    {
                        MeleeSpeed = 0.04f;
                    }

                    public override bool Equals(object obj) => obj is _ViolentPage other && MeleeSpeed == other.MeleeSpeed;
                    public override int GetHashCode() => HashCode.Combine(MeleeSpeed);
                }

                //defense
                public _HardPage Hard { get; set; } = new();
                
                public _GuardingPage Guarding { get; set; } = new();
                
                public _ArmoredPage Armored { get; set; } = new();
                
                public _WardingPage Warding { get; set; } = new();
                //mana
                
                public _ArcanePage Arcane { get; set; } = new();
                //crit  
                
                public _PrecisePage Precise { get; set; } = new();
                
                public _LuckyPage Lucky { get; set; } = new();
                //damage
                
                public _JaggedPage Jagged { get; set; } = new();
                
                public _SpikedPage Spiked { get; set; } = new();
                
                public _AngryPage Angry { get; set; } = new();
                
                public _MenacingPage Menacing { get; set; } = new();
                //movement
                
                public _BriskPage Brisk { get; set; } = new();
                
                public _FleetingPage Fleeting { get; set; } = new();
                
                public _Hasty2Page Hasty2 { get; set; } = new();
                
                public _Quick2Page Quick2 { get; set; } = new();
                //melee
                
                public _WildPage Wild { get; set; } = new();
                
                public _RashPage Rash { get; set; } = new();
                
                public _IntrepidPage Intrepid { get; set; } = new();
                
                public _ViolentPage Violent { get; set; } = new();

                public override bool Equals(object obj) => obj is AccessoryReforgeNerfPage other &&
                    Hard.Equals(other.Hard) &&
                    Guarding.Equals(other.Guarding) &&
                    Armored.Equals(other.Armored) &&
                    Warding.Equals(other.Warding) &&
                    Arcane.Equals(other.Arcane) &&
                    Precise.Equals(other.Precise) &&
                    Lucky.Equals(other.Lucky) &&
                    Jagged.Equals(other.Jagged) &&
                    Spiked.Equals(other.Spiked) &&
                    Angry.Equals(other.Angry) &&
                    Menacing.Equals(other.Menacing) &&
                    Brisk.Equals(other.Brisk) &&
                    Fleeting.Equals(other.Fleeting) &&
                    Hasty2.Equals(other.Hasty2) &&
                    Quick2.Equals(other.Quick2) &&
                    Wild.Equals(other.Wild) &&
                    Rash.Equals(other.Rash) &&
                    Intrepid.Equals(other.Intrepid) &&
                    Violent.Equals(other.Violent);

                public override int GetHashCode()
                {
                    var hash = new HashCode();
                    hash.Add(Hard);
                    hash.Add(Guarding);
                    hash.Add(Armored);
                    hash.Add(Warding);
                    hash.Add(Arcane);
                    hash.Add(Precise);
                    hash.Add(Lucky);
                    hash.Add(Jagged);
                    hash.Add(Spiked);
                    hash.Add(Angry);
                    hash.Add(Menacing);
                    hash.Add(Brisk);
                    hash.Add(Fleeting);
                    hash.Add(Hasty2);
                    hash.Add(Quick2);
                    hash.Add(Wild);
                    hash.Add(Rash);
                    hash.Add(Intrepid);
                    hash.Add(Violent);
                    return hash.ToHashCode();
                }

            }
            
            public WeaponReforgeNerf WpnReforgeNerfs = new();
            
            public AccessoryReforgeNerfPage AccReforgeNerfs = new();

            public override bool Equals(object obj) => obj is ReforgeNerfPage other &&
                WpnReforgeNerfs.Equals(other.WpnReforgeNerfs) &&
                AccReforgeNerfs.Equals(other.AccReforgeNerfs);

            public override int GetHashCode() => HashCode.Combine(WpnReforgeNerfs, AccReforgeNerfs);
        }

        #region new()
        
        public ReforgeNerfPage ReforgeNerf = new();
        #endregion
    }

    [SeparatePage]
    public class Adv_NpcPage
    {
        [SeparatePage]
        public class BossStatPage
        {
            [Range(100, 10000)]
            public int LifePercent;

            [Range(100, 10000)]
            public int DefensePercent;

            [Range(100, 10000)]
            public int DamagePercent;
            
            public bool DisableBossStatChanges;
            public BossStatPage()
            {
                LifePercent = 100;
                DamagePercent = 100;
                DefensePercent = 100;
                DisableBossStatChanges = true;
            }
        }

        [SeparatePage]
        public class InvasionSizePage
        {
            [Range(120, int.MaxValue)]
            public int InvasionSize { get; set; }

            public InvasionSizePage()
            {
                InvasionSize = 120;
            }
        }

        [SeparatePage]
        public class NpcStatPage
        {
            [Range(100, 10000)]
            public int LifePercent;

            [Range(100, 10000)]
            public int DefensePercent;

            [Range(100, 10000)]
            public int DamagePercent;
            
            public bool DisableNPCStatChanges;

            public NpcStatPage()
            {
                LifePercent = 100;
                DamagePercent = 100;
                DefensePercent = 100;
                DisableNPCStatChanges = true;
            }
        }

        [SeparatePage]
        public class PriceMultiplierPage
        {
            [Range(0f, 8f), Increment(0.05f), RoundNumber(2)]
            public float BuyMult { get; set; }

            [Range(0f, 8f), Increment(0.05f), RoundNumber(2)]
            public float SellMult { get; set; }

            public PriceMultiplierPage()
            {
                BuyMult = 1f;
                SellMult = 1f;
            }
        }

        [SeparatePage]
        public class SpawnRatePage
        {
            [Range(int.MinValue, int.MaxValue)]
            public int SpawnRate { get; set; }

            [Range(int.MinValue, int.MaxValue)]
            public int MaxSpawns { get; set; }

            public SpawnRatePage()
            {
                SpawnRate = 0;
                MaxSpawns = 0;
            }
        }

        #region new()
        
        public BossStatPage BossConfig = new();

        public InvasionSizePage InvasionConfig = new();
        
        public NpcStatPage NpcConfig = new();

        public PriceMultiplierPage PriceConfig = new();

        public SpawnRatePage SpawnRateConfig = new();
        #endregion
    }

    [SeparatePage]
    public class Adv_PlayerPage
    {
        [SeparatePage]
        public class PlayerStatPage
        {
            #region Vitality

            [Slider, Range(1, 100)]
            public int LifePercent;

            [Slider, Range(1, 100)]
            public int LifeRegenPercent;

            [Range(1f, 100f), Increment(1f), ReloadRequired]
            public float DefensePercent;

            [Range(1f, 100f), Increment(1f), ReloadRequired]
            public float EndurancePercent;

            #endregion

            #region Offense

            [Range(1f, 100f), Increment(1f), ReloadRequired]
            public float DamagePercent;

            [Range(1f, 100f), Increment(1f), ReloadRequired]
            public float ArmorPenetrationPercent;

            [Range(1f, 100f), Increment(1f), ReloadRequired]
            public float AttackSpeedPercent;

            #endregion

            #region Magic

            [Slider, Range(1, 100)]
            public int ManaPercent;

            [Slider, Range(1, 100)]
            public int ManaRegenPercent;

            [Range(1f, 100f), Increment(1f), ReloadRequired]
            public float ManaCostPercent;

            #endregion

            #region Slavery

            [Slider, Range(1, 100)]
            public int MaxMinionsPercent;

            [Slider, Range(1, 100)]
            public int MaxTurretsPercent;

            #endregion

            #region Mobility

            [Range(1f, 100f), Increment(1f), ReloadRequired]
            public float MoveSpeedPercent;

            [Range(1f, 100f), Increment(1f), ReloadRequired]
            public float JumpSpeedPercent;

            [Slider, Range(1, 100)]
            public int JumpHeightPercent;

            [Slider, Range(1, 100)]
            public int WingTimePercent;

            #endregion

            #region World Shaping

            [Range(1f, 100f), Increment(1f), ReloadRequired]
            public float PickSpeedPercent;

            [Range(1f, 100f), Increment(1f), ReloadRequired]
            public float TileSpeedPercent;

            [Range(1f, 100f), Increment(1f), ReloadRequired]
            public float WallSpeedPercent;

            
            public bool DisablePlayerStatChanges;
            #endregion

            public PlayerStatPage()
            {
                LifePercent = 100;
                LifeRegenPercent = 100;
                DefensePercent = 100;
                EndurancePercent = 100;

                DamagePercent = 100;
                ArmorPenetrationPercent = 100;
                AttackSpeedPercent = 100;

                ManaPercent = 100;
                ManaRegenPercent = 100;
                ManaCostPercent = 100;

                MaxMinionsPercent = 100;
                MaxTurretsPercent = 100;

                MoveSpeedPercent = 100;
                JumpSpeedPercent = 100;
                JumpHeightPercent = 100;
                WingTimePercent = 100;

                PickSpeedPercent = 100;
                TileSpeedPercent = 100;
                WallSpeedPercent = 100;

                DisablePlayerStatChanges = true;
            }
        }

        #region new()
        
        public PlayerStatPage PlayerStats = new();
        #endregion
    }

    //[SeparatePage]
    //public class Adv_RecipesPage
    //{

    //}

    #endregion

    #region new()

    [ColourPalette, BackgroundColor(180, 215, 255, 200)]
    public Adv_EnvironmentPage Adv_Environment = new();

    [ColourPalette, BackgroundColor(145, 185, 230, 200)]
    public Adv_ItemsPage Adv_Items = new();

    [ColourPalette, BackgroundColor(110, 155, 200, 200)]
    public Adv_NpcPage Adv_Npc = new();

    [ColourPalette, BackgroundColor(80, 125, 175, 200)]
    public Adv_PlayerPage Adv_Player = new();

    //[BackgroundColor(55, 105, 155, 200)]
    //public Adv_RecipesPage Adv_Recipes = new();

    #endregion

    public override void OnLoaded() => AdvServerConfig = this;
}
/*

BackgroundColor(180, 215, 255)
BackgroundColor(145, 185, 230)
BackgroundColor(110, 155, 200)
BackgroundColor(80, 125, 175)
BackgroundColor(55, 105, 155)
BackgroundColor(35, 80, 130)
BackgroundColor(20, 60, 105)
BackgroundColor(0, 30, 60)
BackgroundColor(0, 20, 40)

*/