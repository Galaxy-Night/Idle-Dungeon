using UnityEngine;

public class Character
{
	int maxHealth;
	int currentHealth;
	int damage;
	Sprite sprite;

	public Character(int _maxHealth, int _damage) {
		maxHealth = _maxHealth;
		currentHealth = _maxHealth;
		damage = _damage;
	}

	public bool TakeDamage(int _damage) {
		currentHealth -= _damage;
		//checks if victim has died
		if (currentHealth <= 0)
			return true;
		else
			return false;
	}
}
