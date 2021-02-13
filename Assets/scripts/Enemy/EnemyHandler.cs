using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public EnemyData Data;
    public GameObject ui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyHandlerInitialize(EnemyData data) {
        Data = data;
        ui.GetComponent<EnemyUI>().EnemyUIInitialize(Data.EnemyName, Data.XpValue, Data.CoinValue, Data.Level);
	}

    public int TakeDamage(int amount) {
        int returned = Data.TakeDamage(amount);
        float barFill = (float)Data.CurrentHealth / Data.MaxHealth;
        ui.GetComponent<EnemyUI>().ChangeEnemyHPBar(barFill);
        ui.GetComponent<EnemyUI>().ShowDamage(amount);
        return returned;
    }

    public void DamageMessage() {
        SendMessageUpwards("ClickEnemyDamage");
	}
}
