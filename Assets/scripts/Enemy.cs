/// <summary><c>Enemy</c> stores the information needed for the 
/// player to interact with an enemy</summary>
public class Enemy : Character
{
	public int coinValue;
	/// <summary>
	/// The constructor for Enemy
	/// </summary>
	/// <param name="_maxHealth">The enemy's maximum health</param>
	public Enemy(int _maxHealth, string _name, int _coinValue) : 
	base(_maxHealth, _name) {
		coinValue = _coinValue;
	}
}
