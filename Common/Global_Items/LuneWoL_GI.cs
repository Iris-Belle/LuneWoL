namespace LuneWoL.Common.Global_Items;

public partial class LuneWoL_GI : GlobalItem
{
    private enum ItemTimer
    {
        Despawn
    }

    #region Hooks

    public override bool InstancePerEntity => true;

    public override void PostUpdate(Item item)
    {
        DespawnItems(item);
    }

    public override void GetHealLife(Item item, Player player, bool quickHeal, ref int healValue)
    {
        WorseHealingPotions(item, player, quickHeal, ref healValue);
    }

    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        return DisableAccessories(item, player, slot, modded);
    }

    public override bool? CanAutoReuseItem(Item item, Player player)
    {
        return DisableAutoReuse(item, player);
    }

    public override bool? UseItem(Item item, Player player)
    {
        return DeathPenaltyItems(item, player);
    }

    #endregion

    #region Methods

    internal void DespawnItems(Item item)
    {
        LuneWoL_ServerConfig.ItemsPage config = ServerConfig.Items;

        if (config.DespawnItemsTimer <= -1)
            return;

        var timers = item.GetGlobalItem<ItemTimers>().Timers.Get<ItemTimer>();
        if (timers.TickOnce(ItemTimer.Despawn, (uint)(config.DespawnItemsTimer * 60)))
        {
            DustyDespawn(item);
            item.TurnToAir(true);
        }
    }

    internal void DustyDespawn(Item item)
    {
        for (int i = 0; i < 30; i++)
        {
            Dust.NewDust(item.position, 1, 1, DustID.Smoke, 0f, 0f, 100, default, 1f);
        }
    }

    internal bool DisableAccessories(Item item, Player player, int slot, bool unfiltered)
    {
        if (!ServerConfig.Items.DisableAccessories)
            return base.CanEquipAccessory(item, player, slot, unfiltered);
        
        return false;
    }

    internal bool? DisableAutoReuse(Item item, Player player)
    {
        if (!ServerConfig.Items.DisableAutoReuse)
            return base.CanAutoReuseItem(item, player);

        return false;
    }

    internal bool? DeathPenaltyItems(Item item, Player player)
    {
        if (item.type == ItemID.LifeCrystal && ServerConfig.Player.DeathPenaltyMode == 1)
        {
            if (player.ConsumedLifeCrystals >= Player.LifeCrystalMax)
            {
                player.WoLPlayer().DeathFlag0 = true;
            }
        }

        if (item.type == ItemID.LifeFruit && ServerConfig.Player.DeathPenaltyMode == 1)
        {
            if (player.ConsumedLifeFruit >= Player.LifeFruitMax)
            {
                player.WoLPlayer().DeathFlag0 = true;
            }
        }

        if (item.type == ItemID.ManaCrystal)
        {
            if (player.ConsumedManaCrystals >= Player.ManaCrystalMax && ServerConfig.Player.DeathPenaltyMode == 1)
            {
                player.WoLPlayer().DeathFlag1 = true;
            }
        }

        return base.UseItem(item, player);
    }

    internal void WorseHealingPotions(Item item, Player player, bool quickHeal, ref int healValue)
    {
        LuneWoL_ServerConfig.ItemsPage items = ServerConfig.Items;

        if (items.HealingPotionBadPercent > 1 && items.HealingPotionBadPercent < 100)
        {
            healValue = healValue * items.HealingPotionBadPercent / 100;

            base.GetHealLife(item, player, quickHeal, ref healValue);
        }
        else if (items.HealingPotionBadPercent <= 1)
        {
            healValue = 0;
        }
    }

    #endregion
}
