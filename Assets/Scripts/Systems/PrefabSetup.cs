using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This script manages all prefab instantiation and setup.
/// Attach this to a GameObject to configure all prefabs in the game.
/// </summary>
public class PrefabSetup : MonoBehaviour
{
    [Header("Building Prefabs")]
    public GameObject townHallPrefab;
    public GameObject baracksPrefab;
    public GameObject farmPrefab;
    public GameObject lumbermillPrefab;
    public GameObject stoneMinePrefab;
    public GameObject ironMinePrefab;
    public GameObject wallPrefab;
    public GameObject towerPrefab;

    [Header("Unit Prefabs")]
    public GameObject warriorPrefab;
    public GameObject archerPrefab;
    public GameObject knightPrefab;
    public GameObject magePrefab;
    public GameObject scoutPrefab;
    public GameObject healerPrefab;

    [Header("UI Prefabs")]
    public GameObject buildMenuPrefab;
    public GameObject trainMenuPrefab;
    public GameObject clanMenuPrefab;
    public GameObject settingsMenuPrefab;
    public GameObject notificationPrefab;

    private void Awake()
    {
        RegisterAllPrefabs();
    }

    public void RegisterAllPrefabs()
    {
        BuildingManager buildingManager = GameManager.Instance.buildingManager;
        if (buildingManager != null)
        {
            RegisterBuildingPrefabs(buildingManager);
        }

        UnitManager unitManager = GameManager.Instance.unitManager;
        if (unitManager != null)
        {
            RegisterUnitPrefabs(unitManager);
        }
    }

    private void RegisterBuildingPrefabs(BuildingManager buildingManager)
    {
        Debug.Log("Registering building prefabs...");
        // This would register prefabs in the building manager's dictionary
        // Implementation depends on your BuildingManager structure
    }

    private void RegisterUnitPrefabs(UnitManager unitManager)
    {
        Debug.Log("Registering unit prefabs...");
        // This would register prefabs in the unit manager's dictionary
        // Implementation depends on your UnitManager structure
    }

    public GameObject GetBuildingPrefab(Building.BuildingType buildingType)
    {
        switch (buildingType)
        {
            case Building.BuildingType.TownHall:
                return townHallPrefab;
            case Building.BuildingType.Barracks:
                return baracksPrefab;
            case Building.BuildingType.Farm:
                return farmPrefab;
            case Building.BuildingType.Lumbermill:
                return lumbermillPrefab;
            case Building.BuildingType.StoneMine:
                return stoneMinePrefab;
            case Building.BuildingType.IronMine:
                return ironMinePrefab;
            case Building.BuildingType.Wall:
                return wallPrefab;
            case Building.BuildingType.Tower:
                return towerPrefab;
            default:
                Debug.LogWarning("Unknown building type: " + buildingType);
                return null;
        }
    }

    public GameObject GetUnitPrefab(Unit.UnitType unitType)
    {
        switch (unitType)
        {
            case Unit.UnitType.Warrior:
                return warriorPrefab;
            case Unit.UnitType.Archer:
                return archerPrefab;
            case Unit.UnitType.Knight:
                return knightPrefab;
            case Unit.UnitType.Mage:
                return magePrefab;
            case Unit.UnitType.Scout:
                return scoutPrefab;
            case Unit.UnitType.Healer:
                return healerPrefab;
            default:
                Debug.LogWarning("Unknown unit type: " + unitType);
                return null;
        }
    }
}
