using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Config")]
public class GameConfig : ScriptableObject
{
    [Header("Harita Ayarları")]
    public int mapWidth = 100;
    public int mapHeight = 100;
    public int tileSize = 32;

    [Header("Köy Ayarları")]
    public int startingGold = 1000;
    public int startingWood = 500;
    public int startingFood = 750;
    public float buildingConstructionSpeed = 1f;

    [Header("Birim Ayarları")]
    public int unitTrainingSpeed = 2;
    public float combatDamageMultiplier = 1f;

    [Header("Klan Ayarları")]
    public int maxClanMembers = 100;
    public int clanTerritorySize = 10;

    [Header("Reklam Ayarları")]
    public int videoAdCooldownMinutes = 15;
    public int dailyVideoAdLimit = 48; // 24 saatte max 48 reklam (30 dakika arası)
    public int goldRewardPerVideo = 100;
    public int woodRewardPerVideo = 50;
    public int foodRewardPerVideo = 75;

    [Header("Grafik Ayarları")]
    public bool use3DEffects = true;
    public float particleEffectIntensity = 1f;
}
