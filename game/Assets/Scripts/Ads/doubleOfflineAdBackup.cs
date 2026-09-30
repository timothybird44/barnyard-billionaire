// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Advertisements;
 
// public class doubleOfflineAdBackup : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
// {
//     [SerializeField] Button _showAdButton;
//     [SerializeField] Text txt_AdButton;
//     [SerializeField] string _androidAdUnitId = "Rewarded_Android";
//     [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
//     string _adUnitId = null; // This will remain null for unsupported platforms

//     offlineEarning offlineEarningScript;
//     AudioSource aud_purchaseSound, aud_buttonSound; 
//     Color clr_unavailable, clr_available; 
//     scoreTracking scoreTrackingScript;
 
//     void Awake()
//     {   
//         // Get the Ad Unit ID for the current platform:
//         #if UNITY_IOS
//                 _adUnitId = _iOSAdUnitId;
//         #elif UNITY_ANDROID
//                 _adUnitId = _androidAdUnitId;
//         #endif

//         offlineEarningScript = GameObject.Find("Offline Canvas").GetComponent<offlineEarning>();
//         aud_purchaseSound = GameObject.Find("Purchase Sound").GetComponent<AudioSource>();
//         aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();
//         scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();

//         clr_unavailable = scoreTrackingScript.HexToColor("767676");
//         clr_available = Color.white;

//         // Disable the button until the ad is ready to show:
//         if (PlayerPrefs.GetInt("ProvideAds") == 1) {
//             _showAdButton.interactable = false;
//             txt_AdButton.color = clr_unavailable;
//         }
//     }
 
//     // Call this public method when you want to get an ad ready to show.
//     public void LoadAd()
//     {
//         // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
//         Advertisement.Load(_adUnitId, this);
//     }
 
//     // If the ad successfully loads, add a listener to the button and enable it:
//     public void OnUnityAdsAdLoaded(string adUnitId)
//     {
//         if (adUnitId.Equals(_adUnitId))
//         {
//             // Configure the button to call the ShowAd() method when clicked:
//             _showAdButton.onClick.AddListener(ShowAd);
//             // Enable the button for users to click:
//             _showAdButton.interactable = true;
//             txt_AdButton.color = clr_available;
//         }
//     }
 
//     // Implement a method to execute when the user clicks the button:
//     public void ShowAd()
//     {
//         // Disable the button:
//         _showAdButton.interactable = false;
//         txt_AdButton.color = clr_unavailable;
//         // Play button sound effect
//         if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
//             aud_buttonSound.Play();
//         }
//         // Then show the ad:
//         if (PlayerPrefs.GetInt("ProvideAds") == 1) {
//             Advertisement.Show(_adUnitId, this);
//         }
//         else {
//             offlineEarningScript.OfflineCollectDoubleClicked();
//         }
//     }
 
//     // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
//     public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
//     {
//         if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
//         {
//             // Grant a reward. 
//             offlineEarningScript.OfflineCollectDoubleClicked();
//             OnDestroy();
//         }
//     }
 
//     // Implement Load and Show Listener error callbacks:
//     public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
//     {
//         // Use the error details to determine whether to try to load another ad.
//     }
 
//     public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
//     {
//         // Use the error details to determine whether to try to load another ad.
//     }
 
//     public void OnUnityAdsShowStart(string adUnitId) { }
//     public void OnUnityAdsShowClick(string adUnitId) { }
 
//     void OnDestroy()
//     {
//         // Clean up the button listeners:
//         _showAdButton.onClick.RemoveAllListeners();
//     }
// }