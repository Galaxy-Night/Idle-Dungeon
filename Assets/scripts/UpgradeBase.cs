using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.UI;

public class UpgradeBase : MonoBehaviour
{
	protected int cost;
	protected float multiplier;
	protected string description;

	[SerializeField]
	public GameObject purchaseButton;
	[SerializeField]
	public GameObject backButton;

	protected List<Tuple<float, int>> tiers;

	private void Start()
	{
		PopulateTiers();
		NextTier();
	}

	public void OnUpgradeClick() {
		GameObject.Find("upgrades").GetComponent<Upgrade>().OnUpgradeClick(description, cost);
		purchaseButton.SetActive(true);
		backButton.SetActive(true);
	}

	public void OnBackClick() {
		GameObject.Find("upgrades").GetComponent<Upgrade>().OnBackClick();
		purchaseButton.SetActive(false);
		backButton.SetActive(false);
	}
	protected bool checkCoins()
	{
		return GameObject.Find("game_handler").GetComponent<Game>().data.currentCoins >= cost;
	}

	protected virtual void PopulateTiers()
	{
		tiers = new List<Tuple<float, int>>();
	}

	public virtual void OnPurchaseClick() {
		OnBackClick();
	}
	protected void NextTier()
	{
		if (tiers.Count > 0)
		{
			System.Tuple<float, int> removed = tiers[0];
			multiplier = removed.Item1;
			cost = removed.Item2;
			tiers.RemoveAt(0);
			UpdateDescription();
		}
	}
	protected virtual void UpdateDescription() { }
}
