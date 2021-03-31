using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeArcher : Upgrade
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
            PartyMemberData archer = GameObject.Find("archer").GetComponent<PartyMemberData>();
            archer.MaxHealth *= multiplier;
            archer.Damage *= multiplier;
            archer.startingHealth *= multiplier;
            archer.StartingDamage *= multiplier;
        }
    }
}
