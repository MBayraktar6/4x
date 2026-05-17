using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    public List<Building> buildings = new List<Building>();
    private Queue<Building> buildingQueue = new Queue<Building>();

    [Header("Building Prefabs")]
    public Dictionary<Building.BuildingType, GameObject> buildingPrefabs = new Dictionary<Building.BuildingType, GameObject>();

    public void AddBuilding(Building building)
    {
        buildings.Add(building);
    }

    public void RemoveBuilding(Building building)
    {
        buildings.Remove(building);
    }

    public void QueueBuilding(Building building)
    {
        buildingQueue.Enqueue(building);
    }

    public Building CreateBuilding(Building.BuildingType buildingType, Vector3 position)
    {
        if (!buildingPrefabs.ContainsKey(buildingType))
        {
            Debug.LogError("Building prefab not found for: " + buildingType);
            return null;
        }

        GameObject prefab = buildingPrefabs[buildingType];
        GameObject buildingGO = Instantiate(prefab, position, Quaternion.identity);
        Building building = buildingGO.GetComponent<Building>();

        if (building != null)
        {
            AddBuilding(building);
        }

        return building;
    }

    public void UpdateBuildings(float deltaTime)
    {
        foreach (Building building in buildings)
        {
            // Updates handled in Building.cs Update method
        }
    }

    public Building GetBuildingByType(Building.BuildingType buildingType)
    {
        foreach (Building building in buildings)
        {
            if (building.buildingData.buildingType == buildingType)
                return building;
        }
        return null;
    }

    public List<Building> GetAllBuildingsByType(Building.BuildingType buildingType)
    {
        List<Building> result = new List<Building>();
        foreach (Building building in buildings)
        {
            if (building.buildingData.buildingType == buildingType)
                result.Add(building);
        }
        return result;
    }

    public void SaveBuildingData()
    {
        // Implement save logic
    }

    public void LoadBuildingData()
    {
        // Implement load logic
    }
}
