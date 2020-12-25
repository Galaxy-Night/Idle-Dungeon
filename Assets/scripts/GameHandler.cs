using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void DealEnemyDamage(int _amount) {
        if (currentEnemy.TakeDamage(_amount))
            Destroy(enemyPrefab);
	}
}
