using UnityEngine;

public static class DebugHelper
{
    public static void LogGameState()
    {
        GameManager gm = GameManager.Instance;
        if (gm == null) return;

        PlayerData pd = gm.playerData;
        Debug.Log("=== GAME STATE ===");
        Debug.Log("Player: " + pd.playerInfo.playerName + " Level " + pd.playerInfo.level);
        Debug.Log("Gold: " + pd.resources.gold);
        Debug.Log("Wood: " + pd.resources.wood);
        Debug.Log("Stone: " + pd.resources.stone);
        Debug.Log("Food: " + pd.resources.food);
        Debug.Log("Iron: " + pd.resources.iron);
        Debug.Log("Buildings: " + gm.buildingManager.buildings.Count);
        Debug.Log("Units: " + gm.unitManager.GetTotalUnitCount());
    }

    public static void AddResourcesToPlayer(long amount)
    {
        GameManager.Instance.playerData.AddResources("gold", amount);
        GameManager.Instance.playerData.AddResources("wood", amount);
        GameManager.Instance.playerData.AddResources("stone", amount);
        GameManager.Instance.playerData.AddResources("food", amount);
        GameManager.Instance.playerData.AddResources("iron", amount);
        Debug.Log("Added " + amount + " of each resource");
    }
}
