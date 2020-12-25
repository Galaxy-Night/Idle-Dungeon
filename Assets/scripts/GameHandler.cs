using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>GameHandler</c> is a class to handle the main functionality of the game.
/// </summary>
public class GameHandler : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Enemy currentEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyPrefab = Instantiate(enemyPrefab, transform);
        currentEnemy = new Enemy(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// <c>DealEnemyDamage</c> is invoked by <c>EnemyObject</c> when the user
    /// clicks within the enemy's collision box using 
    /// <c>SendMessageUpwards</c>. <c>DealEnemyDamage</c> is used to ensure the
    /// game responds properly when an enemy is killed.
    /// </summary>
    /// <param name="_amount">The amount of damage the enemy is dealt</param>
    void DealEnemyDamage(int _amount) {
        if (currentEnemy.TakeDamage(_amount))
            Destroy(enemyPrefab);
	}
}
