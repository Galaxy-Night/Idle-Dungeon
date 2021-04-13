using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFighter : UpgradeBase
{
	protected override void PopulateTiers()
	{
		base.PopulateTiers();
		tiers.Add(new System.Tuple<float, int>(2f, 250));
		tiers.Add(new System.Tuple<float, int>(2f, 500));
		tiers.Add(new System.Tuple<float, int>(2f, 1000));
		tiers.Add(new System.Tuple<float, int>(2f, 2000));
		tiers.Add(new System.Tuple<float, int>(2f, 4000));
	}

	public override void OnPurchaseClick()
	{
		if (checkCoins() && GameObject.Find("fighter") != null)
		{
			PartyMemberData fighter = GameObject.Find("fighter").GetComponent<PartyMemberData>();
			fighter.Upgrade(multiplier);
			NextTier();
			OnBackClick();
		}
	}

	protected override void UpdateDescription()
	{
		description = $"Doubles the fighter's damage and maximum health";
	}
}
