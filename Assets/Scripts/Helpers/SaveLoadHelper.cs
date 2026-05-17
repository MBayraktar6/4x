using UnityEngine;
using System;
using System.IO;

public static class SaveLoadHelper
{
    private static readonly string SAVE_PATH = Application.persistentDataPath + "/GameData/";

    static SaveLoadHelper()
    {
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
    }

    public static void SaveToJson<T>(string fileName, T data)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);
            string filePath = SAVE_PATH + fileName + ".json";
            File.WriteAllText(filePath, json);
            Debug.Log("Saved: " + filePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Save failed: " + e.Message);
        }
    }

    public static T LoadFromJson<T>(string fileName) where T : new()
    {
        try
        {
            string filePath = SAVE_PATH + fileName + ".json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                T data = JsonUtility.FromJson<T>(json);
                Debug.Log("Loaded: " + filePath);
                return data;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Load failed: " + e.Message);
        }
        return new T();
    }

    public static bool FileExists(string fileName)
    {
        return File.Exists(SAVE_PATH + fileName + ".json");
    }
}
