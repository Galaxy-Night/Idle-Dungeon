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
	private readonly int[] NEXT_FLOOR_XP = { 300, 600, 1800, 3600, 7200 };
    private const int MAX_FLOOR = 5;

    public Render render;
    public GameObject currentEnemy;
    private Enemy enemyData;
    private GameObject[] validMonsters;

    int currentCoins;
    int currentFloor;
    int tapDamage;
    int currentXP;
    
    // Start is called before the first frame update
    void Start()
    {
        currentCoins = 0;
        tapDamage = 1;
        currentFloor = 1;
        currentXP = 0;
        GenerateFloor(currentFloor);
        GenerateNewEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemy == null)
            GenerateNewEnemy();
    }

    /// <summary>
    /// <c>GenerateNewEnemy</c> is called when a new enemy needs to be 
    /// generated. The function generates the new enemy, and updates
    /// the information stored in the <c>render</c> object
    /// </summary>
    private void GenerateNewEnemy() {
        System.Random random = new System.Random();
        if (currentEnemy)
            Destroy(currentEnemy);
        currentEnemy = Instantiate(validMonsters[random.Next(0, validMonsters.Length)]);
        currentEnemy.transform.parent = transform;
        enemyData = currentEnemy.GetComponent<Enemy>();
        render.NewEnemyDisplay(enemyData);
        render.currentCoins.text = currentCoins.ToString();
    }

    /// <summary>
    /// <c>GenerateFloor</c> generates the specifiec floor. It calls the
    /// function to generate the new list of valid enemies.
    /// </summary>
    /// <param name="floor">The level of the floor to be generated</param>
    private void GenerateFloor(int floor) {
        if (floor > 4)
            floor = 4;
        validMonsters = MonsterDefinitions.LoadLevel(floor);
	}

    /// <summary>
    /// <c>DealTapDamage</c> deals damage to the current enemy when the user
    /// taps. It is called in <c>EnemyObject</c>'s <c>OnMouseDown</c> function.
    /// </summary>
    void DealTapDamage() {
        enemyData.currentHealth -= tapDamage;
        if (enemyData.currentHealth <= 0) {
            currentCoins += enemyData.coinValue;
            currentXP += enemyData.xpValue;
            Debug.Log(currentXP);
            if (currentXP >= NEXT_FLOOR_XP[currentFloor - 1]) {
                currentXP -= NEXT_FLOOR_XP[currentFloor - 1];
                currentFloor++;
                GenerateFloor(currentFloor);
			}
            render.xpBar.fillAmount = currentXP / (float)NEXT_FLOOR_XP[currentFloor - 1];
            GenerateNewEnemy();
		}
        render.enemyHealthBar.fillAmount = enemyData.currentHealth / (float)enemyData.maxHealth;
	}
}
