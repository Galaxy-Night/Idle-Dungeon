using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterUpgrade : Upgrade
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
        if (checkCoins()) {
            PartyMemberData fighter = GameObject.Find("fighter").GetComponent<PartyMemberData>();
            fighter.MaxHealth *= multiplier;
            fighter.Damage *= multiplier;
            fighter.startingHealth *= multiplier;
            fighter.StartingDamage *= multiplier;
		}
    }
}
