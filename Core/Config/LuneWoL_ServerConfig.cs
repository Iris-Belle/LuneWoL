namespace LuneWoL.Core.Config;

[BackgroundColor(35, 80, 130, 200)]
public class LuneWoL_ServerConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    public override bool NeedsReload(ModConfig pendingConfig)
    {
        if (pendingConfig is not LuneWoL_ServerConfig cfg)
            return base.NeedsReload(pendingConfig);

        return Environment.NeedsReload(cfg.Environment) ||
               Items.NeedsReload(cfg.Items) ||
               Npc.NeedsReload(cfg.Npc) ||
               Player.NeedsReload(cfg.Player) ||
               Recipes.NeedsReload(cfg.Recipes);
    }

    [SeparatePage]
    public class EnvironmentPage
    {
        [Header("bool")]
        
        public bool EvilBiomeDoT { get; set; }
        
        public bool MurkyWater { get; set; }
        
        public bool OreDropChance { get; set; }

        [ReloadRequired]
        public bool OreScarcityPatchToggle { get; set; }
        
        public bool PoisonousWater { get; set; }
        
        public bool SpaceDoT { get; set; }
        
        public bool TundraGivesChilled { get; set; }
        
        public bool UnderworldGivesOnFire { get; set; }
        
        public bool ViscousWater { get; set; }
        
        public bool WeatherEffects { get; set; }

        public bool WindAffectsArrows { get; set; }


        [Header("I4")]

        [Slider, DrawTicks, Range(0, 2)]
        public int DarkerNightsMode { get; set; }

        [Slider, DrawTicks, Range(0, 3)]
        public int DepthPressureMode { get; set; }

        public EnvironmentPage()
        {
            //bool
            EvilBiomeDoT = false;
            MurkyWater = false;
            OreDropChance = false;
            OreScarcityPatchToggle = false;
            PoisonousWater = false;
            SpaceDoT = false;
            TundraGivesChilled = false;
            UnderworldGivesOnFire = false;
            ViscousWater = false;
            WeatherEffects = false;
            WindAffectsArrows = false;
            //I4
            DarkerNightsMode = 0;
            DepthPressureMode = 0;
        }
        public bool NeedsReload(EnvironmentPage other)
        {
            return OreScarcityPatchToggle != other.OreScarcityPatchToggle;
        }
    }

    [SeparatePage]
    public class ItemsPage
    {
        [Header("bool")]

        [ReloadRequired]
        public bool DisableAutoReuse { get; set; }

        public bool DisableAccessories { get; set; }

        [ReloadRequired]
        public bool ReforgeNerf { get; set; }

        [Header("I4")]

        [Range(-1, int.MaxValue), ReloadRequired]
        public int DespawnItemsTimer { get; set; }

        [Slider, Range(0, 100)]
        public int HealingPotionBadPercent { get; set; }

        public ItemsPage()
        {
            //bool
            DisableAutoReuse = false;
            DisableAccessories = false;
            ReforgeNerf = false;
            //I4
            DespawnItemsTimer = -1;
            HealingPotionBadPercent = 100;
        }

        public bool NeedsReload(ItemsPage other)
        {
            return //bool
                   DisableAutoReuse != other.DisableAutoReuse ||
                   ReforgeNerf != other.ReforgeNerf ||
                   //I4
                   DespawnItemsTimer != other.DespawnItemsTimer;
        }
    }

    [SeparatePage]
    public class NpcPage
    {
        [Header("bool")]

        [ReloadRequired]
        public bool InvasionSizePatchToggle { get; set; }

        [ReloadRequired]
        public bool PricePatchToggle { get; set; }
        
        public bool SpawnRateQuickToggle { get; set; }

        [Header("I4")]

        [Range(-1, int.MaxValue), ReloadRequired]
        public int MaxNpcValue { get; set; }

        [Header("R4")]

        [Range(0f, 1f), Increment(0.05f), RoundNumber(2), ReloadRequired]
        public float NpcValueMult { get; set; }

        public NpcPage()
        {
            //bool
            InvasionSizePatchToggle = false;
            PricePatchToggle = false;
            //I4
            MaxNpcValue = -1;
            //R4
            NpcValueMult = 1f;
        }

        public bool NeedsReload(NpcPage other)
        {
            return //bool
                   InvasionSizePatchToggle != other.InvasionSizePatchToggle ||
                   PricePatchToggle != other.PricePatchToggle ||
                   //I4
                   MaxNpcValue != other.MaxNpcValue ||
                   //R4
                   NpcValueMult != other.NpcValueMult;
        }
    }

    [SeparatePage]
    public class PlayerPage
    {
        [Header("I4")]

        [Slider, DrawTicks, Range(0, 3), ReloadRequired]
        public int DeathPenaltyMode { get; set; }

        [Header("R4")]

        [Range(1f, 16f), Increment(0.05f), RoundNumber(2)]
        public float DebuffMultiplier { get; set; }

        public PlayerPage()
        {
            //I4
            DeathPenaltyMode = 0;
            //R4
            DebuffMultiplier = 1f;
        }

        public bool NeedsReload(PlayerPage other)
        {
            return DeathPenaltyMode != other.DeathPenaltyMode;
        }
    }

    [SeparatePage]
    public class RecipesPage
    {
        [Header("bool")]

        [ReloadRequired]
        public bool IgnoreStacksOfOne;

        [Header("R4")]

        [Range(0f, 100f), Increment(1f), ReloadRequired, RoundNumber(1)]
        public float RecipePercent;

        public RecipesPage()
        {
            //bool
            IgnoreStacksOfOne = true;
            //R4
            RecipePercent = 0f;
        }

        public bool NeedsReload(RecipesPage other)
        {
            return //bool
                   IgnoreStacksOfOne != other.IgnoreStacksOfOne ||
                   //R4
                   RecipePercent != other.RecipePercent;
        }
    }

    #region new()

    [ColourPalette, BackgroundColor(180, 215, 255, 200)]
    public EnvironmentPage Environment = new();

    [ColourPalette, BackgroundColor(145, 185, 230, 200)]
    public ItemsPage Items = new();

    [ColourPalette, BackgroundColor(110, 155, 200, 200)]
    public NpcPage Npc = new();

    [ColourPalette, BackgroundColor(80, 125, 175, 200)]
    public PlayerPage Player = new();

    [ColourPalette, BackgroundColor(55, 105, 155, 200)]
    public RecipesPage Recipes = new();

    #endregion

    public override void OnLoaded() => ServerConfig = this;
}
/*

BackgroundColor(180, 215, 255)
BackgroundColor(145, 185, 230)
BackgroundColor(110, 155, 200)
BackgroundColor(80, 125, 175)
BackgroundColor(55, 105, 155)
BackgroundColor(35, 80, 130)
BackgroundColor(20, 60, 105)
BackgroundColor(0, 20, 40)

*/