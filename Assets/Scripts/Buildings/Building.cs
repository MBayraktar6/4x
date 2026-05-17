using UnityEngine;
using System.Collections.Generic;

public class Building : MonoBehaviour
{
    [System.Serializable]
    public class BuildingData
    {
        public string buildingId;
        public string buildingName;
        public int level = 1;
        public float buildTime = 60f;
        public float buildProgress = 0f;
        public bool isBuilding = false;
        public Vector3 position;
        public BuildingType buildingType;
    }

    [System.Serializable]
    public class ResourceCost
    {
        public long gold;
        public long wood;
        public long stone;
        public long food;
        public long iron;
    }

    public BuildingData buildingData;
    public ResourceCost buildCost;
    public ResourceCost productionPerHour;

    [Header("Visual")]
    public SpriteRenderer spriteRenderer;
    public Canvas buildingCanvas;
    public Color buildingColor = Color.white;
    public Color buildingColorDamaged = Color.red;

    private float productionTimer = 0f;
    private const float PRODUCTION_INTERVAL = 3600f; // 1 hour in seconds

    public enum BuildingType
    {
        TownHall,
        Barracks,
        ResourceStorage,
        Farm,
        Lumbermill,
        StoneMine,
        IronMine,
        Wall,
        Tower,
        Laboratory,
        Market,
        Tavern
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (buildingCanvas == null)
            buildingCanvas = GetComponentInChildren<Canvas>();
    }

    private void Update()
    {
        if (buildingData.isBuilding)
        {
            UpdateBuildProgress();
        }
        else
        {
            UpdateProduction();
        }
    }

    private void UpdateBuildProgress()
    {
        buildingData.buildProgress += Time.deltaTime;

        if (buildingData.buildProgress >= buildingData.buildTime)
        {
            CompleteBuild();
        }
    }

    private void CompleteBuild()
    {
        buildingData.isBuilding = false;
        buildingData.buildProgress = 0f;
        spriteRenderer.color = buildingColor;
        Debug.Log(buildingData.buildingName + " building completed!");
    }

    private void UpdateProduction()
    {
        productionTimer += Time.deltaTime;

        if (productionTimer >= PRODUCTION_INTERVAL)
        {
            ProduceResources();
            productionTimer = 0f;
        }
    }

    private void ProduceResources()
    {
        PlayerData playerData = GameManager.Instance.playerData;

        if (productionPerHour.gold > 0)
            playerData.AddResources("gold", productionPerHour.gold);
        if (productionPerHour.wood > 0)
            playerData.AddResources("wood", productionPerHour.wood);
        if (productionPerHour.stone > 0)
            playerData.AddResources("stone", productionPerHour.stone);
        if (productionPerHour.food > 0)
            playerData.AddResources("food", productionPerHour.food);
        if (productionPerHour.iron > 0)
            playerData.AddResources("iron", productionPerHour.iron);
    }

    public bool StartBuilding(ResourceCost cost)
    {
        PlayerData playerData = GameManager.Instance.playerData;

        // Check resources
        if (playerData.resources.gold < cost.gold ||
            playerData.resources.wood < cost.wood ||
            playerData.resources.stone < cost.stone ||
            playerData.resources.food < cost.food ||
            playerData.resources.iron < cost.iron)
        {
            return false;
        }

        // Deduct resources
        playerData.RemoveResources("gold", cost.gold);
        playerData.RemoveResources("wood", cost.wood);
        playerData.RemoveResources("stone", cost.stone);
        playerData.RemoveResources("food", cost.food);
        playerData.RemoveResources("iron", cost.iron);

        // Start building
        buildingData.isBuilding = true;
        buildingData.buildProgress = 0f;
        spriteRenderer.color = buildingColorDamaged;

        return true;
    }

    public float GetBuildProgressPercent()
    {
        return (buildingData.buildProgress / buildingData.buildTime) * 100f;
    }

    public void UpgradeBuilding()
    {
        buildingData.level++;
        buildingData.buildTime *= 1.2f; // Build time increases 20% per level
    }
}
