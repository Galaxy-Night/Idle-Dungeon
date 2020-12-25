using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary><c>EnemyObject</c> represents the enemy prefab that Unity 
/// interacts with. It contains the interface for the user to interact with 
/// enemies</summary>

public class EnemyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnMouseDown is called when the user clicks on the object
    private void OnMouseDown()
    {
        SendMessageUpwards("DealEnemyDamage", 1);
    }
}
