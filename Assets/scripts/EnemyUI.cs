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
	public EnemyData data { get; private set; }

	private bool isInitialized = false;

	void Start()
	{
		if (!isInitialized) {
			gameHandler = GameObject.Find("game_handler").GetComponent<Game>();
			data = GetComponent<EnemyData>();
			data.currentHealth = data.MAX_HEALTH;
		}

		coinDrop.text = data.coinDrop.ToString();
		xpDrop.text = data.xpDrop.ToString();
	}

	public void OnEnemyClick() {
		TakeDamage(gameHandler.GetComponent<Game>().data.tapDamage);
	}

	public void HandleDeath(EnemyData data, Game game) {
		game.HandleEnemyDeath(data.coinDrop, data.xpDrop);
		Destroy(gameObject);
	}

	public void TakeDamage(int amount) {
		GetComponent<EnemyData>().currentHealth -= amount;
		enemyHP.fillAmount = (float)GetComponent<EnemyData>().currentHealth / GetComponent<EnemyData>().MAX_HEALTH;
		if (data.currentHealth <= 0)
			HandleDeath(GetComponent<EnemyData>(), gameHandler.GetComponent<Game>());
	}

	public void ApplyData (EnemySave save) {
		data = GetComponent<EnemyData>();
		gameHandler = GameObject.Find("game_handler").GetComponent<Game>();

		data.MAX_HEALTH = save.maxHealth;
		data.currentHealth = save.currentHealth;
		data.coinDrop = save.coinDrop;
		data.xpDrop = save.xpDrop;
		data.damage = save.damage;
		data.level = save.level;

		enemyHP.fillAmount = (float)GetComponent<EnemyData>().currentHealth / GetComponent<EnemyData>().MAX_HEALTH;

		isInitialized = true;
	}
}
