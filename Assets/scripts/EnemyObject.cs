using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    private GameObject enemyObject;

    // Start is called before the first frame update
    void Start()
    {
        enemyObject = GameObject.Find("enemy");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Debug.Log("Enemy clicked!");
        GameObject.Destroy(enemyObject);
    }
}
