// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TapjoyUnity;

// public class offerwallAds : MonoBehaviour
// {
//     [SerializeField] string _androidSDKKey = "zcXytFYUSe6_1F037ZwDagECLOyOiZO81NiFrvZ04QSVAa6WiOsXecb3DkZc";
//     [SerializeField] string _iOsSDKKey = "3LIaIqNqSBmDYHhxqqX1nQEBqb6gZZMBD4QisjPyd25NFmPRLlGYNfplQsTc";

//     private string sdkKey;
    
//     TJPlacement earnGoldPlacement;
//     TJPlacement.OnContentReadyHandler readyHandler;
//     TJPlacement.OnContentDismissHandler dismissHandler;

//     void Awake()
//     {
//         #if UNITY_IOS
//         #if UNITY_IOS_AD_SUPPORT
//                 //Show ATT before connection, Only applicable to IOS
//                 if(ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == 
//                 ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
//                 {

//                     ATTrackingStatusBinding.RequestAuthorizationTracking();

//                 }
//         #endif
//                 // This sdk key will be only used when the auto connect mode disabled in Tapjoy window.
//                 sdkKey = _iOsSDKKey;
//         #elif UNITY_ANDROID
//                 sdkKey = _androidSDKKey;
//         #endif

//         // Connect delegates
//         Tapjoy.OnConnectSuccess += HandleConnectSuccess;
//         Tapjoy.OnConnectFailed += HandleConnectFailed;
//         Tapjoy.OnConnectWarning += HandleConnectWarning;

//         // Placement delegates
//         TJPlacement.OnRequestSuccess += HandlePlacementRequestSuccess;
//         TJPlacement.OnRequestFailure += HandlePlacementRequestFailure;
//         TJPlacement.OnContentShow += HandlePlacementContentShow;
//         TJPlacement.OnPurchaseRequest += HandleOnPurchaseRequest;
//         TJPlacement.OnRewardRequest += HandleOnRewardRequest;
//         TJPlacement.OnSetCurrencyBalanceSuccess += HandleSetCurrencyBalanceSuccess;
//         TJPlacement.OnSetCurrencyBalanceFailure += HandleSetCurrencyBalanceFailure;
//         TJPlacement.OnSetCurrencyAmountRequiredSuccess += HandleSetRequiredAmountSuccess;
//         TJPlacement.OnSetCurrencyAmountRequiredFailure += HandleSetRequiredAmountFailure;

//         // Currency delegates
//         Tapjoy.OnAwardCurrencyResponse += HandleAwardCurrencyResponse;
//         Tapjoy.OnAwardCurrencyResponseFailure += HandleAwardCurrencyResponseFailure;
//         Tapjoy.OnSpendCurrencyResponse += HandleSpendCurrencyResponse;
//         Tapjoy.OnSpendCurrencyResponseFailure += HandleSpendCurrencyResponseFailure;
//         Tapjoy.OnGetCurrencyBalanceResponse += HandleGetCurrencyBalanceResponse;
//         Tapjoy.OnGetCurrencyBalanceResponseFailure += HandleGetCurrencyBalanceResponseFailure;
//         Tapjoy.OnEarnedCurrency += HandleEarnedCurrency;
//     }

//     // Request connect if auto connect is not checked in the Unity Tapjoy editor.
//     public void RequestConnect()
//     {
//         Debug.Log($"SDK Key:{sdkKey}");
//         Dictionary<string, object> connectFlags = new Dictionary<string, object>();
//         connectFlags.Add("TJC_OPTION_ENABLE_LOGGING", "true");

//         Tapjoy.Connect(sdkKey, connectFlags);
//     }

//     public void RequestOfferwall()
//     {
//         Debug.Log(Tapjoy.IsConnected);
//         if (!Tapjoy.IsConnected) {
//             RequestConnect();
//         }
//         Debug.Log(Tapjoy.IsConnected);
//         Debug.Log("Requesting offerwall...");

//         earnGoldPlacement = TJPlacement.CreatePlacement("EarnGold");

//         readyHandler = (TJPlacement placement) =>
//         {
//             Debug.Log("Offerwall content is ready.");

//             earnGoldPlacement.ShowContent();
//             TJPlacement.OnContentReady -= readyHandler;
//         };
//         TJPlacement.OnContentReady += readyHandler;

//         dismissHandler = (TJPlacement placement) =>
//         {
//             Debug.Log($"Offerwall has been dismissed.");
//             TJPlacement.OnContentDismiss -= dismissHandler;
//         };
//         TJPlacement.OnContentDismiss += dismissHandler;

//         earnGoldPlacement.RequestContent();
//     }

//     #region Connect Delegate Handlers

//     void HandleConnectSuccess()
//     {
//         Debug.Log("Tapjoy Connected");
//     }

//     void HandleConnectFailed(int code, string message)
//     {
//         Debug.Log($"Tapjoy SDK failed to connect. Code: {code} Message: {message}");
//     }

//     void HandleConnectWarning(int code, string message)
//     {
//         Debug.Log($"Tapjoy SDK connect succeeded with warning Code: {code} Message: {message}");
//     }

//     #endregion

//     #region Placement Delegate Handlers

//     public void HandlePlacementRequestSuccess(TJPlacement placement)
//     {
//         if (placement.IsContentAvailable())
//         {
//             Debug.Log($"Content available for {placement.GetName()}");
//         }
//         else
//         {
//             Debug.Log($"No content available for {placement.GetName()}");
//         }
//     }

//     void HandlePlacementRequestFailure(TJPlacement placement, string error)
//     {
//         Debug.Log($"Request for {placement.GetName()} has failed because: {error}");
//     }

//     void HandlePlacementContentShow(TJPlacement placement)
//     {
//         Debug.Log("Handle placement content show");
//     }

//     void HandleOnPurchaseRequest(TJPlacement placement, TJActionRequest request, string productId)
//     {
//         Debug.Log("HandleOnPurchaseRequest");
//         request.Completed();
//     }

//     void HandleOnRewardRequest(TJPlacement placement, TJActionRequest request, string itemId, int quantity)
//     {
//         Debug.Log("HandleOnRewardRequest");
//         request.Completed();
//     }

//     void HandleSetCurrencyBalanceSuccess(TJPlacement placement)
//     {
//         // Debug.Log($"SetCurrencyBalance for {placement.GetName()} has succeeded. Balance: {placement.GetCurrencyBalance(currencyIdInput.text)}");
//     }

//     void HandleSetCurrencyBalanceFailure(TJPlacement placement, int code, string error)
//     {
//         Debug.Log($"SetCurrencyBalance for {placement.GetName()} has failed because: {error}(code: {code})");
//     }

//     void HandleSetRequiredAmountSuccess(TJPlacement placement)
//     {
//         // Debug.Log($"SetRequiredAmount for {placement.GetName()} has succeeded. Balance: {placement.GetRequiredAmount(currencyIdInput.text)}");
//     }

//     void HandleSetRequiredAmountFailure(TJPlacement placement, int code, string error)
//     {
//         Debug.Log($"SetRequiredAmount for {placement.GetName()} has failed because: {error}(code: {code})");
//     }

//     #endregion

//     #region Currency Delegate Handlers

//     void HandleAwardCurrencyResponse(string currencyName, int balance)
//     {
//         Debug.Log($"Awarded currency -- {currencyName} balance: {balance}");
//     }

//     void HandleAwardCurrencyResponseFailure(string error)
//     {
//         Debug.Log($"AwardCurrency failed: {error}");
//     }

//     void HandleGetCurrencyBalanceResponse(string currencyName, int balance)
//     {
//         Debug.Log($"{currencyName} balance: {balance}");
//     }

//     void HandleGetCurrencyBalanceResponseFailure(string error)
//     {
//         Debug.Log($"GetCurrencyBalance failed: {error}");
//     }

//     void HandleSpendCurrencyResponse(string currencyName, int balance)
//     {
//         Debug.Log($"{currencyName} balance: {balance}");
//     }

//     void HandleSpendCurrencyResponseFailure(string error)
//     {
//         Debug.Log($"SpendCurrency failed: {error}");
//     }

//     void HandleEarnedCurrency(string currencyName, int amount)
//     {
//         Debug.Log($"{currencyName} earned: {amount}");

//         Tapjoy.ShowDefaultEarnedCurrencyAlert();
//     }

//     #endregion

//     // public void HandleConnectSuccess()
//     // {
//     //     Debug.Log("Tapjoy Connect Success");

//     //     earnGoldPlacement = TJPlacement.CreatePlacement("EarnGold");
//     //     earnGoldPlacement.RequestContent();

//     //     earnGoldPlacement.OnRequestSuccess += OnContentReady;
//     //     earnGoldPlacement.OnRequestFailure += OnContentRequestFailure;
//     // }

//     // public void HandleConnectFailure()
//     // {
//     //     Debug.Log("Tapjoy Connect Failure");
//     // }

//     // public void OnContentReady(TJPlacement placement)
//     // {
//     //     Debug.Log("Tapjoy Content Ready: " + placement.GetName());
//     // }

//     // public void OnContentRequestFailure(TJPlacement placement, string error)
//     // {
//     //     Debug.Log("Tapjoy Content Request Failed: " + placement.GetName() + " - " + error);
//     // }

//     // public void OnContentDismiss(TJPlacement placement)
//     // {
//     //     Debug.Log("Tapjoy Content Dismissed: " + placement.GetName());
//     //     placement.RequestContent(); // Request new content after dismiss
//     // }

//     // public void LoadOfferWall()
//     // {
//     //     if (earnGoldPlacement != null && earnGoldPlacement.IsContentReady())
//     //     {
//     //         earnGoldPlacement.ShowContent();
//     //     }
//     //     else
//     //     {
//     //         Debug.Log("Tapjoy Content Not Ready");
//     //     }
//     // }
// }