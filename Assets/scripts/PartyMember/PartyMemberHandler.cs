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
        ui.GetComponent<PartyMemberUI>().initialize(data.UnlockCost, data.Locked, data.MemberName);
	}

    public void onUnlockClick()
    {
        GameObject temp = GameObject.Find("game_handler");
        Game handler = temp.GetComponent<Game>();

        if (handler.GetCurrentCoins() >= data.UnlockCost) {
            data.LevelUp();
            ui.GetComponent<PartyMemberUI>().Unlock(data.Active, data.LevelCost);
            handler.UnlockPartyMember(this);
        }
    }

    public void TakeDamage(int _amount) {
        int returned = data.TakeDamage(_amount);
        float barFill = (float)data.CurrentHealth / data.MaxHealth;
        ui.GetComponent<PartyMemberUI>().ChangeHPBar(barFill);
        if (returned == GameData.INJURED_INDICATOR) {
            if (!data.IsInjured) {
                ui.GetComponent<PartyMemberUI>().NeedsHealInit();
                data.IsInjured = true;
			}
            data.NeedsHealData();
            ui.GetComponent<PartyMemberUI>().NeedsHealUI(data.HealCost);
        }

	}

    public void OnHealClick() {
        GameObject temp = GameObject.Find("game_handler");
        Game handler = temp.GetComponent<Game>();

        if (handler.GetCurrentCoins() >= data.HealCost) {
            handler.Heal(data.HealCost);
            data.Heal();
            ui.GetComponent<PartyMemberUI>().Heal();
            ui.GetComponent<PartyMemberUI>().ChangeHPBar(1);
		}
    }
}
