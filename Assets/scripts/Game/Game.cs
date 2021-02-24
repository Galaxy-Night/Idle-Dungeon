using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

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

    private GameObject currentEnemy;
    private EnemyHandler currentEnemyHandler;

    private List<PartyMemberHandler> partyMemberHandlers;

    // Start is called before the first frame update
    void Start()
    {
        partyMemberHandlers = new List<PartyMemberHandler>();
        ui = gameObject.GetComponent<GameUI>();

        LoadSave();

        InvokeRepeating("AutoEnemyDamage", 1, 1f);
        InvokeRepeating("AutoPlayerDamage", 1, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnApplicationQuit is called when the game is closed
	private void OnApplicationQuit()
	{
        Save save = new Save(currentEnemyHandler.Data, data, partyMemberHandlers);
        save.Serialize();
	}

    /// <summary>
    /// A public function to get the number of coins the player has
    /// </summary>
    /// <returns>The amount of coins the player has</returns>
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

        data.visiblePartyMembers.Add(_data);
    }

    private void ConstructUnlockedMember(PartyMemberData data) {
        //get party member container's transform
        Transform parent = GameObject.Find("party_members").GetComponent<Transform>();
        //get UI container's transform
        Transform uiParent = GameObject.Find("Content").GetComponent<Transform>();
        //construct new party member handler and UI objects
        GameObject newMember = new GameObject(data.MemberName);

        newMember.AddComponent<PartyMemberHandler>();
        //set member variables
        newMember.GetComponent<PartyMemberHandler>().ui = Instantiate(uiPrefab, uiParent);
        newMember.GetComponent<PartyMemberHandler>().ui.name = data.MemberName + "_ui";
        //initialize UI data
        newMember.GetComponent<PartyMemberHandler>().InitializeUnlocked(data);

        newMember.transform.SetParent(parent);
        newMember.GetComponent<PartyMemberHandler>().ui.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(0, partyMemberY, 0);
        partyMemberY -= PARTY_MEMBER_OFFSET;

        partyMemberHandlers.Add(newMember.GetComponent<PartyMemberHandler>());
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

    /// <summary>
    /// Deals damage to enemy when player clicks them
    /// </summary>
    public void ClickEnemyDamage() {
        int status = currentEnemyHandler.TakeDamage(data.tapDamage, true);
        if (status == GameData.DEATH_INDICATOR)
            HandleEnemyDeath();
	}

    /// <summary>
    /// Handles the logic of an enemy dying. Removes the current enemy,
    /// determines if the game should progress, and creates a new enemy
    /// </summary>
    private void HandleEnemyDeath() {
        int floor = UnityEngine.Random.Range(0, 3);
        int enemy = UnityEngine.Random.Range(0, data.validEnemies[floor].Count);
        data.currentCoins += currentEnemyHandler.Data.CoinValue;
        data.currentXP += currentEnemyHandler.Data.XpValue;
        Destroy(currentEnemyHandler.ui);
        Destroy(currentEnemy);

        //should next party member be displayed?
        if (data.unlockCost.Count > 0)
        {
            if (data.unlockCost[0].Item1 / 2 <= data.currentCoins)
            {
                ConstructPartyMember(data.unlockCost[0].Item2);
                data.unlockCost.RemoveAt(0);
            }
        }
        //should player move to next floor?
        if (data.currentXP > data.XP_TO_LEVEL[data.currentFloor - 1]) {
            data.AdvanceFloor();
		}
        //update UI
        ui.ChangeXPBar((float)data.currentXP / data.XP_TO_LEVEL[data.currentFloor - 1]);
        ui.ChangeCurrentCoins(data.currentCoins);
        //Create the next enemy
        ConstructEnemy(data.validEnemies[floor][enemy]);
    }

    /// <summary>
    /// Runs when a party member is hired by the player
    /// </summary>
    /// <param name="handler">The <c>PartyMemberHandler</c> corresponding to 
    /// the newly hired hero</param>
    public void UnlockPartyMember(PartyMemberHandler handler) {
        data.UnlockMember(handler.data);
        ui.ChangeCurrentCoins(data.currentCoins);
        ui.ChangePlayerHpBar((float)data.currentHealth / data.totalHealth);
        partyMemberHandlers.Add(handler);
	}

    /// <summary>
    /// Deals damage to the current enemy every second
    /// </summary>
    public void AutoEnemyDamage() {
        foreach(PartyMemberHandler member in partyMemberHandlers) {
            if(!member.data.IsDead) {
                int status = currentEnemyHandler.TakeDamage(member.data.Damage, false);
                if (status == GameData.DEATH_INDICATOR)
                    HandleEnemyDeath();
            }
		}
	}

    /// <summary>
    /// Handles damage that enemies deal every second.
    /// </summary>
    public void AutoPlayerDamage() {
        int damage = currentEnemyHandler.Data.Damage;
        //determines how much of the damage the player should take
        int playerDamage = (int)Math.Round(damage * 
            ((float)data.STARTING_HEALTH / data.totalHealth)); 

        if (data.playerHealth != 0) {
            if (playerDamage >= data.playerHealth)
                data.playerHealth = 0;
            else 
                data.playerHealth -= playerDamage;
        }
        foreach (PartyMemberHandler member in partyMemberHandlers) {
            if (!member.data.IsDead) {
                //Determines how much of the damage the party member should
                //take
                int damageTaken = (int)Math.Round(damage * 
                    ((float)member.data.MaxHealth / data.totalHealth));
                member.TakeDamage(damageTaken);
            }
		}
        UpdateCurrentHealth();
        ui.ChangePlayerHpBar((float)data.currentHealth / data.totalHealth);
    }

    /// <summary>
    /// Determines the total health of the player and all their party members
    /// </summary>
    private void UpdateCurrentHealth() {
        data.currentHealth = data.playerHealth;
        foreach (PartyMemberHandler member in partyMemberHandlers)
            data.currentHealth += member.data.CurrentHealth;
        if (data.currentHealth == 0)
            SceneManager.LoadScene("LeaveDungeon");
	}

    /// <summary>
    /// Runs when a player heals a party member or levels them up. The same
    /// logic happens for both, so I put them in one function
    /// </summary>
    /// <param name="cost">How much the player spent</param>
    public void HealLevel(int cost) {
        data.currentCoins -= cost;
        UpdateCurrentHealth();
        ui.ChangePlayerHpBar((float)data.currentHealth / data.totalHealth);
	}

    /// <summary>
    /// Pauses the game when the menu opens
    /// </summary>
    public void Pause() {
        GetComponent<BoxCollider2D>().enabled = false;
        CancelInvoke("AutoEnemyDamage");
        CancelInvoke("AutoPlayerDamage");
	}

    /// <summary>
    /// Resumes the game when the menu closes
    /// </summary>
    public void Unpause() {
        GetComponent<BoxCollider2D>().enabled = true;
        InvokeRepeating("AutoEnemyDamage", 0, 1f);
        InvokeRepeating("AutoPlayerDamage", 0, 1f);
    }

    /// <summary>
    /// Loads game data stored in load_handler, then destroys it
    /// </summary>
    private void LoadSave() {
        Load load = GameObject.Find("load_handler").GetComponent<Load>();

        if (load != null)
        {
            if (load.save != null)
            {
                data = load.save.gameData;
                ConstructEnemy(load.save.currentEnemy);
                ui.ChangeCurrentCoins(data.currentCoins);
                ui.ChangePlayerHpBar((float)data.currentHealth / data.totalHealth);
                ui.ChangeXPBar((float)data.currentXP / data.XP_TO_LEVEL[data.currentFloor - 1]);
                if(data.visiblePartyMembers.Count != 0) {
                    List<PartyMemberData> oldList = new List<PartyMemberData>(data.visiblePartyMembers);
                    data.visiblePartyMembers = new List<PartyMemberData>();
                    foreach (PartyMemberData memberData in oldList) {
                        if (!memberData.IsUnlocked)
                            ConstructPartyMember(memberData);
                        else
                            ConstructUnlockedMember(memberData);
					}    
                }
            }
            else
            {
                data = new GameData();
                ConstructEnemy(data.validEnemies[0][0]); //Always starts with an Animated Shrub
            }
        }
        else
        {
            data = new GameData();
            ConstructEnemy(data.validEnemies[0][0]);
        }

        Destroy(GameObject.Find("load_handler"));
    }
}
