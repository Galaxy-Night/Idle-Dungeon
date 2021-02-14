using System;
using System.Collections.Generic;

using UnityEngine;

using Utility;

/// <summary>
/// <c>Game is a class designed to handle the logic of the game</c>
/// </summary>
public class Game : MonoBehaviour
{
    private const int PARTY_MEMBER_OFFSET = -75;

    private GameData data;
    [SerializeField]
    private GameObject uiPrefab;
    [SerializeField]
    private GameObject enemyUI;

    private List<List<EnemyData>> validEnemies;

    private GameObject currentEnemy;
    private EnemyHandler currentEnemyHandler;

    private List<GameObject> partyMemberHandlers;
    private int partyMembersUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        data = new GameData();
        partyMemberHandlers = new List<GameObject>();
        partyMembersUnlocked = 1;
        validEnemies = new List<List<EnemyData>>();
        validEnemies.Add(FileIO.GetFloorEnemies("Assets/Resources/txt/", 0));
        ConstructEnemy(validEnemies[0][0]);
    }

    // Update is called once per frame
    void Update()
    {

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
        //initialize UI data
        newMember.GetComponent<PartyMemberHandler>().initialize(_data);

        newMember.transform.SetParent(parent);
        newMember.GetComponent<PartyMemberHandler>().ui.GetComponent<RectTransform>().localPosition = 
            new Vector3Int(0, PARTY_MEMBER_OFFSET * partyMembersUnlocked, 0);

        partyMemberHandlers.Add(newMember);
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
        int status = currentEnemyHandler.TakeDamage(data.tapDamage);
        if (status == PartyMemberData.DEATH_INDICATOR)
            HandleEnemyDeath();
	}

    private void HandleEnemyDeath() {
        data.currentCoins += currentEnemyHandler.Data.CoinValue;
        data.currentXP += currentEnemyHandler.Data.XpValue;
        Destroy(currentEnemyHandler.ui);
        Destroy(currentEnemy);
        ConstructEnemy(validEnemies[0][0]);
    }
}
