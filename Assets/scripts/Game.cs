using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameData data;

    //ui elements
    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private Image xpBar;
    [SerializeField]
    private Text currentCoins;

    [SerializeField]
    private Transform enemyUIParent;
    [SerializeField]
    private Transform partyMemberUIParent;

    private EnemyUI currentEnemy;

    List<List<GameObject>> validEnemies;

    private int partyMemberYOffset;
    private int partyMemberY;

    // Start is called before the first frame update
    void Start()
    {
        data = new GameData();
        partyMemberYOffset = -133;
        partyMemberY = partyMemberYOffset * -1;
        validEnemies = new List<List<GameObject>>();

        LoadLevelEnemies(0);
        LoadLevelEnemies(1);
        LoadLevelEnemies(2);
        LoadEnemy();

        InvokeRepeating("DealAllPlayerAutoDamage", 1f, 1f);
        InvokeRepeating("DealEnemyAutoDamage", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause() {
        CancelInvoke("DealAllPlayerAutoDamage");
        CancelInvoke("DealEnemyAutoDamage");
	}

    public void Unpause() {
        InvokeRepeating("DealAllPlayerAutoDamage", 1f, 1f);
        InvokeRepeating("DealEnemyAutoDamage", 1f, 1f);
    }
        
    public void HandleEnemyDeath(int coinDrop, int xpDrop) {
        data.HandleEnemyDeath(coinDrop, xpDrop);
        currentCoins.text = data.currentCoins.ToString();
        xpBar.fillAmount = (float)data.xp / data.XP_TO_LEVEL[data.currentFloor - 1];
        if (data.charactersToUnlock.Count > 0) {
            if (data.charactersToUnlock[0].Item1 / 2 <= data.currentCoins)
            {
                GameObject newMember = (GameObject)Instantiate(Resources.Load("partymembers/" + data.charactersToUnlock[0].Item2), partyMemberUIParent);
                newMember.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, partyMemberY, 0);
                partyMemberY += partyMemberYOffset;
                data.charactersToUnlock.RemoveAt(0);
            }
        }
        if (data.currentFloor < data.MAX_LEVEL)
        {
            if (data.xp >= data.XP_TO_LEVEL[data.currentFloor - 1]) {
                LevelUp();
			}
		}
        LoadEnemy();
	}

    public void LoadEnemy() {
        int floor = Random.Range(0, validEnemies.Count);
        Debug.Log(floor);
        int enemy = Random.Range(0, validEnemies[floor].Count - 1);
        GameObject temp = (GameObject)Instantiate(validEnemies[floor][enemy], enemyUIParent);
        currentEnemy = temp.GetComponent<EnemyUI>();
	}

    public void UnlockPartyMember(PartyMemberData memberData) {
        data.UnlockPartyMember(memberData);
        currentCoins.text = data.currentCoins.ToString();
	}

    private void DealAllPlayerAutoDamage() {
        if (data.unlockedPartyMembers.Count != 0) {
            foreach (PartyMemberData member in data.unlockedPartyMembers) {
                if (!member.IsDead)
                    DealPlayerAutoDamage(member);
            }
		}
	}

    private void DealPlayerAutoDamage(PartyMemberData data) {
        currentEnemy.TakeDamage(data.Damage);
	}

    private void DealEnemyAutoDamage() {
        if (data.unlockedPartyMembers.Count != 0)
        {
            int totalMaxHealth = 0;
            int totalCurrentHealth = 0;

            foreach (PartyMemberData member in data.unlockedPartyMembers)
                totalMaxHealth += member.MaxHealth;
            foreach (PartyMemberData member in data.unlockedPartyMembers)
            {
                float fractionDamageTaken = (float)member.MaxHealth / totalMaxHealth;
                if (currentEnemy != null)
                    member.TakeDamage(Mathf.CeilToInt(fractionDamageTaken * currentEnemy.data.damage));
                totalCurrentHealth += member.CurrentHealth;
            }
            if (totalCurrentHealth == 0) {
                SceneManager.LoadScene("LeaveDungeon");
			}
            hpBar.fillAmount = (float)totalCurrentHealth / totalMaxHealth;
        }
    }

    public void HealRevivePartyMember(PartyMemberData memberData) {
        data.HealRevivePartyMember(memberData.HealCost);
        currentCoins.text = data.currentCoins.ToString();
    }

    public void LevelUpPartyMember(PartyMemberData memberData) {
        data.LevelUpPartyMember(memberData.LevelCost);
        currentCoins.text = data.currentCoins.ToString();
    }

    private void LoadLevelEnemies(int level) {
        List<GameObject> added = new List<GameObject>();
        GameObject[] loaded = Resources.LoadAll<GameObject>("enemyprefabs/lvl" + level.ToString());
        foreach (GameObject item in loaded)
            added.Add(item);
        validEnemies.Add(added);
	}

    private void LevelUp() {
        data.LevelUp();
        validEnemies.RemoveAt(0);
        LoadLevelEnemies(data.currentFloor + 1);
        xpBar.fillAmount = (float)data.xp / data.XP_TO_LEVEL[data.currentFloor - 1];
    }
}
