using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <c>Render</c> contains the render 
/// </summary>
public class Render : MonoBehaviour
{
    public Image enemyHealthBar;
    public Image xpBar;
    public Image totalHealth;
    public Text enemyCoin;
    public Text enemyName;
    public Text currentCoins;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealthBar = GameObject.Find("enemy_health").GetComponent<Image>();
        totalHealth = GameObject.Find("health_bar").GetComponent<Image>();
        xpBar = GameObject.Find("xp_bar").GetComponent<Image>();
        enemyCoin = GameObject.Find("coin_value").GetComponent<Text>();
        enemyName = GameObject.Find("enemy_name").GetComponent<Text>();
        currentCoins = GameObject.Find("current_coins").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// <c>NewEnemyDisplay</c> updates the ui elements stored in the 
    /// <c>Render</c> object to reflect a new enemy that has been 
    /// generated
    /// </summary>
    /// <param name="_enemy">The enemy for which the information is to be
    /// displayed</param>
    public void NewEnemyDisplay(Enemy _enemy) {
        enemyName.text = _enemy.enemyName;
        enemyCoin.text = _enemy.coinValue.ToString();
        enemyHealthBar.fillAmount = 1;
    }

    /// <summary>
    /// <c>UpdateCoinDisplay</c> updates the number of coins that are displayed
    /// as the player's current total
    /// </summary>
    /// <param name="coinTotal">The number of coins to be displayed</param>
    public void UpdateCoinDisplay(int coinTotal) {
        currentCoins.text = coinTotal.ToString();
    }

    /// <summary>
    /// <c>UpdateTotalHealth</c> updates the total health that is displayed to
    /// the player.
    /// </summary>
    /// <param name="max">The maximum possible health points the player can
    /// have</param>
    /// <param name="current">The current number of health points the player
    /// has</param>
    public void UpdateTotalHealth(int max, int current) {
        totalHealth.fillAmount = current / (float)max;
        Debug.Log(current/ (float)max);
        //Debug.Log("auto");
    }
}
