using System.Collections.Generic;

public class ResourceManager
{
    public int Food { get; set; }
    public int Wood { get; set; }
    public int Stone { get; set; }
    public int Gold { get; set; }

    public int MaxFood { get; set; }
    public int MaxWood { get; set; }
    public int MaxStone { get; set; }
    public int MaxGold { get; set; }

    public ResourceManager()
    {
        Food = 100;
        Wood = 50;
        Stone = 30;
        Gold = 20;

        MaxFood = 500;
        MaxWood = 300;
        MaxStone = 200;
        MaxGold = 150;
    }

    public bool HasResources(int foodCost, int woodCost, int stoneCost, int goldCost)
    {
        return Food >= foodCost && Wood >= woodCost && Stone >= stoneCost && Gold >= goldCost;
    }

    public void ConsumeResources(int foodCost, int woodCost, int stoneCost, int goldCost)
    {
        if (HasResources(foodCost, woodCost, stoneCost, goldCost))
        {
            Food -= foodCost;
            Wood -= woodCost;
            Stone -= stoneCost;
            Gold -= goldCost;
        }
    }

    public void AddResources(int food, int wood, int stone, int gold)
    {
        Food = Mathf.Min(MaxFood, Food + food);
        Wood = Mathf.Min(MaxWood, Wood + wood);
        Stone = Mathf.Min(MaxStone, Stone + stone);
        Gold = Mathf.Min(MaxGold, Gold + gold);
    }

    public void Update()
    {
        // Natural resource degradation
        Food = Mathf.Max(0, Food - 2);
    }
}
