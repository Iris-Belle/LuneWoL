namespace LuneWoL.Common.Systems;

public partial class LuneWoL_Sys : ModSystem
{

    #region Fields

    private enum Once
    {
        InvasionMult
    }
    private static readonly RunOneTimeLib<Once> _once = new();


    internal int _currentMoonPhase;
    internal bool _wasDaytime = true;

    #endregion

    #region Hooks

    public override void PreUpdateInvasions() => LongerInvasions();

    public override void PostAddRecipes() => RecipeMulti();

    public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
    {
        if (ServerConfig.Environment.DarkerNightsMode != 0)
            DarkerNightsSurfaceLight(ref tileColor, ref backgroundColor);
    }

    public override void PostUpdateWorld()
    {
        if (Main.dayTime && !_wasDaytime)
        {
            _currentMoonPhase = (_currentMoonPhase + 1) % 8;
            Main.moonPhase = _currentMoonPhase;
        }
        _wasDaytime = Main.dayTime;
    }

    #endregion

    #region Methods

    public void DarkerNightsSurfaceLight(ref Color tileColor, ref Color backgroundColor)
    {
        if (Main.dayTime)
            return;

        LuneWoL_ServerConfig.EnvironmentPage cfg = ServerConfig.Environment;
        LuneWoL_AdvServerConfig.Adv_EnvironmentPage.DarkerNightsPage mcfg = AdvServerConfig.Adv_Environment.DarkerNights;
        float moonMultiplier = GetMoonPhaseMultiplier(_currentMoonPhase);

        const float nightLength = 32400f;
        float fadeTicks = MathHelper.Clamp(mcfg.NightFadeDuration * 60f, 0f, nightLength / 2f);
        float t = (float)Main.time;

        float minB = cfg.DarkerNightsMode == 2 ? mcfg.MinBrightness : 1f;
        if (cfg.DarkerNightsMode == 1)
            minB *= moonMultiplier;

        float brightness = (t <= fadeTicks) ? MathHelper.Lerp(1f, minB, t / fadeTicks) : (t >= nightLength - fadeTicks) ? MathHelper.Lerp(minB, 1f, (t - (nightLength - fadeTicks)) / fadeTicks) : minB;

        tileColor = ToColour(tileColor.ToVector3() * brightness);
        backgroundColor = ToColour(backgroundColor.ToVector3() * brightness);
    }

    private static float GetMoonPhaseMultiplier(int phase)
    {
        LuneWoL_AdvServerConfig.Adv_EnvironmentPage.DarkerNightsPage cfg = AdvServerConfig.Adv_Environment.DarkerNights;
        return phase switch
        {
            0 => cfg.MoonPhases.FullMoonMult,
            1 => cfg.MoonPhases.WaningGibbousMult,
            2 => cfg.MoonPhases.ThirdQuarterMult,
            3 => cfg.MoonPhases.WaningCrescentMult,
            4 => cfg.MoonPhases.NewMoonMult,
            5 => cfg.MoonPhases.WaxingCrescentMult,
            6 => cfg.MoonPhases.FirstQuarterMult,
            7 => cfg.MoonPhases.WaxingGibbousMult,
            _ => 1f,
        };
    }

    public static void LongerInvasions()
    {
        LuneWoL_ServerConfig.NpcPage Config = ServerConfig.Npc;

        if (Config.InvasionMultiplier > 1 && Main.invasionType != 0 && _once.Once(Once.InvasionMult))
        {
            Main.invasionSizeStart = Config.InvasionMultiplier switch
            {
                1 => 120,
                2 => 240,
                3 => 360,
                4 => 480,
                5 => 600,
                6 => 720,
                7 => 840,
                8 => 960,
                9 => 1080,
                10 => 1200,
                11 => 1320,
                12 => 1440,
                13 => 1560,
                14 => 1680,
                15 => 1800,
                16 => 1920,
                17 => 2040,
                18 => 2160,
                19 => 2280,
                20 => 2400,
                21 => 2520,
                22 => 2640,
                23 => 2760,
                24 => 2880,
                25 => 3000,
                26 => 3120,
                27 => 3240,
                28 => 3360,
                29 => 3480,
                30 => 3600,
                31 => 3720,
                32 => 3840,
                33 => 3960,
                34 => 4080,
                35 => 4200,
                36 => 4320,
                37 => 4440,
                38 => 4560,
                39 => 4680,
                40 => 4800,
                41 => 4920,
                42 => 5040,
                43 => 5160,
                44 => 5280,
                45 => 5400,
                46 => 5520,
                47 => 5640,
                48 => 5760,
                49 => 5880,
                50 => 6000,
                _ => 123
            };
            Main.invasionSize = Config.InvasionMultiplier switch
            {
                1 => 120,
                2 => 240,
                3 => 360,
                4 => 480,
                5 => 600,
                6 => 720,
                7 => 840,
                8 => 960,
                9 => 1080,
                10 => 1200,
                11 => 1320,
                12 => 1440,
                13 => 1560,
                14 => 1680,
                15 => 1800,
                16 => 1920,
                17 => 2040,
                18 => 2160,
                19 => 2280,
                20 => 2400,
                21 => 2520,
                22 => 2640,
                23 => 2760,
                24 => 2880,
                25 => 3000,
                26 => 3120,
                27 => 3240,
                28 => 3360,
                29 => 3480,
                30 => 3600,
                31 => 3720,
                32 => 3840,
                33 => 3960,
                34 => 4080,
                35 => 4200,
                36 => 4320,
                37 => 4440,
                38 => 4560,
                39 => 4680,
                40 => 4800,
                41 => 4920,
                42 => 5040,
                43 => 5160,
                44 => 5280,
                45 => 5400,
                46 => 5520,
                47 => 5640,
                48 => 5760,
                49 => 5880,
                50 => 6000,
                _ => 123
            };
            Main.invasionProgressMax = Config.InvasionMultiplier switch
            {
                1 => 120,
                2 => 240,
                3 => 360,
                4 => 480,
                5 => 600,
                6 => 720,
                7 => 840,
                8 => 960,
                9 => 1080,
                10 => 1200,
                11 => 1320,
                12 => 1440,
                13 => 1560,
                14 => 1680,
                15 => 1800,
                16 => 1920,
                17 => 2040,
                18 => 2160,
                19 => 2280,
                20 => 2400,
                21 => 2520,
                22 => 2640,
                23 => 2760,
                24 => 2880,
                25 => 3000,
                26 => 3120,
                27 => 3240,
                28 => 3360,
                29 => 3480,
                30 => 3600,
                31 => 3720,
                32 => 3840,
                33 => 3960,
                34 => 4080,
                35 => 4200,
                36 => 4320,
                37 => 4440,
                38 => 4560,
                39 => 4680,
                40 => 4800,
                41 => 4920,
                42 => 5040,
                43 => 5160,
                44 => 5280,
                45 => 5400,
                46 => 5520,
                47 => 5640,
                48 => 5760,
                49 => 5880,
                50 => 6000,
                _ => 123
            };
        }

        if (Config.InvasionMultiplier > 1 && Main.invasionType == 0)
        {
            _once.Reset(Once.InvasionMult);
        }
    }

    public static void RecipeMulti()
    {
        LuneWoL_ServerConfig.RecipesPage Config = ServerConfig.Recipes;

        if (Config.RecipePercent == 0)
            return;

        float multiplier = 1 + (Config.RecipePercent / 100f);

        foreach (Recipe recipe in Main.recipe)
        {
            foreach (Item item in recipe.requiredItem)
            {
                if (item.stack > 0 && !Config.IgnoreStacksOfOne)
                {
                    item.stack = (int)(item.stack * multiplier);
                }
                else if (item.stack > 1 && Config.IgnoreStacksOfOne)
                {
                    item.stack = (int)(item.stack * multiplier);
                }
            }
        }
    }

    public static Color ToColour(Vector3 v) => new((int)(v.X * 255), (int)(v.Y * 255), (int)(v.Z * 255));

    #endregion

}
