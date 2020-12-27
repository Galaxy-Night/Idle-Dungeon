using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void NewEnemyDisplay(Enemy _enemy) {
        enemyName.text = _enemy.name;
        enemyCoin.text = _enemy.coinValue.ToString();
	}
}
