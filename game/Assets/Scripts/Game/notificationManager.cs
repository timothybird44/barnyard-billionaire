// using Unity.Notifications.Android; // Use this for Android-specific notification classes
// using Unity.Notifications.iOS;     // Use this for iOS-specific notification classes
using UnityEngine;
using System; // Keep this for TimeSpan and DateTime

public class notificationManager : MonoBehaviour
{
    constants constantsScript;

    void Start()
    {
        GameObject scoreObject = GameObject.Find("Score");
        if (scoreObject != null)
        {
            constantsScript = scoreObject.GetComponent<constants>();
            if (constantsScript == null)
            {
                Debug.LogError("notificationManager: 'Score' GameObject found, but it is missing the 'constants' script component!");
            }
        }
        else
        {
            Debug.LogError("notificationManager: 'Score' GameObject not found in the scene! Cannot initialize constantsScript.");
        }
    }

    void TriggerNotification()
    {
        if (constantsScript == null)
        {
            Debug.LogError("notificationManager: constantsScript is null. Cannot trigger notification.");
            return;
        }

        // #if UNITY_ANDROID
        //         var channel = new AndroidNotificationChannel()
        //         {
        //             Id = "default_notification_channel",
        //             Name = "Default Notification Channel",
        //             Importance = Importance.Default,
        //             Description = "Generic notifications",
        //         };
        //         AndroidNotificationCenter.RegisterNotificationChannel(channel);

        //         var notification = new AndroidNotification();
        //         notification.Title = "Earn More";
        //         notification.Text = "Your production rate has reduced. Come back to double your production speed again!";
        //         notification.FireTime = System.DateTime.Now.AddSeconds(constantsScript.flt_earnTimeReset);

        //         AndroidNotificationCenter.SendNotification(notification, "default_notification_channel");
        // #if UNITY_IOS
        //         var notification = new iOSNotification(); // Now correctly resolves via the specific using directive
        //         notification.Identifier = "_notification_01";
        //         notification.Title = "Barnyard Billionaire: Earn More";
        //         notification.Body = "Your production rate has reduced. Come back to double your production speed again!";
        //         notification.ShowInForeground = true;
        //         notification.Trigger = new iOSNotificationTimeIntervalTrigger() { TimeInterval = TimeSpan.FromSeconds(constantsScript.flt_earnTimeReset) };

        //         iOSNotificationCenter.ScheduleNotification(notification);
        // #else
        //         Debug.Log("Notifications not supported on this platform.");
        // #endif
    }

    public void StartNotificationTimer()
    {
        TriggerNotification();
    }
}