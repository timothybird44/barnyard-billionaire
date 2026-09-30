using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using TapjoyUnity;

public class earnMore : MonoBehaviour
{
    constants constantsScript;
    scoreTracking scoreTrackingScript;
    doubleProductionAd doubleProductionAdScript;
    // offerwallAds offerwallAdsScript;
    GameObject gmo_earnCanvas;
    AudioSource aud_purchaseSound, aud_buttonSound;  

    stage1 stage1Script;
    stage2 stage2Script;
    stage3 stage3Script;
    stage4 stage4Script;
    stage5 stage5Script;
    stage6 stage6Script;
    stage7 stage7Script;
    stage8 stage8Script;
    stage9 stage9Script;
    stage10 stage10Script;

    Text txt_waitTime, txt_doubleDescription;

    // Start is called before the first frame update
    void Start()
    {
        constantsScript = GameObject.Find("Score").GetComponent<constants>();
        scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();
        aud_purchaseSound = GameObject.Find("Purchase Sound").GetComponent<AudioSource>();
        aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();
        doubleProductionAdScript = GameObject.Find("btn_doubleProduction").GetComponent<doubleProductionAd>();
        // offerwallAdsScript = GameObject.Find("Ads Manager").GetComponent<offerwallAds>();
        txt_waitTime = GameObject.Find("txt_doubleWaitTime").GetComponent<Text>();
        txt_doubleDescription = GameObject.Find("txt_doubleDescription").GetComponent<Text>();

        stage1Script = GameObject.Find("Stage1").GetComponent<stage1>();
        stage2Script = GameObject.Find("Stage2").GetComponent<stage2>();
        stage3Script = GameObject.Find("Stage3").GetComponent<stage3>();
        stage4Script = GameObject.Find("Stage4").GetComponent<stage4>();
        stage5Script = GameObject.Find("Stage5").GetComponent<stage5>();
        stage6Script = GameObject.Find("Stage6").GetComponent<stage6>();
        stage7Script = GameObject.Find("Stage7").GetComponent<stage7>();
        stage8Script = GameObject.Find("Stage8").GetComponent<stage8>();
        stage9Script = GameObject.Find("Stage9").GetComponent<stage9>();
        stage10Script = GameObject.Find("Stage10").GetComponent<stage10>();
        
        gmo_earnCanvas = GameObject.Find("Earn Canvas");
        gmo_earnCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EarnMoreClicked() 
    {
        gmo_earnCanvas.SetActive(true);

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }

        if (PlayerPrefs.GetInt("ProvideAds") == 0) {
            txt_doubleDescription.text = "Double the production of your farm for 1 hour.";
        }
    }

    // public void OfferwallClicked()
    // {
    //     offerwallAdsScript.RequestOfferwall();
    // }

    public void CloseClicked() 
    {
        gmo_earnCanvas.SetActive(false);

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    void UpdateRewardUIs() 
    {
        stage1Script.UpdateRewardText();
        stage2Script.UpdateRewardText();
        stage3Script.UpdateRewardText();
        stage4Script.UpdateRewardText();
        stage5Script.UpdateRewardText();
        stage6Script.UpdateRewardText();
        stage7Script.UpdateRewardText();
        stage8Script.UpdateRewardText();
        stage9Script.UpdateRewardText();
        stage10Script.UpdateRewardText();
    }

    public IEnumerator ResetTime(float flt_elapsedTime = 0)
    {
        UpdateRewardUIs();

        while (flt_elapsedTime < constantsScript.flt_earnTimeReset)
        {
            flt_elapsedTime += Time.deltaTime;
            txt_waitTime.text = SimplifyTime(constantsScript.flt_earnTimeReset - flt_elapsedTime);
            PlayerPrefs.SetFloat("EarnTimeRemaining", constantsScript.flt_earnTimeReset-flt_elapsedTime);
            PlayerPrefs.Save();

            yield return null;
        }

        scoreTrackingScript.SetOtherMultiplier(constantsScript.flt_initialOtherMultiplier);
        PlayerPrefs.SetFloat("EarnTimeRemaining", 0);
        PlayerPrefs.Save();
        flt_elapsedTime = 0;
        txt_waitTime.text = "";
        UpdateRewardUIs();
        doubleProductionAdScript.LoadAd();
    }

    public string SimplifyTime(float flt_time) 
    {
        string str_return = "";
        if (flt_time < 0) {
            flt_time = 0;
        }

        if (flt_time < 60)
        {
            str_return = (int)flt_time + "s";
        }
        else if (flt_time < 3600)
        {
            int int_mins = (int)(flt_time / 60);
            int int_secs = (int)(flt_time - (int_mins * 60));
            str_return = int_mins + "m";
            if (int_secs != 0)
            {
                str_return += " " + int_secs + "s";
            }
        }
        else
        {
            int int_hours = (int)(flt_time / 3600);
            int int_mins = (int)((flt_time - (int_hours * 3600)) / 60);
            str_return = int_hours + "h";
            if (int_mins != 0)
            {
                str_return += " " + int_mins + "m";
            }
        }

        return str_return;
    }
}
