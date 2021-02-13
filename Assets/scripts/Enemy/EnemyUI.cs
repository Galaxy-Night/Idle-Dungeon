using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Utility;

public class EnemyUI : MonoBehaviour
{
    public GameObject TapDamagePrefab;
    [SerializeField]
    private GameObject sprite;
    [SerializeField]
    private Text enemyName;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Text xpValue;
    [SerializeField]
    private Text coinValue;

    private List<GameObject> tapDamageObjects;

    // Start is called before the first frame update
    void Start()
    {
        tapDamageObjects = new List<GameObject>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyUIInitialize(string _enemyName, int _xpValue, int _coinValue, int level) {
        //Generate location where sprite is saved
        string spriteLocation = "enemy/lvl" + level.ToString() + "/" + StringManip.StripWhitespace(_enemyName.ToLower());
        Debug.Log(spriteLocation);

        enemyName.text = _enemyName;
        xpValue.text = _xpValue.ToString();
        coinValue.text = _coinValue.ToString();

        sprite.GetComponent<Image>().sprite = Resources.Load<Sprite>(spriteLocation);
	}

    public void ChangeEnemyHPBar(float fill) {
        healthBar.fillAmount = fill;
    }

    public void ShowDamage(int amount) {
        GameObject newTapDamage = Instantiate(TapDamagePrefab, GameObject.Find("Canvas").GetComponent<Transform>());
        newTapDamage.GetComponent<Text>().text = amount.ToString();
        //sets the position of newTapDamage to the location that the user clicked
        newTapDamage.transform.position = GameObject.Find("game_handler").GetComponent<EnemyClick>().clickPosition;
        tapDamageObjects.Add(newTapDamage);
	}
}
