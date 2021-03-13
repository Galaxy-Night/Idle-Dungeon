using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private int partyMemberYOffset;
    private int partyMemberY;

    // Start is called before the first frame update
    void Start()
    {
        data = new GameData();
        LoadEnemy();
        partyMemberYOffset = -133;
        partyMemberY = partyMemberYOffset * -1;

        InvokeRepeating("DealAllPlayerAutoDamage", 1f, 1f);
        InvokeRepeating("DealEnemyAutoDamage", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        LoadEnemy();
	}

    public void LoadEnemy() {
        GameObject temp = (GameObject)Instantiate(Resources.Load("enemyprefabs/animated-shrub"), enemyUIParent);
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

            hpBar.fillAmount = (float)totalCurrentHealth / totalMaxHealth;
        }
    }

    public void HealRevivePartyMember(PartyMemberData memberData) {
        data.HealRevivePartyMember(memberData.HealCost);
        currentCoins.text = data.currentCoins.ToString();
    }
}
