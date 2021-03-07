public class GameData
{
    public readonly int[] XP_TO_LEVEL = { 300, 600, 1800, 3600, 7200 };
    public int tapDamage { get; private set; }
    public int currentCoins { get; private set; }
    public int xp { get; private set; }
    public int currentFloor { get; private set; }

    public GameData() {
        tapDamage = 1;
        currentFloor = 1;
        xp = 0;
        currentCoins = 0;
    }

    public void HandleEnemyDeath(int coinDrop, int xpDrop) {
        currentCoins += coinDrop;
        xp += xpDrop;
    }

    private void LoadEnemy() {
          
	}
}
