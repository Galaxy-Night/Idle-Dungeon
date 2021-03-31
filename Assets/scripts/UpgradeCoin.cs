using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCoin : Upgrade
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
            Game handler = GameObject.Find("game_handler").GetComponent<Game>();
            handler.data.UpgradeCoin(multiplier);
        }
    }
}
