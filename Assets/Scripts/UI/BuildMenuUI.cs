using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BuildMenuUI : MonoBehaviour
{
    [System.Serializable]
    public class BuildingUIElement
    {
        public Button buildButton;
        public TextMeshProUGUI buildingNameText;
        public TextMeshProUGUI costText;
        public Image buildingIcon;
        public Slider buildProgressSlider;
        public Building.BuildingType buildingType;
    }

    public List<BuildingUIElement> buildingElements = new List<BuildingUIElement>();
    public GameObject buildMenuPanel;
    public GridLayoutGroup buildingGrid;
    public TextMeshProUGUI selectedBuildingDescription;
    public Button closeBuildMenuButton;

    private Building selectedBuilding;

    private void Start()
    {
        closeBuildMenuButton.onClick.AddListener(CloseBuildMenu);
        InitializeBuildingButtons();
    }

    private void InitializeBuildingButtons()
    {
        foreach (BuildingUIElement element in buildingElements)
        {
            element.buildButton.onClick.AddListener(() => OnBuildingSelected(element));
        }
    }

    private void OnBuildingSelected(BuildingUIElement element)
    {
        // Load building config and display details
        selectedBuildingDescription.text = GetBuildingDescription(element.buildingType);
        Debug.Log("Selected building: " + element.buildingType);
    }

    public void OpenBuildMenu()
    {
        buildMenuPanel.SetActive(true);
        RefreshBuildingCosts();
    }

    public void CloseBuildMenu()
    {
        buildMenuPanel.SetActive(false);
    }

    private void RefreshBuildingCosts()
    {
        PlayerData playerData = GameManager.Instance.playerData;

        foreach (BuildingUIElement element in buildingElements)
        {
            // Update costs based on player resources
            element.costText.text = $"Gold: {playerData.resources.gold}";
        }
    }

    private string GetBuildingDescription(Building.BuildingType buildingType)
    {
        switch (buildingType)
        {
            case Building.BuildingType.Farm:
                return "Produces 500 Food per hour. Essential for training units.";
            case Building.BuildingType.Lumbermill:
                return "Produces 600 Wood per hour. Required for building construction.";
            case Building.BuildingType.StoneMine:
                return "Produces 550 Stone per hour. Used for advanced buildings.";
            case Building.BuildingType.IronMine:
                return "Produces 300 Iron per hour. Rare resource for powerful buildings.";
            case Building.BuildingType.Barracks:
                return "Train warriors, archers, knights and more. Each unit requires time and food.";
            case Building.BuildingType.TownHall:
                return "Your village center. Produces 100 Gold per hour.";
            case Building.BuildingType.Wall:
                return "Defensive structure. Protects your village from attacks.";
            case Building.BuildingType.Tower:
                return "Powerful defensive tower. Deals damage to attacking units.";
            default:
                return "";
        }
    }

    private void Update()
    {
        RefreshBuildingCosts();
    }
}
