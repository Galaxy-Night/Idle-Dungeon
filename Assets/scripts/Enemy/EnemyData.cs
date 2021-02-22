using System;

[Serializable]
public class EnemyData
{
	public readonly string EnemyName;
	public readonly int MaxHealth;
	public readonly int CoinValue;
	public readonly int XpValue;
	public readonly int Damage;
	public readonly int DamageInterval;
	public readonly int Level;
	public int CurrentHealth { get; private set; }

	/// <summary>
	/// The constructor for <c>EnemyData</c>
	/// </summary>
	/// <param name="enemyName">The name of the enemy</param>
	/// <param name="maxHealth">The enemy's maximum health</param>
	/// <param name="coinValue">The number of coins paid out when the enemy dies</param>
	/// <param name="xpValue">The number of experience points killing the enemy grants</param>
	/// <param name="damage">The amount of damage the enemy deals every second</param>
	/// <param name="damageInterval">Not used and needs to be removed</param>
	/// <param name="level">The level of the dungeon the enemy is encountered on</param>
	public EnemyData(string enemyName, int maxHealth, int coinValue, int xpValue, int damage, int damageInterval, int level) {
		EnemyName = enemyName;
		MaxHealth = maxHealth;
		CoinValue = coinValue;
		XpValue = xpValue;
		Damage = damage;
		DamageInterval = damageInterval;
		Level = level;

		CurrentHealth = MaxHealth;
	}

	/// <summary>
	/// The copy constructor for enemy data
	/// </summary>
	/// <param name="data">The enemy data object to be copied</param>
	public EnemyData(EnemyData data) {
		EnemyName = data.EnemyName;
		MaxHealth = data.MaxHealth;
		CoinValue = data.CoinValue;
		XpValue = data.XpValue;
		Damage = data.Damage;
		DamageInterval = data.DamageInterval;
		Level = data.Level;

		CurrentHealth = data.CurrentHealth;
	}

	/// <summary>
	/// Handles the logic when an enemy takes damage
	/// </summary>
	/// <param name="amount">The amount of damage the enemy took</param>
	/// <returns><c>DEATH_INDICATOR</c> if the enemy's resulting hp is 0 or lower, otherwise 0</returns>
	public int TakeDamage(int amount) {
		CurrentHealth -= amount;
		if (CurrentHealth > 0)
			return 0;
		else
			return GameData.DEATH_INDICATOR;
	}
}
