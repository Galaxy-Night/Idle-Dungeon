using System.Xml;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class SaveData
{
    public int tapDamage;
    public int currentCoins;
    public int xp;
    public int currentFloor;
    public int totalMaxHealth;
    public int visibleMembers;

    public EnemySave currentEnemy;
    public List<PartyMemberSave> partyMembers;

    public DateTime lastPlayed;

    public void init(GameData data, EnemyUI enemy, List<PartyMemberData> party) {
        tapDamage = data.tapDamage;
        currentCoins = data.currentCoins;
        xp = data.xp;
        currentFloor = data.currentFloor;
        visibleMembers = data.visibleMembers;
        currentEnemy = EnemySave.init(enemy);
        partyMembers = new List<PartyMemberSave>();
        foreach (PartyMemberData member in party)
            partyMembers.Add(PartyMemberSave.init(member));
        lastPlayed = DateTime.UtcNow;
	}
}
