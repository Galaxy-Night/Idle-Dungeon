public class Enemy
{
	int currentHealth;
	int maxHealth;
	
	public Enemy(int _maxHealth) {
		currentHealth = _maxHealth;
		maxHealth = _maxHealth;
	}

	public bool TakeDamage(int _amount) {
		currentHealth -= _amount;
		if (currentHealth <= 0)
			return true;
		else
			return false;
	}
}
