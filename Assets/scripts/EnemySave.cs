public class EnemySave
{
	public string name;
	public int maxHealth;
	public int currentHealth;
	public int coinDrop;
	public int xpDrop;
	public int damage;

	static public EnemySave init(EnemyUI enemy) {
		EnemySave returned = new EnemySave();
		returned.name = enemy.name.Substring(0, enemy.name.Length - 7);
		returned.maxHealth = enemy.data.MAX_HEALTH;
		returned.currentHealth = enemy.data.currentHealth;
		returned.coinDrop = enemy.data.coinDrop;
		returned.xpDrop = enemy.data.xpDrop;
		returned.damage = enemy.data.damage;
		return returned;
	}
}
