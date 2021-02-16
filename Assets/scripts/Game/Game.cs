using System;
using System.Collections.Generic;

using UnityEngine;

using Utility;

/// <summary>
/// <c>Game is a class designed to handle the logic of the game</c>
/// </summary>
public class Game : MonoBehaviour
{
    private const int PARTY_MEMBER_OFFSET = 150;
    private int partyMemberY = 150;

    private GameUI ui;
    private GameData data;
    [SerializeField]
    private GameObject uiPrefab;
    [SerializeField]
    private GameObject enemyUI;

    private List<List<EnemyData>> validEnemies;

    private GameObject currentEnemy;
    private EnemyHandler currentEnemyHandler;

    private List<PartyMemberHandler> partyMemberHandlers;
    private int partyMembersUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        data = new GameData();
        ui = gameObject.GetComponent<GameUI>();
        partyMemberHandlers = new List<PartyMemberHandler>();
        validEnemies = new List<List<EnemyData>>();
        validEnemies.Add(FileIO.GetFloorEnemies("Assets/Resources/txt/", 0));
        validEnemies.Add(FileIO.GetFloorEnemies("Assets/Resources/txt/", 1));
        validEnemies.Add(FileIO.GetFloorEnemies("Assets/Resources/txt/", 2));
        ConstructEnemy(validEnemies[0][0]); //Always starts with an Animated Shrub
        InvokeRepeating("AutoEnemyDamage", 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetCurrentCoins() {
        return data.currentCoins;
	}

    /// <summary>
    /// <c>ConstructPartyMember</c> creates the appropriate <c>GameObjects</c>
    /// to represent the party member
    /// </summary>
    /// <param name="_data">The <c>PartyMemberData</c> object corresponding to
    /// the party member being created</param>
    private void ConstructPartyMember(PartyMemberData _data)
    {
        //get party member container's transform
        Transform parent = GameObject.Find("party_members").GetComponent<Transform>();
        //get UI container's transform
        Transform uiParent = GameObject.Find("Content").GetComponent<Transform>();
        //construct new party member handler and UI objects
        GameObject newMember = new GameObject(_data.MemberName);

        newMember.AddComponent<PartyMemberHandler>();
        //set member variables
        newMember.GetComponent<PartyMemberHandler>().ui = Instantiate(uiPrefab, uiParent);
        newMember.GetComponent<PartyMemberHandler>().ui.name = _data.MemberName + "_ui";
        //initialize UI data
        newMember.GetComponent<PartyMemberHandler>().initialize(_data);

        newMember.transform.SetParent(parent);
        newMember.GetComponent<PartyMemberHandler>().ui.GetComponent<RectTransform>().anchoredPosition = 
            new Vector3(0, partyMemberY, 0);
        partyMemberY -= PARTY_MEMBER_OFFSET;
    }

    /// <summary>
    /// <c>ConstructEnemy</c> creates the appropriate <c>GameObject</c>s to 
    /// represent an enemy
    /// </summary>
    /// <param name="data">The <c>EnemyData</c> object corresponding to the 
    /// enemy to be created</param>
    private void ConstructEnemy(EnemyData data) {
        Transform parent = GameObject.Find("game_handler").GetComponent<Transform>();
        Transform uiParent = GameObject.Find("Canvas").GetComponent<Transform>();

        GameObject enemy = new GameObject(data.EnemyName);
        enemy.AddComponent<EnemyHandler>();
        currentEnemyHandler = enemy.GetComponent<EnemyHandler>();
        currentEnemyHandler.ui = Instantiate(enemyUI, uiParent);
        currentEnemyHandler.EnemyHandlerInitialize(new EnemyData(data));

        enemy.transform.SetParent(parent);

        currentEnemy = enemy;
	}

    public void ClickEnemyDamage() {
        int status = currentEnemyHandler.TakeDamage(data.tapDamage, true);
        if (status == PartyMemberData.DEATH_INDICATOR)
            HandleEnemyDeath();
	}

    private void HandleEnemyDeath() {
        int floor = UnityEngine.Random.Range(data.currentFloor - 1, data.currentFloor + 2);
        data.currentCoins += currentEnemyHandler.Data.CoinValue;
        data.currentXP += currentEnemyHandler.Data.XpValue;
        ui.ChangeXPBar((float) data.currentXP / data.XP_TO_LEVEL[data.currentFloor]);
        ui.ChangeCurrentCoins(data.currentCoins);
        Destroy(currentEnemyHandler.ui);
        Destroy(currentEnemy);
        //TODO: sometimes throws an out of range bug on the random.range part
        ConstructEnemy(validEnemies[floor][UnityEngine.Random.Range(0, validEnemies[0].Count)]);

        Debug.Log(data.currentCoins / 2);
        if (data.unlockCost.Count > 0)
        {
            if (data.unlockCost[0].Item1 / 2 <= data.currentCoins)
            {
                ConstructPartyMember(data.unlockCost[0].Item2);
                data.unlockCost.RemoveAt(0);
            }
        }
    }

    public void UnlockPartyMember(PartyMemberHandler handler) {
        data.currentCoins -= handler.data.UnlockCost;
        ui.ChangeCurrentCoins(data.currentCoins);
        partyMemberHandlers.Add(handler);
	}

    public void AutoEnemyDamage() {
        foreach(PartyMemberHandler member in partyMemberHandlers) {
            int status = currentEnemyHandler.TakeDamage(member.data.Damage, false);
            if (status == PartyMemberData.DEATH_INDICATOR)
                HandleEnemyDeath();
		}
	}
}
