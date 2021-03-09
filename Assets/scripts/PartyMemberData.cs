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
    public bool IsInjured { get; private set; }
    public int HealCost { get; private set; }
    public bool IsDead { get; private set; }
    /*public bool IsUnlocked { get; private set; }*/

    // Start is called before the first frame update
    void Start()
    {
        CurrentLevel = 0;
        CurrentHealth = MaxHealth;
        Damage = StartingDamage;
        IsInjured = false;
        IsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock() {
        CurrentLevel++;
        LevelCost = (int)(UnlockCost * Mathf.Pow(CostMultiplier, CurrentLevel));
    }
    public void LevelUp() {
        CurrentLevel++;
        LevelCost = (int)(UnlockCost * Mathf.Pow(CostMultiplier, CurrentLevel));
        Damage += StartingDamage;
        MaxHealth += startingHealth;
        CurrentHealth += startingHealth;
    }

    public void TakeDamage(int amount) {
        CurrentHealth -= amount;
        if (CurrentHealth < MaxHealth / 2 && CurrentHealth > 0) {
            if (!IsInjured) {
                IsInjured = true;
                GetComponent<PartyMemberUI>().Injure();
            }
            HealCost = Mathf.CeilToInt((float)LevelCost * (1 - (float)CurrentHealth / MaxHealth));
            GetComponent<PartyMemberUI>().UpdateHealCost(HealCost);
        }
        else if (CurrentHealth <= 0) {
            if (!IsDead) {
                IsDead = true;
                IsInjured = false;
                GetComponent<PartyMemberUI>().Kill();
                HealCost = Mathf.CeilToInt((float)LevelCost * (1 - (float)CurrentHealth / MaxHealth)) * 2;
                GetComponent<PartyMemberUI>().UpdateHealCost(HealCost);
            }
            CurrentHealth = 0;
		}
        GetComponent<PartyMemberUI>().UpdateHealthBar();
	}

    public void HealRevive() {
        CurrentHealth = MaxHealth;
        IsInjured = false;
        IsDead = false;
        HealCost = 0;
	}
}
