using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <c>PartyMemberUI</c> is a class to handle the display of a party member's information to the
/// player
/// </summary>
//TODO: move sprite information here or to the handler class
public class PartyMemberUI : MonoBehaviour
{
	[SerializeField]
	private GameObject memberName;
	[SerializeField]
	private Button unlockButton;
	[SerializeField]
	private Text unlockCost;
	[SerializeField]
	private GameObject sprite;
	[SerializeField]
	private GameObject levelButton;
	[SerializeField]
	private GameObject currentLevel;
	[SerializeField]
	private Image hpBar;
	[SerializeField]
	private GameObject healButton;
	[SerializeField]
	private Text healCost;
	[SerializeField]
	private GameObject healLabel;

	private Sprite locked;
	private Sprite active;
	private Sprite dead;

	private void Start()
	{
		
	}

	/// <summary>
	/// Because Unity does not allow constructors to be called for objects that inherit from
	/// <c>MonoBehavior</c>, <c>initialize</c> sets the ui data to its initial values
	/// </summary>
	/// <param name="_unlockCost">The cost to unlock the character</param>
	/// <param name="_sprite">The sprite that should be displayed when the character is first
	/// rendered</param>
	/// <param name="_name">The name of the character</param>
	public void initialize(int _unlockCost, string _name) {
		//generate locations of sprites
		string activeLocation = "partymembers/" + _name.ToLower();
		string lockedLocation = activeLocation + "_blackout";
		string deadLocation = activeLocation + "_grey";

		active = Resources.Load<Sprite>(activeLocation);
		locked = Resources.Load<Sprite>(lockedLocation);
		dead = Resources.Load<Sprite>(deadLocation);

		unlockCost.text = _unlockCost.ToString();
		sprite.GetComponent<Image>().sprite = locked;
		memberName.GetComponent<Text>().text = _name;
	}

	/// <summary>
	/// <c>onUnlockClick</c> runs when the unlock button is clicked
	/// </summary>
	public void onUnlockClick() {
		FindHandler(name).onUnlockClick();
	}

	/// <summary>
	/// <c>onLevelClick</c> runs when the player character gains a level
	/// </summary>
	public void onLevelClick() {
		FindHandler(name).OnLevelClick();
	}

	public void onHealClick() {
		FindHandler(name).OnHealClick();
	}
	/// <summary>
	/// <c>Unlock</c> handles the UI changes that occur when a character is first unlocked
	/// </summary>
	/// <param name="_newSprite">The updated sprite</param>
	/// <param name="_newCost">How much it costs the character to advance to the next level</param>
	public void Unlock(int _newCost) {
		Destroy(unlockButton.gameObject);
		sprite.GetComponent<Image>().sprite = active;
		levelButton.SetActive(true);
		currentLevel.SetActive(true);
		LevelUp(1, _newCost);
	}

	/// <summary>
	/// <c>LevelUp</c> handles the UI changes that occur when a character is leveled up
	/// </summary>
	/// <param name="_newLevel">The character's new level</param>
	/// <param name="_newCost">How much it costs the character to advance to the next level</param>
	public void LevelUp(int _newLevel, int _newCost) {
		Text levelText = levelButton.transform.GetChild(0).GetComponent<Text>();
		currentLevel.GetComponent<Text>().text = _newLevel.ToString();
		levelText.text = _newCost.ToString();
	}

	/// <summary>
	/// <c>ChangeHPBar</c> changes the fill value of the HP bar
	/// </summary>
	/// <param name="_fill">How far the hp bar should be filled</param>
	public void ChangeHPBar(float _fill) {
		hpBar.fillAmount = _fill;
	}

	public void UpdateHealCost(int cost) {
		healCost.text = cost.ToString();
	}

	public void Injure() {
		memberName.SetActive(false);
		healButton.SetActive(true);
		healLabel.SetActive(true);
		healLabel.GetComponent<Text>().text = "Heal";
	}

	public void Kill() {
		memberName.SetActive(false);
		healButton.SetActive(true);
		healLabel.SetActive(true);
		healLabel.GetComponent<Text>().text = "Revive";
		sprite.GetComponent<Image>().sprite = dead;
	}

	public void Heal() {
		memberName.SetActive(true);
		healButton.SetActive(false);
		healLabel.SetActive(false);
		sprite.GetComponent<Image>().sprite = active;
	}

	private static PartyMemberHandler FindHandler(string name) {
		GameObject temp = GameObject.Find(name.Substring(0, name.Length - 3));
		return temp.GetComponent<PartyMemberHandler>();
	}
}
