using UnityEngine;
using System.IO;

public static class SaveManager
{
    private static string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "gamedata.json");
    }

    public static void SavePlayerData(PlayerData playerData)
    {
        try
        {
            string json = JsonUtility.ToJson(playerData, true);
            string path = GetSavePath();
            File.WriteAllText(path, json);
            Debug.Log($"[SaveManager] Veriler kaydedildi: {path}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SaveManager] Kaydetme hatası: {e.Message}");
        }
    }

    public static PlayerData LoadPlayerData()
    {
        try
        {
            string path = GetSavePath();
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
                Debug.Log($"[SaveManager] Veriler yüklendi: {path}");
                return playerData;
            }
            else
            {
                Debug.Log("[SaveManager] Kayıtlı veri bulunamadı.");
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SaveManager] Yükleme hatası: {e.Message}");
            return null;
        }
    }

    public static void DeletePlayerData()
    {
        try
        {
            string path = GetSavePath();
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("[SaveManager] Oyuncu verileri silindi.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SaveManager] Silme hatası: {e.Message}");
        }
    }
}
