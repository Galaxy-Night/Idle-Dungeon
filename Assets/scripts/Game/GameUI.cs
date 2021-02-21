using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Image xpBar;
    [SerializeField]
    private Text currentCoins;

	public void ChangePlayerHpBar(float fill) {
        healthBar.fillAmount = fill;
	}

    public void ChangeXPBar(float fill) {
        xpBar.fillAmount = fill;
	}

    public void ChangeCurrentCoins(int amount) {
        currentCoins.text = amount.ToString();
	}
}
