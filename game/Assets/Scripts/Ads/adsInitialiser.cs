using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Import the new LevelPlay namespace
using Unity.Services.LevelPlay;


public class adsInitialiser : MonoBehaviour
{
    // For LevelPlay, these are typically referred to as App Keys.
    [SerializeField] string _androidAppKey; 
    [SerializeField] string _iOSAppKey;
    [SerializeField] bool _testMode = true; 
    private string _appKey; 

    // IMPORTANT: These scripts will also need to be updated to the new API!
    // We'll update them in subsequent steps.
    doubleOfflineAd doubleOfflineAdScript;
    doubleProductionAd doubleProductionAdScript; 
    interstitialAds interstitialAdsScript;
 
    void Awake()
    {
        doubleOfflineAdScript = GameObject.Find("btn_offlineCollectDouble").GetComponent<doubleOfflineAd>();
        doubleProductionAdScript = GameObject.Find("btn_doubleProduction").GetComponent<doubleProductionAd>(); 
        interstitialAdsScript = GameObject.Find("Ads Manager").GetComponent<interstitialAds>();

        InitializeAds();
    }
 
    public void InitializeAds()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
                // LevelPlay (IronSource) does not support WebGL, so ads are disabled in the browser demo
                return;
        #endif

        #if UNITY_IOS
                _appKey = _iOSAppKey;
        #elif UNITY_ANDROID
                _appKey = _androidAppKey;
        #elif UNITY_EDITOR
                _appKey = _androidAppKey; 
        #endif

        // Optional: Still good practice for integration validation and debugging
        IronSource.Agent.validateIntegration();
        IronSource.Agent.setAdaptersDebug(_testMode); // Use _testMode for debug mode

        Debug.Log("unity-script: unity version " + IronSource.unityVersion()); // Still useful for logging

        // Subscribe to the new LevelPlay initialization callbacks
        LevelPlay.OnInitSuccess += OnInitializationComplete;
        LevelPlay.OnInitFailed += OnInitializationFailed;
        
        // Initialize LevelPlay using the new API
        Debug.Log($"LevelPlay SDK initializing with App Key: {_appKey}");
        LevelPlay.Init(_appKey, adFormats: new []{com.unity3d.mediation.LevelPlayAdFormat.REWARDED, com.unity3d.mediation.LevelPlayAdFormat.INTERSTITIAL});
    }

    void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks when this object is destroyed
        LevelPlay.OnInitSuccess -= OnInitializationComplete;
        LevelPlay.OnInitFailed -= OnInitializationFailed;
    }

    public void OnInitializationComplete(LevelPlayConfiguration config)
    {
        Debug.Log("LevelPlay SDK Initialization Complete. Config: " + config);
        // Unsubscribe to prevent multiple calls if OnInitializationComplete is called again for some reason
        LevelPlay.OnInitSuccess -= OnInitializationComplete; 

        doubleOfflineAdScript.LoadAd();

        if ( PlayerPrefs.GetFloat("EarnTimeRemaining") == 0) {
            doubleProductionAdScript.LoadAd();
        }
        
        if (PlayerPrefs.GetInt("ProvideAds") == 1) {
            StartCoroutine(interstitialAdsScript.AdTimer()); 
        }
    }
 
    // FIX HERE: Change error.Description to error directly
    public void OnInitializationFailed(LevelPlayInitError error) 
    {
        Debug.LogError($"LevelPlay SDK Initialization Failed: {error}"); // Line 90 corrected
    }
}