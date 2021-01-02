using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <c>GameHandler</c> is a class to handle the main functionality of the game.
/// </summary>
public class GameHandler : MonoBehaviour
{
    public Render render;
    public GameObject currentEnemy;
    private EnemyObject enemyData;
    private GameObject[] validMonsters;

    int currentCoins;
    int currentFloor;
    int tapDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        currentCoins = 0;
        tapDamage = 1;
        currentFloor = 1;
        GenerateFloor(currentFloor);
        GenerateNewEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemy == null)
            GenerateNewEnemy();
    }

    void GenerateNewEnemy() {
        System.Random random = new System.Random();
        if (currentEnemy)
            Destroy(currentEnemy);
        currentEnemy = Instantiate(validMonsters[random.Next(0, validMonsters.Length)]);
        currentEnemy.transform.parent = transform;
        enemyData = currentEnemy.GetComponent<EnemyObject>();
    }

    void GenerateFloor(int floor) {
        validMonsters = MonsterDefinitions.TEMPLoadLevel();
	}

    void DealTapDamage() {
        enemyData.currentHealth -= tapDamage;
        if (enemyData.currentHealth <= 0) {
            currentCoins += enemyData.coinValue;
            GenerateNewEnemy();
		}
	}
}
