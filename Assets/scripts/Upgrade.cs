using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
	[SerializeField]
	protected int cost;
	[SerializeField]
	protected int multiplier;
	[SerializeField]
	protected string description;
	[SerializeField]
	private GameObject descriptionBox;
	[SerializeField]
	private Text descriptionBoxText;
	[SerializeField]
	private Text costText;

	private void Start()
	{
		descriptionBoxText.text = description;
		costText.text = cost.ToString() + " coins";
		transform.localScale = new Vector3(.01f, .01f, 1f);
	}

	public void OnUpgradeClick() {
		descriptionBox.transform.position = new Vector3(0f, 0f, 0f);
		descriptionBox.SetActive(true);
		GameObject.Find("upgrade_blackout").SetActive(true);
	}

	public virtual void OnPurchaseClick() { }

	public void OnBackClick() {
		descriptionBox.SetActive(false);
		GameObject.Find("upgrade_blackout").SetActive(false);
	}

	protected bool checkCoins() {
		return GameObject.Find("game_handler").GetComponent<Game>().data.currentCoins >= cost;
	}
}
