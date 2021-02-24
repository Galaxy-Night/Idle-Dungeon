using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Move sprite information here or to the UI class
public class PartyMemberHandler : MonoBehaviour
{
    public PartyMemberData data;
    public GameObject ui;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialize(PartyMemberData _data) {
        data = _data;
        ui.GetComponent<PartyMemberUI>().initialize(data.UnlockCost, data.MemberName);
	}

    public void InitializeUnlocked(PartyMemberData data) {
        this.data = data;
        ui.GetComponent<PartyMemberUI>().InitializeUnlocked(data);
    }

    public void onUnlockClick()
    {
        Game game = FindGame();

        if (game.GetCurrentCoins() >= data.UnlockCost) {
            data.Unlock();
            ui.GetComponent<PartyMemberUI>().Unlock(data.LevelCost);
            game.UnlockPartyMember(this);
        }
    }

    public void TakeDamage(int _amount) {
        int returned = data.TakeDamage(_amount);
        float barFill = (float)data.CurrentHealth / data.MaxHealth;
        ui.GetComponent<PartyMemberUI>().ChangeHPBar(barFill);
        if (returned == GameData.INJURED_INDICATOR) {
            if (!data.IsInjured) {
                ui.GetComponent<PartyMemberUI>().Injure();
			}
            data.UpdateHealCost();
            ui.GetComponent<PartyMemberUI>().UpdateHealCost(data.HealCost);
        }

        else if (returned == GameData.DEATH_INDICATOR) {
            if (!data.IsDead) {
                data.Death();
                ui.GetComponent<PartyMemberUI>().Kill();
                ui.GetComponent<PartyMemberUI>().UpdateHealCost(data.HealCost);
            }
		}

	}

    public void OnHealClick() {
        Game game = FindGame();

        if (game.GetCurrentCoins() >= data.HealCost) {
            data.Heal();
            game.HealLevel(data.HealCost);
            ui.GetComponent<PartyMemberUI>().Heal();
            ui.GetComponent<PartyMemberUI>().ChangeHPBar(1);
		}
    }

    public void OnLevelClick() {
        Game game = FindGame();
        if (game.GetCurrentCoins() >= data.HealCost)
        {
            Debug.Log("Level Up!");
            int oldCost = data.LevelUp();
            game.HealLevel(oldCost);
            ui.GetComponent<PartyMemberUI>().LevelUp(data.CurrentLevel, data.LevelCost);
            ui.GetComponent<PartyMemberUI>().ChangeHPBar((float)data.CurrentHealth / data.MaxHealth);
        }
    }

    private static Game FindGame() {
        GameObject temp = GameObject.Find("game_handler");
        return temp.GetComponent<Game>();
    }
}
