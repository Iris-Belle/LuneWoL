
namespace LuneWoL.Common.LWoLGlobalProjectiles;

public partial class LWoL_GP : GlobalProjectile
{
    public override void OnSpawn(Projectile Projectile, IEntitySource source)
    {
        LWoL_Plr p = Main.CurrentPlayer.GetModPlayer<LWoL_Plr>();
        LWoLServerConfig.PlayerDented Config = LuneWoL.LWoLServerConfig.LPlayer;

        if (p.DmgPlrBcCrit && Config.CritFailMode != 0 && Projectile.owner == Main.myPlayer)
        {
            Projectile.damage = 0;
            Projectile.penetrate = -1;
        }
        base.OnSpawn(Projectile, source);
    }
}
