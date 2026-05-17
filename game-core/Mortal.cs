using UnityEngine;

public class Mortal : Unit
{
    public string Profession { get; set; } // Farmer, Soldier, Builder, etc.
    public float Productivity { get; set; }
    public int ResourcesProduced { get; set; }

    public Mortal(int x, int y, string profession) 
        : base(x, y, 50f, 30f, UnitType.Mortal)
    {
        Profession = profession;
        Productivity = 1f;
        ResourcesProduced = 0;
        Id = Random.Range(100000, 999999);
    }

    public override void Update()
    {
        base.Update();
        
        if (IsAlive && Divine > 5f)
        {
            ProduceResources();
        }
    }

    private void ProduceResources()
    {
        int production = (int)(Productivity * 5f);
        ResourcesProduced += production;
        Divine -= 3f;
    }

    public void Work(Immortal god)
    {
        if (Divine > 10f)
        {
            Productivity += 0.1f;
            god.Followers++;
        }
    }
}
