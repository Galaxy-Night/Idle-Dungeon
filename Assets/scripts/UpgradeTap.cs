using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTap : UpgradeBase
{
	protected override void PopulateTiers()
	{
        base.PopulateTiers();
        tiers.Add(new System.Tuple<float, int>(2f, 50));
        tiers.Add(new System.Tuple<float, int>(2.5f, 250));
        tiers.Add(new System.Tuple<float, int>(3f, 800));
        tiers.Add(new System.Tuple<float, int>(3.5f, 2000));
        tiers.Add(new System.Tuple<float, int>(4f, 4500));
	}
	public override void OnPurchaseClick()
    {
        if (checkCoins())
        {
            Game handler = GameObject.Find("game_handler").GetComponent<Game>();
            handler.data.UpgradeTap(multiplier);
            NextTier();
            OnBackClick();
        }
    }
    protected override void UpdateDescription() {
        description = $"Multiplies tap damage by {multiplier}";
	}
}
