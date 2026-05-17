using UnityEngine;

public class Hero : Unit
{
    public string HeroName { get; set; }
    public string PatronGod { get; set; }
    public float Loyalty { get; set; }
    public int Level { get; set; }
    public float Experience { get; set; }

    public Hero(int x, int y, string name, string patronGod) 
        : base(x, y, 120f, 80f, UnitType.Hero)
    {
        HeroName = name;
        PatronGod = patronGod;
        Loyalty = 100f;
        Level = 1;
        Experience = 0f;
        Id = Random.Range(10000, 99999);
    }

    public override void Update()
    {
        base.Update();
        
        if (Loyalty < 30f)
            Level--;
    }

    public void GainExperience(float exp)
    {
        Experience += exp;
        if (Experience >= 100f)
        {
            Level++;
            Experience = 0f;
            MaxHealth += 10f;
        }
    }

    public void ServeLoyalty(Immortal god)
    {
        if (god.DivineType == PatronGod)
        {
            Loyalty = Mathf.Min(100f, Loyalty + 5f);
        }
    }
}
