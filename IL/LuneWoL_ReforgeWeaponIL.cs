namespace LuneWoL.IL;

internal class LuneWoL_ReforgeWeaponIL : ILoadable
{
    public bool IsLoadingEnabled(Mod mod) => ServerConfig.Items.ReforgeNerf;
    public void Unload() { }

    public void Load(Mod mod)
    {
        var method0 = typeof(Item).GetMethod("TryGetPrefixStatMultipliersForItem", BindingFlags.NonPublic | BindingFlags.Instance, null, [typeof(int), typeof(float).MakeByRefType(), typeof(float).MakeByRefType(), typeof(float).MakeByRefType(), typeof(float).MakeByRefType(), typeof(float).MakeByRefType(), typeof(float).MakeByRefType(), typeof(int).MakeByRefType()], null)
        ?? throw new Exception("smelly hooking on TryGetPrefixStatMultipliersForItem. if you are not me, dm me on discord at ´iris_lune' or join my server and ping me. you could also leave a comment on the mod but that might take longer to see and patch");
        
        MonoModHooks.Modify(method0, ReforgeWeaponIL);
    }

    private readonly static LuneWoL_AdvServerConfig.Adv_ItemsPage.ReforgeNerfPage.WeaponReforgeNerf cfg = AdvServerConfig.Adv_Items.ReforgeNerf.WpnReforgeNerfs;

    private void ReforgeWeaponIL(ILContext il)
    {
        ILCursor c = new(il);

        #region Large
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.12f))) //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Large.Size);
        }
        #endregion

        #region Massive
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.18f))) //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Massive.Size);
        }
        #endregion

        #region Dangerous
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Dangerous.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(2))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Dangerous.Crt);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f))) //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Dangerous.Size);
        }
        #endregion
        
        #region Savage
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Savage.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Savage.Size);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Savage.Kb);
        }
        #endregion

        #region Sharp
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Sharp.Dmg);
        }
        #endregion

        #region Pointy
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Pointy.Dmg);
        }
        #endregion

        #region Legendary
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Legendary.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Legendary.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(5))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Legendary.Crt);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Legendary.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Legendary.Size);
        }
        #endregion

        #region Tiny
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.82f))) //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Tiny.Size);
        }
        #endregion

        #region Terrible
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Terrible.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Terrible.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.87f))) //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Terrible.Size);
        }
        #endregion

        #region Small
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Small.Size);
        }
        #endregion

        #region Dull
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Dull.Dmg);
        }
        #endregion

        #region Unhappy
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unhappy.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unhappy.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unhappy.Size);
        }
        #endregion

        #region Bulky
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Bulky.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Bulky.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Bulky.Size);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Bulky.Spd);
        }
        #endregion

        #region Shameful
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.8f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Shameful.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Shameful.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Shameful.Size);
        }
        #endregion

        #region Heavy
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Heavy.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Heavy.Spd);
        }
        #endregion

        #region Light
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Light.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Light.Spd);
        }
        #endregion

        #region Sighted
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Sighted.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(3))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Sighted.Crt);
        }
        #endregion

        #region Rapid
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Rapid.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Rapid.Shtspd);
        }
        #endregion

        #region Hasty
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Hasty.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Hasty.Shtspd);
        }
        #endregion

        #region Intimidating
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Intimidating.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f))) //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Intimidating.Shtspd);
        }
        #endregion

        #region Deadly
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deadly.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f))) //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deadly.Shtspd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deadly.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.95f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deadly.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(2))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Deadly.Crt);
        }
        #endregion

        #region Staunch
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Staunch.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Staunch.Dmg);
        }
        #endregion

        #region Unreal
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unreal.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unreal.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(5))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Unreal.Crt);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unreal.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unreal.Shtspd);
        }
        #endregion

        #region Awful
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Awful.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Awful.Shtspd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Awful.Dmg);
        }
        #endregion

        #region Lethargic
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Lethargic.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Lethargic.Shtspd);
        }
        #endregion

        #region Awkward
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Awkward.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.8f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Awkward.Kb);
        }
        #endregion

        #region Powerful
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Powerful.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Powerful.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(1))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Powerful.Crt);
        }
        #endregion

        #region Frenzying
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Frenzying.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Frenzying.Dmg);
        }
        #endregion

        #region Mystic
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f))) //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Mystic.Mcst);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Mystic.Dmg);
        }
        #endregion

        #region Adept
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f))) //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Adept.Mcst);
        }
        #endregion

        #region Masterful
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f))) //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Masterful.Mcst);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Masterful.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Masterful.Kb);
        }
        #endregion

        #region Mythical
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Mythical.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Mythical.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(5))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Mythical.Crt);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Mythical.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Mythical.Mcst);
        }
        #endregion

        #region Inept
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Inept.Mcst);
        }
        #endregion

        #region Ignorant
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.2f))) //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Ignorant.Mcst);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Ignorant.Dmg);
        }
        #endregion

        #region Deranged
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deranged.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deranged.Dmg);
        }
        #endregion

        #region Intense
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Intense.Mcst);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Intense.Dmg);
        }
        #endregion

        #region Taboo
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Taboo.Mcst);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Taboo.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Taboo.Spd);
        }
        #endregion

        #region Celestial
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Celestial.Mcst);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Celestial.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Celestial.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Celestial.Dmg);
        }
        #endregion

        #region Furious powerfist aha aha aha
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.2f))) //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Furious.Mcst);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Furious.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Furious.Kb);
        }
        #endregion

        #region Manic
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Manic.Mcst);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Manic.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Manic.Spd);
        }
        #endregion

        #region Legendary2??? waht
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.17f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Legendary2.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.17f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Legendary2.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(8))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Legendary2.Crt);
        }
        #endregion

        #region Keen
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(3))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Keen.Crt);
        }
        #endregion

        #region Superior
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Superior.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(3))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Superior.Crt);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Superior.Kb);
        }
        #endregion

        #region Forceful
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Forceful.Kb);
        }
        #endregion

        #region Hurtful
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Hurtful.Dmg);
        }
        #endregion

        #region Strong
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Strong.Kb);
        }
        #endregion

        #region Unpleasant
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unpleasant.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unpleasant.Dmg);
        }
        #endregion

        #region Godly
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Godly.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Godly.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(5))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Godly.Crt);
        }
        #endregion

        #region Demonic
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Demonic.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(5))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Demonic.Crt);
        }
        #endregion

        #region Zealous
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(5))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Zealous.Crt);
        }
        #endregion

        #region Broken
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.7f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Broken.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.8f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Broken.Kb);
        }
        #endregion

        #region Damaged
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Damaged.Dmg);
        }
        #endregion

        #region Weak
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.8f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Weak.Kb);
        }
        #endregion

        #region Shoddy
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Shoddy.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Shoddy.Dmg);
        }
        #endregion

        #region Ruthless
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Ruthless.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.18f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Ruthless.Dmg);
        }
        #endregion

        #region Quick
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Quick.Spd);
        }
        #endregion

        #region Deadly2?????
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deadly2.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deadly2.Spd);
        }
        #endregion

        #region Agile
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Agile.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(3))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Agile.Crt);
        }
        #endregion

        #region Nimble
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.95f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Nimble.Spd);
        }
        #endregion

        #region Murderous
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(3))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Murderous.Crt);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.94f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Murderous.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.07f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Murderous.Dmg);
        }
        #endregion

        #region Slow
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Slow.Spd);
        }
        #endregion

        #region Sluggish
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.2f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Sluggish.Spd);
        }
        #endregion

        #region Lazy
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.08f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Lazy.Spd);
        }
        #endregion

        #region Annoying
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.8f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Annoying.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Annoying.Spd);
        }
        #endregion

        #region Nasty
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Nasty.Kb);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f))) //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Nasty.Spd);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f))) //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Nasty.Dmg);
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(2))) //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Nasty.Crt);
        }
        #endregion
    }
}
