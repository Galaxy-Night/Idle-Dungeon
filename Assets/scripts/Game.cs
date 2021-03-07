using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameData data;

    //ui elements
    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private Image xpBar;
    [SerializeField]
    private Text currentCoins;

    [SerializeField]
    private Transform enemyUIParent;

    // Start is called before the first frame update
    void Start()
    {
        data = new GameData();
        LoadEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleEnemyDeath(int coinDrop, int xpDrop) {
        data.HandleEnemyDeath(coinDrop, xpDrop);
        currentCoins.text = data.currentCoins.ToString();
        xpBar.fillAmount = (float)data.xp / data.XP_TO_LEVEL[data.currentFloor - 1];
        LoadEnemy();
	}

    public void LoadEnemy() {
        Instantiate(Resources.Load("enemyprefabs/animated-shrub"), enemyUIParent);
	}
}
