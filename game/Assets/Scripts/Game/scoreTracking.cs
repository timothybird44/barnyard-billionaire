using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtendedNumerics;

public class scoreTracking : MonoBehaviour
{
    constants constantsScript;
    earnMore earnMoreScript;
    doubleProductionAd doubleProductionAdScript;
    scoreSubmission scoreSubmissionScript;

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

    BigDecimal dec_score, int_prestigeCost, dec_awayTime;
    float flt_prestigeBonus, flt_prestigeMultiplier, flt_otherMultiplier,
        flt_s1Multiplier, flt_s2Multiplier, flt_s3Multiplier, flt_s4Multiplier, flt_s5Multiplier,
        flt_s6Multiplier, flt_s7Multiplier, flt_s8Multiplier, flt_s9Multiplier, flt_s10Multiplier;
    int int_gold, int_goldBonus;
    string str_scoreTag, str_goldTag;
    Text txt_score;
    GameObject[] arr_scoreObjects, arr_goldObjects;

    Image img_earnMore;
    Color clr_earnButtonStart, clr_earnButtonEnd;
    float flt_pulseSpeed, flt_timer;

    // Start is called before the first frame update
    void Start()
    {
        constantsScript = GameObject.Find("Score").GetComponent<constants>();
        earnMoreScript = GameObject.Find("Earn Canvas").GetComponent<earnMore>();
        doubleProductionAdScript = GameObject.Find("btn_doubleProduction").GetComponent<doubleProductionAd>();
        scoreSubmissionScript = GameObject.Find("Score Submit Canvas").GetComponent<scoreSubmission>();

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

        dec_score = BigDecimal.Parse(constantsScript.str_initialScore);
        int_gold = constantsScript.int_initialGold;
        int_goldBonus = constantsScript.int_initialGoldBonus;
        int_prestigeCost = constantsScript.int_initialPrestigeCost;
        flt_prestigeBonus = constantsScript.flt_prestigeBonus;
        flt_prestigeMultiplier = constantsScript.flt_initialPrestigeMultiplier;
        flt_otherMultiplier = constantsScript.flt_initialOtherMultiplier;

        flt_s1Multiplier = constantsScript.flt_initialS1RewardMultiplier;
        flt_s2Multiplier = constantsScript.flt_initialS2RewardMultiplier;
        flt_s3Multiplier = constantsScript.flt_initialS3RewardMultiplier;
        flt_s4Multiplier = constantsScript.flt_initialS4RewardMultiplier;
        flt_s5Multiplier = constantsScript.flt_initialS5RewardMultiplier;
        flt_s6Multiplier = constantsScript.flt_initialS6RewardMultiplier;
        flt_s7Multiplier = constantsScript.flt_initialS7RewardMultiplier;
        flt_s8Multiplier = constantsScript.flt_initialS8RewardMultiplier;
        flt_s9Multiplier = constantsScript.flt_initialS9RewardMultiplier;
        flt_s10Multiplier = constantsScript.flt_initialS10RewardMultiplier;

        str_scoreTag = "ScoreText";
        arr_scoreObjects = GameObject.FindGameObjectsWithTag(str_scoreTag);
        str_goldTag = "GoldText";
        arr_goldObjects = GameObject.FindGameObjectsWithTag(str_goldTag);
        UpdateScoreUI();
        UpdateGoldUI();

        img_earnMore = GameObject.Find("btn_earnMore").GetComponent<Image>();
        clr_earnButtonStart = Color.white;
        clr_earnButtonEnd = HexToColor("FF8D8D");
        flt_pulseSpeed = 2f;
        flt_timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetFloat("EarnTimeRemaining") == 0) {
            flt_timer += Time.deltaTime * flt_pulseSpeed; 
            float t = Mathf.PingPong(flt_timer, 1f);
            img_earnMore.color = Color.Lerp(clr_earnButtonStart, clr_earnButtonEnd, t);
        }
        else {
            img_earnMore.color = clr_earnButtonStart;
        }
    }

    public IEnumerator InitiateScoreTracking(string str_setScore, int int_setGold, int int_setGoldBonus,
        string str_setPrestigeCost, float flt_setPrestigeBonus, float flt_setPrestigeMultiplier,
        float flt_setOtherMultiplier, float flt_setS1Multiplier, float flt_setS2Multiplier,
        float flt_setS3Multiplier, float flt_setS4Multiplier, float flt_setS5Multiplier,
        float flt_setS6Multiplier, float flt_setS7Multiplier, float flt_setS8Multiplier,
        float flt_setS9Multiplier, float flt_setS10Multiplier)
    {
        dec_score = BigDecimal.Parse(str_setScore);
        int_gold = int_setGold;
        int_goldBonus = int_setGoldBonus;
        int_prestigeCost = BigDecimal.Parse(str_setPrestigeCost);
        flt_prestigeBonus = flt_setPrestigeBonus;
        flt_prestigeMultiplier = flt_setPrestigeMultiplier;
        flt_otherMultiplier = flt_setOtherMultiplier;

        flt_s1Multiplier = flt_setS1Multiplier;
        flt_s2Multiplier = flt_setS2Multiplier;
        flt_s3Multiplier = flt_setS3Multiplier;
        flt_s4Multiplier = flt_setS4Multiplier;
        flt_s5Multiplier = flt_setS5Multiplier;
        flt_s6Multiplier = flt_setS6Multiplier;
        flt_s7Multiplier = flt_setS7Multiplier;
        flt_s8Multiplier = flt_setS8Multiplier;
        flt_s9Multiplier = flt_setS9Multiplier;
        flt_s10Multiplier = flt_setS10Multiplier;

        UpdateScoreUI();
        UpdateGoldUI();

        while (PlayerPrefs.GetInt("Loaded") == 0) {
            yield return new WaitForSeconds(0.2f);
        }

        dec_awayTime = float.Parse(PlayerPrefs.GetString("AwayTime"));

        if (PlayerPrefs.GetFloat("EarnTimeRemaining") > dec_awayTime) {
            float flt_earnTime = (float)(BigDecimal.Parse(PlayerPrefs.GetFloat("EarnTimeRemaining").ToString()) - dec_awayTime);
            StartCoroutine(earnMoreScript.ResetTime(constantsScript.flt_earnTimeReset - flt_earnTime));
        }
        else {
            SetOtherMultiplier(constantsScript.flt_initialOtherMultiplier);
            doubleProductionAdScript.LoadAd();
        }
        
        if (PlayerPrefs.GetFloat("ScoreSaveCooldownRemaining") > dec_awayTime) {
            float flt_scoreCooldownTime = (float)(BigDecimal.Parse(PlayerPrefs.GetFloat("ScoreSaveCooldownRemaining").ToString()) - dec_awayTime);
            StartCoroutine(scoreSubmissionScript.ScoreResetTime(constantsScript.flt_scoreSaveCooldown - flt_scoreCooldownTime));
        }
        else {
            PlayerPrefs.SetFloat("ScoreSaveCooldownRemaining", 0);
            PlayerPrefs.Save();
            scoreSubmissionScript.CheckScoreSubmitAvailability();
        }

        if (PlayerPrefs.GetFloat("WalletSaveCooldownRemaining") > dec_awayTime) {
            float flt_walletCooldownTime = (float)(BigDecimal.Parse(PlayerPrefs.GetFloat("WalletSaveCooldownRemaining").ToString()) - dec_awayTime);
            StartCoroutine(scoreSubmissionScript.WalletResetTime(constantsScript.flt_walletSaveCooldown - flt_walletCooldownTime));
        }
        else {
            PlayerPrefs.SetFloat("WalletSaveCooldownRemaining", 0);
            PlayerPrefs.Save();
            scoreSubmissionScript.CheckWalletChangeAvailability();
        }

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

    public BigDecimal GetScore()
    {
        return dec_score;
    }

    public int GetGold()
    {
        return int_gold;
    }

    public BigDecimal GetPrestigeCost()
    {
        return int_prestigeCost;
    } 

    public float GetPrestigeMultiplier()
    {
        return flt_prestigeMultiplier;
    }

    public float GetOtherMultiplier()
    {
        return flt_otherMultiplier;
    }

    public void SetOtherMultiplier(float flt_newMultiplier) 
    {
        flt_otherMultiplier = flt_newMultiplier;
        PlayerPrefs.SetFloat("OtherMultiplier", flt_newMultiplier);
        PlayerPrefs.Save();
    }

    public float GetS1Multiplier()
    {
        return flt_s1Multiplier;
    }

    public void UpgradeS1Multiplier()
    {
        flt_s1Multiplier *= constantsScript.int_upgradeStageMultiplier;
        PlayerPrefs.SetFloat("S1Multiplier", GetS1Multiplier());
        PlayerPrefs.Save();
    }

    public float GetS2Multiplier()
    {
        return flt_s2Multiplier;
    }

    public void UpgradeS2Multiplier()
    {
        flt_s2Multiplier *= constantsScript.int_upgradeStageMultiplier;
        PlayerPrefs.SetFloat("S2Multiplier", GetS2Multiplier());
        PlayerPrefs.Save();
    }

    public float GetS3Multiplier()
    {
        return flt_s3Multiplier;
    }

    public void UpgradeS3Multiplier()
    {
        flt_s3Multiplier *= constantsScript.int_upgradeStageMultiplier;
        PlayerPrefs.SetFloat("S3Multiplier", GetS3Multiplier());
        PlayerPrefs.Save();
    }

    public float GetS4Multiplier()
    {
        return flt_s4Multiplier;
    }

    public void UpgradeS4Multiplier()
    {
        flt_s4Multiplier *= constantsScript.int_upgradeStageMultiplier;
        PlayerPrefs.SetFloat("S4Multiplier", GetS4Multiplier());
        PlayerPrefs.Save();
    }

    public float GetS5Multiplier()
    {
        return flt_s5Multiplier;
    }

    public void UpgradeS5Multiplier()
    {
        flt_s5Multiplier *= constantsScript.int_upgradeStageMultiplier;
        PlayerPrefs.SetFloat("S5Multiplier", GetS5Multiplier());
        PlayerPrefs.Save();
    }

    public float GetS6Multiplier()
    {
        return flt_s6Multiplier;
    }

    public void UpgradeS6Multiplier()
    {
        flt_s6Multiplier *= constantsScript.int_upgradeStageMultiplier;
        PlayerPrefs.SetFloat("S6Multiplier", GetS6Multiplier());
        PlayerPrefs.Save();
    }

    public float GetS7Multiplier()
    {
        return flt_s7Multiplier;
    }

    public void UpgradeS7Multiplier()
    {
        flt_s7Multiplier *= constantsScript.int_upgradeStageMultiplier;
        PlayerPrefs.SetFloat("S7Multiplier", GetS7Multiplier());
        PlayerPrefs.Save();
    }

    public float GetS8Multiplier()
    {
        return flt_s8Multiplier;
    }

    public void UpgradeS8Multiplier()
    {
        flt_s8Multiplier *= constantsScript.int_upgradeStageMultiplier;
        PlayerPrefs.SetFloat("S8Multiplier", GetS8Multiplier());
        PlayerPrefs.Save();
    }

    public float GetS9Multiplier()
    {
        return flt_s9Multiplier;
    }

    public void UpgradeS9Multiplier()
    {
        flt_s9Multiplier *= constantsScript.int_upgradeStageMultiplier;
        PlayerPrefs.SetFloat("S9Multiplier", GetS9Multiplier());
        PlayerPrefs.Save();
    }

    public float GetS10Multiplier()
    {
        return flt_s10Multiplier;
    }

    public void UpgradeS10Multiplier()
    {
        flt_s10Multiplier *= constantsScript.int_upgradeStageMultiplier;
        PlayerPrefs.SetFloat("S10Multiplier", GetS10Multiplier());
        PlayerPrefs.Save();
    }

    public void AddScore(BigDecimal dec_change)
    {
        dec_score += BigDecimal.Round(dec_change, 2);
        PlayerPrefs.SetString("Score", GetScore().ToString());
        PlayerPrefs.Save();
        UpdateScoreUI();
    }

    public void MinusScore(BigDecimal dec_change)
    {
        dec_score -= BigDecimal.Round(dec_change, 2);
        PlayerPrefs.SetString("Score", GetScore().ToString());
        PlayerPrefs.Save();
        UpdateScoreUI();
    }

    public void AddGold(int int_change)
    {
        int_gold += int_change;
        PlayerPrefs.SetInt("Gold", GetGold());
        PlayerPrefs.Save();
        UpdateGoldUI();
    }

    public void MinusGold(int int_change)
    {
        int_gold -= int_change;
        PlayerPrefs.SetInt("Gold", GetGold());
        PlayerPrefs.Save();
        UpdateGoldUI();
    }

    public int GetGoldBonus()
    {
        return int_goldBonus;
    }

    public void UpgradeGoldBonus()
    {
        int_goldBonus = (int)Math.Round(int_goldBonus * constantsScript.flt_goldBonusMultiplier);
        PlayerPrefs.SetInt("GoldBonus", GetGoldBonus());
        PlayerPrefs.Save();
    }

    public float GetPrestigeBonus()
    {
        return flt_prestigeBonus;
    }

    public void UpgradePrestigeBonus()
    {
        flt_prestigeBonus += constantsScript.flt_prestigeBonus;
        PlayerPrefs.SetFloat("PrestigeBonus", GetPrestigeBonus());
        PlayerPrefs.Save();
    }

    void UpdateScoreUI()
    {
        foreach (GameObject gmo_score in arr_scoreObjects)
        {
            Text txt_score = gmo_score.GetComponent<Text>();
            txt_score.text = "$ " + SimplifyScore(BigDecimal.Round(GetScore(), 2));
        }
        
    }

    void UpdateGoldUI()
    {
        foreach (GameObject gmo_gold in arr_goldObjects)
        {
            Text txt_gold = gmo_gold.GetComponent<Text>();
            txt_gold.text = SimplifyScore(GetGold());
        }
    }

    public void BuyPrestige()
    {
        if (GetScore() >= int_prestigeCost)
        {
            MinusScore(GetScore());
            AddScore(BigDecimal.Parse(constantsScript.str_initialScore));
            flt_prestigeMultiplier += flt_prestigeBonus;
            int_prestigeCost = BigDecimal.Round((int_prestigeCost * constantsScript.flt_prestigeCostMultiplier));
            PlayerPrefs.SetString("PrestigeCost", GetPrestigeCost().ToString());
            PlayerPrefs.SetFloat("PrestigeMultiplier", GetPrestigeMultiplier());
            PlayerPrefs.Save();
            AddGold(GetGoldBonus());
        }
    }

    public void DoubleScoreWatched() {
        SetOtherMultiplier(constantsScript.flt_earnRewardMultiplier);
        StartCoroutine(earnMoreScript.ResetTime());
    }

    public void WalletSaveClicked() {
        StartCoroutine(scoreSubmissionScript.WalletResetTime());
    }

    public void ScoreSubmitClicked() {
        StartCoroutine(scoreSubmissionScript.ScoreResetTime());
    }

    public string SimplifyScore(BigDecimal dec_originalScore)
    {
        string str_return = BigDecimal.Round(dec_originalScore, 2).ToString();
        String[] arr_scoreSplit = str_return.Split('.');

        if (arr_scoreSplit[0].Length < 4)
        {
            str_return = str_return;
        }
        else if (arr_scoreSplit[0].Length < 7)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 3);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 3);
            str_return = str_front + "," + str_back;
            if (arr_scoreSplit.Length > 1)
            {
                str_return += "." + arr_scoreSplit[1];
            }
        }
        else if (arr_scoreSplit[0].Length < 10)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 6);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 6, 2);

            if (str_back == "00")
            {
                str_return = str_front + "m";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "m";
            }
            else
            {
                str_return = str_front + "." + str_back + "m";
            }
        }
        else if (arr_scoreSplit[0].Length < 13)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 9);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 9, 2);

            if (str_back == "00")
            {
                str_return = str_front + "b";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "b";
            }
            else
            {
                str_return = str_front + "." + str_back + "b";
            }
        }
        else if (arr_scoreSplit[0].Length < 16)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 12);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 12, 2);

            if (str_back == "00")
            {
                str_return = str_front + "t";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "t";
            }
            else
            {
                str_return = str_front + "." + str_back + "t";
            }
        }
        else if (arr_scoreSplit[0].Length < 19)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 15);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 15, 2);

            if (str_back == "00")
            {
                str_return = str_front + "qd";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "qd";
            }
            else
            {
                str_return = str_front + "." + str_back + "qd";
            }
        }
        else if (arr_scoreSplit[0].Length < 22)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 18);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 18, 2);

            if (str_back == "00")
            {
                str_return = str_front + "qn";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "qn";
            }
            else
            {
                str_return = str_front + "." + str_back + "qn";
            }
        }
        else if (arr_scoreSplit[0].Length < 25)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 21);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 21, 2);

            if (str_back == "00")
            {
                str_return = str_front + "sx";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "sx";
            }
            else
            {
                str_return = str_front + "." + str_back + "sx";
            }
        }
        else if (arr_scoreSplit[0].Length < 28)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 24);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 24, 2);

            if (str_back == "00")
            {
                str_return = str_front + "sp";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "sp";
            }
            else
            {
                str_return = str_front + "." + str_back + "sp";
            }
        }
        else if (arr_scoreSplit[0].Length < 31)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 27);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 27, 2);

            if (str_back == "00")
            {
                str_return = str_front + "oc";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "oc";
            }
            else
            {
                str_return = str_front + "." + str_back + "oc";
            }
        }
        else if (arr_scoreSplit[0].Length < 34)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 30);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 30, 2);

            if (str_back == "00")
            {
                str_return = str_front + "nn";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "nn";
            }
            else
            {
                str_return = str_front + "." + str_back + "nn";
            }
        }
        else if (arr_scoreSplit[0].Length < 37)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 33);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 33, 2);

            if (str_back == "00")
            {
                str_return = str_front + "dc";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "dc";
            }
            else
            {
                str_return = str_front + "." + str_back + "dc";
            }
        }
        else if (arr_scoreSplit[0].Length < 40)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 36);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 36, 2);

            if (str_back == "00")
            {
                str_return = str_front + "ud";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "ud";
            }
            else
            {
                str_return = str_front + "." + str_back + "ud";
            }
        }
        else if (arr_scoreSplit[0].Length < 43)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 36);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 36, 2);

            if (str_back == "00")
            {
                str_return = str_front + "dd";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "dd";
            }
            else
            {
                str_return = str_front + "." + str_back + "dd";
            }
        }
        else if (arr_scoreSplit[0].Length < 46)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 39);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 39, 2);

            if (str_back == "00")
            {
                str_return = str_front + "trd";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "trd";
            }
            else
            {
                str_return = str_front + "." + str_back + "trd";
            }
        }
        else if (arr_scoreSplit[0].Length < 49)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 42);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 42, 2);

            if (str_back == "00")
            {
                str_return = str_front + "qtrd";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "qtrd";
            }
            else
            {
                str_return = str_front + "." + str_back + "qtrd";
            }
        }
        else if (arr_scoreSplit[0].Length < 52)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 45);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 45, 2);

            if (str_back == "00")
            {
                str_return = str_front + "qnd";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "qnd";
            }
            else
            {
                str_return = str_front + "." + str_back + "qnd";
            }
        }
        else if (arr_scoreSplit[0].Length < 55)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 48);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 48, 2);

            if (str_back == "00")
            {
                str_return = str_front + "sxd";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "sxd";
            }
            else
            {
                str_return = str_front + "." + str_back + "sxd";
            }
        }
        else if (arr_scoreSplit[0].Length < 58)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 51);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 51, 2);

            if (str_back == "00")
            {
                str_return = str_front + "spd";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "spd";
            }
            else
            {
                str_return = str_front + "." + str_back + "spd";
            }
        }
        else if (arr_scoreSplit[0].Length < 61)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 54);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 54, 2);

            if (str_back == "00")
            {
                str_return = str_front + "ocd";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "ocd";
            }
            else
            {
                str_return = str_front + "." + str_back + "ocd";
            }
        }
        else if (arr_scoreSplit[0].Length < 64)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 57);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 57, 2);

            if (str_back == "00")
            {
                str_return = str_front + "nnd";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "nnd";
            }
            else
            {
                str_return = str_front + "." + str_back + "nnd";
            }
        }
        else if (arr_scoreSplit[0].Length < 67)
        {
            string str_front = arr_scoreSplit[0].Substring(0, arr_scoreSplit[0].Length - 60);
            string str_back = arr_scoreSplit[0].Substring(arr_scoreSplit[0].Length - 60, 2);

            if (str_back == "00")
            {
                str_return = str_front + "vg";
            }
            else if (str_back[1] == '0')
            {
                str_return = str_front + "." + str_back[0] + "vg";
            }
            else
            {
                str_return = str_front + "." + str_back + "vg";
            }
        }

        return str_return;
    }

    public Color HexToColor(string str_hex)
    {
        if (str_hex.StartsWith("#"))
        {
            str_hex = str_hex.Substring(1);
        }

        if (str_hex.Length != 6)
        {
            return Color.white;
        }

        byte r = byte.Parse(str_hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(str_hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(str_hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color(r / 255f, g / 255f, b / 255f);
    }
}
