using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMember : MonoBehaviour
{
    public int currentHealth;
    private GameObject unlockButton;
    private GameObject levelDisplay;
    private GameObject levelUpButton;
    public SpriteRenderer renderer;
    public Sprite locked;
    public Sprite normal;
    public Sprite unconcious;
    private int startingDamage;
    public Text nameField;
    public Image hpBar;
    private int startingHealth;
    public string memberName;
    public int maxHealth;
    public int damage;
    public int currentLevel;
    public float levelUpMultiplier;
    public int healCost;
    public int unlockCost;
    public bool isUnlocked;
    public bool isVisible;
    public bool isUnconcious;

    // Start is called before the first frame update
    void Start()
    {
		unlockButton = transform.Find("unlock_cost").gameObject;
        levelDisplay = transform.Find("member_level").gameObject;
        levelUpButton = transform.Find("level_cost").gameObject;
        nameField = transform.Find("member_name").GetComponent<Text>();
        hpBar = transform.Find("member_health_bar").GetComponent<Image>();
        unlockButton.GetComponent<Text>().text = unlockCost.ToString();
        renderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        renderer.sprite = locked;
        nameField.text = "?????";
        startingDamage = damage;
        startingHealth = maxHealth;
        isUnlocked = false;
        isVisible = false;
        isUnconcious = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UnlockMessage() {
        SendMessageUpwards("UnlockPartyMember", this);
	}

    void LevelUpMessage() {
        SendMessageUpwards("LevelUpPartyMember", this);
	}

    public void Unlock() {
        isUnlocked = true;
        renderer.sprite = normal;
        Destroy(unlockButton);
        levelDisplay.SetActive(true);
        levelUpButton.SetActive(true);
        currentLevel = 1;
        unlockCost = (int)(unlockCost * levelUpMultiplier);
        UpdateDisplay();
	}

    public void LevelUp() {
        currentLevel++;
        damage += startingDamage;
        maxHealth += startingHealth;
        unlockCost = (int)(unlockCost * levelUpMultiplier);
        UpdateDisplay();
    }

    private void UpdateDisplay() {
        levelDisplay.GetComponent<Text>().text = currentLevel.ToString();
        levelUpButton.GetComponent<Text>().text = unlockCost.ToString();
    }

    public void UpdateHPBar() {
        hpBar.fillAmount = currentHealth / (float)maxHealth;
	}

    public void KnockOut() {
        isUnconcious = true;
        renderer.sprite = unconcious;
	}
}
