using UnityEngine;

public class Immortal : Unit
{
    public string DivineType { get; set; } // Zeus, Ares, Aphrodite, vb.
    public float Dominance { get; set; }
    public int Followers { get; set; }

    public Immortal(int x, int y, string divineType) 
        : base(x, y, 200f, 150f, UnitType.Immortal)
    {
        DivineType = divineType;
        Dominance = 100f;
        Followers = 20;
        Id = Random.Range(1000, 9999);
    }

    public override void Update()
    {
        base.Update();
        Dominance = Mathf.Max(0, Dominance - 0.2f);
        
        if (Divine > MaxDivine * 0.8f)
            Followers++;
    }

    public void Hunt(Mortal prey)
    {
        if (Divine > 10f && Vector2.Distance(new Vector2(X, Y), new Vector2(prey.X, prey.Y)) <= 2f)
        {
            prey.TakeDamage(50f);
            Divine = Mathf.Min(MaxDivine, Divine + 30f);
        }
    }

    public void ConvertFollower(Hero hero)
    {
        if (Dominance > hero.Loyalty)
        {
            hero.Loyalty -= 10f;
            Followers++;
        }
    }
}
