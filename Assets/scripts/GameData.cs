using System.Collections;
using System.Collections.Generic;
using System;

public class GameData
{
    public readonly int[] XP_TO_LEVEL = { 300, 600, 1800, 3600, 7200 };
    public int tapDamage { get; private set; }
    public int currentCoins { get; private set; }
    public int xp { get; private set; }
    public int currentFloor { get; private set; }
    public int totalMaxHealth { get; private set; }
    public List<Tuple<int, string>> charactersToUnlock;
    public List<PartyMemberData> unlockedPartyMembers;

    public GameData() {
        tapDamage = 1;
        currentFloor = 1;
        xp = 0;
        currentCoins = 0;
        totalMaxHealth = 0;
        charactersToUnlock = generateUnlockCosts();
        unlockedPartyMembers = new List<PartyMemberData>();
    }

    public void HandleEnemyDeath(int coinDrop, int xpDrop) {
        currentCoins += coinDrop;
        xp += xpDrop;
    }

    private void LoadEnemy() {
          
	}

    private List<Tuple<int, string>> generateUnlockCosts() {
        List<Tuple<int, string>> returned = new List<Tuple<int, string>>();
        returned.Add(new Tuple<int, string>(10, "fighter"));
        /*returned.Add(new Tuple<int, string>(100, "archer"));
        returned.Add(new Tuple<int, string>(500, "cleric"));
        returned.Add(new Tuple<int, string>(1000, "wizard"));*/
        return returned;
    }

    public void UnlockPartyMember(PartyMemberData memberData) {
        currentCoins -= memberData.UnlockCost;
        unlockedPartyMembers.Add(memberData);
    }

    public void HealRevivePartyMember(int cost) {
        currentCoins -= cost;
	}
}
