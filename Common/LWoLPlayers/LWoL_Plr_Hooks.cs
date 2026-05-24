
namespace LuneWoL.Common.WoL_Plrs;

public partial class LWoL_Plr : ModPlayer
{
    public override void OnEnterWorld() => EnterWorldMessage();

    public override void OnRespawn()
    {
        DeathPenaltyAppliedOnRespawn();

        DeathPenaltyConsumedCrystals();

        DeathPenaltyConsumedFloor();
    }

    public override void PostUpdateEquips() => HellIsHot();

    public override void PostUpdateRunSpeeds() => ViscousWater();

    public override void PreUpdateBuffs()
    {
        ApplySpaceVacuum();

        PoisonedWater();

        WeatherChanges();

        FreezingTundra();

        OnlyEnterEvilAtDay();

        MurkyWater();
    }

    public override void PostUpdate()
    {
        ResetDeathPenalty();

        // https://steamcommunity.com/sharedfiles/filedetails/?id=2395507804
    }

    public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
    {
        if (Player.LibPlayer().CrimtuptionzoneNight && Player.whoAmI == Main.myPlayer)
        {
            damageSource = PlayerDeathReason.ByCustomReason(GetText("Status.Death.CrimtuptionzoneDeath").ToNetworkText(Player.name));
        }

        return true;
    }
    public override void ModifyMaxStats(out StatModifier health, out StatModifier mana) => DeathPenaltyStatmod(out health, out mana);

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) => SyncDeathPenalty(toWho, fromWho, newPlayer);

    public override void CopyClientState(ModPlayer targetCopy) => CloneClientsDeathPenalty(targetCopy);

    public override void SendClientChanges(ModPlayer clientPlayer) => SendDeathPenalty(clientPlayer);

    public override void SaveData(TagCompound tag) => SaveDeathPenaltyTag(tag);

    public override void LoadData(TagCompound tag) => LoadDeathPenaltyTag(tag);
}