using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCleric : Upgrade
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnPurchaseClick()
    {
        if (checkCoins())
        {
            PartyMemberData cleric = GameObject.Find("cleric").GetComponent<PartyMemberData>();
            cleric.MaxHealth *= multiplier;
            cleric.Damage *= multiplier;
            cleric.startingHealth *= multiplier;
            cleric.StartingDamage *= multiplier;
        }
    }
}
