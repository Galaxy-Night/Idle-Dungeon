using System;
using System.Collections.Generic;

using Utility;

/// <summary>
/// <c>GameData</c> is a class used to store the information from the game that needs to be saved
/// </summary>
[Serializable]
public class GameData
{
    public static readonly int DEATH_INDICATOR = 1;
    public static readonly int INJURED_INDICATOR = 2;

    public readonly int STARTING_HEALTH = 100;
    public readonly int[] XP_TO_LEVEL = new int[] { 300, 600, 1800, 3600, 7200 };
    public readonly int MAX_FLOOR = 4;
    public List<Tuple<int, PartyMemberData>> unlockCost;
    public List<List<EnemyData>> validEnemies;
    public List<PartyMemberData> visiblePartyMembers;
    public int currentCoins;
    public int tapDamage;
    public int currentXP;
    public int currentFloor;
    public int partyMembersUnlocked;
    public int currentHealth;
    public int totalHealth;
    public int playerHealth;

    public GameData() {
        unlockCost = FileIO.GetUnlockCosts("Assets/Resources/txt/member_defs.txt");
        currentCoins = 0;
        currentXP = 0;
        tapDamage = 1;
        currentFloor = 1;
        partyMembersUnlocked = 0;
        totalHealth = STARTING_HEALTH;
        currentHealth = STARTING_HEALTH;
        playerHealth = STARTING_HEALTH;
        visiblePartyMembers = new List<PartyMemberData>();
        validEnemies = new List<List<EnemyData>>();
        validEnemies.Add(FileIO.GetFloorEnemies("Assets/Resources/txt/", 0));
        validEnemies.Add(FileIO.GetFloorEnemies("Assets/Resources/txt/", 1));
        validEnemies.Add(FileIO.GetFloorEnemies("Assets/Resources/txt/", 2));
    }

    public void AdvanceFloor() {
        if (currentFloor < MAX_FLOOR) {
            currentFloor++;
            validEnemies.RemoveAt(0);
            validEnemies.Add(FileIO.GetFloorEnemies("Assets/Resources/txt/", currentFloor + 1));
            currentXP -= XP_TO_LEVEL[currentFloor];
        }
	}

    public void UnlockMember(PartyMemberData data) {
        currentCoins -= data.UnlockCost;
        totalHealth += data.MaxHealth;
        currentHealth += data.CurrentHealth;
    }
}
