using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberUI : MonoBehaviour
{
    [SerializeField]
    private GameObject unlockButton;
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
    
    public void OnUnlockClick(Text currentCoins) {
        Debug.Log("Unlock Click");
	}

    public void OnLevelClick(Text currentCoins) {

	}

    public void OnHealClick(Text currentCoins) {

	}
}
