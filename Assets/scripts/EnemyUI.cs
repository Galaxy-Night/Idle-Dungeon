using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
	[SerializeField]
	private Button enemyButton;
	[SerializeField]
	private Image enemyHP;
	[SerializeField]
	private Text coinDrop;
	[SerializeField]
	private Text xpDrop;

	private Game gameHandler;

	void Start()
	{
		gameHandler = GameObject.Find("game_handler").GetComponent<Game>();
		EnemyData data = GetComponent<EnemyData>();
		coinDrop.text = data.coinDrop.ToString();
		xpDrop.text = data.xpDrop.ToString();
	}

	public void OnEnemyClick() {
		GetComponent<EnemyData>().currentHealth -= gameHandler.GetComponent<Game>().data.tapDamage;
		enemyHP.fillAmount = (float)GetComponent<EnemyData>().currentHealth / GetComponent<EnemyData>().MAX_HEALTH;
		if (GetComponent<EnemyData>().currentHealth <= 0)
			HandleDeath(GetComponent<EnemyData>(), gameHandler.GetComponent<Game>());
	}

	public void HandleDeath(EnemyData data, Game game) {
		game.HandleEnemyDeath(data.coinDrop, data.xpDrop);
		Destroy(gameObject);
	}
}
