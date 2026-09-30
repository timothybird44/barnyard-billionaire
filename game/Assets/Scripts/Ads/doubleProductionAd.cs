using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Import the new LevelPlay namespace
using Unity.Services.LevelPlay;

public class doubleProductionAd : MonoBehaviour
{
    [SerializeField] Button _showAdButton;
    [SerializeField] Text txt_AdButton;

    // Re-introducing fields for ad unit IDs, similar to interstitialAds
    // You should set these in the Unity Inspector.
    [SerializeField] string _androidRewardedAdUnitId;
    [SerializeField] string _iOSRewardedAdUnitId;

    // Declare an instance of the new LevelPlayRewardedAd class
    private LevelPlayRewardedAd _rewardedAd;
    // Variable to hold the chosen platform-specific ad unit ID
    private string _currentRewardedAdUnitId;

    notificationManager notificationManagerScript;
    AudioSource aud_purchaseSound, aud_buttonSound;
    Color clr_unavailable, clr_available;
    earnMore earnMoreScript;
    scoreTracking scoreTrackingScript;
    Text txt_earnDouble;

    void Awake()
    {
        scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();
        notificationManagerScript = GameObject.Find("Notification Manager").GetComponent<notificationManager>();
        aud_purchaseSound = GameObject.Find("Purchase Sound").GetComponent<AudioSource>();
        aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();
        clr_unavailable = scoreTrackingScript.HexToColor("767676");
        clr_available = Color.white;
        earnMoreScript = GameObject.Find("Earn Canvas").GetComponent<earnMore>();
        txt_earnDouble = GameObject.Find("txt_earnDouble").GetComponent<Text>();

        // Disable the button until the ad is ready to show:
        _showAdButton.interactable = false;
        txt_earnDouble.color = clr_unavailable;

        // Determine the correct ad unit ID based on the platform
        #if UNITY_IOS
                _currentRewardedAdUnitId = _iOSRewardedAdUnitId;
        #elif UNITY_ANDROID
                _currentRewardedAdUnitId = _androidRewardedAdUnitId;
        #elif UNITY_EDITOR
                _currentRewardedAdUnitId = _androidRewardedAdUnitId; // Use Android for editor testing
        #endif

        // Initialize the LevelPlayRewardedAd instance with the ad unit ID
        _rewardedAd = new LevelPlayRewardedAd(_currentRewardedAdUnitId);

        // Subscribe to the new LevelPlayRewardedAd events in Awake
        _rewardedAd.OnAdLoaded += RewardedVideoOnAdLoadedEvent;
        _rewardedAd.OnAdLoadFailed += RewardedVideoOnAdLoadFailedEvent;
        _rewardedAd.OnAdDisplayed += RewardedVideoOnAdDisplayedEvent;
        _rewardedAd.OnAdDisplayFailed += RewardedVideoOnAdDisplayFailedEvent;
        _rewardedAd.OnAdClicked += RewardedVideoOnAdClickedEvent; // FIX: This line references the corrected method name
        _rewardedAd.OnAdClosed += RewardedVideoOnAdClosedEvent;
        _rewardedAd.OnAdRewarded += RewardedVideoOnAdRewardedEvent;

        // Also subscribe to the global impression data event if needed
        IronSourceEvents.onImpressionDataReadyEvent += ImpressionDataReadyEvent;
    }

    void OnEnable()
    {
        // ...
    }

    void OnDisable()
    {
        // ...
    }

    public void LoadAd()
    {
        Debug.Log("Requesting rewarded ad load using LevelPlayRewardedAd.");
        _rewardedAd.LoadAd(); // Use the new LoadAd method
    }

    public void ShowAd()
    {
        _showAdButton.interactable = false;
        txt_AdButton.color = clr_unavailable;
        txt_earnDouble.color = clr_unavailable;

        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1))
        {
            aud_buttonSound.Play();
        }

        if (PlayerPrefs.GetInt("ProvideAds") == 1)
        {
            if (_rewardedAd.IsAdReady()) // Use the new IsAdReady method
            {
                _rewardedAd.ShowAd(); // Use the new ShowAd method
                Debug.Log("Showing rewarded ad via LevelPlayRewardedAd.");
            }
            else
            {
                Debug.LogWarning("Rewarded ad is not ready via LevelPlayRewardedAd. Attempting to reload.");
                _showAdButton.interactable = true;
                txt_AdButton.color = clr_available;
                txt_earnDouble.color = clr_available;
                LoadAd();
            }
        }
        else
        {
            scoreTrackingScript.DoubleScoreWatched();
        }
    }

    // --- New LevelPlayRewardedAd Event Handlers ---

    void RewardedVideoOnAdLoadedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log($"Rewarded video ad loaded: {adInfo.AdUnitId}");
        if (PlayerPrefs.GetFloat("EarnTimeRemaining") == 0)
        {
            _showAdButton.interactable = true;
            txt_AdButton.color = clr_available;
            txt_earnDouble.color = clr_available;
        }
        else
        {
            _showAdButton.interactable = false;
            txt_AdButton.color = clr_unavailable;
            txt_earnDouble.color = clr_unavailable;
        }
    }

    void RewardedVideoOnAdLoadFailedEvent(LevelPlayAdError error)
    {
        Debug.LogError($"Rewarded video ad load failed: {error}");
        _showAdButton.interactable = true;
        txt_AdButton.color = clr_available;
        txt_earnDouble.color = clr_available;
        LoadAd();
    }

    void RewardedVideoOnAdDisplayedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log($"Rewarded video ad opened: {adInfo.AdUnitId}");
    }

    void RewardedVideoOnAdClosedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log($"Rewarded video ad closed: {adInfo.AdUnitId}");
        LoadAd(); // Load a new ad after it's closed
    }

    // FIX HERE: Changed 'Type' to 'Name' and 'Amount' is correct
    void RewardedVideoOnAdRewardedEvent(LevelPlayAdInfo adInfo, LevelPlayReward reward)
    {
        Debug.Log($"Rewarded video ad rewarded: {adInfo.AdUnitId}, Name: {reward.Name}, Amount: {reward.Amount}"); // Line 158 corrected
        notificationManagerScript.StartNotificationTimer();
        scoreTrackingScript.DoubleScoreWatched();
    }

    void RewardedVideoOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError error)
    {
        Debug.LogError($"Rewarded video ad show failed: {error}");
        _showAdButton.interactable = true;
        txt_AdButton.color = clr_available;
        txt_earnDouble.color = clr_available;
        LoadAd();
    }

    // FIX HERE: Renamed method from RewardedVideoAdClickedEvent to RewardedVideoOnAdClickedEvent
    void RewardedVideoOnAdClickedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log($"Rewarded video ad clicked: {adInfo.AdUnitId}");
    }

    // --- Global Impression Data Event (if you need it) ---
    void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
    {
        Debug.Log("unity - script: I got ImpressionDataReadyEvent ToString(): " + impressionData.ToString());
    }

    void OnDestroy()
    {
        // Clean up button listeners
        _showAdButton.onClick.RemoveAllListeners();

        // Unsubscribe from LevelPlayRewardedAd events
        _rewardedAd.OnAdLoaded -= RewardedVideoOnAdLoadedEvent;
        _rewardedAd.OnAdLoadFailed -= RewardedVideoOnAdLoadFailedEvent;
        _rewardedAd.OnAdDisplayed -= RewardedVideoOnAdDisplayedEvent;
        _rewardedAd.OnAdDisplayFailed -= RewardedVideoOnAdDisplayFailedEvent;
        _rewardedAd.OnAdClicked -= RewardedVideoOnAdClickedEvent;
        _rewardedAd.OnAdClosed -= RewardedVideoOnAdClosedEvent;
        _rewardedAd.OnAdRewarded -= RewardedVideoOnAdRewardedEvent;

        // Unsubscribe from global IronSourceEvents if subscribed in Awake/OnEnable
        IronSourceEvents.onImpressionDataReadyEvent -= ImpressionDataReadyEvent;

        // Destroy the ad instance when the GameObject is destroyed
        _rewardedAd.DestroyAd();
    }
}