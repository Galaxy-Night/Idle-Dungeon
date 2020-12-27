using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Render : MonoBehaviour
{
    public Image enemyHealthBar;
    public Text coinDisplay;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealthBar = GameObject.Find("enemy_health").GetComponent<Image>();
        coinDisplay = GameObject.Find("current_coins").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
