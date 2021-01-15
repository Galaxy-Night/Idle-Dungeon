using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <c>PartyMember</c> is a class to handle the functionality of the characters
/// the player is able to hire.
/// </summary>
public class PartyMember : MonoBehaviour
{
    public int currentHealth;
    public MemberRenderer mRenderer;
    private GameObject unlockButton;
    private GameObject levelDisplay;
    private GameObject levelUpButton;
    private int startingDamage;
    private int startingHealth;
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
        mRenderer = GetComponent<MemberRenderer>();
		unlockButton = transform.Find("unlock_cost").gameObject;
        levelDisplay = transform.Find("member_level").gameObject;
        levelUpButton = transform.Find("level_cost").gameObject;
        unlockButton.GetComponent<Text>().text = unlockCost.ToString();
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

    /// <summary>
    /// <c>UnlockMessage</c> is a function that exists to pass the 
    /// <c>PartyMember</c> object to the <c>UnlockPartyMember</c> function in
    /// <c>GameHandler</c>
    /// </summary>
    void UnlockMessage() {
        SendMessageUpwards("UnlockPartyMember", this);
	}

    /// <summary>
    /// <c>LevelUpMessage</c> is a function that exists to pass the 
    /// <c>PartyMember</c> object to the <c>LevelUpPartyMember</c> function in
    /// <c>GameHandler</c>
    /// </summary>
    void LevelUpMessage() {
        SendMessageUpwards("LevelUpPartyMember", this);
	}

    /// <summary>
    /// <c>Unlock</c> handles the changes that occur when the player unlocks a 
    /// new party member. It sets the current level, displays the member's 
    /// level and the cost to level them up,and calculates the cost to level up
    /// the party member.
    /// </summary>
    public void Unlock() {
        isUnlocked = true;
        mRenderer.RenderUnlock();
        Destroy(unlockButton);
        levelDisplay.SetActive(true);
        levelUpButton.SetActive(true);
        currentLevel = 1;
        unlockCost = (int)(unlockCost * levelUpMultiplier);
        UpdateDisplay();
	}

    /// <summary>
    /// <c>LevelUp</c> is a function to handle the changes that occur when the
    /// player levels up a member of the party. It increases the party member's 
    /// stats and calculates the new cost to unlock the party member.
    /// </summary>
    public void LevelUp() {
        currentLevel++;
        damage += startingDamage;
        maxHealth += startingHealth;
        unlockCost = (int)(unlockCost * levelUpMultiplier);
        UpdateDisplay();
    }

    /// <summary>
    /// <c>UpdateDisplay</c> updates the appropriate parts of the party 
    /// member's UI when the user unlocks them or levels them up
    /// </summary>
    private void UpdateDisplay() {
        levelDisplay.GetComponent<Text>().text = currentLevel.ToString();
        levelUpButton.GetComponent<Text>().text = unlockCost.ToString();
    }

    /// <summary>
    /// <c>UpdateHPBar</c> is a function that exists to be called by the
    /// <c>GameHandler</c> whenever the <c>PartyMember's</c> hp value needs to
    /// be updated, so the HP bar's length represents the portion of their 
    /// maximum health the character has remaining.
    /// </summary>
    public void UpdateHPBar() {
        mRenderer.hpBar.fillAmount = currentHealth / (float)maxHealth;
	}

    /// <summary>
    /// <c>KnockOut</c> is a function that exists to be called by the
    /// <c>GameHandler</c> object when a party member's health drops below zero
    /// </summary>
    public void KnockOut() {
        isUnconcious = true;
        mRenderer.RenderKnockOut();
	}
}
