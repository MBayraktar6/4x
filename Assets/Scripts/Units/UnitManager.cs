using UnityEngine;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();
    private Dictionary<Unit.UnitType, GameObject> unitPrefabs = new Dictionary<Unit.UnitType, GameObject>();

    public void AddUnit(Unit unit)
    {
        units.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        units.Remove(unit);
    }

    public Unit CreateUnit(Unit.UnitType unitType, Vector3 position)
    {
        if (!unitPrefabs.ContainsKey(unitType))
        {
            Debug.LogError("Unit prefab not found for: " + unitType);
            return null;
        }

        GameObject prefab = unitPrefabs[unitType];
        GameObject unitGO = Instantiate(prefab, position, Quaternion.identity);
        Unit unit = unitGO.GetComponent<Unit>();

        if (unit != null)
        {
            AddUnit(unit);
        }

        return unit;
    }

    public void UpdateUnits(float deltaTime)
    {
        // Unit updates handled in Unit.cs Update method
    }

    public List<Unit> GetUnitsByType(Unit.UnitType unitType)
    {
        List<Unit> result = new List<Unit>();
        foreach (Unit unit in units)
        {
            if (unit.unitData.unitType == unitType)
                result.Add(unit);
        }
        return result;
    }

    public int GetTotalUnitCount()
    {
        return units.Count;
    }
}
