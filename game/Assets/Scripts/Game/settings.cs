using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    GameObject gmo_settingsCanvas, gmo_menuItems;
    scoreTracking scoreTrackingScript;
    Color clr_error, clr_success, clr_unavailable, clr_available;
    AudioSource aud_backgroundMusic, aud_buttonSound; 
    Button btn_adsSetting;

    Text txt_musicSetting, txt_effectsSetting, txt_adsSetting, txt_referralCode;

    #if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void IOS_Share(string text);
    #endif

    // Start is called before the first frame update
    void Start()
    {
        scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();
        clr_unavailable = scoreTrackingScript.HexToColor("767676");
        clr_available = Color.white;
        gmo_settingsCanvas = GameObject.Find("Settings Canvas");
        gmo_menuItems = GameObject.Find("Hamburger Menu Items");
        aud_backgroundMusic = GameObject.Find("Background Music").GetComponent<AudioSource>();
        aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();

        btn_adsSetting = GameObject.Find("btn_adsSetting").GetComponent<Button>();
        
        txt_musicSetting = GameObject.Find("txt_musicSetting").GetComponent<Text>();
        txt_effectsSetting = GameObject.Find("txt_effectsSetting").GetComponent<Text>();
        txt_adsSetting = GameObject.Find("txt_adsSetting").GetComponent<Text>();
        txt_referralCode = GameObject.Find("txt_referralCodeTitle").GetComponent<Text>();
        
        if (PlayerPrefs.GetInt("MusicAudio") == 0) {
            txt_musicSetting.text = "Off";
        }
        if (PlayerPrefs.GetInt("EffectsAudio") == 0) {
            txt_effectsSetting.text = "Off";
        }
        
        gmo_settingsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("ProvideAds") == 0) {
            btn_adsSetting.interactable = false;
            txt_adsSetting.color = clr_unavailable;
        }
        else {
            btn_adsSetting.interactable = true;
            txt_adsSetting.color = clr_available;

            if (PlayerPrefs.GetInt("AdFrequency") == 180) {
                txt_adsSetting.text = "3 min";
            }
            else if (PlayerPrefs.GetInt("AdFrequency") == 120) {
                txt_adsSetting.text = "2 min";
            }
            else if (PlayerPrefs.GetInt("AdFrequency") == 60) {
                txt_adsSetting.text = "1 min";
            }
            else if (PlayerPrefs.GetInt("AdFrequency") == 30) {
                txt_adsSetting.text = "30 sec";
            }
        }
    }

    public void SettingsOpenClicked()
    {
        txt_referralCode.text = "Referral Code: " + PlayerPrefs.GetString("ReferralCode");

        gmo_settingsCanvas.SetActive(true);
        gmo_menuItems.SetActive(false);

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    public void SettingsCloseClicked()
    {
        gmo_settingsCanvas.SetActive(false);

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    public void MusicSettingClicked()
    {
        if (PlayerPrefs.GetInt("MusicAudio") == 1) {
            txt_musicSetting.text = "Off";
            PlayerPrefs.SetInt("MusicAudio", 0);
            aud_backgroundMusic.Stop();

            // Play button sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_buttonSound.Play();
            }
        }
        else {
            txt_musicSetting.text = "On";
            PlayerPrefs.SetInt("MusicAudio", 1);
            aud_backgroundMusic.Play();

            // Play button sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_buttonSound.Play();
            }
        }
    }

    public void EffectsSettingClicked()
    {
        if (PlayerPrefs.GetInt("EffectsAudio") == 1) {
            txt_effectsSetting.text = "Off";
            PlayerPrefs.SetInt("EffectsAudio", 0);
        }
        else {
            txt_effectsSetting.text = "On";
            PlayerPrefs.SetInt("EffectsAudio", 1);

            // Play button sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_buttonSound.Play();
            }
        }
    }

    public void AdsSettingClicked() 
    {
        if (PlayerPrefs.GetInt("AdFrequency") == 180) {
            txt_adsSetting.text = "30 sec";
            PlayerPrefs.SetInt("AdFrequency", 30);
        }
        else if (PlayerPrefs.GetInt("AdFrequency") == 120) {
            txt_adsSetting.text = "3 min";
            PlayerPrefs.SetInt("AdFrequency", 180);
        }
        else if (PlayerPrefs.GetInt("AdFrequency") == 60) {
            txt_adsSetting.text = "2 min";
            PlayerPrefs.SetInt("AdFrequency", 120);
        }
        else if (PlayerPrefs.GetInt("AdFrequency") == 30) {
            txt_adsSetting.text = "1 min";
            PlayerPrefs.SetInt("AdFrequency", 60);
        }
    }

    public void ShareReferralClicked()
    {
        string str_referralCode = PlayerPrefs.GetString("ReferralCode");
        string textToShare = "Come check out Barnyard Billionaire. You can earn real rewards every week! Make sure to use my referral code when you first start: " + str_referralCode + ". https://barnyardbillionaire.com/";
        #if UNITY_ANDROID
            AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", "android.intent.action.SEND");
            intent.Call<AndroidJavaObject>("setType", "text/plain");
            intent.Call<AndroidJavaObject>("putExtra", "android.content.Intent.EXTRA_SUBJECT", "Referral Code"); //Subject
            intent.Call<AndroidJavaObject>("putExtra", "android.content.Intent.EXTRA_TEXT", textToShare);
            AndroidJavaObject chooser = AndroidIntent.CreateChooser(intent, "Share Referral Code"); //Title
            currentActivity.Call("startActivity", chooser);
        #elif UNITY_IOS
            #if UNITY_EDITOR
                Debug.Log("Sharing on iOS would happen here (Editor). Code: " + textToShare);
                // You could add some Editor-specific testing logic here if needed.
            #else
                IOS_Share(textToShare);
            #endif
        #else
            // Fallback for other platforms (e.g., for testing in the Unity Editor)
            Debug.Log("Sharing not supported on this platform.  Code to share: " + textToShare);
        #endif
    }

    public class AndroidIntent
    {
        private static AndroidJavaClass intentClass;
        private static AndroidJavaObject chooser;

        public static AndroidJavaObject CreateChooser(AndroidJavaObject intent, string title)
        {
            if (intentClass == null)
                intentClass = new AndroidJavaClass("android.content.Intent");
            chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intent, title);
            return chooser;
        }
    }

    public void OpenWebsiteClicked() {
        Application.OpenURL("https://www.barnyardbillionaire.com");
    }

    public void OpenTwitterClicked() {
        Application.OpenURL("https://www.x.com/BarnyardBillion");
    }

    public void OpenInstagramClicked() {
        Application.OpenURL("https://www.instagram.com/barnyardbillion/");
    }
}
