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
    public Text enemyCoin;
    public Text enemyName;
    public Text currentCoins;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealthBar = GameObject.Find("enemy_health").GetComponent<Image>();
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
    public void NewEnemyDisplay(EnemyObject _enemy) {
        enemyName.text = _enemy.enemyName;
        enemyCoin.text = _enemy.coinValue.ToString();
        enemyHealthBar.fillAmount = 1;
	}
}
