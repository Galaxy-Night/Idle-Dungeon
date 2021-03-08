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
	public void OnUnlockClick() {
        if (GameObject.Find("game_handler").GetComponent<Game>().data.currentCoins >= GetComponent<PartyMemberData>().UnlockCost) {
            GameObject.Find("game_handler").GetComponent<Game>().UnlockPartyMember(GetComponent<PartyMemberData>());
            GetComponent<PartyMemberData>().LevelUp();
            Destroy(unlockButton);
            levelButton.SetActive(true);
            levelCost.SetActive(true);
            currentLevel.SetActive(true);
            image.sprite = active;
            LevelUp();
        }
	}

    public void OnLevelClick() {

	}

    public void OnHealClick() {

	}

    public void LevelUp() {
        levelCost.GetComponent<Text>().text = GetComponent<PartyMemberData>().LevelCost.ToString();
        currentLevel.GetComponent<Text>().text = GetComponent<PartyMemberData>().CurrentLevel.ToString();
    }
}
