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

    internal void DarkerNightsSurfaceLight(ref Color tileColor, ref Color backgroundColor)
    {
        if (Main.dayTime)
            return;

        LuneWoL_ServerConfig.EnvironmentPage cfg = ServerConfig.Environment;
        LuneWoL_AdvServerConfig.Adv_EnvironmentPage.DarkerNightsPage mcfg = AdvServerConfig.Adv_Environment.DarkerNights;
        float moonMultiplier = GetMoonPhaseMultiplier(_currentMoonPhase);

        float fadetime = MathHelper.Clamp(mcfg.NightFadeDuration * 60f, 0f, 32400f / 2f);
        float time = (float)Main.time;

        float minB = cfg.DarkerNightsMode == 2 ? mcfg.MinBrightness : 1f;
        if (cfg.DarkerNightsMode == 1)
            minB *= moonMultiplier;

        float brightness = (time <= fadetime) ? MathHelper.Lerp(1f, minB, time / fadetime) : (time >= 32400f - fadetime) ? MathHelper.Lerp(minB, 1f, (time - (32400f - fadetime)) / fadetime) : minB;

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

    internal static void RecipeMulti()
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

    internal static Color ToColour(Vector3 v) => new((int)(v.X * 255), (int)(v.Y * 255), (int)(v.Z * 255));

    #endregion

}
