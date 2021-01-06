using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>Enemy</c> represents the current enemy that the user is facing. It 
/// contains the information needed to handle the user's interaction with the 
/// enemy.
/// </summary>

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int maxHealth;
    public int currentHealth;
    public int coinValue;
    public int xpValue;
    public int damage;
    public int damageInterval;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnMouseDown is called when the user clicks on the object
    private void OnMouseDown()
    {
        SendMessageUpwards("DealTapDamage");
    }
}
