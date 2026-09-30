using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeBuyAmount : MonoBehaviour
{
    string str_buyAmount, str_buyAmountTag;
    AudioSource aud_buttonSound; 
    Text txt_buyAmount;

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

    // Start is called before the first frame update
    void Start()
    {
        str_buyAmount = "x1";
        str_buyAmountTag = "BuyAmount";
        aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();
        txt_buyAmount = GameObject.Find("txt_buyAmount").GetComponent<Text>();

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBuyAmount()
    {
        switch (str_buyAmount)
        {
            case "x1":
                str_buyAmount = "x10";
                break;
            case "x10":
                str_buyAmount = "Next";
                break;
            case "Next":
                str_buyAmount = "Max";
                break;
            case "Max":
                str_buyAmount = "x1";
                break;
        }

        UpdateBuyAmountUI();

        stage1Script.UpdatePurchaseCost(str_buyAmount);
        stage2Script.UpdatePurchaseCost(str_buyAmount);
        stage3Script.UpdatePurchaseCost(str_buyAmount);
        stage4Script.UpdatePurchaseCost(str_buyAmount);
        stage5Script.UpdatePurchaseCost(str_buyAmount);
        stage6Script.UpdatePurchaseCost(str_buyAmount);
        stage7Script.UpdatePurchaseCost(str_buyAmount);
        stage8Script.UpdatePurchaseCost(str_buyAmount);
        stage9Script.UpdatePurchaseCost(str_buyAmount);
        stage10Script.UpdatePurchaseCost(str_buyAmount);

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    void UpdateBuyAmountUI()
    {
        txt_buyAmount.text = str_buyAmount;
    }
}
