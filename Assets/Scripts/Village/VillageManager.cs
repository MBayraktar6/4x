using UnityEngine;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour
{
    private PlayerData playerData;
    private Dictionary<int, VillageData> villagesById = new Dictionary<int, VillageData>();
    private List<Building> activeBuildings = new List<Building>();

    public void Initialize(PlayerData data)
    {
        playerData = data;
        Debug.Log("[VillageManager] Köyler başlatılıyor...");

        foreach (var village in playerData.villages)
        {
            villagesById[village.villageId] = village;
        }
    }

    public VillageData CreateNewVillage(string name, int mapX, int mapY)
    {
        if (playerData.villages.Count >= 5) // Max 5 köy
        {
            Debug.LogWarning("[VillageManager] Maximum köy sayısına ulaştınız!");
            return null;
        }

        VillageData newVillage = new VillageData();
        newVillage.Initialize(name, playerData.villages.Count, mapX, mapY);
        playerData.villages.Add(newVillage);
        villagesById[newVillage.villageId] = newVillage;

        Debug.Log($"[VillageManager] Yeni köy oluşturuldu: {name}");
        return newVillage;
    }

    public bool UpgradeBuilding(int villageId, int buildingId)
    {
        if (!villagesById.TryGetValue(villageId, out var village))
            return false;

        var building = village.buildings.Find(b => b.buildingId == buildingId);
        if (building == null)
            return false;

        BuildingConfig config = GetBuildingConfig(building.buildingType);
        if (config == null)
            return false;

        // Kaynak kontrolü
        long upgradeCost = config.GetUpgradeCost(building.level);
        if (playerData.resources.gold < upgradeCost)
            return false;

        playerData.RemoveResource(ResourceType.Gold, upgradeCost);
        building.level++;
        building.lastUpgradeTime = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        Debug.Log($"[VillageManager] Bina yükseltildi: {building.buildingType} Level {building.level}");
        return true;
    }

    public Building ConstructBuilding(int villageId, BuildingType type, int gridX, int gridY)
    {
        if (!villagesById.TryGetValue(villageId, out var village))
            return null;

        BuildingConfig config = GetBuildingConfig(type);
        if (config == null)
            return null;

        Building newBuilding = new Building()
        {
            buildingId = village.buildings.Count,
            buildingType = type,
            level = 1,
            gridX = gridX,
            gridY = gridY,
            constructionStartTime = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            constructionDuration = config.constructionTimeSeconds
        };

        // Kaynak kontrolü
        if (!playerData.RemoveResource(ResourceType.Gold, config.goldCost))
            return null;
        if (!playerData.RemoveResource(ResourceType.Wood, config.woodCost))
        {
            playerData.AddResource(ResourceType.Gold, config.goldCost);
            return null;
        }

        village.buildings.Add(newBuilding);
        activeBuildings.Add(newBuilding);

        Debug.Log($"[VillageManager] Bina inşaası başlandı: {type}");
        return newBuilding;
    }

    public VillageData GetVillage(int villageId)
    {
        villagesById.TryGetValue(villageId, out var village);
        return village;
    }

    private BuildingConfig GetBuildingConfig(BuildingType type)
    {
        // Yapılandırma veritabanından döndür
        return BuildingDatabase.GetConfig(type);
    }

    private void Update()
    {
        UpdateConstructions();
    }

    private void UpdateConstructions()
    {
        long currentTime = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        foreach (var building in activeBuildings)
        {
            if (building.IsConstructionComplete(currentTime))
            {
                Debug.Log($"[VillageManager] İnşaa tamamlandı: {building.buildingType}");
            }
        }
    }
}
