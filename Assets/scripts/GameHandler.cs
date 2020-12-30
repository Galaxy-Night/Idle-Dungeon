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
    HashSet<GameObject> validMonsters;

    int currentCoins;
    int currentFloor;
    int tapDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        currentCoins = 0;
        tapDamage = 1;
        GenerateFloor(1);
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
        GameObject[] tempArray = new GameObject[validMonsters.Count];
        validMonsters.CopyTo(tempArray);
        if (currentEnemy)
            Destroy(currentEnemy);
        currentEnemy = Instantiate(tempArray[random.Next(0, validMonsters.Count)]);
    }

    void GenerateFloor(int floor) {
        validMonsters = MonsterDefinitions.TEMPLoadLevel();
	}
}
