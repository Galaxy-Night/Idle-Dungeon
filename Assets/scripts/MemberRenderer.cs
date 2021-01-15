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
    public Sprite locked;
    public Sprite normal;
    public Sprite unconcious;
    private Text nameField;
    public Image hpBar;
    public string memberName;

    // Start is called before the first frame update
    void Start()
    {
        nameField = transform.Find("member_name").GetComponent<Text>();
        hpBar = transform.Find("member_health_bar").GetComponent<Image>();
        sRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        nameField.text = "?????";
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
        nameField.text = memberName;
    }

    /// <summary>
    /// <c>RenderKnockOut</c> is a function that changes the sprite stored in
    /// the <c>MemberRenderer</c> class to show that the party member has 
    /// fallen unconcious
    /// </summary>
    public void RenderKnockOut()
    {
        sRenderer.sprite = unconcious;
    }
}
