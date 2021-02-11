using UnityEngine;

public class PartyMemberData : MonoBehaviour
{
    public static readonly int INJURED_INDICATOR = 1;
    public static readonly int DEATH_INDICATOR = 2;
    
    public string MemberName {  get { return memberName; } private set { memberName = value; } }
    public int MaxHealth { get { return maxHealth; } private set { maxHealth = value; } }
    public int UnlockCost { get { return unlockCost; } private set { unlockCost = value; } }
    public Sprite Locked { get { return locked; } private set { locked = value; } }
    public Sprite Active { get { return active; } private set { active = value;  } }

    public int CurrentLevel { get; private set; }
    public int LevelCost { get; private set; }
    public int CurrentHealth { get; private set; }

    [SerializeField]
    private string memberName;
    [SerializeField]
    private int unlockCost;
    [SerializeField]
    private Sprite locked;
    [SerializeField]
    private Sprite active;
    [SerializeField]
    private int costMultiplier;
    [SerializeField]
    private int maxHealth;

    void Start()
    {
        CurrentLevel = 0;
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock() {
        CurrentLevel++;
        LevelCost = UnlockCost * (int)Mathf.Pow(costMultiplier, CurrentLevel);
	}

    public int TakeDamage(int _amount) {
        CurrentHealth -= _amount;
        if (CurrentHealth <= MaxHealth / 2 && CurrentHealth > 0) {
            return INJURED_INDICATOR;
		}
        else if (CurrentHealth <= 0) {
            return DEATH_INDICATOR;
		}
        else {
            return 0;
		}
	}
}
