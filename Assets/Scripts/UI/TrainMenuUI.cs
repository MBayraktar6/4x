using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TrainMenuUI : MonoBehaviour
{
    [System.Serializable]
    public class UnitUIElement
    {
        public Button trainButton;
        public TextMeshProUGUI unitNameText;
        public TextMeshProUGUI statsText;
        public TextMeshProUGUI trainTimeText;
        public Image unitIcon;
        public Slider trainProgressSlider;
        public Unit.UnitType unitType;
    }

    public List<UnitUIElement> unitElements = new List<UnitUIElement>();
    public GameObject trainMenuPanel;
    public GridLayoutGroup unitGrid;
    public TextMeshProUGUI unitDescription;
    public TextMeshProUGUI totalUnitsCountText;
    public Button closeTrainMenuButton;

    private Dictionary<Unit.UnitType, int> unitTrainQueue = new Dictionary<Unit.UnitType, int>();

    private void Start()
    {
        closeTrainMenuButton.onClick.AddListener(CloseTrainMenu);
        InitializeUnitButtons();
    }

    private void InitializeUnitButtons()
    {
        foreach (UnitUIElement element in unitElements)
        {
            element.trainButton.onClick.AddListener(() => OnUnitSelected(element));
        }
    }

    private void OnUnitSelected(UnitUIElement element)
    {
        unitDescription.text = GetUnitDescription(element.unitType);
        element.statsText.text = GetUnitStats(element.unitType);
        Debug.Log("Selected unit: " + element.unitType);
    }

    public void OpenTrainMenu()
    {
        trainMenuPanel.SetActive(true);
        RefreshUnitInfo();
    }

    public void CloseTrainMenu()
    {
        trainMenuPanel.SetActive(false);
    }

    private void RefreshUnitInfo()
    {
        UnitManager unitManager = GameManager.Instance.unitManager;
        totalUnitsCountText.text = "Total Units: " + unitManager.GetTotalUnitCount();
    }

    private string GetUnitDescription(Unit.UnitType unitType)
    {
        switch (unitType)
        {
            case Unit.UnitType.Warrior:
                return "Balanced unit. Good attack and defense. Great for general combat.";
            case Unit.UnitType.Archer:
                return "Ranged unit. High damage but low defense. Best for dealing damage from distance.";
            case Unit.UnitType.Knight:
                return "Heavy armor unit. Highest defense and health. Takes damage to protect others.";
            case Unit.UnitType.Mage:
                return "Magical unit. Highest attack power. Weak defense but powerful spells.";
            case Unit.UnitType.Scout:
                return "Fastest unit. Lowest stats but excellent for reconnaissance and speed.";
            case Unit.UnitType.Healer:
                return "Support unit. Heals other units during battle. Essential for long campaigns.";
            default:
                return "";
        }
    }

    private string GetUnitStats(Unit.UnitType unitType)
    {
        switch (unitType)
        {
            case Unit.UnitType.Warrior:
                return "Health: 100 | Attack: 15 | Defense: 5 | Speed: 3.5";
            case Unit.UnitType.Archer:
                return "Health: 60 | Attack: 20 | Defense: 2 | Speed: 4.0";
            case Unit.UnitType.Knight:
                return "Health: 150 | Attack: 20 | Defense: 12 | Speed: 2.5";
            case Unit.UnitType.Mage:
                return "Health: 50 | Attack: 25 | Defense: 3 | Speed: 3.0";
            case Unit.UnitType.Scout:
                return "Health: 30 | Attack: 8 | Defense: 1 | Speed: 5.5";
            case Unit.UnitType.Healer:
                return "Health: 70 | Attack: 5 | Defense: 4 | Speed: 3.0";
            default:
                return "";
        }
    }
}
