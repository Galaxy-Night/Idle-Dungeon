using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public int MAX_HEALTH;
    public int currentHealth;
    public int coinDrop;
    public int xpDrop;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MAX_HEALTH;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
