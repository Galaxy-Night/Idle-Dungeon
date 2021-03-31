using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;

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

    public List<Tuple<int, string>> charactersToUnlock;

    private int partyMemberYOffset;
    private int partyMemberY;

    // Start is called before the first frame update
    void Start()
    {
        data = new GameData();
        partyMemberYOffset = -133;
        partyMemberY = partyMemberYOffset * -1;
        validEnemies = new List<List<GameObject>>();
        charactersToUnlock = generateUnlockCosts();

        if (GameObject.Find("load-handler")) {
            Load();
		}

        LoadLevelEnemies(data.currentFloor - 1);
        LoadLevelEnemies(data.currentFloor);
        LoadLevelEnemies(data.currentFloor + 1);

        if (!GameObject.Find("load-handler"))
        {
            LoadEnemy();
        }

        InvokeRepeating("DealAllPlayerAutoDamage", 1f, 1f);
        InvokeRepeating("DealEnemyAutoDamage", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnApplicationQuit()
	{
        Save();
        Debug.Log("Quit Complete");
        Debug.Log(Application.persistentDataPath);
	}

	public void Pause() {
        CancelInvoke("DealAllPlayerAutoDamage");
        CancelInvoke("DealEnemyAutoDamage");
	}

    public void Unpause() {
        InvokeRepeating("DealAllPlayerAutoDamage", 1f, 1f);
        InvokeRepeating("DealEnemyAutoDamage", 1f, 1f);
    }

    private List<Tuple<int, string>> generateUnlockCosts()
    {
        List<Tuple<int, string>> returned = new List<Tuple<int, string>>();
        returned.Add(new Tuple<int, string>(10, "fighter"));
        returned.Add(new Tuple<int, string>(100, "archer"));
        returned.Add(new Tuple<int, string>(500, "wizard"));
        returned.Add(new Tuple<int, string>(1000, "cleric"));
        return returned;
    }

    public void HandleEnemyDeath(int coinDrop, int xpDrop) {
        data.HandleEnemyDeath((int)(coinDrop * data.coinMultiplier), (int)(xpDrop * data.xpMultiplier));
        currentCoins.text = data.currentCoins.ToString();
        xpBar.fillAmount = (float)data.xp / data.XP_TO_LEVEL[data.currentFloor - 1];
        if (charactersToUnlock.Count > 0) {
            if (charactersToUnlock[0].Item1 / 2 <= data.currentCoins)
            {
                GameObject newMember = (GameObject)Instantiate(Resources.Load("partymembers/" + charactersToUnlock[0].Item2), partyMemberUIParent);
                newMember.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, partyMemberY, 0);
                partyMemberY += partyMemberYOffset;
                charactersToUnlock.RemoveAt(0);
                data.visibleMembers++;
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
        int floor = UnityEngine.Random.Range(0, validEnemies.Count);
        Debug.Log(floor);
        int enemy = UnityEngine.Random.Range(0, validEnemies[floor].Count - 1);
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

    private void Save() {
        SaveData save = new SaveData();
        FileStream file = File.Create(Application.persistentDataPath + "/dungeonstate.save");
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));

        save.init(data, currentEnemy, data.unlockedPartyMembers);

        serializer.Serialize(file, save);
        file.Close();

        Debug.Log("Save Complete");
	}

    private void Load() {
        SaveData save = GameObject.Find("load-handler").GetComponent<Load>().saveData;

        GameObject temp = (GameObject)Instantiate(Resources.Load("enemyprefabs/lvl" + save.currentEnemy.level.ToString() + "/" + save.currentEnemy.name), enemyUIParent);
        currentEnemy = temp.GetComponent<EnemyUI>();
        currentEnemy.ApplyData(save.currentEnemy);

        if(save.partyMembers.Count > 0) {
            foreach (PartyMemberSave saveData in save.partyMembers) {
                GameObject newMember = (GameObject)Instantiate(Resources.Load("partymembers/" + saveData.name), partyMemberUIParent);
                newMember.GetComponent<PartyMemberUI>().Initialize(saveData);
                data.unlockedPartyMembers.Add(newMember.GetComponent<PartyMemberData>());
            }
		}

        data.Initialize(save);
        UIInitialize();
    }
    private void UIInitialize() {
        currentCoins.text = data.currentCoins.ToString();
        xpBar.fillAmount = (float)data.xp / data.XP_TO_LEVEL[data.currentFloor - 1];
    }
}
