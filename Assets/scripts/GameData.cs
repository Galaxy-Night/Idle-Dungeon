using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;

[Serializable]
public class GameData
{
    public readonly int MAX_LEVEL = 4;
    public readonly int[] XP_TO_LEVEL = { 300, 600, 1800, 3600, 7200 };
    public int tapDamage { get; private set; }
    public int currentCoins { get; private set; }
    public int xp { get; private set; }
    public int currentFloor { get; private set; }
    public int totalMaxHealth { get; private set; }

    public int visibleMembers;

    public float xpMultiplier { get; private set; }
    public float coinMultiplier { get; private set; }

    public List<PartyMemberData> unlockedPartyMembers;

    public GameData() {
        tapDamage = 1;
        currentFloor = 1;
        xp = 0;
        currentCoins = 0;
        totalMaxHealth = 0;
        unlockedPartyMembers = new List<PartyMemberData>();
        visibleMembers = 0;
        xpMultiplier = 1;
        coinMultiplier = 1;
    }

    public void HandleEnemyDeath(int coinDrop, int xpDrop) {
        currentCoins += coinDrop;
        xp += xpDrop;
    }

    private void LoadEnemy() {
          
	}

    public void UnlockPartyMember(PartyMemberData memberData) {
        currentCoins -= memberData.UnlockCost;
        unlockedPartyMembers.Add(memberData);
    }

    public void HealRevivePartyMember(int cost) {
        currentCoins -= cost;
	}

    public void LevelUpPartyMember(int cost) {
        currentCoins -= cost;
	}

    public void LevelUp() {
        xp -= XP_TO_LEVEL[currentFloor - 1];
        currentFloor++;
	}

    public void Initialize(SaveData data) {
        tapDamage = data.tapDamage;
        currentCoins = data.currentCoins;
        xp = data.xp;
        currentFloor = data.currentFloor;
        visibleMembers = data.visibleMembers;
    }

    public void UpgradeTap(float multiplier, int cost) {
        tapDamage = (int)(tapDamage * multiplier);
        currentCoins -= cost;
	}

    public void UpgradeXp(float multiplier, int cost) {
        xpMultiplier *= multiplier;
        currentCoins -= cost;
    }

    public void UpgradeCoin(float multiplier, int cost) {
        coinMultiplier *= multiplier;
        currentCoins -= cost;
	}
}
