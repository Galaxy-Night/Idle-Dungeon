public class PartyMemberSave
{
    public string name;
    public int MaxHealth;
    public int Damage;
    public int CurrentLevel;
    public int LevelCost;
    public int CurrentHealth;
    public bool IsInjured;
    public int HealCost;
    public bool IsDead;

    static public PartyMemberSave init(PartyMemberData data) {
        PartyMemberSave returned = new PartyMemberSave();
        returned.name = data.name.Substring(0, data.name.Length - 7);
        returned.MaxHealth = data.MaxHealth;
        returned.Damage = data.Damage;
        returned.CurrentLevel = data.CurrentLevel;
        returned.LevelCost = data.LevelCost;
        returned.CurrentHealth = data.CurrentHealth;
        returned.IsInjured = data.IsInjured;
        returned.HealCost = data.HealCost;
        returned.IsDead = data.IsDead;
        return returned;
	}
}
