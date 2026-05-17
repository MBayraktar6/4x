using UnityEngine;
using System;
using System.Collections.Generic;

public class AnalyticsSystem : MonoBehaviour
{
    public static AnalyticsSystem Instance { get; private set; }

    [System.Serializable]
    public class GameEvent
    {
        public string eventName;
        public DateTime timestamp;
        public Dictionary<string, string> eventData = new Dictionary<string, string>();
    }

    private List<GameEvent> eventLog = new List<GameEvent>();
    private float sessionStartTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        sessionStartTime = Time.time;
    }

    public void LogEvent(string eventName, Dictionary<string, string> eventData = null)
    {
        GameEvent gameEvent = new GameEvent
        {
            eventName = eventName,
            timestamp = DateTime.UtcNow,
            eventData = eventData ?? new Dictionary<string, string>()
        };

        eventLog.Add(gameEvent);
        Debug.Log("Event logged: " + eventName);
    }

    public void LogBuildingConstructed(string buildingName)
    {
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            { "building_name", buildingName },
            { "timestamp", DateTime.UtcNow.ToString() }
        };
        LogEvent("building_constructed", data);
    }

    public void LogUnitTrained(string unitType)
    {
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            { "unit_type", unitType },
            { "timestamp", DateTime.UtcNow.ToString() }
        };
        LogEvent("unit_trained", data);
    }

    public void LogBattleResult(bool isVictory, int enemyClanId)
    {
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            { "victory", isVictory.ToString() },
            { "enemy_clan_id", enemyClanId.ToString() },
            { "timestamp", DateTime.UtcNow.ToString() }
        };
        LogEvent("battle_completed", data);
    }

    public void LogAdWatched()
    {
        LogEvent("ad_watched");
    }

    public float GetSessionDuration()
    {
        return Time.time - sessionStartTime;
    }

    public List<GameEvent> GetEventLog()
    {
        return eventLog;
    }
}
