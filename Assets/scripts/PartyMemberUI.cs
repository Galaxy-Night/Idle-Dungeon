using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberUI : MonoBehaviour
{
    [SerializeField]
    private GameObject unlockButton;
    [SerializeField]
    private Text unlockCost;
    [SerializeField]
    private GameObject levelButton;
    [SerializeField]
    private GameObject currentLevel;
    [SerializeField]
    private GameObject levelCost;
    [SerializeField]
    private GameObject healCost;
    [SerializeField]
    private GameObject healButton;
    [SerializeField]
    private GameObject heal;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite active;
    [SerializeField]
    private Sprite injured;

	private void Start()
	{
        unlockCost.text = GetComponent<PartyMemberData>().UnlockCost.ToString();
	}
	public void OnUnlockClick(Text currentCoins) {
        Debug.Log("Unlock Click");
        image.sprite = active;
	}

    public void OnLevelClick(Text currentCoins) {

	}

    public void OnHealClick(Text currentCoins) {

	}
}
