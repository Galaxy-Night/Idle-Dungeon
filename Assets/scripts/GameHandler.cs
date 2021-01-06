﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <c>GameHandler</c> is a class to handle the main functionality of the game.
/// </summary>
public class GameHandler : MonoBehaviour
{
	private readonly int[] NEXT_FLOOR_XP = { 300, 600, 1800, 3600, 7200 };
    private const int MAX_FLOOR = 4;
    private const int PARTY_MEMBER_OFFSET = -100;

    public GameObject partyMemberDisplay;

    public Render render;
    public GameObject currentEnemy;
    private Enemy enemyData;
    private GameObject[] validMonsters;

    private List<PartyMember> partyMembers;

    int currentCoins;
    int currentFloor;
    int tapDamage;
    int currentXP;
    int totalHealthMax;
    int totalHealth;
    private int partyMembersUnlocked;

    private int visiblePartyMembers;
    private List<Tuple<int, GameObject>> unlockCost;
    
    // Start is called before the first frame update
    void Start()
    {
        partyMembers = new List<PartyMember>();
        PartyMemberUnlockCosts();
        currentCoins = 0;
        tapDamage = 1;
        currentFloor = 1;
        currentXP = 0;
        totalHealth = 100;
        totalHealthMax = 100;
        visiblePartyMembers = 0;
        InvokeRepeating("DealPartyDamage", 1f, 1f);
        GenerateFloor(currentFloor);
        GenerateNewEnemy();
        partyMembersUnlocked = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemy == null)
            GenerateNewEnemy();
        if (unlockCost.Count != 0 && currentCoins >= unlockCost[0].Item1 / 2) {
            GameObject newMember = Instantiate(unlockCost[0].Item2);
            newMember.GetComponent<RectTransform>().localPosition = 
                new Vector3Int(0, (0 + PARTY_MEMBER_OFFSET * 
                partyMembersUnlocked), -2);
            newMember.transform.SetParent(partyMemberDisplay.transform, false);
            unlockCost.RemoveAt(0);
            partyMembersUnlocked++;
		}
    }

    /// <summary>
    /// <c>GenerateNewEnemy</c> is called when a new enemy needs to be 
    /// generated. The function generates the new enemy, and updates
    /// the information stored in the <c>render</c> object
    /// </summary>
    private void GenerateNewEnemy() {
        System.Random random = new System.Random();
        if (currentEnemy)
            Destroy(currentEnemy);
        currentEnemy = Instantiate(validMonsters[random.Next(0, validMonsters.Length)]);
        currentEnemy.transform.parent = transform;
        enemyData = currentEnemy.GetComponent<Enemy>();
        InvokeRepeating("DealAutoDamage", enemyData.damageInterval, enemyData.damageInterval);
        render.NewEnemyDisplay(enemyData);
        render.UpdateCoinDisplay(currentCoins);
    }

    /// <summary>
    /// <c>GenerateFloor</c> generates the specifiec floor. It calls the
    /// function to generate the new list of valid enemies.
    /// </summary>
    /// <param name="floor">The level of the floor to be generated</param>
    private void GenerateFloor(int floor) {
        if (floor > MAX_FLOOR)
            floor = MAX_FLOOR;
        validMonsters = MonsterDefinitions.LoadLevel(floor);
	}

    /// <summary>
    /// <c>DealTapDamage</c> deals damage to the current enemy when the user
    /// taps. It is called in <c>EnemyObject</c>'s <c>OnMouseDown</c> function.
    /// </summary>
    void DealTapDamage() {
        enemyData.currentHealth -= tapDamage;
        if (enemyData.currentHealth <= 0) {
            HandleEnemyDeath();
		}
        render.enemyHealthBar.fillAmount = enemyData.currentHealth / (float)enemyData.maxHealth;
	}

    private void UnlockPartyMember(PartyMember unlocked) {
        //Debug.Log("clicked");
        if (unlocked.unlockCost <= currentCoins) {
            currentCoins -= (int)unlocked.unlockCost;
            unlocked.Unlock();
            Debug.Log(currentCoins);
            render.UpdateCoinDisplay(currentCoins);
            partyMembers.Add(unlocked);
            totalHealthMax += unlocked.maxHealth;
            totalHealth += unlocked.currentHealth;
            render.UpdateTotalHealth(totalHealthMax, totalHealth);
            unlocked.nameField.text = unlocked.memberName;
        }
	}

    private void LevelUpPartyMember(PartyMember leveledUp) {
        if (leveledUp.unlockCost <= currentCoins) {
            //Debug.Log("Level up!");
            leveledUp.LevelUp();
            currentCoins -= (int)leveledUp.unlockCost;
            render.UpdateCoinDisplay(currentCoins);
            totalHealthMax += leveledUp.maxHealth;
            totalHealth += leveledUp.currentHealth;
            render.UpdateTotalHealth(totalHealthMax, totalHealth);
        }
	}

    private void DealAutoDamage() {
        totalHealth -= (int)((100 / (float)totalHealthMax) * enemyData.damage);
        foreach (PartyMember member in partyMembers) {
            if (member.isUnlocked && !member.isUnconcious) {
                float temp = member.maxHealth / (float)totalHealthMax;
                int damage = (int)(temp * enemyData.damage);
                member.currentHealth -= damage;
                totalHealth -= damage;
                member.UpdateHPBar();
                if (member.currentHealth <= 0)
                    member.KnockOut();
            }
        }
        render.UpdateTotalHealth(totalHealthMax, totalHealth);
        //Debug.Log(totalHealth);
	}

    private void CalculateTotalHealth() {
        totalHealth = 100;
        foreach (PartyMember member in partyMembers)
            totalHealth += member.currentHealth;
	}

    private void DealPartyDamage() {
        foreach (PartyMember member in partyMembers) {
            if (member.isUnlocked && !member.isUnconcious) {
                if (enemyData.currentHealth <= 0)
                    HandleEnemyDeath();
                enemyData.currentHealth -= member.damage;
                render.enemyHealthBar.fillAmount = enemyData.currentHealth / (float)enemyData.maxHealth;
            }
        }
	}

    private void HandleEnemyDeath() {
        currentCoins += enemyData.coinValue;
        currentXP += enemyData.xpValue;
        //Debug.Log(currentXP);
        if (currentXP >= NEXT_FLOOR_XP[currentFloor - 1])
        {
            currentXP -= NEXT_FLOOR_XP[currentFloor - 1];
            currentFloor++;
            GenerateFloor(currentFloor);
        }
        render.xpBar.fillAmount = currentXP / (float)NEXT_FLOOR_XP[currentFloor - 1];
        GenerateNewEnemy();
    }

    private void PartyMemberUnlockCosts() {
        unlockCost = new List<Tuple<int, GameObject>>();
        unlockCost.Add(new Tuple<int, GameObject>(10, (GameObject)Resources.Load("partyMembers/fighter")));
        unlockCost.Add(new Tuple<int, GameObject>(100, (GameObject)Resources.Load("partyMembers/archer")));
	}
}
