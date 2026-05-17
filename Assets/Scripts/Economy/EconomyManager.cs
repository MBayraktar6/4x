using UnityEngine;
using System.Collections.Generic;

public class EconomyManager : MonoBehaviour
{
    [System.Serializable]
    public class TradeRoute
    {
        public int tradeId;
        public string resourceType;
        public int sellPrice;
        public int buyPrice;
        public float profitMargin;
    }

    public List<TradeRoute> tradeRoutes = new List<TradeRoute>();
    private float economyUpdateTimer = 0f;
    private const float ECONOMY_UPDATE_INTERVAL = 60f; // Update every minute

    [Header("Market Prices")]
    public int goldPrice = 1;
    public int woodPrice = 50;
    public int stonePrice = 60;
    public int foodPrice = 40;
    public int ironPrice = 80;

    private void Start()
    {
        InitializeTradeRoutes();
    }

    private void InitializeTradeRoutes()
    {
        // Initialize base trade routes
        // These can be modified based on supply/demand
    }

    public void UpdateEconomy(float deltaTime)
    {
        economyUpdateTimer += deltaTime;

        if (economyUpdateTimer >= ECONOMY_UPDATE_INTERVAL)
        {
            UpdateMarketPrices();
            economyUpdateTimer = 0f;
        }
    }

    private void UpdateMarketPrices()
    {
        // Simulate price fluctuations
        woodPrice += Random.Range(-5, 6);
        stonePrice += Random.Range(-5, 6);
        foodPrice += Random.Range(-5, 6);
        ironPrice += Random.Range(-8, 9);

        // Keep prices in reasonable range
        woodPrice = Mathf.Clamp(woodPrice, 30, 100);
        stonePrice = Mathf.Clamp(stonePrice, 30, 100);
        foodPrice = Mathf.Clamp(foodPrice, 20, 80);
        ironPrice = Mathf.Clamp(ironPrice, 50, 150);
    }

    public void SellResource(string resourceType, long amount)
    {
        PlayerData playerData = GameManager.Instance.playerData;
        long goldEarned = 0;

        switch (resourceType.ToLower())
        {
            case "wood":
                if (playerData.RemoveResources("wood", amount))
                    goldEarned = amount * woodPrice;
                break;
            case "stone":
                if (playerData.RemoveResources("stone", amount))
                    goldEarned = amount * stonePrice;
                break;
            case "food":
                if (playerData.RemoveResources("food", amount))
                    goldEarned = amount * foodPrice;
                break;
            case "iron":
                if (playerData.RemoveResources("iron", amount))
                    goldEarned = amount * ironPrice;
                break;
        }

        if (goldEarned > 0)
        {
            playerData.AddResources("gold", goldEarned);
            Debug.Log("Sold " + amount + " " + resourceType + " for " + goldEarned + " gold");
        }
    }

    public void BuyResource(string resourceType, long amount)
    {
        PlayerData playerData = GameManager.Instance.playerData;
        long goldRequired = 0;

        switch (resourceType.ToLower())
        {
            case "wood":
                goldRequired = amount * woodPrice;
                break;
            case "stone":
                goldRequired = amount * stonePrice;
                break;
            case "food":
                goldRequired = amount * foodPrice;
                break;
            case "iron":
                goldRequired = amount * ironPrice;
                break;
        }

        if (playerData.RemoveResources("gold", goldRequired))
        {
            playerData.AddResources(resourceType, amount);
            Debug.Log("Bought " + amount + " " + resourceType + " for " + goldRequired + " gold");
        }
    }

    public void SaveEconomyData()
    {
        // Implement save logic
    }

    public void LoadEconomyData()
    {
        // Implement load logic
    }
}
