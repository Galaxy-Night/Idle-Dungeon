using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCoin : UpgradeBase
{
	protected override void PopulateTiers()
	{
		base.PopulateTiers();
		tiers.Add(new System.Tuple<float, int>(1.1f, 250));
		tiers.Add(new System.Tuple<float, int>(1.2f, 1000));
		tiers.Add(new System.Tuple<float, int>(1.3f, 2000));
		tiers.Add(new System.Tuple<float, int>(1.4f, 4000));
		tiers.Add(new System.Tuple<float, int>(1.5f, 8000));
	}

	public override void OnPurchaseClick()
	{
		if (checkCoins()) {
			Game handler = GameObject.Find("game_handler").GetComponent<Game>();
			handler.data.UpgradeCoin(multiplier, cost);
			handler.UpdateCurrentCoins();
			NextTier();
			OnBackClick();
		}
	}

	protected override void UpdateDescription()
	{
		description = $"Earn {multiplier} times more coins!";
	}
}
