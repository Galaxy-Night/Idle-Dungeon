using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMemberData : MonoBehaviour
{
    public int UnlockCost;
    public int StartingDamage;
    public int startingHealth;
    public int MaxHealth;
    public int Damage;

    public int CurrentLevel { get; private set; }
    public int LevelCost { get; private set; }
    public int CurrentHealth { get; private set; }
    public float CostMultiplier;
    /*public int HealCost { get; private set; }
    public bool IsInjured { get; private set; }
    public bool IsDead { get; private set; }
    public bool IsUnlocked { get; private set; }*/

    // Start is called before the first frame update
    void Start()
    {
        CurrentLevel = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp() {
        CurrentLevel++;
        LevelCost = (int)(UnlockCost * Mathf.Pow(CostMultiplier, CurrentLevel));
        Damage += StartingDamage;
        MaxHealth += startingHealth;
        CurrentHealth += startingHealth;
    }
}
