using System.Collections;
using UnityEngine;
using Unity.Services.LevelPlay;

public class interstitialAds : MonoBehaviour
{
    [SerializeField] string _androidInterstitialAdUnitId; 
    [SerializeField] string _iOSInterstitialAdUnitId;

    private LevelPlayInterstitialAd _interstitialAd; 
    private string _currentInterstitialAdUnitId;
    private bool _isLoadingAd = false; // New flag to track if an ad load is in progress

    void Awake()
    {
        #if UNITY_IOS
                _currentInterstitialAdUnitId = _iOSInterstitialAdUnitId;
        #elif UNITY_ANDROID
                _currentInterstitialAdUnitId = _androidInterstitialAdUnitId;
        #elif UNITY_EDITOR
                _currentInterstitialAdUnitId = _androidInterstitialAdUnitId; // For testing in editor
        #endif

        _interstitialAd = new LevelPlayInterstitialAd(_currentInterstitialAdUnitId);

        // Subscribe to all ad events
        _interstitialAd.OnAdLoaded += InterstitialOnAdLoadedEvent;
        _interstitialAd.OnAdLoadFailed += InterstitialOnAdLoadFailedEvent;
        _interstitialAd.OnAdDisplayed += InterstitialOnAdDisplayedEvent;
        _interstitialAd.OnAdDisplayFailed += InterstitialOnAdDisplayFailedEvent;
        _interstitialAd.OnAdClicked += InterstitialOnAdClickedEvent;
        _interstitialAd.OnAdClosed += InterstitialOnAdClosedEvent;
        _interstitialAd.OnAdInfoChanged += InterstitialOnAdInfoChangedEvent;

        // Subscribe to IronSource impression data event
        IronSourceEvents.onImpressionDataReadyEvent += ImpressionDataReadyEvent;
        // Subscribe to SDK Initialization Event
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
    }

    void SdkInitializationCompletedEvent()
    {
        Debug.Log("IronSource SDK Initialization Completed! Now safe to load ads.");
        RequestLoadAd(); 
    }

    // This method is now safe to call from anywhere, it will check the loading state
    public void RequestLoadAd()
    {
        // Only load if not currently loading AND ad is not already ready
        if (!_isLoadingAd && !_interstitialAd.IsAdReady())
        {
            _isLoadingAd = true; // Set flag to true to indicate loading is in progress
            Debug.Log("Requesting interstitial ad load using LevelPlayInterstitialAd.");
            _interstitialAd.LoadAd();
        }
        else if (_interstitialAd.IsAdReady())
        {
            Debug.Log("Interstitial ad is already ready. No new load requested.");
        }
        else // _isLoadingAd is true
        {
            Debug.Log("Interstitial ad is already loading. Skipping new load request.");
        }
    }
 
    public void ShowAd()
    {
        // Check if an ad is ready before attempting to show
        if (_interstitialAd.IsAdReady())
        {
            Debug.Log("Showing interstitial ad via LevelPlayInterstitialAd.");
            _interstitialAd.ShowAd();
            _isLoadingAd = false; // Reset loading flag as ad is now being displayed
        }
        else
        {
            Debug.LogWarning("Interstitial ad is not ready to show via LevelPlayInterstitialAd.");
            // If ad is not ready, try to load it, but don't force a load if one is already in progress
            RequestLoadAd(); 
            StartCoroutine(AdTimer());
        }
    }
 
    void InterstitialOnAdLoadedEvent(LevelPlayAdInfo adInfo)
    {
        _isLoadingAd = false; // Loading process completed successfully
        Debug.Log($"Interstitial ad loaded: {adInfo.AdUnitId}");
    }
 
    void InterstitialOnAdLoadFailedEvent(LevelPlayAdError error)
    {
        _isLoadingAd = false; // Loading process completed with a failure
        Debug.LogError($"Interstitial ad failed to load: {error}"); 
        // Do NOT call RequestLoadAd() directly here, as it might lead to endless loops or rapid re-attempts.
        // Instead, allow your game logic (e.g., the AdTimer, or a game event) to request a new ad.
    }
 
    void InterstitialOnAdDisplayedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log($"Interstitial ad opened: {adInfo.AdUnitId}");
    }

    void InterstitialOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError error)
    {
        _isLoadingAd = false; // Ad display failed, reset loading flag
        Debug.LogError($"Interstitial ad failed to show: {error}"); 
        // Similar to load failed, do NOT call RequestLoadAd() directly here.
        // The game logic should decide when to request a new ad.
    }
 
    void InterstitialOnAdClickedEvent(LevelPlayAdInfo adInfo) { 
        Debug.Log($"Interstitial ad clicked: {adInfo.AdUnitId}");
    }

    void InterstitialOnAdClosedEvent(LevelPlayAdInfo adInfo) { 
        Debug.Log($"Interstitial ad closed: {adInfo.AdUnitId}");
        // After an ad is closed, immediately request a new one to be ready for the next display
        RequestLoadAd(); 
        StartCoroutine(AdTimer()); // Start timer for the next show
    }

    void InterstitialOnAdInfoChangedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log($"Interstitial ad info changed: {adInfo.AdUnitId}");
    }

    void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
    {
        Debug.Log("unity - script: I got ImpressionDataReadyEvent ToString(): " + impressionData.ToString());
    }

    public IEnumerator AdTimer() {
        float flt_adFrequency = (float)PlayerPrefs.GetInt("AdFrequency", 30);
        yield return new WaitForSeconds(flt_adFrequency);

        if (PlayerPrefs.GetInt("ProvideAds") == 1) {
            // Check if ad is ready. If not, request load.
            // ShowAd() itself will call RequestLoadAd() if not ready.
            ShowAd(); 
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from all events to prevent memory leaks and issues when the object is destroyed
        _interstitialAd.OnAdLoaded -= InterstitialOnAdLoadedEvent;
        _interstitialAd.OnAdLoadFailed -= InterstitialOnAdLoadFailedEvent;
        _interstitialAd.OnAdDisplayed -= InterstitialOnAdDisplayedEvent;
        _interstitialAd.OnAdDisplayFailed -= InterstitialOnAdDisplayFailedEvent;
        _interstitialAd.OnAdClicked -= InterstitialOnAdClickedEvent;
        _interstitialAd.OnAdClosed -= InterstitialOnAdClosedEvent;
        _interstitialAd.OnAdInfoChanged -= InterstitialOnAdInfoChangedEvent;

        IronSourceEvents.onImpressionDataReadyEvent -= ImpressionDataReadyEvent;
        
        // Destroy the ad instance to clean up resources
        _interstitialAd.DestroyAd();
    }
}