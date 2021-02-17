using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public EnemyData(EnemyData data) {
		EnemyName = data.EnemyName;
		MaxHealth = data.MaxHealth;
		CoinValue = data.CoinValue;
		XpValue = data.XpValue;
		Damage = data.Damage;
		DamageInterval = data.DamageInterval;
		Level = data.Level;

		CurrentHealth = MaxHealth;
	}

	public int TakeDamage(int amount) {
		CurrentHealth -= amount;
		if (CurrentHealth > 0)
			return 0;
		else
			return GameData.DEATH_INDICATOR;
	}
}
