using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <c>MemberRenderer</c> is a class to store the user interface belonging
/// to a party member. It primarily exists for ease of coding.
/// </summary>
public class MemberRenderer : MonoBehaviour
{
    private SpriteRenderer sRenderer;
    private GameObject healReviveCost;
    public Sprite locked;
    public Sprite normal;
    public Sprite unconcious;
    private GameObject nameField;
    public GameObject HealLabel { private get; set; }
    public Image hpBar;
    public string memberName;

    // Start is called before the first frame update
    void Start()
    {
        nameField = transform.Find("member_name").gameObject;
        HealLabel = transform.Find("heal_label").gameObject;
        healReviveCost = transform.Find("heal_revive_cost").gameObject;
        hpBar = transform.Find("member_health_bar").GetComponent<Image>();
        sRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        nameField.GetComponent<Text>().text = "?????";
        sRenderer.sprite = locked;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// <c>RenderUnlock</c> is a function that changes the appropriate values
    /// in the <c>MemberRenderer</c> to signal that a character has been 
    /// unlocked
    /// </summary>
    public void RenderUnlock()
    {
        sRenderer.sprite = normal;
        nameField.GetComponent<Text>().text = memberName;
    }

    /// <summary>
    /// <c>RenderKnockOut</c> is a function that changes the sprite stored in
    /// the <c>MemberRenderer</c> class to show that the party member has 
    /// fallen unconcious
    /// </summary>
    public void RenderKnockOut(int healCost)
    {
        sRenderer.sprite = unconcious;
        HealLabel.GetComponent<Text>().text = "Revive:";
        healReviveCost.GetComponent<Text>().text = healCost.ToString();
    }

    public void RenderInjury(int healCost) {
        HealLabel.GetComponent<Text>().text = "Heal:";
        nameField.SetActive(false);
        HealLabel.SetActive(true);
        healReviveCost.SetActive(true);
        healReviveCost.GetComponent<Text>().text = healCost.ToString();
	}

    public void UpdateInjury(int healCost) {
        healReviveCost.GetComponent<Text>().text = healCost.ToString();
    }

    public void RenderHeal() {
        HealLabel.GetComponent<Text>().text = "";
        healReviveCost.GetComponent<Text>().text = "";
        nameField.SetActive(true);
        HealLabel.SetActive(false);
        healReviveCost.SetActive(false);
	}

    public void RenderRevive() {
        HealLabel.GetComponent<Text>().text = "";
        healReviveCost.GetComponent<Text>().text = "";
        nameField.SetActive(true);
        HealLabel.SetActive(false);
        healReviveCost.SetActive(false);
        sRenderer.sprite = normal;
	}
}
