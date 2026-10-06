namespace LuneWoL.Common.Global_Projectile;

public class ArrowEffects : GlobalProjectile
{
    public override void PostAI(Projectile Projectile)
    {
        if (ServerConfig.Environment.WindAffectsArrows &&
        Projectile.arrow &&
        Projectile.Center.Y < Main.worldSurface * 16.0 &&
        Main.tile[(int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16] != null &&
        Main.tile[(int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16].WallType == WallID.None &&
        ((Projectile.velocity.X > 0f &&
        Main.windSpeedCurrent < 0f) || (Projectile.velocity.X < 0f &&
        Main.windSpeedCurrent > 0f) || Math.Abs(Projectile.velocity.X) < Math.Abs(Main.windSpeedCurrent * Main.windPhysicsStrength) * 180f) &&
        Math.Abs(Projectile.velocity.X) < 16f)
        {
            Projectile.velocity.X += Main.windSpeedCurrent * Main.windPhysicsStrength;
            Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -16f, 16f);
        };
        base.PostAI(Projectile);
    }
}