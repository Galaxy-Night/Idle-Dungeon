using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWizard : Upgrade
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
            PartyMemberData wizard = GameObject.Find("wizard").GetComponent<PartyMemberData>();
            wizard.MaxHealth *= multiplier;
            wizard.Damage *= multiplier;
            wizard.startingHealth *= multiplier;
            wizard.StartingDamage *= multiplier;
        }
    }
}
