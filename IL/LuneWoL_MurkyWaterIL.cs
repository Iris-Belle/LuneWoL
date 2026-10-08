namespace LuneWoL.IL;

internal class LuneWoL_MurkyWaterIL : ILoadable
{
	//private static Mod _mod;
    public bool IsLoadingEnabled(Mod mod) => ServerConfig.Environment.MurkyWater;
    public void Unload() { }

    public void Load(Mod mod)
    {
        var method0 = typeof(LightingEngine).GetMethod("UpdateLightDecay", BindingFlags.NonPublic | BindingFlags.Instance, null, [], null)
        ?? throw new Exception("smelly hooking on UpdateLightDecay (Murky Water). if you are not me, dm me on discord at ´iris_lune' or join my server and ping me. you could also leave a comment on the mod but that might take longer to see and patch");
		//_mod = mod;
        MonoModHooks.Modify(method0, MurkyWaterIL);
    }

    private static void MurkyWaterIL(ILContext il)
    {
        ILCursor c = new(il);

        c.GotoNext(i => i.MatchSwitch(out _));

        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.91f));
        c.Remove();
        c.EmitDelegate<Func<float>>(() => Main.LocalPlayer.WoLPlayer()._SurfaceDecay); //         workingLightMap.LightDecayThroughWater = new Vector3(0.88f, 0.96f, 1.015f) * 0.91f;

        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.91f));
        c.Remove();
        c.EmitDelegate<Func<float>>(() => Main.LocalPlayer.WoLPlayer()._CorruptionDecay); //         workingLightMap.LightDecayThroughWater = new Vector3(0.94f, 0.85f, 1.01f) * 0.91f;

        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.91f));
        c.Remove();
        c.EmitDelegate<Func<float>>(() => Main.LocalPlayer.WoLPlayer()._JungleDecay); //         workingLightMap.LightDecayThroughWater = new Vector3(0.84f, 0.95f, 1.015f) * 0.91f;

        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.91f));
        c.Remove();
        c.EmitDelegate<Func<float>>(() => Main.LocalPlayer.WoLPlayer()._HallowDecay); //         workingLightMap.LightDecayThroughWater = new Vector3(0.9f, 0.86f, 1.01f) * 0.91f;

        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.91f));
        c.Remove();
        c.EmitDelegate<Func<float>>(() => Main.LocalPlayer.WoLPlayer()._SnowDecay); //         workingLightMap.LightDecayThroughWater = new Vector3(0.84f, 0.99f, 1.01f) * 0.91f;

        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.91f));
        c.Remove();
        c.EmitDelegate<Func<float>>(() => Main.LocalPlayer.WoLPlayer()._DesertDecay); //         workingLightMap.LightDecayThroughWater = new Vector3(0.83f, 0.93f, 0.98f) * 0.91f;

        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.91f));
        c.Remove();
        c.EmitDelegate<Func<float>>(() => Main.LocalPlayer.WoLPlayer()._BloodMoonDecay); //         workingLightMap.LightDecayThroughWater = new Vector3(1f, 0.88f, 0.84f) * 0.91f;

        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.91f));
        c.Remove();
        c.EmitDelegate<Func<float>>(() => Main.LocalPlayer.WoLPlayer()._CrimsonDecay); //         workingLightMap.LightDecayThroughWater = new Vector3(0.83f, 1f, 1f) * 0.91f;

        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.91f));
        c.Remove();
        c.EmitDelegate<Func<float>>(() => Main.LocalPlayer.WoLPlayer()._UndergroundDesertDecay); //         workingLightMap.LightDecayThroughWater = new Vector3(0.95f, 0.98f, 0.85f) * 0.91f;

        c.GotoNext(MoveType.Before, i => i.MatchLdcR4(0.91f));
        c.Remove();
        c.EmitDelegate<Func<float>>(() => Main.LocalPlayer.WoLPlayer()._OceanDecay); //         workingLightMap.LightDecayThroughWater = new Vector3(0.9f, 1f, 1.02f) * 0.91f;

        c.GotoNext(MoveType.After, i => i.MatchLdcR4(0.91f));
        c.EmitPop();
        c.EmitDelegate<Func<float>>(() => Main.LocalPlayer.WoLPlayer()._ModdedDecay); //     float factor = 0.91f; for modded nbiomes yk
                                                                //     LoaderManager.Get<WaterStylesLoader>().LightColorMultiplier(Main.waterStyle, factor, ref throughWaterR, ref throughWaterG, ref throughWaterB);
        //MonoModHooks.DumpIL(_mod, il);
    }
}

/*
IL_0087: br        IL_0218


	//         workingLightMap.LightDecayThroughWater = new Vector3(0.88f, 0.96f, 1.015f) * 0.91f;

	IL_008C: ldloc.0
	IL_008D: ldc.r4    0.88
	IL_0092: ldc.r4    0.96
	IL_0097: ldc.r4    1.015
	IL_009C: newobj    instance void [FNA]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
	IL_00A1: ldc.r4    0.91
	IL_00A6: call      valuetype [FNA]Microsoft.Xna.Framework.Vector3 [FNA]Microsoft.Xna.Framework.Vector3::op_Multiply(valuetype [FNA]Microsoft.Xna.Framework.Vector3, float32)
	IL_00AB: callvirt  instance void Terraria.Graphics.Light.LightMap::set_LightDecayThroughWater(valuetype [FNA]Microsoft.Xna.Framework.Vector3)
	IL_00B0: br        IL_0218


	//         workingLightMap.LightDecayThroughWater = new Vector3(0.94f, 0.85f, 1.01f) * 0.91f;

	IL_00B5: ldloc.0
	IL_00B6: ldc.r4    0.94
	IL_00BB: ldc.r4    0.85
	IL_00C0: ldc.r4    1.01
	IL_00C5: newobj    instance void [FNA]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
	IL_00CA: ldc.r4    0.91
	IL_00CF: call      valuetype [FNA]Microsoft.Xna.Framework.Vector3 [FNA]Microsoft.Xna.Framework.Vector3::op_Multiply(valuetype [FNA]Microsoft.Xna.Framework.Vector3, float32)
	IL_00D4: callvirt  instance void Terraria.Graphics.Light.LightMap::set_LightDecayThroughWater(valuetype [FNA]Microsoft.Xna.Framework.Vector3)
	IL_00D9: br        IL_0218


	//         workingLightMap.LightDecayThroughWater = new Vector3(0.84f, 0.95f, 1.015f) * 0.91f;

	IL_00DE: ldloc.0
	IL_00DF: ldc.r4    0.84
	IL_00E4: ldc.r4    0.95
	IL_00E9: ldc.r4    1.015
	IL_00EE: newobj    instance void [FNA]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
	IL_00F3: ldc.r4    0.91
	IL_00F8: call      valuetype [FNA]Microsoft.Xna.Framework.Vector3 [FNA]Microsoft.Xna.Framework.Vector3::op_Multiply(valuetype [FNA]Microsoft.Xna.Framework.Vector3, float32)
	IL_00FD: callvirt  instance void Terraria.Graphics.Light.LightMap::set_LightDecayThroughWater(valuetype [FNA]Microsoft.Xna.Framework.Vector3)
	IL_0102: br        IL_0218


	//         workingLightMap.LightDecayThroughWater = new Vector3(0.9f, 0.86f, 1.01f) * 0.91f;

	IL_0107: ldloc.0
	IL_0108: ldc.r4    0.9
	IL_010D: ldc.r4    0.86
	IL_0112: ldc.r4    1.01
	IL_0117: newobj    instance void [FNA]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
	IL_011C: ldc.r4    0.91
	IL_0121: call      valuetype [FNA]Microsoft.Xna.Framework.Vector3 [FNA]Microsoft.Xna.Framework.Vector3::op_Multiply(valuetype [FNA]Microsoft.Xna.Framework.Vector3, float32)
	IL_0126: callvirt  instance void Terraria.Graphics.Light.LightMap::set_LightDecayThroughWater(valuetype [FNA]Microsoft.Xna.Framework.Vector3)
	IL_012B: br        IL_0218


	//         workingLightMap.LightDecayThroughWater = new Vector3(0.84f, 0.99f, 1.01f) * 0.91f;

	IL_0130: ldloc.0
	IL_0131: ldc.r4    0.84
	IL_0136: ldc.r4    0.99
	IL_013B: ldc.r4    1.01
	IL_0140: newobj    instance void [FNA]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
	IL_0145: ldc.r4    0.91
	IL_014A: call      valuetype [FNA]Microsoft.Xna.Framework.Vector3 [FNA]Microsoft.Xna.Framework.Vector3::op_Multiply(valuetype [FNA]Microsoft.Xna.Framework.Vector3, float32)
	IL_014F: callvirt  instance void Terraria.Graphics.Light.LightMap::set_LightDecayThroughWater(valuetype [FNA]Microsoft.Xna.Framework.Vector3)
	IL_0154: br        IL_0218


	//         workingLightMap.LightDecayThroughWater = new Vector3(0.83f, 0.93f, 0.98f) * 0.91f;

	IL_0159: ldloc.0
	IL_015A: ldc.r4    0.83
	IL_015F: ldc.r4    0.93
	IL_0164: ldc.r4    0.98
	IL_0169: newobj    instance void [FNA]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
	IL_016E: ldc.r4    0.91
	IL_0173: call      valuetype [FNA]Microsoft.Xna.Framework.Vector3 [FNA]Microsoft.Xna.Framework.Vector3::op_Multiply(valuetype [FNA]Microsoft.Xna.Framework.Vector3, float32)
	IL_0178: callvirt  instance void Terraria.Graphics.Light.LightMap::set_LightDecayThroughWater(valuetype [FNA]Microsoft.Xna.Framework.Vector3)
	IL_017D: br        IL_0218


	//         workingLightMap.LightDecayThroughWater = new Vector3(1f, 0.88f, 0.84f) * 0.91f;

	IL_0182: ldloc.0
	IL_0183: ldc.r4    1
	IL_0188: ldc.r4    0.88
	IL_018D: ldc.r4    0.84
	IL_0192: newobj    instance void [FNA]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
	IL_0197: ldc.r4    0.91
	IL_019C: call      valuetype [FNA]Microsoft.Xna.Framework.Vector3 [FNA]Microsoft.Xna.Framework.Vector3::op_Multiply(valuetype [FNA]Microsoft.Xna.Framework.Vector3, float32)
	IL_01A1: callvirt  instance void Terraria.Graphics.Light.LightMap::set_LightDecayThroughWater(valuetype [FNA]Microsoft.Xna.Framework.Vector3)
	IL_01A6: br.s      IL_0218


	//         workingLightMap.LightDecayThroughWater = new Vector3(0.83f, 1f, 1f) * 0.91f;

	IL_01A8: ldloc.0
	IL_01A9: ldc.r4    0.83
	IL_01AE: ldc.r4    1
	IL_01B3: ldc.r4    1
	IL_01B8: newobj    instance void [FNA]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
	IL_01BD: ldc.r4    0.91
	IL_01C2: call      valuetype [FNA]Microsoft.Xna.Framework.Vector3 [FNA]Microsoft.Xna.Framework.Vector3::op_Multiply(valuetype [FNA]Microsoft.Xna.Framework.Vector3, float32)
	IL_01C7: callvirt  instance void Terraria.Graphics.Light.LightMap::set_LightDecayThroughWater(valuetype [FNA]Microsoft.Xna.Framework.Vector3)
	IL_01CC: br.s      IL_0218


	//         workingLightMap.LightDecayThroughWater = new Vector3(0.95f, 0.98f, 0.85f) * 0.91f;

	IL_01CE: ldloc.0
	IL_01CF: ldc.r4    0.95
	IL_01D4: ldc.r4    0.98
	IL_01D9: ldc.r4    0.85
	IL_01DE: newobj    instance void [FNA]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
	IL_01E3: ldc.r4    0.91
	IL_01E8: call      valuetype [FNA]Microsoft.Xna.Framework.Vector3 [FNA]Microsoft.Xna.Framework.Vector3::op_Multiply(valuetype [FNA]Microsoft.Xna.Framework.Vector3, float32)
	IL_01ED: callvirt  instance void Terraria.Graphics.Light.LightMap::set_LightDecayThroughWater(valuetype [FNA]Microsoft.Xna.Framework.Vector3)
	IL_01F2: br.s      IL_0218


	//         workingLightMap.LightDecayThroughWater = new Vector3(0.9f, 1f, 1.02f) * 0.91f;

	IL_01F4: ldloc.0
	IL_01F5: ldc.r4    0.9
	IL_01FA: ldc.r4    1
	IL_01FF: ldc.r4    1.02
	IL_0204: newobj    instance void [FNA]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
	IL_0209: ldc.r4    0.91
	IL_020E: call      valuetype [FNA]Microsoft.Xna.Framework.Vector3 [FNA]Microsoft.Xna.Framework.Vector3::op_Multiply(valuetype [FNA]Microsoft.Xna.Framework.Vector3, float32)
	IL_0213: callvirt  instance void Terraria.Graphics.Light.LightMap::set_LightDecayThroughWater(valuetype [FNA]Microsoft.Xna.Framework.Vector3)


	//     float factor = 0.91f;

	IL_0218: ldc.r4    0.91
	IL_021D: stloc.1
*/