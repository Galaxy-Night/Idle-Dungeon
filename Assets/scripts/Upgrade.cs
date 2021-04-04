using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
	[SerializeField]
	private GameObject descriptionBox;
	[SerializeField]
	private Text descriptionBoxText;
	[SerializeField]
	private Text descriptionBoxCost;

	public void OnUpgradeClick() {
		Debug.Log("yes");
		descriptionBox.SetActive(true);
	}

	public void OnUpgradeClick(string description, int cost) {
		descriptionBox.SetActive(true);
		descriptionBoxText.text = description;
		descriptionBoxCost.text = cost.ToString() + " coins";
	}

	public void OnBackClick() {
		descriptionBox.SetActive(false);
	}
}
