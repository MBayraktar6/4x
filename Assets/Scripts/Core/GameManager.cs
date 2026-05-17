using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameConfig gameConfig;
    private PlayerData playerData;
    private MapManager mapManager;
    private VillageManager villageManager;
    private ClanManager clanManager;
    private AdManager adManager;
    private UIManager uiManager;

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
        Debug.Log("[GameManager] Oyun başlatılıyor...");

        // Oyuncu verisini yükle
        playerData = SaveManager.LoadPlayerData();
        if (playerData == null)
        {
            playerData = new PlayerData();
            playerData.Initialize();
        }

        // Yöneticileri başlat
        mapManager = GetComponent<MapManager>();
        villageManager = GetComponent<VillageManager>();
        clanManager = GetComponent<ClanManager>();
        adManager = GetComponent<AdManager>();
        uiManager = GetComponent<UIManager>();

        if (mapManager != null) mapManager.Initialize(gameConfig);
        if (villageManager != null) villageManager.Initialize(playerData);
        if (clanManager != null) clanManager.Initialize(playerData);
        if (adManager != null) adManager.Initialize();
        if (uiManager != null) uiManager.Initialize();

        Debug.Log("[GameManager] Oyun hazır!");
    }

    public void SaveGame()
    {
        SaveManager.SavePlayerData(playerData);
        Debug.Log("[GameManager] Oyun kaydedildi.");
    }

    public PlayerData GetPlayerData() => playerData;
    public MapManager GetMapManager() => mapManager;
    public VillageManager GetVillageManager() => villageManager;
    public ClanManager GetClanManager() => clanManager;
    public AdManager GetAdManager() => adManager;
}
