using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    GameObject enemySprite;
    
    // Start is called before the first frame update
    void Start()
    {
        enemySprite = GameObject.Find("enemy_sprite");
        Debug.Log("Found sprite");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
