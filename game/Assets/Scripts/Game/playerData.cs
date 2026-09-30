using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtendedNumerics;

public class playerData : MonoBehaviour
{
    constants constantsScript;
    GameObject gmo_loadingCanvas;
    AudioSource aud_backgroundMusic;
    offlineEarning offlineEarningScript;
    scoreTracking scoreTrackingScript;
    doubleProductionAd doubleProductionAdScript;
    Text txt_loading;
    upgrades upgradesScript;
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
        constantsScript = GameObject.Find("Score").GetComponent<constants>();
        gmo_loadingCanvas = GameObject.Find("Loading Canvas");
        scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();
        doubleProductionAdScript = GameObject.Find("btn_doubleProduction").GetComponent<doubleProductionAd>();
        txt_loading = GameObject.Find("txt_loading").GetComponent<Text>();
        offlineEarningScript = GameObject.Find("Offline Canvas").GetComponent<offlineEarning>();
        upgradesScript = GameObject.Find("Upgrades Canvas").GetComponent<upgrades>();
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

        // Play the background music
        aud_backgroundMusic = GameObject.Find("Background Music").GetComponent<AudioSource>();
        if ((!PlayerPrefs.HasKey("MusicAudio")) || (PlayerPrefs.GetInt("MusicAudio") == 1)) {
            aud_backgroundMusic.Play();
        }

        PlayerPrefs.SetInt("Loaded", 0);
        StartCoroutine(LoadData());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SaveData()
    {
        while (!PlayerPrefs.HasKey("LastSessionTime")) {
            yield return new WaitForSeconds(0.2f);
        }

        String str_currentSessionTime = PlayerPrefs.GetString("LastSessionTime");
        DateTime dte_currentSessionTime;
        DateTime.TryParse(str_currentSessionTime, out dte_currentSessionTime);

        while (true) {
            dte_currentSessionTime = dte_currentSessionTime.AddSeconds(1);
            PlayerPrefs.SetString("LastSessionTime", dte_currentSessionTime.ToString());
            PlayerPrefs.Save();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator LoadData()
    {
        float flt_elapsedTime = 0f;

        if (PlayerPrefs.HasKey("HasPlayedBefore"))
        {
            yield return new WaitForSeconds(0.02f);

            StartCoroutine(scoreTrackingScript.InitiateScoreTracking(PlayerPrefs.GetString("Score"), PlayerPrefs.GetInt("Gold"),
                PlayerPrefs.GetInt("GoldBonus"), PlayerPrefs.GetString("PrestigeCost"), PlayerPrefs.GetFloat("PrestigeBonus"),
                PlayerPrefs.GetFloat("PrestigeMultiplier"), PlayerPrefs.GetFloat("OtherMultiplier"), PlayerPrefs.GetFloat("S1Multiplier"),
                PlayerPrefs.GetFloat("S2Multiplier"), PlayerPrefs.GetFloat("S3Multiplier"), PlayerPrefs.GetFloat("S4Multiplier"),
                PlayerPrefs.GetFloat("S5Multiplier"), PlayerPrefs.GetFloat("S6Multiplier"), PlayerPrefs.GetFloat("S7Multiplier"),
                PlayerPrefs.GetFloat("S8Multiplier"), PlayerPrefs.GetFloat("S9Multiplier"), PlayerPrefs.GetFloat("S10Multiplier")));

            stage1Script.InitiateStage(PlayerPrefs.GetInt("Stage1Level"), PlayerPrefs.GetInt("Stage1Manager"), PlayerPrefs.GetInt("Stage1ManagerReward1"), PlayerPrefs.GetInt("Stage1ManagerTime1"));
            stage2Script.InitiateStage(PlayerPrefs.GetInt("Stage2Level"), PlayerPrefs.GetInt("Stage2Manager"), PlayerPrefs.GetInt("Stage2ManagerReward1"), PlayerPrefs.GetInt("Stage2ManagerTime1"));
            stage3Script.InitiateStage(PlayerPrefs.GetInt("Stage3Level"), PlayerPrefs.GetInt("Stage3Manager"), PlayerPrefs.GetInt("Stage3ManagerReward1"), PlayerPrefs.GetInt("Stage3ManagerTime1"));
            stage4Script.InitiateStage(PlayerPrefs.GetInt("Stage4Level"), PlayerPrefs.GetInt("Stage4Manager"), PlayerPrefs.GetInt("Stage4ManagerReward1"), PlayerPrefs.GetInt("Stage4ManagerTime1"));
            stage5Script.InitiateStage(PlayerPrefs.GetInt("Stage5Level"), PlayerPrefs.GetInt("Stage5Manager"), PlayerPrefs.GetInt("Stage5ManagerReward1"), PlayerPrefs.GetInt("Stage5ManagerTime1"));
            stage6Script.InitiateStage(PlayerPrefs.GetInt("Stage6Level"), PlayerPrefs.GetInt("Stage6Manager"), PlayerPrefs.GetInt("Stage6ManagerReward1"), PlayerPrefs.GetInt("Stage6ManagerTime1"));
            stage7Script.InitiateStage(PlayerPrefs.GetInt("Stage7Level"), PlayerPrefs.GetInt("Stage7Manager"), PlayerPrefs.GetInt("Stage7ManagerReward1"), PlayerPrefs.GetInt("Stage7ManagerTime1"));
            stage8Script.InitiateStage(PlayerPrefs.GetInt("Stage8Level"), PlayerPrefs.GetInt("Stage8Manager"), PlayerPrefs.GetInt("Stage8ManagerReward1"), PlayerPrefs.GetInt("Stage8ManagerTime1"));
            stage9Script.InitiateStage(PlayerPrefs.GetInt("Stage9Level"), PlayerPrefs.GetInt("Stage9Manager"), PlayerPrefs.GetInt("Stage9ManagerReward1"), PlayerPrefs.GetInt("Stage9ManagerTime1"));
            stage10Script.InitiateStage(PlayerPrefs.GetInt("Stage10Level"), PlayerPrefs.GetInt("Stage10Manager"), PlayerPrefs.GetInt("Stage10ManagerReward1"), PlayerPrefs.GetInt("Stage10ManagerTime1"));

            upgradesScript.InitiateUpgrades(PlayerPrefs.GetInt("Upgrade1Cost"), PlayerPrefs.GetInt("Upgrade2Cost"),
                PlayerPrefs.GetInt("Upgrade3Cost"), PlayerPrefs.GetInt("Upgrade4Cost"), PlayerPrefs.GetInt("Upgrade5Cost"),
                PlayerPrefs.GetInt("Upgrade6Cost"), PlayerPrefs.GetInt("Upgrade7Cost"), PlayerPrefs.GetInt("Upgrade8Cost"),
                PlayerPrefs.GetInt("Upgrade9Cost"), PlayerPrefs.GetInt("Upgrade10Cost"), PlayerPrefs.GetInt("Upgrade11Cost"),
                PlayerPrefs.GetInt("Upgrade12Cost"));
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", "");
            PlayerPrefs.SetInt("MusicAudio", 1);
            PlayerPrefs.SetInt("EffectsAudio", 1);
            PlayerPrefs.SetString("SolanaWallet", "");
            PlayerPrefs.SetFloat("WalletSaveCooldownRemaining", 0);
            PlayerPrefs.SetFloat("ScoreSaveCooldownRemaining", 0);
            PlayerPrefs.SetString("LastSessionTime", "");
            PlayerPrefs.SetInt("ProvideAds", 1);
            PlayerPrefs.SetInt("AdFrequency", 180);
            PlayerPrefs.SetString("ReferralCode", "");
            PlayerPrefs.Save();

            PlayerPrefs.SetString("Score", constantsScript.str_initialScore);
            PlayerPrefs.SetInt("Gold", constantsScript.int_initialGold);
            PlayerPrefs.SetInt("GoldBonus", constantsScript.int_initialGoldBonus);
            PlayerPrefs.SetString("PrestigeCost", constantsScript.int_initialPrestigeCost.ToString());
            PlayerPrefs.SetFloat("PrestigeBonus", constantsScript.flt_prestigeBonus);
            PlayerPrefs.SetFloat("PrestigeMultiplier", constantsScript.flt_initialPrestigeMultiplier);
            PlayerPrefs.SetFloat("EarnTimeRemaining", 0);
            PlayerPrefs.SetString("AwayTime", "0");
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("Upgrade1Cost", constantsScript.int_initialU1Cost);
            PlayerPrefs.SetInt("Upgrade2Cost", constantsScript.int_initialU2Cost);
            PlayerPrefs.SetInt("Upgrade3Cost", constantsScript.int_initialU3Cost);
            PlayerPrefs.SetInt("Upgrade4Cost", constantsScript.int_initialU4Cost);
            PlayerPrefs.SetInt("Upgrade5Cost", constantsScript.int_initialU5Cost);
            PlayerPrefs.SetInt("Upgrade6Cost", constantsScript.int_initialU6Cost);
            PlayerPrefs.SetInt("Upgrade7Cost", constantsScript.int_initialU7Cost);
            PlayerPrefs.SetInt("Upgrade8Cost", constantsScript.int_initialU8Cost);
            PlayerPrefs.SetInt("Upgrade9Cost", constantsScript.int_initialU9Cost);
            PlayerPrefs.SetInt("Upgrade10Cost", constantsScript.int_initialU10Cost);
            PlayerPrefs.SetInt("Upgrade11Cost", constantsScript.int_initialU11Cost);
            PlayerPrefs.SetInt("Upgrade12Cost", constantsScript.int_initialU12Cost);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("Stage1Level", constantsScript.int_initialLevel);
            PlayerPrefs.SetFloat("S1Multiplier", constantsScript.flt_initialS1RewardMultiplier);
            PlayerPrefs.SetInt("Stage1Manager", 0);
            PlayerPrefs.SetInt("Stage1ManagerReward1", 0);
            PlayerPrefs.SetInt("Stage1ManagerTime1", 0);
            PlayerPrefs.SetInt("S1BaseRewardAmount", (int)constantsScript.flt_initialS1UnlockCost);
            PlayerPrefs.SetFloat("S1BaseRewardTime", constantsScript.flt_initialS1RewardTime);
            PlayerPrefs.SetFloat("Stage1Progress", 0);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("Stage2Level", constantsScript.int_initialLevel);
            PlayerPrefs.SetFloat("S2Multiplier", constantsScript.flt_initialS2RewardMultiplier);
            PlayerPrefs.SetInt("Stage2Manager", 0);
            PlayerPrefs.SetInt("Stage2ManagerReward1", 0);
            PlayerPrefs.SetInt("Stage2ManagerTime1", 0);
            PlayerPrefs.SetInt("S2BaseRewardAmount", (int)constantsScript.flt_initialS2UnlockCost);
            PlayerPrefs.SetFloat("S2BaseRewardTime", constantsScript.flt_initialS2RewardTime);
            PlayerPrefs.SetFloat("Stage2Progress", 0);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("Stage3Level", constantsScript.int_initialLevel);
            PlayerPrefs.SetFloat("S3Multiplier", constantsScript.flt_initialS3RewardMultiplier);
            PlayerPrefs.SetInt("Stage3Manager", 0);
            PlayerPrefs.SetInt("Stage3ManagerReward1", 0);
            PlayerPrefs.SetInt("Stage3ManagerTime1", 0);
            PlayerPrefs.SetInt("S3BaseRewardAmount", (int)constantsScript.flt_initialS3UnlockCost);
            PlayerPrefs.SetFloat("S3BaseRewardTime", constantsScript.flt_initialS3RewardTime);
            PlayerPrefs.SetFloat("Stage3Progress", 0);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("Stage4Level", constantsScript.int_initialLevel);
            PlayerPrefs.SetFloat("S4Multiplier", constantsScript.flt_initialS4RewardMultiplier);
            PlayerPrefs.SetInt("Stage4Manager", 0);
            PlayerPrefs.SetInt("Stage4ManagerReward1", 0);
            PlayerPrefs.SetInt("Stage4ManagerTime1", 0);
            PlayerPrefs.SetInt("S4BaseRewardAmount", (int)constantsScript.flt_initialS4UnlockCost);
            PlayerPrefs.SetFloat("S4BaseRewardTime", constantsScript.flt_initialS4RewardTime);
            PlayerPrefs.SetFloat("Stage4Progress", 0);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("Stage5Level", constantsScript.int_initialLevel);
            PlayerPrefs.SetFloat("S5Multiplier", constantsScript.flt_initialS5RewardMultiplier);
            PlayerPrefs.SetInt("Stage5Manager", 0);
            PlayerPrefs.SetInt("Stage5ManagerReward1", 0);
            PlayerPrefs.SetInt("Stage5ManagerTime1", 0);
            PlayerPrefs.SetInt("S5BaseRewardAmount", (int)constantsScript.flt_initialS5UnlockCost);
            PlayerPrefs.SetFloat("S5BaseRewardTime", constantsScript.flt_initialS5RewardTime);
            PlayerPrefs.SetFloat("Stage5Progress", 0);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("Stage6Level", constantsScript.int_initialLevel);
            PlayerPrefs.SetFloat("S6Multiplier", constantsScript.flt_initialS6RewardMultiplier);
            PlayerPrefs.SetInt("Stage6Manager", 0);
            PlayerPrefs.SetInt("Stage6ManagerReward1", 0);
            PlayerPrefs.SetInt("Stage6ManagerTime1", 0);
            PlayerPrefs.SetInt("S6BaseRewardAmount", (int)constantsScript.flt_initialS6UnlockCost);
            PlayerPrefs.SetFloat("S6BaseRewardTime", constantsScript.flt_initialS6RewardTime);
            PlayerPrefs.SetFloat("Stage6Progress", 0);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("Stage7Level", constantsScript.int_initialLevel);
            PlayerPrefs.SetFloat("S7Multiplier", constantsScript.flt_initialS7RewardMultiplier);
            PlayerPrefs.SetInt("Stage7Manager", 0);
            PlayerPrefs.SetInt("Stage7ManagerReward1", 0);
            PlayerPrefs.SetInt("Stage7ManagerTime1", 0);
            PlayerPrefs.SetInt("S7BaseRewardAmount", (int)constantsScript.flt_initialS7UnlockCost);
            PlayerPrefs.SetFloat("S7BaseRewardTime", constantsScript.flt_initialS7RewardTime);
            PlayerPrefs.SetFloat("Stage7Progress", 0);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("Stage8Level", constantsScript.int_initialLevel);
            PlayerPrefs.SetFloat("S8Multiplier", constantsScript.flt_initialS8RewardMultiplier);
            PlayerPrefs.SetInt("Stage8Manager", 0);
            PlayerPrefs.SetInt("Stage8ManagerReward1", 0);
            PlayerPrefs.SetInt("Stage8ManagerTime1", 0);
            PlayerPrefs.SetInt("S8BaseRewardAmount", (int)constantsScript.flt_initialS8UnlockCost);
            PlayerPrefs.SetFloat("S8BaseRewardTime", constantsScript.flt_initialS8RewardTime);
            PlayerPrefs.SetFloat("Stage8Progress", 0);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("Stage9Level", constantsScript.int_initialLevel);
            PlayerPrefs.SetFloat("S9Multiplier", constantsScript.flt_initialS9RewardMultiplier);
            PlayerPrefs.SetInt("Stage9Manager", 0);
            PlayerPrefs.SetInt("Stage9ManagerReward1", 0);
            PlayerPrefs.SetInt("Stage9ManagerTime1", 0);
            PlayerPrefs.SetInt("S9BaseRewardAmount", (int)constantsScript.flt_initialS9UnlockCost);
            PlayerPrefs.SetFloat("S9BaseRewardTime", constantsScript.flt_initialS9RewardTime);
            PlayerPrefs.SetFloat("Stage9Progress", 0);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("Stage10Level", constantsScript.int_initialLevel);
            PlayerPrefs.SetFloat("S10Multiplier", constantsScript.flt_initialS10RewardMultiplier);
            PlayerPrefs.SetInt("Stage10Manager", 0);
            PlayerPrefs.SetInt("Stage10ManagerReward1", 0);
            PlayerPrefs.SetInt("Stage10ManagerTime1", 0);
            PlayerPrefs.SetInt("S10BaseRewardAmount", (int)constantsScript.flt_initialS10UnlockCost);
            PlayerPrefs.SetFloat("S10BaseRewardTime", constantsScript.flt_initialS10RewardTime);
            PlayerPrefs.SetFloat("Stage10Progress", 0);
            PlayerPrefs.Save();

            stage1Script.Init();
            stage2Script.Init();
            stage3Script.Init();
            stage4Script.Init();
            stage5Script.Init();
            stage6Script.Init();
            stage7Script.Init();
            stage8Script.Init();
            stage9Script.Init();
            stage10Script.Init();
        }

        yield return new WaitForSeconds(2f);
        offlineEarningScript.InitiateOfflineCollection();

        while (PlayerPrefs.GetInt("Loaded") == 0) {
            switch(txt_loading.text) {
                case "Loading.":
                    txt_loading.text = "Loading..";
                    break;
                case "Loading..":
                    txt_loading.text = "Loading...";
                    break;
                case "Loading...":
                    txt_loading.text = "Loading.";
                    break;
            }

            yield return new WaitForSeconds(0.2f);
            flt_elapsedTime += Time.deltaTime;
        }

        gmo_loadingCanvas.SetActive(false);
        StartCoroutine(SaveData());
    }
}
