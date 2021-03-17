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
    [SerializeField]
    private Image hpBar;

    PartyMemberData data;

    private bool isInitialized = false;
	private void Start()
	{
        if (!isInitialized) {
            data = GetComponent<PartyMemberData>();
        }
        unlockCost.text = GetComponent<PartyMemberData>().UnlockCost.ToString();
    }

    public void Initialize(PartyMemberSave save) {
        data = GetComponent<PartyMemberData>();
        data.Initialize(save);
        UIUnlock();
    }
	public void OnUnlockClick() {
        if (GameObject.Find("game_handler").GetComponent<Game>().data.currentCoins >= GetComponent<PartyMemberData>().UnlockCost) {
            GameObject.Find("game_handler").GetComponent<Game>().UnlockPartyMember(GetComponent<PartyMemberData>());
            GetComponent<PartyMemberData>().Unlock();
            UIUnlock();
        }
	}

    public void OnLevelClick() {
        Debug.Log("Click");
        if (GameObject.Find("game_handler").GetComponent<Game>().data.currentCoins >= GetComponent<PartyMemberData>().LevelCost)
        {
            GameObject.Find("game_handler").GetComponent<Game>().LevelUpPartyMember(GetComponent<PartyMemberData>());
            data.LevelUp();
            LevelUp();
        }
    }

    public void OnHealClick()
    {
        if (GameObject.Find("game_handler").GetComponent<Game>().data.currentCoins >= data.HealCost)
        {
            GameObject.Find("game_handler").GetComponent<Game>().HealRevivePartyMember(GetComponent<PartyMemberData>());
            if (data.IsDead) {
                image.sprite = active;
            }
            levelButton.SetActive(true);
            currentLevel.SetActive(true);
            heal.SetActive(false);
            healButton.SetActive(false);
            data.HealRevive();
        }
    }
    public void LevelUp() {
        levelCost.GetComponent<Text>().text = GetComponent<PartyMemberData>().LevelCost.ToString();
        currentLevel.GetComponent<Text>().text = GetComponent<PartyMemberData>().CurrentLevel.ToString();
    }

    public void UpdateHealthBar () {
        hpBar.fillAmount = (float)data.CurrentHealth / data.MaxHealth;
	}

    public void Injure() {
        levelButton.SetActive(false);
        currentLevel.SetActive(false);
        heal.SetActive(true);
        healButton.SetActive(true);
	}

    public void UpdateHealCost(int cost) {
        healCost.GetComponent<Text>().text = cost.ToString();
	}

    public void Kill() {
        image.sprite = injured;
        Injure();
	}

    public void UIUnlock() {
        Destroy(unlockButton);
        levelButton.SetActive(true);
        levelCost.SetActive(true);
        currentLevel.SetActive(true);
        image.sprite = active;
        LevelUp();
    }
}
