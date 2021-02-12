using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void initialize() {
        ui.GetComponent<PartyMemberUI>().initialize(data.UnlockCost, data.Locked, data.MemberName);
	}

    public void onUnlockClick()
    {
        GameObject temp = GameObject.Find("game_handler");
        GameHandler handler = temp.GetComponent<GameHandler>();

        if (handler.currentCoins >= data.UnlockCost) {
            data.Unlock();
            ui.GetComponent<PartyMemberUI>().Unlock(data.Active, data.UnlockCost);
            //handler.Unlock(data);
        }

        Debug.Log("Clicked");
    }

    public void TakeDamage(int _amount) {
        int returned = data.TakeDamage(_amount);
        float barFill = (float)data.CurrentHealth / data.MaxHealth;
        ui.GetComponent<PartyMemberUI>().ChangeHPBar(barFill);
	}
}
