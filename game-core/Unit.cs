public abstract class Unit
{
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Energy { get; set; }
    public float MaxEnergy { get; set; }
    public int Age { get; set; }

    protected Unit(int x, int y, float maxHealth, float maxEnergy)
    {
        X = x;
        Y = y;
        MaxHealth = maxHealth;
        Health = maxHealth;
        MaxEnergy = maxEnergy;
        Energy = maxEnergy;
        Age = 0;
    }

    public virtual void Update()
    {
        Age++;
        Energy = Mathf.Max(0, Energy - 0.5f);
        Health = Mathf.Max(0, Health - 0.1f);
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public bool IsAlive => Health > 0;
}
