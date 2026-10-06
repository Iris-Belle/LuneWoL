
namespace LuneWoL.Common.Global_Npc;

public partial class LuneWoL_GNPC : GlobalNPC
{

    #region Hooks

    public override bool InstancePerEntity => true;

    public override void SetDefaults(NPC npc)
    {
        NpcValueMult(npc);
        NpcValueCap(npc);
        ApplyStatChanges(npc);
        ApplyBossStatChanges(npc);
    }

    public override void SetDefaultsFromNetId(NPC npc)
    {
        ApplyStatChanges(npc);
        ApplyBossStatChanges(npc);
    }

    public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
    {
        SpawnRateChanges(ref spawnRate, ref maxSpawns);
    }

    #endregion

    #region Methods

    private void ApplyBossStatChanges(NPC npc)
    {
        LuneWoL_AdvServerConfig.Adv_NpcPage.BossStatPage cfg = AdvServerConfig.Adv_Npc.BossConfig;

        if (cfg.DisableBossStatChanges || !npc.boss)
            return;

        npc.damage *= cfg.DamagePercent / 100;
        npc.defense *= cfg.DefensePercent / 100;
        npc.lifeMax *= cfg.LifePercent / 100;
        npc.life = npc.lifeMax;
    }

    internal void ApplyStatChanges(NPC npc)
    {
        LuneWoL_AdvServerConfig.Adv_NpcPage.NpcStatPage cfg = AdvServerConfig.Adv_Npc.NpcConfig;

        if (cfg.DisableNPCStatChanges || npc.CountsAsACritter || npc.friendly || npc.boss)
            return;

        npc.damage *= cfg.DamagePercent / 100;
        npc.defense *= cfg.DefensePercent / 100;
        npc.lifeMax *= cfg.LifePercent / 100;
        npc.life = npc.lifeMax;
    }

    internal void NpcValueCap(NPC npc)
    {
        LuneWoL_ServerConfig.NpcPage cfg = ServerConfig.Npc;

        if (cfg.MaxNpcValue == -1)
            return;

        npc.value = Math.Clamp(npc.value, 0, cfg.MaxNpcValue);
    }

    internal void NpcValueMult(NPC npc)
    {
        LuneWoL_ServerConfig.NpcPage cfg = ServerConfig.Npc;

        if (cfg.NpcValueMult == 1f)
            return;

        npc.value *= cfg.NpcValueMult;
    }

    internal void SpawnRateChanges(ref int spawnRate, ref int maxSpawns)
    {
        if (!ServerConfig.Npc.SpawnRateQuickToggle) 
            return;

        if (AdvServerConfig.Adv_Npc.SpawnRate.SpawnRate != 0)
        {
            spawnRate -= AdvServerConfig.Adv_Npc.SpawnRate.SpawnRate;
            spawnRate = Math.Max(0, spawnRate);
        }

        if (AdvServerConfig.Adv_Npc.SpawnRate.MaxSpawns != 0)
        {
            maxSpawns += AdvServerConfig.Adv_Npc.SpawnRate.MaxSpawns;
            maxSpawns = Math.Max(0, maxSpawns);
        }
    }

    #endregion

}