// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Advertisements;

// public class adsInitialiserBackup : MonoBehaviour, IUnityAdsInitializationListener
// {
//     [SerializeField] string _androidGameId;
//     [SerializeField] string _iOSGameId;
//     [SerializeField] bool _testMode = true;
//     private string _gameId;

//     doubleOfflineAd doubleOfflineAdScript;
//     doubleProductionAd doubleProductionAdScript;
//     interstitialAds interstitialAdsScript;
 
//     void Awake()
//     {
//         doubleOfflineAdScript = GameObject.Find("btn_offlineCollectDouble").GetComponent<doubleOfflineAd>();
//         doubleProductionAdScript = GameObject.Find("btn_doubleProduction").GetComponent<doubleProductionAd>();
//         interstitialAdsScript = GameObject.Find("Ads Manager").GetComponent<interstitialAds>();

//         InitializeAds();
//     }
 
//     public void InitializeAds()
//     {
//         #if UNITY_IOS
//                 _gameId = _iOSGameId;
//         #elif UNITY_ANDROID
//                 _gameId = _androidGameId;
//         #elif UNITY_EDITOR
//                 _gameId = _androidGameId; //Only for testing the functionality in the Editor
//         #endif

//         if (!Advertisement.isInitialized && Advertisement.isSupported)
//         {
//             Advertisement.Initialize(_gameId, _testMode, this);
//         }
//         if (Advertisement.isInitialized && Advertisement.isSupported)
//         {
//             OnInitializationComplete();
//         }
//     }

 
//     public void OnInitializationComplete()
//     {
//         doubleOfflineAdScript.LoadAd();

//         if ( PlayerPrefs.GetFloat("EarnTimeRemaining") == 0) {
//             doubleProductionAdScript.LoadAd();
//         }
        
//         if (PlayerPrefs.GetInt("ProvideAds") == 1) {
//             StartCoroutine(interstitialAdsScript.AdTimer());
//         }
//     }
 
//     public void OnInitializationFailed(UnityAdsInitializationError error, string message)
//     {
//     }
// }
