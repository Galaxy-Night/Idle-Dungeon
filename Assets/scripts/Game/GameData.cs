using System;
using System.Collections.Generic;

using Utility;

/// <summary>
/// <c>GameData</c> is a class used to store the information from the game that needs to be saved
/// </summary>
public class GameData
{
    public readonly int[] XP_TO_LEVEL = new int[] { 300, 600, 1800, 3600, 7200 };
    public List<Tuple<int, PartyMemberData>> unlockCost;
    public int currentCoins;
    public int tapDamage;
    public int currentXP;
    public int currentFloor;
    public int partyMembersUnlocked;

    public GameData() {
        unlockCost = FileIO.GetUnlockCosts("Assets/Resources/txt/member_defs.txt");
        currentCoins = 0;
        currentXP = 0;
        tapDamage = 1;
        currentFloor = 1;
        partyMembersUnlocked = 0;
    }
}
