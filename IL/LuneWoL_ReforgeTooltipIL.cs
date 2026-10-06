namespace LuneWoL.IL;

internal class LuneWoL_ReforgeTooltipIL : ILoadable
{
    public bool IsLoadingEnabled(Mod mod) => ServerConfig.Items.ReforgeNerf;
    public void Unload() { }

    public void Load(Mod mod)
    {
        var method0 = typeof(Main).GetMethod("MouseText_DrawItemTooltip_GetLinesInfo", BindingFlags.Public | BindingFlags.Static, null, [typeof(Item), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(float), typeof(int).MakeByRefType(), typeof(string[]), typeof(bool[]), typeof(bool[]), typeof(string[]), typeof(int).MakeByRefType()], null)
        ?? throw new Exception("smelly hooking on MouseText_DrawItemTooltip_GetLinesInfo. if you are not me, dm me on discord at ´iris_lune' or join my server and ping me. you could also leave a comment on the mod but that might take longer to see and patch");
        
        MonoModHooks.Modify(method0, ReforgeTooltipIL);
    }

    private readonly static LuneWoL_AdvServerConfig.Adv_ItemsPage.ReforgeNerfPage.AccessoryReforgeNerfPage cfg = AdvServerConfig.Adv_Items.ReforgeNerf.AccReforgeNerfs;

    private void ReforgeTooltipIL(ILContext il)
    {
        ILCursor c = new(il);

        #region defense
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+1")))  // Hard
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Hard.Defense}");
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+2"))) // Guarding
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Guarding.Defense}");
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+3"))) // Armored
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Armored.Defense}");
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+4"))) // Warding
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Warding.Defense}");
        }
        #endregion

        #region mana
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+20 "))) // Arcane
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Arcane.MaxMana}");
        }
        #endregion

        #region crit
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+2"))) // Precise
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Precise.Crit}");
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+4"))) // Lucky
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Precise.Crit}");
        }
        #endregion

        #region damage
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+1"))) // Jagged
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Jagged.Damage * 100}");
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+2"))) // Spiked
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Spiked.Damage * 100}");
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+3"))) // Angry
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Angry.Damage * 100}");
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+4"))) // Menacing
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Menacing.Damage * 100}");
        }
        #endregion

        #region move speed
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+1"))) // Brisk
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Brisk.MovementSpeed * 100}");
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+2"))) // Fleeting
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Fleeting.MovementSpeed * 100}");
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+3"))) // Hasty2
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Hasty2.MovementSpeed * 100}");
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+4"))) // Quick2
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Quick2.MovementSpeed * 100}");
        }
        #endregion

        #region melee speed
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+1"))) // Wild
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Wild.MeleeSpeed * 100}");
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+2"))) // Rash
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Rash.MeleeSpeed * 100}");
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+3"))) // Intrepid
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Intrepid.MeleeSpeed * 100}");
        }
        if (c.TryGotoNext(MoveType.Before, i => i.MatchLdstr("+4"))) // Violent
        {
            c.Remove();
            c.Emit(OpCodes.Ldstr, $"+{cfg.Violent.MeleeSpeed * 100}");
        }
        #endregion
    }
}