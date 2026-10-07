namespace LuneWoL.IL
{
    internal class LuneWoL_ShopIL : ILoadable
    {
        public bool IsLoadingEnabled(Mod mod) => ServerConfig.Npc.PricePatchToggle;
        public void Unload() { }

        public void Load(Mod mod)
        {
            var method0 = typeof(Player).GetMethod("GetItemExpectedPrice", BindingFlags.Public | BindingFlags.Instance, null, [typeof(Item), typeof(long).MakeByRefType(), typeof(long).MakeByRefType()], null)
            ?? throw new Exception("smelly hooking on GetItemExpectedPrice. if you are not me, dm me on discord at ´iris_lune' or join my server and ping me. you could also leave a comment on the mod but that might take longer to see and patch");
            
            MonoModHooks.Modify(method0, ShopIL);
        }

        private void ShopIL(ILContext il)
        {
            LuneWoL_AdvServerConfig.Adv_NpcPage.PriceMultiplierPage cfg = AdvServerConfig.Adv_Npc.PriceConfig;
            ILCursor c = new(il);

            c.GotoNext(MoveType.Before, i => i.MatchLdarg(0), i => i.MatchLdflda<Player>("currentShoppingSettings"));
            c.RemoveRange(4);
            c.EmitDelegate<Func<float>>(() => cfg.BuyMult);
            c.Emit(OpCodes.Conv_R8);
            c.Emit(OpCodes.Mul);
            
            c.GotoNext(MoveType.Before, i => i.MatchLdarg(0), i => i.MatchLdflda<Player>("currentShoppingSettings"));
            c.RemoveRange(4);
            c.EmitDelegate<Func<float>>(() => cfg.SellMult);
            c.Emit(OpCodes.Conv_R8);
            c.Emit(OpCodes.Mul);
            
            c.GotoNext(MoveType.Before, i => i.MatchLdarg(0), i => i.MatchLdflda<Player>("currentShoppingSettings"));
            c.RemoveRange(4);
            c.EmitDelegate<Func<float>>(() => cfg.BuyMult);
            c.Emit(OpCodes.Conv_R8);
            c.Emit(OpCodes.Mul);

            c.GotoNext(MoveType.Before, i => i.MatchLdarg(0), i => i.MatchLdflda<Player>("currentShoppingSettings"));
            c.RemoveRange(4);
            c.EmitDelegate<Func<float>>(() => cfg.SellMult);
            c.Emit(OpCodes.Conv_R8);
            c.Emit(OpCodes.Mul);
            
        }
    }
}
