using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public EnemyData Data;
    public GameObject ui;

    public void EnemyHandlerInitialize(EnemyData data) {
        Data = data;
        ui.GetComponent<EnemyUI>().EnemyUIInitialize(Data.EnemyName, Data.XpValue, Data.CoinValue, Data.Level, (float)Data.CurrentHealth / Data.MaxHealth);
	}

    public int TakeDamage(int amount, bool fromTap) {
        int returned = Data.TakeDamage(amount);
        float barFill = (float)Data.CurrentHealth / Data.MaxHealth;
        ui.GetComponent<EnemyUI>().ChangeEnemyHPBar(barFill);
        if (fromTap)
            ui.GetComponent<EnemyUI>().ShowDamage(amount);
        return returned;
    }

    public void DamageMessage() {
        SendMessageUpwards("ClickEnemyDamage");
	}
}
