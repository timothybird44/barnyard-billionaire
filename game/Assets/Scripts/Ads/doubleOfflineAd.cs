using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Import the new LevelPlay namespace
using Unity.Services.LevelPlay;

public class doubleOfflineAd : MonoBehaviour
{
    [SerializeField] Button _showAdButton;
    [SerializeField] Text txt_AdButton;

    // Ad unit IDs for LevelPlay
    [SerializeField] string _androidRewardedAdUnitId;
    [SerializeField] string _iOSRewardedAdUnitId;

    private LevelPlayRewardedAd _rewardedAd;
    private string _currentRewardedAdUnitId; // Platform-specific ad unit ID

    offlineEarning offlineEarningScript;
    AudioSource aud_purchaseSound, aud_buttonSound;
    Color clr_unavailable, clr_available;
    Text txt_earnDouble;

    void Awake()
    {
        offlineEarningScript = GameObject.Find("Offline Canvas").GetComponent<offlineEarning>();
        aud_purchaseSound = GameObject.Find("Purchase Sound").GetComponent<AudioSource>();
        aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();
        clr_unavailable = new Color(118f / 255f, 118f / 255f, 118f / 255f); // Directly set the color
        clr_available = Color.white;
        txt_earnDouble = GameObject.Find("txt_earnDouble").GetComponent<Text>();

        _showAdButton.interactable = false;
        txt_AdButton.color = clr_unavailable;
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

        _rewardedAd.OnAdLoaded += RewardedVideoOnAdLoadedEvent;
        _rewardedAd.OnAdLoadFailed += RewardedVideoOnAdLoadFailedEvent;
        _rewardedAd.OnAdDisplayed += RewardedVideoOnAdDisplayedEvent;
        _rewardedAd.OnAdDisplayFailed += RewardedVideoOnAdDisplayFailedEvent;
        _rewardedAd.OnAdClicked += RewardedVideoOnAdClickedEvent; // Ensure this name is consistent
        _rewardedAd.OnAdClosed += RewardedVideoOnAdClosedEvent;
        _rewardedAd.OnAdRewarded += RewardedVideoOnAdRewardedEvent;

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
        _rewardedAd.LoadAd();
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
            if (_rewardedAd.IsAdReady())
            {
                _rewardedAd.ShowAd();
                Debug.Log("Showing rewarded ad via LevelPlayRewardedAd.");
            }
            else
            {
                Debug.LogWarning("Rewarded ad is not ready. Attempting to reload.");
                _showAdButton.interactable = true;
                txt_AdButton.color = clr_available;
                txt_earnDouble.color = clr_available;
                LoadAd();
            }
        }
        else
        {
            offlineEarningScript.OfflineCollectDoubleClicked();
        }
    }

    void RewardedVideoOnAdLoadedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log($"Rewarded video ad loaded: {adInfo.AdUnitId}");
        _showAdButton.interactable = true;
        txt_AdButton.color = clr_available;
        txt_earnDouble.color = clr_available;
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
        LoadAd();
    }

    // FIX HERE: Changed 'Type' to 'Name' and 'Amount' is correct
    void RewardedVideoOnAdRewardedEvent(LevelPlayAdInfo adInfo, LevelPlayReward reward)
    {
        Debug.Log($"Rewarded video ad rewarded: {adInfo.AdUnitId}, Name: {reward.Name}, Amount: {reward.Amount}"); // Line 141 corrected
        offlineEarningScript.OfflineCollectDoubleClicked();
    }

    void RewardedVideoOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError error)
    {
        Debug.LogError($"Rewarded video ad show failed: {error}");
        _showAdButton.interactable = true;
        txt_AdButton.color = clr_available;
        txt_earnDouble.color = clr_available;
        LoadAd();
    }

    void RewardedVideoOnAdClickedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log($"Rewarded video ad clicked: {adInfo.AdUnitId}");
    }

    void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
    {
        Debug.Log("unity - script: I got ImpressionDataReadyEvent ToString(): " + impressionData.ToString());
    }

    void OnDestroy()
    {
        _showAdButton.onClick.RemoveAllListeners();

        _rewardedAd.OnAdLoaded -= RewardedVideoOnAdLoadedEvent;
        _rewardedAd.OnAdLoadFailed -= RewardedVideoOnAdLoadFailedEvent;
        _rewardedAd.OnAdDisplayed -= RewardedVideoOnAdDisplayedEvent;
        _rewardedAd.OnAdDisplayFailed -= RewardedVideoOnAdDisplayFailedEvent;
        _rewardedAd.OnAdClicked -= RewardedVideoOnAdClickedEvent;
        _rewardedAd.OnAdClosed -= RewardedVideoOnAdClosedEvent;
        _rewardedAd.OnAdRewarded -= RewardedVideoOnAdRewardedEvent;

        IronSourceEvents.onImpressionDataReadyEvent -= ImpressionDataReadyEvent;
        _rewardedAd.DestroyAd();
    }
}