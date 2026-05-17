public abstract class Unit
{
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Divine { get; set; }
    public float MaxDivine { get; set; }
    public int Age { get; set; }
    public UnitType Type { get; set; }

    protected Unit(int x, int y, float maxHealth, float maxDivine, UnitType type)
    {
        X = x;
        Y = y;
        MaxHealth = maxHealth;
        Health = maxHealth;
        MaxDivine = maxDivine;
        Divine = maxDivine;
        Age = 0;
        Type = type;
    }

    public virtual void Update()
    {
        Age++;
        Divine = Mathf.Max(0, Divine - 0.3f);
        Health = Mathf.Max(0, Health - 0.1f);
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public bool IsAlive => Health > 0;
}

public enum UnitType
{
    Immortal,  // Tanrılar
    Hero,      // İnsan Kahramanlar
    Mortal     // Sıradan İnsanlar
}
