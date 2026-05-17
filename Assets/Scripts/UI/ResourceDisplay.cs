using UnityEngine;
using TMPro;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resourceText;
    [SerializeField] private ResourceType resourceType;

    private PlayerData playerData;

    private void Start()
    {
        playerData = GameManager.Instance.GetPlayerData();
        UpdateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (playerData == null || resourceText == null) return;

        long amount = GetResourceAmount();
        resourceText.text = FormatNumber(amount);
    }

    private long GetResourceAmount()
    {
        return resourceType switch
        {
            ResourceType.Gold => playerData.resources.gold,
            ResourceType.Wood => playerData.resources.wood,
            ResourceType.Food => playerData.resources.food,
            ResourceType.Gems => playerData.resources.gems,
            _ => 0
        };
    }

    private string FormatNumber(long number)
    {
        if (number >= 1000000)
            return (number / 1000000f).ToString("F1") + "M";
        if (number >= 1000)
            return (number / 1000f).ToString("F1") + "K";
        return number.ToString();
    }
}
