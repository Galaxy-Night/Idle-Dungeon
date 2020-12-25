using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject enemyPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
