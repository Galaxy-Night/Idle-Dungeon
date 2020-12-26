/// <summary><c>Enemy</c> stores the information needed for the 
/// player to interact with an enemy</summary>
public class Enemy
{
	public int currentHealth;
	public int maxHealth;
	
	/// <summary>
	/// The constructor for Enemy
	/// </summary>
	/// <param name="_maxHealth">The enemy's maximum health</param>
	public Enemy(int _maxHealth) {
		currentHealth = _maxHealth;
		maxHealth = _maxHealth;
	}

	/// <summary>
	/// Decrememnts the enemy's health to represent damage it has taken in
	/// response to an attack
	/// </summary>
	/// <param name="_amount">The amount of damage the enemy takes</param>
	/// <returns>True if the enemy's health dropped below zero, otherwise false
	/// </returns>
	public bool TakeDamage(int _amount) {
		currentHealth -= _amount;
		if (currentHealth <= 0) {
			currentHealth = 0;
			return true;
		}
		else
			return false;
	}
}
