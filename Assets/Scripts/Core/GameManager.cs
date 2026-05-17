using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game State")]
    public GameState currentGameState;
    public float gameSpeed = 1f;
    private float gameTimer = 0f;

    [Header("Player Data")]
    public PlayerData playerData;
    public ClanManager clanManager;
    public EconomyManager economyManager;
    public MapManager mapManager;
    public BuildingManager buildingManager;
    public UnitManager unitManager;
    public UIManager uiManager;
    public AdsManager adsManager;

    [Header("Settings")]
    public bool isPaused = false;
    public bool isOfflineMode = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        // Initialize all managers
        playerData = GetComponent<PlayerData>();
        clanManager = GetComponent<ClanManager>();
        economyManager = GetComponent<EconomyManager>();
        mapManager = GetComponent<MapManager>();
        buildingManager = GetComponent<BuildingManager>();
        unitManager = GetComponent<UnitManager>();
        uiManager = GetComponent<UIManager>();
        adsManager = GetComponent<AdsManager>();

        // Load saved data
        LoadGameData();

        // Set initial state
        currentGameState = GameState.Playing;
    }

    private void Update()
    {
        if (isPaused)
            return;

        gameTimer += Time.deltaTime * gameSpeed;

        // Update all systems
        economyManager.UpdateEconomy(Time.deltaTime);
        buildingManager.UpdateBuildings(Time.deltaTime);
        unitManager.UpdateUnits(Time.deltaTime);
        clanManager.UpdateClan(Time.deltaTime);
    }

    public void PauseGame()
    {
        isPaused = true;
        currentGameState = GameState.Paused;
    }

    public void ResumeGame()
    {
        isPaused = false;
        currentGameState = GameState.Playing;
    }

    public void SaveGameData()
    {
        playerData.SaveData();
        clanManager.SaveClanData();
        mapManager.SaveMapData();
        economyManager.SaveEconomyData();
    }

    public void LoadGameData()
    {
        playerData.LoadData();
        clanManager.LoadClanData();
        mapManager.LoadMapData();
        economyManager.LoadEconomyData();
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    public enum GameState
    {
        Playing,
        Paused,
        Building,
        Combat,
        MenuScreen
    }
}
