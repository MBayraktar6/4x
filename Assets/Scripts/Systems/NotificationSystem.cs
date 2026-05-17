using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem Instance { get; private set; }

    [System.Serializable]
    public class Notification
    {
        public string title;
        public string message;
        public float duration = 3f;
        public Color color = Color.white;
        public NotificationType type;
    }

    public enum NotificationType
    {
        Info,
        Success,
        Warning,
        Error
    }

    [Header("UI Elements")]
    public Transform notificationContainer;
    public GameObject notificationPrefab;
    public float notificationSpacing = 10f;

    private Queue<Notification> notificationQueue = new Queue<Notification>();
    private List<GameObject> activeNotifications = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShowNotification(string title, string message, NotificationType type = NotificationType.Info, float duration = 3f)
    {
        Notification notification = new Notification
        {
            title = title,
            message = message,
            type = type,
            duration = duration,
            color = GetColorForType(type)
        };

        notificationQueue.Enqueue(notification);
    }

    private Color GetColorForType(NotificationType type)
    {
        switch (type)
        {
            case NotificationType.Success:
                return Color.green;
            case NotificationType.Warning:
                return Color.yellow;
            case NotificationType.Error:
                return Color.red;
            default:
                return Color.white;
        }
    }

    private void Update()
    {
        if (notificationQueue.Count > 0 && activeNotifications.Count < 3) // Max 3 notifications
        {
            Notification notification = notificationQueue.Dequeue();
            DisplayNotification(notification);
        }
    }

    private void DisplayNotification(Notification notification)
    {
        GameObject notifGO = Instantiate(notificationPrefab, notificationContainer);
        TextMeshProUGUI textComponent = notifGO.GetComponent<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = notification.title + ": " + notification.message;
            textComponent.color = notification.color;
        }

        activeNotifications.Add(notifGO);
        StartCoroutine(RemoveNotificationAfterDelay(notifGO, notification.duration));
    }

    private IEnumerator RemoveNotificationAfterDelay(GameObject notificationGO, float delay)
    {
        yield return new WaitForSeconds(delay);
        activeNotifications.Remove(notificationGO);
        Destroy(notificationGO);
    }
}
