using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtendedNumerics;

public class stage4 : MonoBehaviour
{
    constants constantsScript;

    int int_baseRewardAmount, int_level, int_subLevel, int_maxSubLevel,
        int_buyAmount;
    string str_globalBuyAmount;
    BigDecimal dec_currentScore, dec_rewardAmount, flt_purchaseCost, flt_basePurchaseCost,
        flt_tempBasePurchaseCost;
    float flt_rewardTime, flt_baseRewardTime,
        flt_costMultiplier, flt_unlockCost, flt_rewardTimeMultiplier;
    scoreTracking scoreTrackingScript;
    managers managersScript;
    public bool bool_canCollect, bool_hasManager, bool_isUnlocked;
    Text txt_rewardTime, txt_level, txt_buy, txt_price, txt_amount, txt_reward, txt_unlock;
    Slider sld_progress;
    Button btn_buyButton, btn_collectButton;
    Color clr_unavailable, clr_available;
    GameObject gmo_unlock;
    public Coroutine cor_rewardCoroutine;
    AudioSource aud_purchaseSound, aud_buttonSound;  


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HasPlayedBefore")) {
            constantsScript = GameObject.Find("Score").GetComponent<constants>();
            scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();
            managersScript = GameObject.Find("Managers Canvas").GetComponent<managers>();
            clr_unavailable = scoreTrackingScript.HexToColor("767676");
            clr_available = Color.white;
            aud_purchaseSound = GameObject.Find("Purchase Sound").GetComponent<AudioSource>();
            aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();

            txt_rewardTime = GameObject.Find("txt_s4rewardTime").GetComponent<Text>();
            txt_level = GameObject.Find("txt_s4level").GetComponent<Text>();
            txt_buy = GameObject.Find("txt_s4buy").GetComponent<Text>();
            txt_price = GameObject.Find("txt_s4price").GetComponent<Text>();
            txt_amount = GameObject.Find("txt_s4amount").GetComponent<Text>();
            txt_reward = GameObject.Find("txt_s4reward").GetComponent<Text>();
            txt_unlock = GameObject.Find("txt_s4unlock").GetComponent<Text>();
            sld_progress = GameObject.Find("sld_s4progress").GetComponent<Slider>();
            btn_buyButton = GameObject.Find("btn_s4buy").GetComponent<Button>();
            btn_collectButton = GameObject.Find("btn_s4collect").GetComponent<Button>();
            gmo_unlock = GameObject.Find("btn_s4unlock");

            flt_unlockCost = constantsScript.flt_initialS4UnlockCost;
            flt_basePurchaseCost = flt_unlockCost;
            flt_tempBasePurchaseCost = flt_basePurchaseCost;
            flt_purchaseCost = flt_basePurchaseCost;
            int_baseRewardAmount = PlayerPrefs.GetInt("S4BaseRewardAmount");
            dec_rewardAmount = BigDecimal.Parse(int_baseRewardAmount.ToString());
            flt_baseRewardTime = PlayerPrefs.GetFloat("S4BaseRewardTime");
            flt_rewardTime = flt_baseRewardTime;
            flt_rewardTimeMultiplier = constantsScript.flt_initialRewardTimeMultiplier;
            flt_costMultiplier = constantsScript.flt_initialS4CostMultiplier;
            int_level = constantsScript.int_initialLevel;
            int_buyAmount = constantsScript.int_initialBuyAmount;
            int_subLevel = constantsScript.int_intialSubLevel;
            int_maxSubLevel = constantsScript.int_initialMaxSubLevel;
            bool_canCollect = false;
            bool_hasManager = false;
            bool_isUnlocked = false;
            str_globalBuyAmount = constantsScript.str_initialBuyAmount;
            btn_buyButton.interactable = false;
            txt_unlock.text = "Unlock\n$" + scoreTrackingScript.SimplifyScore(flt_unlockCost);
            txt_price.text = "$" + scoreTrackingScript.SimplifyScore(flt_unlockCost);
            txt_rewardTime.text = SimplifyRewardTime(flt_rewardTime);
            sld_progress.value = 0;

            UpdateRewardText();
        }
    }

    public void Init() {
        constantsScript = GameObject.Find("Score").GetComponent<constants>();
        scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();
        managersScript = GameObject.Find("Managers Canvas").GetComponent<managers>();
        clr_unavailable = scoreTrackingScript.HexToColor("767676");
        clr_available = Color.white;
        aud_purchaseSound = GameObject.Find("Purchase Sound").GetComponent<AudioSource>();
        aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();

        txt_rewardTime = GameObject.Find("txt_s4rewardTime").GetComponent<Text>();
        txt_level = GameObject.Find("txt_s4level").GetComponent<Text>();
        txt_buy = GameObject.Find("txt_s4buy").GetComponent<Text>();
        txt_price = GameObject.Find("txt_s4price").GetComponent<Text>();
        txt_amount = GameObject.Find("txt_s4amount").GetComponent<Text>();
        txt_reward = GameObject.Find("txt_s4reward").GetComponent<Text>();
        txt_unlock = GameObject.Find("txt_s4unlock").GetComponent<Text>();
        sld_progress = GameObject.Find("sld_s4progress").GetComponent<Slider>();
        btn_buyButton = GameObject.Find("btn_s4buy").GetComponent<Button>();
        btn_collectButton = GameObject.Find("btn_s4collect").GetComponent<Button>();
        gmo_unlock = GameObject.Find("btn_s4unlock");

        flt_unlockCost = constantsScript.flt_initialS4UnlockCost;
        flt_basePurchaseCost = flt_unlockCost;
        flt_tempBasePurchaseCost = flt_basePurchaseCost;
        flt_purchaseCost = flt_basePurchaseCost;
        int_baseRewardAmount = PlayerPrefs.GetInt("S4BaseRewardAmount");
        dec_rewardAmount = BigDecimal.Parse(int_baseRewardAmount.ToString());
        flt_baseRewardTime = PlayerPrefs.GetFloat("S4BaseRewardTime");
        flt_rewardTime = flt_baseRewardTime;
        flt_rewardTimeMultiplier = constantsScript.flt_initialRewardTimeMultiplier;
        flt_costMultiplier = constantsScript.flt_initialS4CostMultiplier;
        int_level = constantsScript.int_initialLevel;
        int_buyAmount = constantsScript.int_initialBuyAmount;
        int_subLevel = constantsScript.int_intialSubLevel;
        int_maxSubLevel = constantsScript.int_initialMaxSubLevel;
        bool_canCollect = false;
        bool_hasManager = false;
        bool_isUnlocked = false;
        str_globalBuyAmount = constantsScript.str_initialBuyAmount;
        btn_buyButton.interactable = false;
        txt_unlock.text = "Unlock\n$" + scoreTrackingScript.SimplifyScore(flt_unlockCost);
        txt_price.text = "$" + scoreTrackingScript.SimplifyScore(flt_unlockCost);
        txt_rewardTime.text = SimplifyRewardTime(flt_rewardTime);
        sld_progress.value = 0;

        UpdateRewardText();
    }

    // Update is called once per frame
    void Update()
    {
        dec_currentScore = scoreTrackingScript.GetScore();

        // Check if buy amount is "Max" and update the price and buy amount
        if (str_globalBuyAmount == "Max")
        {
            int_buyAmount = 0;
            flt_purchaseCost = flt_basePurchaseCost;
            while (flt_purchaseCost <= dec_currentScore)
            {
                CalculateCost(int_buyAmount);
                int_buyAmount++;
            }

            int_buyAmount -= 2;
            if (int_buyAmount < 0)
            {
                int_buyAmount = 0;
            }
            CalculateCost(int_buyAmount);

            txt_price.text = "$" + scoreTrackingScript.SimplifyScore(BigDecimal.Round(flt_purchaseCost, 2));
            txt_amount.text = "x" + int_buyAmount;
        }

        if (!bool_isUnlocked)
        {
            if (dec_currentScore >= flt_unlockCost)
            {
                txt_unlock.color = clr_available;
            }
            else
            {
                txt_unlock.color = clr_unavailable;
            }
        }

        // Grey out the box if purchase can't be made
        if ((dec_currentScore >= flt_purchaseCost) && (int_buyAmount != 0))
        {
            btn_buyButton.interactable = true;
            txt_buy.color = clr_available;
            txt_price.color = clr_available;
            txt_amount.color = clr_available;
        }
        else
        {
            btn_buyButton.interactable = false;
            txt_buy.color = clr_unavailable;
            txt_price.color = clr_unavailable;
            txt_amount.color = clr_unavailable;
        }

        // Collect automatically if manager has been purchased
        if ((bool_canCollect) && (bool_hasManager) && (int_level > 0) && (cor_rewardCoroutine == null) && (PlayerPrefs.GetInt("Loaded") == 1))
        {
            bool_canCollect = false;
            cor_rewardCoroutine = StartCoroutine(WaitRewardTime());
        }

    }

    public void InitiateStage(int int_stageLevel, int int_stageManager, int int_stageManagerReward1, int int_stageManagerTime1) {
        for (int i = 0; i < int_stageLevel; i++) 
        {
            scoreTrackingScript.AddScore(flt_purchaseCost);
            if (i == 0) 
            {
                UnlockStageClicked();
            }
            else {
                BuyButtonClicked();
            }
        }

        if (int_subLevel == 0) {
            bool_canCollect = true;
        }

        if (int_stageManager == 1) {
            scoreTrackingScript.AddScore(constantsScript.flt_initialM4Cost);
            managersScript.BuyManager4Clicked();
        }
        if (int_stageManagerReward1 == 1) {
            scoreTrackingScript.AddScore(constantsScript.flt_initialM14Cost);
            managersScript.BuyManager14Clicked();
        }
        if (int_stageManagerTime1 == 1) {
            scoreTrackingScript.AddScore(constantsScript.flt_initialM24Cost);
            managersScript.BuyManager24Clicked();
        }
    }

    public void UpdateRewardText() {
        if (int_level == 0) {
            txt_reward.text = "$0";
        }
        else {
            dec_rewardAmount = BigDecimal.Parse(int_baseRewardAmount * int_subLevel *
                scoreTrackingScript.GetPrestigeMultiplier() * scoreTrackingScript.GetOtherMultiplier() *
                scoreTrackingScript.GetS4Multiplier());

            if ((flt_rewardTime <= 0.2f) && (bool_hasManager)) {
                txt_reward.text = "$" + scoreTrackingScript.SimplifyScore(GetPerSecond()) + " /sec";
            }
            else {
                txt_reward.text = "$" + scoreTrackingScript.SimplifyScore(dec_rewardAmount);
            }
        }
    }

    public BigDecimal GetRewardAmount() {
        return dec_rewardAmount;
    }

    public float GetRewardTime() {
        return flt_rewardTime;
    }

    public BigDecimal GetPerSecond() {
        return BigDecimal.Parse((dec_rewardAmount / flt_rewardTime).ToString());
    }

    public int GetLevel()
    {
        return int_subLevel;
    }

    public void UpdatePurchaseCost(string str_buyAmount)
    {
        str_globalBuyAmount = str_buyAmount;

        switch (str_buyAmount)
        {
            case "x1":
                int_buyAmount = 1;
                CalculateCost(int_buyAmount);
                break;
            case "x10":
                int_buyAmount = 10;
                CalculateCost(int_buyAmount);
                break;
            case "Next":
                int int_nextLevel = int_maxSubLevel * int_level;
                int_buyAmount = int_nextLevel - int_subLevel;
                if (int_buyAmount < 0)
                {
                    int_buyAmount = 0;
                }
                CalculateCost(int_buyAmount);
                break;
            case "Max":
                int_buyAmount = 0;
                flt_purchaseCost = flt_basePurchaseCost;
                while (flt_purchaseCost <= dec_currentScore)
                {
                    CalculateCost(int_buyAmount);
                    int_buyAmount++;
                }

                int_buyAmount -= 2;
                if (int_buyAmount < 0)
                {
                    int_buyAmount = 0;
                }
                CalculateCost(int_buyAmount);
                break;
        }

        txt_price.text = "$" + scoreTrackingScript.SimplifyScore(BigDecimal.Round(flt_purchaseCost, 2));
        txt_amount.text = "x" + int_buyAmount;
    }

    void CalculateCost(int int_buyAmount)
    {
        BigDecimal flt_tempPurchaseCost = flt_basePurchaseCost;
        flt_tempBasePurchaseCost = flt_basePurchaseCost;

        if (int_buyAmount == 1)
        {
            flt_purchaseCost = flt_tempBasePurchaseCost;
            flt_tempBasePurchaseCost *= flt_costMultiplier;
        }
        else
        { 
            for (int i = 1; i < int_buyAmount; i++)
            {
                flt_tempBasePurchaseCost *= flt_costMultiplier;
                flt_tempPurchaseCost += flt_tempBasePurchaseCost;
            }

            flt_purchaseCost = flt_tempPurchaseCost;
        }
    }

    public string SimplifyRewardTime(float flt_time) {
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

    public void BuyButtonClicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if (dec_currentScore >= flt_purchaseCost)
        {
            scoreTrackingScript.MinusScore(flt_purchaseCost);
            int_subLevel += int_buyAmount;
            PlayerPrefs.SetInt("Stage4Level", GetLevel());
            PlayerPrefs.Save();
            int_level = (int_subLevel / int_maxSubLevel) + 1;

            txt_level.text = scoreTrackingScript.SimplifyScore(int_subLevel) + " / " + scoreTrackingScript.SimplifyScore(int_level * int_maxSubLevel);
            UpdateRewardText();
            flt_rewardTime = flt_baseRewardTime * Mathf.Pow(flt_rewardTimeMultiplier, (int_level - 1));
            txt_rewardTime.text = SimplifyRewardTime(flt_rewardTime);

            flt_basePurchaseCost = flt_tempBasePurchaseCost;
            UpdatePurchaseCost(str_globalBuyAmount);

            // Play purchase sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_purchaseSound.Play();
            }
        }
    }

    public void CollectButtonClicked()
    {
        if (bool_canCollect)
        {
            bool_canCollect = false;
            cor_rewardCoroutine = StartCoroutine(WaitRewardTime());

            // Play button sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_buttonSound.Play();
            }
        }
    }

    public void PurchaseManager()
    {
        bool_hasManager = true;
        PlayerPrefs.SetInt("Stage4Manager", 1);
        PlayerPrefs.Save();
        if (int_level > 0) {
            UpdateRewardText();
        }
    }

    public void PurchaseManagerReward()
    {
        PlayerPrefs.SetInt("S4BaseRewardAmount", (PlayerPrefs.GetInt("S4BaseRewardAmount") * 2));
        PlayerPrefs.Save();
        int_baseRewardAmount = PlayerPrefs.GetInt("S4BaseRewardAmount");
        if (int_level > 0) {
            UpdateRewardText();
        }
    }

    public void PurchaseManagerTime()
    {
        PlayerPrefs.SetFloat("S4BaseRewardTime", (PlayerPrefs.GetFloat("S4BaseRewardTime") / 2));
        PlayerPrefs.Save();
        flt_rewardTime = PlayerPrefs.GetFloat("S4BaseRewardTime");
        if (PlayerPrefs.GetFloat("Stage4Progress") == 0) {
            txt_rewardTime.text = SimplifyRewardTime(flt_rewardTime);
        }
    }

    public bool GetManager() {
        return bool_hasManager;
    }

    public void PurchaseUpgrade()
    {
        UpdateRewardText();
    }

    public void StartRewardCoroutine(string str_elapsedTime = "0") {
        cor_rewardCoroutine = StartCoroutine(WaitRewardTime(str_elapsedTime));
    }

    IEnumerator WaitRewardTime(string str_elapsedTime = "0")
    {
        bool_canCollect = false;
        BigDecimal elapsedTime = BigDecimal.Parse(str_elapsedTime);
        btn_collectButton.interactable = false;

        while (elapsedTime < flt_rewardTime)
        {
            if ((flt_rewardTime <= 0.2f) && (bool_hasManager)) {
                sld_progress.value = 1;
            }
            else {
                float progress = Mathf.Clamp01(float.Parse((elapsedTime / flt_rewardTime).ToString()));
                sld_progress.value = progress;
            }

            elapsedTime += BigDecimal.Parse(Time.deltaTime.ToString());
            txt_rewardTime.text = SimplifyRewardTime(float.Parse((flt_rewardTime - elapsedTime).ToString()));
            PlayerPrefs.SetFloat("Stage4Progress", float.Parse(elapsedTime.ToString()));
            PlayerPrefs.Save();

            yield return null;
        }

        // Single, consistent reset point
        sld_progress.value = 0;

        Canvas.ForceUpdateCanvases();
        PlayerPrefs.SetFloat("Stage4Progress", 0);
        PlayerPrefs.Save();
        txt_rewardTime.text = SimplifyRewardTime(flt_rewardTime);
        if (PlayerPrefs.GetInt("Stage4Level") > 0) {
            scoreTrackingScript.AddScore(GetRewardAmount());
        }  
        if (!bool_hasManager) {
            btn_collectButton.interactable = true;
        }

        bool_canCollect = true;
        cor_rewardCoroutine = null;
        yield return null;
    }

    public void UnlockStageClicked()
    {
        dec_currentScore = scoreTrackingScript.GetScore();
        if ((!bool_isUnlocked) && (dec_currentScore >= flt_unlockCost))
        {
            gmo_unlock.SetActive(false);
            string str_tempBuyAmount = str_globalBuyAmount;
            UpdatePurchaseCost("x1");
            BuyButtonClicked();
            UpdatePurchaseCost(str_tempBuyAmount);

            if (!bool_hasManager) {
                bool_canCollect = true;
            }
        }
    }

    public void Prestige()
    {
        gmo_unlock.SetActive(true);
        if (cor_rewardCoroutine != null) {
            StopCoroutine(cor_rewardCoroutine);
        }
        sld_progress.value = 0;
        btn_collectButton.interactable = true;
        PlayerPrefs.SetInt("Stage4Level", 0);
        PlayerPrefs.SetInt("Stage4Manager", 0);
        PlayerPrefs.SetInt("S4BaseRewardAmount", (int)constantsScript.flt_initialS4UnlockCost);
        PlayerPrefs.SetFloat("S4BaseRewardTime", constantsScript.flt_initialS4RewardTime);
        PlayerPrefs.Save();
        Start();
    }
}
