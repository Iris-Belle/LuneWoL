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
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.12f)); //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Large.Size);
        }
        #endregion

        #region Massive
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.18f)); //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Massive.Size);
        }
        #endregion

        #region Dangerous
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Dangerous.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(2)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Dangerous.Crt);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f)); //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Dangerous.Size);
        }
        #endregion
        
        #region Savage
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Savage.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Savage.Size);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Savage.Kb);
        }
        #endregion

        #region Sharp
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Sharp.Dmg);
        }
        #endregion

        #region Pointy
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Pointy.Dmg);
        }
        #endregion

        #region Legendary
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Legendary.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Legendary.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(5)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Legendary.Crt);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Legendary.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Legendary.Size);
        }
        #endregion

        #region Tiny
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.82f)); //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Tiny.Size);
        }
        #endregion

        #region Terrible
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Terrible.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Terrible.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.87f)); //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Terrible.Size);
        }
        #endregion

        #region Small
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Small.Size);
        }
        #endregion

        #region Dull
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Dull.Dmg);
        }
        #endregion

        #region Unhappy
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unhappy.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unhappy.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unhappy.Size);
        }
        #endregion

        #region Bulky
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Bulky.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Bulky.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Bulky.Size);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Bulky.Spd);
        }
        #endregion

        #region Shameful
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.8f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Shameful.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Shameful.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //size
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Shameful.Size);
        }
        #endregion

        #region Heavy
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Heavy.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Heavy.Spd);
        }
        #endregion

        #region Light
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Light.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Light.Spd);
        }
        #endregion

        #region Sighted
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Sighted.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(3)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Sighted.Crt);
        }
        #endregion

        #region Rapid
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Rapid.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Rapid.Shtspd);
        }
        #endregion

        #region Hasty
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Hasty.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Hasty.Shtspd);
        }
        #endregion

        #region Intimidating
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Intimidating.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f)); //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Intimidating.Shtspd);
        }
        #endregion

        #region Deadly
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deadly.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f)); //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deadly.Shtspd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deadly.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.95f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deadly.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(2)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Deadly.Crt);
        }
        #endregion

        #region Staunch
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Staunch.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Staunch.Dmg);
        }
        #endregion

        #region Unreal
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unreal.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unreal.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(5)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Unreal.Crt);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unreal.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unreal.Shtspd);
        }
        #endregion

        #region Awful
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Awful.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Awful.Shtspd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Awful.Dmg);
        }
        #endregion

        #region Lethargic
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Lethargic.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //shtspd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Lethargic.Shtspd);
        }
        #endregion

        #region Awkward
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Awkward.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.8f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Awkward.Kb);
        }
        #endregion

        #region Powerful
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Powerful.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Powerful.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(1)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Powerful.Crt);
        }
        #endregion

        #region Frenzying
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Frenzying.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Frenzying.Dmg);
        }
        #endregion

        #region Mystic
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f)); //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Mystic.Mcst);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Mystic.Dmg);
        }
        #endregion

        #region Adept
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f)); //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Adept.Mcst);
        }
        #endregion

        #region Masterful
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f)); //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Masterful.Mcst);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Masterful.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Masterful.Kb);
        }
        #endregion

        #region Mythical
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Mythical.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Mythical.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(5)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Mythical.Crt);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Mythical.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Mythical.Mcst);
        }
        #endregion

        #region Inept
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Inept.Mcst);
        }
        #endregion

        #region Ignorant
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.2f)); //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Ignorant.Mcst);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Ignorant.Dmg);
        }
        #endregion

        #region Deranged
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deranged.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deranged.Dmg);
        }
        #endregion

        #region Intense
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Intense.Mcst);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Intense.Dmg);
        }
        #endregion

        #region Taboo
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Taboo.Mcst);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Taboo.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Taboo.Spd);
        }
        #endregion

        #region Celestial
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Celestial.Mcst);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Celestial.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Celestial.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Celestial.Dmg);
        }
        #endregion

        #region Furious powerfist aha aha aha
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.2f)); //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Furious.Mcst);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Furious.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Furious.Kb);
        }
        #endregion

        #region Manic
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //mcst
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Manic.Mcst);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Manic.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Manic.Spd);
        }
        #endregion

        #region Legendary2??? waht
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.17f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Legendary2.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.17f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Legendary2.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(8)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Legendary2.Crt);
        }
        #endregion

        #region Keen
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(3)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Keen.Crt);
        }
        #endregion

        #region Superior
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Superior.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(3)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Superior.Crt);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Superior.Kb);
        }
        #endregion

        #region Forceful
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Forceful.Kb);
        }
        #endregion

        #region Hurtful
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Hurtful.Dmg);
        }
        #endregion

        #region Strong
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Strong.Kb);
        }
        #endregion

        #region Unpleasant
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unpleasant.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Unpleasant.Dmg);
        }
        #endregion

        #region Godly
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Godly.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Godly.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(5)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Godly.Crt);
        }
        #endregion

        #region Demonic
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Demonic.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(5)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Demonic.Crt);
        }
        #endregion

        #region Zealous
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(5)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Zealous.Crt);
        }
        #endregion

        #region Broken
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.7f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Broken.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.8f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Broken.Kb);
        }
        #endregion

        #region Damaged
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Damaged.Dmg);
        }
        #endregion

        #region Weak
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.8f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Weak.Kb);
        }
        #endregion

        #region Shoddy
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.85f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Shoddy.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Shoddy.Dmg);
        }
        #endregion

        #region Ruthless
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Ruthless.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.18f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Ruthless.Dmg);
        }
        #endregion

        #region Quick
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Quick.Spd);
        }
        #endregion

        #region Deadly2?????
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deadly2.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Deadly2.Spd);
        }
        #endregion

        #region Agile
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Agile.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(3)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Agile.Crt);
        }
        #endregion

        #region Nimble
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.95f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Nimble.Spd);
        }
        #endregion

        #region Murderous
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(3)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Murderous.Crt);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.94f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Murderous.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.07f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Murderous.Dmg);
        }
        #endregion

        #region Slow
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Slow.Spd);
        }
        #endregion

        #region Sluggish
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.2f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Sluggish.Spd);
        }
        #endregion

        #region Lazy
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.08f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Lazy.Spd);
        }
        #endregion

        #region Annoying
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.8f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Annoying.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.15f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Annoying.Spd);
        }
        #endregion

        #region Nasty
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //kb
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Nasty.Kb);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.9f)); //spd
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Nasty.Spd);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(1.05f)); //dmg
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Nasty.Dmg);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(2)); //crit
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Nasty.Crt);
        }
        #endregion
    }
}
