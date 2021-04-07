using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeXP : UpgradeBase
{
    protected override void PopulateTiers()
    {
        base.PopulateTiers();
        tiers.Add(new System.Tuple<float, int>(1.1f, 100));
        tiers.Add(new System.Tuple<float, int>(1.2f, 500));
        tiers.Add(new System.Tuple<float, int>(1.3f, 1000));
        tiers.Add(new System.Tuple<float, int>(1.4f, 1500));
        tiers.Add(new System.Tuple<float, int>(1.5f, 2000));
    }
    public override void OnPurchaseClick()
    {
        if (checkCoins())
        {
            Game handler = GameObject.Find("game_handler").GetComponent<Game>();
            handler.data.UpgradeXp(multiplier, cost);
            handler.UpdateCurrentCoins();
            NextTier();
            OnBackClick();
        }
    }
    protected override void UpdateDescription()
    {
        description = $"Earn {multiplier} times more xp!";
    }
}
