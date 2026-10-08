namespace LuneWoL.IL;

internal class LuneWoL_ReforgeAccessoryIL : ILoadable
{
    public bool IsLoadingEnabled(Mod mod) => ServerConfig.Items.ReforgeNerf;
    public void Unload() { }

    public void Load(Mod mod)
    {
        var method0 = typeof(Player).GetMethod("GrantPrefixBenefits", BindingFlags.Public | BindingFlags.Instance, null, [typeof(Item)], null)
        ?? throw new Exception("smelly hooking on GrantPrefixBenefits. if you are not me, dm me on discord at ´iris_lune' or join my server and ping me. you could also leave a comment on the mod but that might take longer to see and patch");
        
        MonoModHooks.Modify(method0, ReforgeAccessoryIL);
    }

    private readonly static LuneWoL_AdvServerConfig.Adv_ItemsPage.ReforgeNerfPage.AccessoryReforgeNerfPage cfg = AdvServerConfig.Adv_Items.ReforgeNerf.AccReforgeNerfs;

    private void ReforgeAccessoryIL(ILContext il)
    {
        ILCursor c = new(il);

        #region defense
        c.GotoNext(MoveType.After, i => i.MatchLdfld<Player>("statDefense")); // Hard
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Hard.Defense);
            c.EmitCall(typeof(Player.DefenseStat).GetMethod("op_Addition", [typeof(Player.DefenseStat), typeof(int)]));
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(2)); // Guarding
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Guarding.Defense);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(3)); // Armored
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Armored.Defense);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(4)); // Warding
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Warding.Defense);
        }
        #endregion

        #region mana
        c.GotoNext(MoveType.Before, i => i.MatchLdcI4(20)); // Arcane
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_I4, cfg.Arcane.MaxMana);
        }
        #endregion

        #region crit
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(2)); // Precise
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Precise.Crit);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(4)); // Lucky
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Lucky.Crit);
        }
        #endregion
        
        #region damage
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.01f)); // Jagged
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Jagged.Damage);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.02f)); // Spiked
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Spiked.Damage);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.03f)); // Angry
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Angry.Damage);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.04f)); // Menacing
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Menacing.Damage);
        }
        #endregion

        #region move speed
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.01f)); // Brisk
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Brisk.MovementSpeed);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.02f)); // Fleeting
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Fleeting.MovementSpeed);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.03f)); // Hasty2
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Hasty2.MovementSpeed);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.04f)); // Quick2
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Quick2.MovementSpeed);
        }
        #endregion

        #region melee speed
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.01f)); // Wild
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Wild.MeleeSpeed);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.02f)); // Rash
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Rash.MeleeSpeed);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.03f)); // Intrepid
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Intrepid.MeleeSpeed);
        }
        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.04f)); // Violent
        {
            c.Remove();
            c.Emit(OpCodes.Ldc_R4, cfg.Violent.MeleeSpeed);
        }
        #endregion
    }
}