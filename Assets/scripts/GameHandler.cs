using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    GameObject enemyObject;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyObject = GameObject.Find("enemy");
        Debug.Log("Found enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
