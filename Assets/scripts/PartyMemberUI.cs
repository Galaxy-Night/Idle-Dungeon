using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberUI : MonoBehaviour
{
	[SerializeField]
	private Text memberName;
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

	private void Start()
	{
		
	}

	public void initialize(int _unlockCost, Sprite _sprite, string _name) {
		unlockCost.text = _unlockCost.ToString();
		sprite.GetComponent<Image>().sprite = _sprite;
		memberName.GetComponent<Text>().text = _name;
	}

	public void onUnlockClick() {
		GameObject temp = GameObject.Find("member");
		PartyMemberHandler handler = temp.GetComponent<PartyMemberHandler>();
		handler.onUnlockClick();
	}

	public void onLevelClick() {
		Debug.Log("Level Up!");
	}

	public void Unlock(Sprite _newSprite, int _newCost) {
		Destroy(unlockButton.gameObject);
		sprite.GetComponent<Image>().sprite = _newSprite;
		levelButton.SetActive(true);
		currentLevel.SetActive(true);
		LevelUp(1, _newCost);
	}

	public void LevelUp(int _newLevel, int _newCost) {
		Text levelText = levelButton.transform.GetChild(0).GetComponent<Text>();
		currentLevel.GetComponent<Text>().text = _newLevel.ToString();
		levelText.text = _newCost.ToString();
	}

	public void ChangeHPBar(float _fill) {
		hpBar.fillAmount = _fill;
	}
}
