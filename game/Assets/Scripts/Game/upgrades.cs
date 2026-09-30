using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgrades : MonoBehaviour
{
    constants constantsScript;
    Color clr_unavailable, clr_available;

    float flt_currentGold;
    string str_upgradesTag;
    public GameObject gmo_upgradesCanvas;
    GameObject gmo_menuItems;
    GameObject[] arr_upgradesObjects;
    scoreTracking scoreTrackingScript;
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


    int int_costUpgrade1, int_costUpgrade2, int_costUpgrade3, int_costUpgrade4,
        int_costUpgrade5, int_costUpgrade6, int_costUpgrade7, int_costUpgrade8,
        int_costUpgrade9, int_costUpgrade10, int_costUpgrade11, int_costUpgrade12;
    GameObject gmo_upgrades1, gmo_upgrades2, gmo_upgrades3, gmo_upgrades4, gmo_upgrades5,
        gmo_upgrades6, gmo_upgrades7, gmo_upgrades8, gmo_upgrades9, gmo_upgrades10, gmo_upgrades11,
        gmo_upgrades12;
    Button btn_buyUpgrade1, btn_buyUpgrade2, btn_buyUpgrade3, btn_buyUpgrade4, btn_buyUpgrade5,
        btn_buyUpgrade6, btn_buyUpgrade7, btn_buyUpgrade8, btn_buyUpgrade9, btn_buyUpgrade10,
        btn_buyUpgrade11, btn_buyUpgrade12;
    Text txt_costUpgrade1, txt_exampleUpgrade1, txt_buyUpgrade1, txt_costUpgrade2, txt_exampleUpgrade2, txt_buyUpgrade2,
        txt_costUpgrade3, txt_exampleUpgrade3, txt_buyUpgrade3, txt_costUpgrade4, txt_exampleUpgrade4, txt_buyUpgrade4,
        txt_costUpgrade5, txt_exampleUpgrade5, txt_buyUpgrade5, txt_costUpgrade6, txt_exampleUpgrade6, txt_buyUpgrade6,
        txt_costUpgrade7, txt_exampleUpgrade7, txt_buyUpgrade7, txt_costUpgrade8, txt_exampleUpgrade8, txt_buyUpgrade8,
        txt_costUpgrade9, txt_exampleUpgrade9, txt_buyUpgrade9, txt_costUpgrade10, txt_exampleUpgrade10, txt_buyUpgrade10,
        txt_costUpgrade11, txt_exampleUpgrade11, txt_buyUpgrade11, txt_costUpgrade12, txt_exampleUpgrade12, txt_buyUpgrade12;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();
        constantsScript = GameObject.Find("Score").GetComponent<constants>();
        aud_purchaseSound = GameObject.Find("Purchase Sound").GetComponent<AudioSource>();
        aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();
        gmo_menuItems = GameObject.Find("Hamburger Menu Items");

        clr_unavailable = scoreTrackingScript.HexToColor("767676");
        clr_available = Color.white;

        str_upgradesTag = "Upgrade";
        arr_upgradesObjects = GameObject.FindGameObjectsWithTag(str_upgradesTag);

        gmo_upgradesCanvas = GameObject.Find("Upgrades Canvas");
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

        int_costUpgrade1 = constantsScript.int_initialU1Cost;
        gmo_upgrades1 = GameObject.Find("Upgrade1");
        btn_buyUpgrade1 = GameObject.Find("btn_u1buy").GetComponent<Button>();
        txt_costUpgrade1 = GameObject.Find("txt_u1cost").GetComponent<Text>();
        txt_buyUpgrade1 = GameObject.Find("txt_u1buy").GetComponent<Text>();
        
        int_costUpgrade2 = constantsScript.int_initialU2Cost;
        gmo_upgrades2 = GameObject.Find("Upgrade2");
        btn_buyUpgrade2 = GameObject.Find("btn_u2buy").GetComponent<Button>();
        txt_costUpgrade2 = GameObject.Find("txt_u2cost").GetComponent<Text>();
        txt_buyUpgrade2 = GameObject.Find("txt_u2buy").GetComponent<Text>();
        
        int_costUpgrade3 = constantsScript.int_initialU3Cost;
        gmo_upgrades3 = GameObject.Find("Upgrade3");
        btn_buyUpgrade3 = GameObject.Find("btn_u3buy").GetComponent<Button>();
        txt_costUpgrade3 = GameObject.Find("txt_u3cost").GetComponent<Text>();
        txt_buyUpgrade3 = GameObject.Find("txt_u3buy").GetComponent<Text>();
        
        int_costUpgrade4 = constantsScript.int_initialU4Cost;
        gmo_upgrades4 = GameObject.Find("Upgrade4");
        btn_buyUpgrade4 = GameObject.Find("btn_u4buy").GetComponent<Button>();
        txt_costUpgrade4 = GameObject.Find("txt_u4cost").GetComponent<Text>();
        txt_buyUpgrade4 = GameObject.Find("txt_u4buy").GetComponent<Text>();
        
        int_costUpgrade5 = constantsScript.int_initialU5Cost;
        gmo_upgrades5 = GameObject.Find("Upgrade5");
        btn_buyUpgrade5 = GameObject.Find("btn_u5buy").GetComponent<Button>();
        txt_costUpgrade5 = GameObject.Find("txt_u5cost").GetComponent<Text>();
        txt_buyUpgrade5 = GameObject.Find("txt_u5buy").GetComponent<Text>();
        
        int_costUpgrade6 = constantsScript.int_initialU6Cost;
        gmo_upgrades6 = GameObject.Find("Upgrade6");
        btn_buyUpgrade6 = GameObject.Find("btn_u6buy").GetComponent<Button>();
        txt_costUpgrade6 = GameObject.Find("txt_u6cost").GetComponent<Text>();
        txt_buyUpgrade6 = GameObject.Find("txt_u6buy").GetComponent<Text>();
        
        int_costUpgrade7 = constantsScript.int_initialU7Cost;
        gmo_upgrades7 = GameObject.Find("Upgrade7");
        btn_buyUpgrade7 = GameObject.Find("btn_u7buy").GetComponent<Button>();
        txt_costUpgrade7 = GameObject.Find("txt_u7cost").GetComponent<Text>();
        txt_buyUpgrade7 = GameObject.Find("txt_u7buy").GetComponent<Text>();
        
        int_costUpgrade8 = constantsScript.int_initialU8Cost;
        gmo_upgrades8 = GameObject.Find("Upgrade8");
        btn_buyUpgrade8 = GameObject.Find("btn_u8buy").GetComponent<Button>();
        txt_costUpgrade8 = GameObject.Find("txt_u8cost").GetComponent<Text>();
        txt_buyUpgrade8 = GameObject.Find("txt_u8buy").GetComponent<Text>();
        
        int_costUpgrade9 = constantsScript.int_initialU9Cost;
        gmo_upgrades9 = GameObject.Find("Upgrade9");
        btn_buyUpgrade9 = GameObject.Find("btn_u9buy").GetComponent<Button>();
        txt_costUpgrade9 = GameObject.Find("txt_u9cost").GetComponent<Text>();
        txt_buyUpgrade9 = GameObject.Find("txt_u9buy").GetComponent<Text>();
        
        int_costUpgrade10 = constantsScript.int_initialU10Cost;
        gmo_upgrades10 = GameObject.Find("Upgrade10");
        btn_buyUpgrade10 = GameObject.Find("btn_u10buy").GetComponent<Button>();
        txt_costUpgrade10 = GameObject.Find("txt_u10cost").GetComponent<Text>();
        txt_buyUpgrade10 = GameObject.Find("txt_u10buy").GetComponent<Text>();
        
        int_costUpgrade11 = constantsScript.int_initialU11Cost;
        gmo_upgrades11 = GameObject.Find("Upgrade11");
        btn_buyUpgrade11 = GameObject.Find("btn_u11buy").GetComponent<Button>();
        txt_costUpgrade11 = GameObject.Find("txt_u11cost").GetComponent<Text>();
        txt_buyUpgrade11 = GameObject.Find("txt_u11buy").GetComponent<Text>();
        
        int_costUpgrade12 = constantsScript.int_initialU12Cost;
        gmo_upgrades12 = GameObject.Find("Upgrade12");
        btn_buyUpgrade12 = GameObject.Find("btn_u12buy").GetComponent<Button>();
        txt_costUpgrade12 = GameObject.Find("txt_u12cost").GetComponent<Text>();
        txt_buyUpgrade12 = GameObject.Find("txt_u12buy").GetComponent<Text>();
        
        yield return new WaitForSeconds(0.01f);

        gmo_upgradesCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreTrackingScript != null)
        {
            flt_currentGold = scoreTrackingScript.GetGold();

            if (flt_currentGold >= int_costUpgrade1)
            {
                btn_buyUpgrade1.interactable = true;
                txt_buyUpgrade1.color = clr_available;
            }
            else
            {
                btn_buyUpgrade1.interactable = false;
                txt_buyUpgrade1.color = clr_unavailable;
            }
            if (flt_currentGold >= int_costUpgrade2)
            {
                btn_buyUpgrade2.interactable = true;
                txt_buyUpgrade2.color = clr_available;
            }
            else
            {
                btn_buyUpgrade2.interactable = false;
                txt_buyUpgrade2.color = clr_unavailable;
            }
            if (flt_currentGold >= int_costUpgrade3)
            {
                btn_buyUpgrade3.interactable = true;
                txt_buyUpgrade3.color = clr_available;
            }
            else
            {
                btn_buyUpgrade3.interactable = false;
                txt_buyUpgrade3.color = clr_unavailable;
            }
            if (flt_currentGold >= int_costUpgrade4)
            {
                btn_buyUpgrade4.interactable = true;
                txt_buyUpgrade4.color = clr_available;
            }
            else
            {
                btn_buyUpgrade4.interactable = false;
                txt_buyUpgrade4.color = clr_unavailable;
            }
            if (flt_currentGold >= int_costUpgrade5)
            {
                btn_buyUpgrade5.interactable = true;
                txt_buyUpgrade5.color = clr_available;
            }
            else
            {
                btn_buyUpgrade5.interactable = false;
                txt_buyUpgrade5.color = clr_unavailable;
            }
            if (flt_currentGold >= int_costUpgrade6)
            {
                btn_buyUpgrade6.interactable = true;
                txt_buyUpgrade6.color = clr_available;
            }
            else
            {
                btn_buyUpgrade6.interactable = false;
                txt_buyUpgrade6.color = clr_unavailable;
            }
            if (flt_currentGold >= int_costUpgrade7)
            {
                btn_buyUpgrade7.interactable = true;
                txt_buyUpgrade7.color = clr_available;
            }
            else
            {
                btn_buyUpgrade7.interactable = false;
                txt_buyUpgrade7.color = clr_unavailable;
            }
            if (flt_currentGold >= int_costUpgrade8)
            {
                btn_buyUpgrade8.interactable = true;
                txt_buyUpgrade8.color = clr_available;
            }
            else
            {
                btn_buyUpgrade8.interactable = false;
                txt_buyUpgrade8.color = clr_unavailable;
            }
            if (flt_currentGold >= int_costUpgrade9)
            {
                btn_buyUpgrade9.interactable = true;
                txt_buyUpgrade9.color = clr_available;
            }
            else
            {
                btn_buyUpgrade9.interactable = false;
                txt_buyUpgrade9.color = clr_unavailable;
            }
            if (flt_currentGold >= int_costUpgrade10)
            {
                btn_buyUpgrade10.interactable = true;
                txt_buyUpgrade10.color = clr_available;
            }
            else
            {
                btn_buyUpgrade10.interactable = false;
                txt_buyUpgrade10.color = clr_unavailable;
            }
            if (flt_currentGold >= int_costUpgrade11)
            {
                btn_buyUpgrade11.interactable = true;
                txt_buyUpgrade11.color = clr_available;
            }
            else
            {
                btn_buyUpgrade11.interactable = false;
                txt_buyUpgrade11.color = clr_unavailable;
            }
            if (flt_currentGold >= int_costUpgrade12)
            {
                btn_buyUpgrade12.interactable = true;
                txt_buyUpgrade12.color = clr_available;
            }
            else
            {
                btn_buyUpgrade12.interactable = false;
                txt_buyUpgrade12.color = clr_unavailable;
            }
        }
    }

    public void InitiateUpgrades(int int_upgrade1Cost, int int_upgrade2Cost, int int_upgrade3Cost,
        int int_upgrade4Cost, int int_upgrade5Cost, int int_upgrade6Cost, int int_upgrade7Cost,
        int int_upgrade8Cost, int int_upgrade9Cost, int int_upgrade10Cost, int int_upgrade11Cost,
        int int_upgrade12Cost) {
        flt_currentGold = scoreTrackingScript.GetGold();

        int_costUpgrade1 = int_upgrade1Cost;
        int_costUpgrade2 = int_upgrade2Cost;
        int_costUpgrade3 = int_upgrade3Cost;
        int_costUpgrade4 = int_upgrade4Cost;
        int_costUpgrade5 = int_upgrade5Cost;
        int_costUpgrade6 = int_upgrade6Cost;
        int_costUpgrade7 = int_upgrade7Cost;
        int_costUpgrade8 = int_upgrade8Cost;
        int_costUpgrade9 = int_upgrade9Cost;
        int_costUpgrade10 = int_upgrade10Cost;
        int_costUpgrade11 = int_upgrade11Cost;
        int_costUpgrade12 = int_upgrade12Cost;
    }

    void UpdateUI() 
    {
        txt_costUpgrade1.text = scoreTrackingScript.SimplifyScore(int_costUpgrade1);
        txt_exampleUpgrade1 = GameObject.Find("txt_u1example").GetComponent<Text>();
        txt_exampleUpgrade1.text = "+" + scoreTrackingScript.GetPrestigeBonus() + "% >> " +
            "+" + (scoreTrackingScript.GetPrestigeBonus() + constantsScript.flt_prestigeBonus) + "%";

        txt_costUpgrade2.text = scoreTrackingScript.SimplifyScore(int_costUpgrade2);
        txt_exampleUpgrade2 = GameObject.Find("txt_u2example").GetComponent<Text>();
        txt_exampleUpgrade2.text = "+" + scoreTrackingScript.GetGoldBonus() + " >> " +
            "+" + (int)Math.Round(scoreTrackingScript.GetGoldBonus() * constantsScript.flt_goldBonusMultiplier);

        txt_costUpgrade3.text = scoreTrackingScript.SimplifyScore(int_costUpgrade3);
        txt_exampleUpgrade3 = GameObject.Find("txt_u3example").GetComponent<Text>();
        txt_exampleUpgrade3.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS1Multiplier() * constantsScript.flt_initialS1UnlockCost * scoreTrackingScript.GetPrestigeMultiplier()) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS1Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS1UnlockCost * scoreTrackingScript.GetPrestigeMultiplier());

        txt_costUpgrade4.text = scoreTrackingScript.SimplifyScore(int_costUpgrade4);
        txt_exampleUpgrade4 = GameObject.Find("txt_u4example").GetComponent<Text>();
        txt_exampleUpgrade4.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS2Multiplier() * constantsScript.flt_initialS2UnlockCost * scoreTrackingScript.GetPrestigeMultiplier()) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS2Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS2UnlockCost * scoreTrackingScript.GetPrestigeMultiplier());

        txt_costUpgrade5.text = scoreTrackingScript.SimplifyScore(int_costUpgrade5);
        txt_exampleUpgrade5 = GameObject.Find("txt_u5example").GetComponent<Text>();
        txt_exampleUpgrade5.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS3Multiplier() * constantsScript.flt_initialS3UnlockCost * scoreTrackingScript.GetPrestigeMultiplier()) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS3Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS3UnlockCost * scoreTrackingScript.GetPrestigeMultiplier());

        txt_costUpgrade6.text = scoreTrackingScript.SimplifyScore(int_costUpgrade6);
        txt_exampleUpgrade6 = GameObject.Find("txt_u6example").GetComponent<Text>();
        txt_exampleUpgrade6.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS4Multiplier() * constantsScript.flt_initialS4UnlockCost * scoreTrackingScript.GetPrestigeMultiplier()) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS4Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS4UnlockCost * scoreTrackingScript.GetPrestigeMultiplier());

        txt_costUpgrade7.text = scoreTrackingScript.SimplifyScore(int_costUpgrade7);
        txt_exampleUpgrade7 = GameObject.Find("txt_u7example").GetComponent<Text>();
        txt_exampleUpgrade7.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS5Multiplier() * constantsScript.flt_initialS5UnlockCost * scoreTrackingScript.GetPrestigeMultiplier()) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS5Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS5UnlockCost * scoreTrackingScript.GetPrestigeMultiplier());

        txt_costUpgrade8.text = scoreTrackingScript.SimplifyScore(int_costUpgrade8);
        txt_exampleUpgrade8 = GameObject.Find("txt_u8example").GetComponent<Text>();
        txt_exampleUpgrade8.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS6Multiplier() * constantsScript.flt_initialS6UnlockCost * scoreTrackingScript.GetPrestigeMultiplier()) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS6Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS6UnlockCost * scoreTrackingScript.GetPrestigeMultiplier());

        txt_costUpgrade9.text = scoreTrackingScript.SimplifyScore(int_costUpgrade9);
        txt_exampleUpgrade9 = GameObject.Find("txt_u9example").GetComponent<Text>();
        txt_exampleUpgrade9.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS7Multiplier() * constantsScript.flt_initialS7UnlockCost * scoreTrackingScript.GetPrestigeMultiplier()) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS7Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS7UnlockCost * scoreTrackingScript.GetPrestigeMultiplier());

        txt_costUpgrade10.text = scoreTrackingScript.SimplifyScore(int_costUpgrade10);
        txt_exampleUpgrade10 = GameObject.Find("txt_u10example").GetComponent<Text>();
        txt_exampleUpgrade10.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS8Multiplier() * constantsScript.flt_initialS8UnlockCost * scoreTrackingScript.GetPrestigeMultiplier()) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS8Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS8UnlockCost * scoreTrackingScript.GetPrestigeMultiplier());

        txt_costUpgrade11.text = scoreTrackingScript.SimplifyScore(int_costUpgrade11);
        txt_exampleUpgrade11 = GameObject.Find("txt_u11example").GetComponent<Text>();
        txt_exampleUpgrade11.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS9Multiplier() * constantsScript.flt_initialS9UnlockCost * scoreTrackingScript.GetPrestigeMultiplier()) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS9Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS9UnlockCost * scoreTrackingScript.GetPrestigeMultiplier());

        txt_costUpgrade12.text = scoreTrackingScript.SimplifyScore(int_costUpgrade12);
        txt_exampleUpgrade12 = GameObject.Find("txt_u12example").GetComponent<Text>();
        txt_exampleUpgrade12.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS10Multiplier() * constantsScript.flt_initialS10UnlockCost * scoreTrackingScript.GetPrestigeMultiplier()) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS10Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS10UnlockCost * scoreTrackingScript.GetPrestigeMultiplier());
    }

    public void UpgradeOpenClicked()
    {
        gmo_upgradesCanvas.SetActive(true);
        gmo_menuItems.SetActive(false);
        UpdateUI();

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    public void UpgradeCloseClicked()
    {
        gmo_upgradesCanvas.SetActive(false);

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    // Increase the prestige multiplier for all future prestiges.
    public void BuyUpgrade1Clicked()
    {
        if (flt_currentGold >= int_costUpgrade1)
        {
            scoreTrackingScript.MinusGold(int_costUpgrade1);
            scoreTrackingScript.UpgradePrestigeBonus();
            int_costUpgrade1 = (int)Math.Round(int_costUpgrade1 * constantsScript.flt_upgradesCostMultiplier);
            PlayerPrefs.SetInt("Upgrade1Cost", GetUpgrade1Cost());
            PlayerPrefs.Save();
            txt_costUpgrade1.text = scoreTrackingScript.SimplifyScore(int_costUpgrade1);
            txt_exampleUpgrade1.text = "+" + scoreTrackingScript.GetPrestigeBonus() + " >> " +
            "+" + (scoreTrackingScript.GetPrestigeBonus() + constantsScript.flt_prestigeBonus);

            // Play purchase sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_purchaseSound.Play();
            }
        }
    }

    public int GetUpgrade1Cost() {
        return int_costUpgrade1;
    }

    // Increase the amount of gold you receive for each prestige.
    public void BuyUpgrade2Clicked()
    {
        if (flt_currentGold >= int_costUpgrade2)
        {
            scoreTrackingScript.MinusGold(int_costUpgrade2);
            scoreTrackingScript.UpgradeGoldBonus();
            int_costUpgrade2 = (int)Math.Round(int_costUpgrade2 * constantsScript.flt_upgradesCostMultiplier);
            PlayerPrefs.SetInt("Upgrade2Cost", GetUpgrade2Cost());
            PlayerPrefs.Save();
            txt_costUpgrade2.text = scoreTrackingScript.SimplifyScore(int_costUpgrade2);
            txt_exampleUpgrade2.text = "+" + scoreTrackingScript.GetGoldBonus() + " >> " +
            "+" + (int)Math.Round(scoreTrackingScript.GetGoldBonus() * constantsScript.flt_goldBonusMultiplier);

            // Play purchase sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_purchaseSound.Play();
            }
        }
    }

    public int GetUpgrade2Cost() {
        return int_costUpgrade2;
    }

    // Double the base earning rate for stage 1.
    public void BuyUpgrade3Clicked()
    {
        if (flt_currentGold >= int_costUpgrade3)
        {
            scoreTrackingScript.MinusGold(int_costUpgrade3);
            scoreTrackingScript.UpgradeS1Multiplier();
            int_costUpgrade3 = (int)Math.Round(int_costUpgrade3 * constantsScript.flt_upgradesCostMultiplier);
            PlayerPrefs.SetInt("Upgrade3Cost", GetUpgrade3Cost());
            PlayerPrefs.Save();
            txt_costUpgrade3.text = scoreTrackingScript.SimplifyScore(int_costUpgrade3);
            txt_exampleUpgrade3.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS1Multiplier() * constantsScript.flt_initialS1UnlockCost) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS1Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS1UnlockCost);
            stage1Script.PurchaseUpgrade();

            // Play purchase sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_purchaseSound.Play();
            }
        }
    }

    public int GetUpgrade3Cost() {
        return int_costUpgrade3;
    }

    // Double the base earning rate for stage 2.
    public void BuyUpgrade4Clicked()
    {
        if (flt_currentGold >= int_costUpgrade4)
        {
            scoreTrackingScript.MinusGold(int_costUpgrade4);
            scoreTrackingScript.UpgradeS2Multiplier();
            int_costUpgrade4 = (int)Math.Round(int_costUpgrade4 * constantsScript.flt_upgradesCostMultiplier);
            PlayerPrefs.SetInt("Upgrade4Cost", GetUpgrade4Cost());
            PlayerPrefs.Save();
            txt_costUpgrade4.text = scoreTrackingScript.SimplifyScore(int_costUpgrade4);
            txt_exampleUpgrade4.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS2Multiplier() * constantsScript.flt_initialS2UnlockCost) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS2Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS2UnlockCost);
            stage2Script.PurchaseUpgrade();

            // Play purchase sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_purchaseSound.Play();
            }
        }
    }

    public int GetUpgrade4Cost() {
        return int_costUpgrade4;
    }

    // Double the base earning rate for stage 3.
    public void BuyUpgrade5Clicked()
    {
        if (flt_currentGold >= int_costUpgrade5)
        {
            scoreTrackingScript.MinusGold(int_costUpgrade5);
            scoreTrackingScript.UpgradeS3Multiplier();
            int_costUpgrade5 = (int)Math.Round(int_costUpgrade5 * constantsScript.flt_upgradesCostMultiplier);
            PlayerPrefs.SetInt("Upgrade5Cost", GetUpgrade5Cost());
            PlayerPrefs.Save();
            txt_costUpgrade5.text = scoreTrackingScript.SimplifyScore(int_costUpgrade5);
            txt_exampleUpgrade5.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS3Multiplier() * constantsScript.flt_initialS3UnlockCost) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS3Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS3UnlockCost);
            stage3Script.PurchaseUpgrade();

            // Play purchase sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_purchaseSound.Play();
            }
        }
    }

    public int GetUpgrade5Cost() {
        return int_costUpgrade5;
    }

    // Double the base earning rate for stage 4.
    public void BuyUpgrade6Clicked()
    {
        if (flt_currentGold >= int_costUpgrade6)
        {
            scoreTrackingScript.MinusGold(int_costUpgrade6);
            scoreTrackingScript.UpgradeS4Multiplier();
            int_costUpgrade6 = (int)Math.Round(int_costUpgrade6 * constantsScript.flt_upgradesCostMultiplier);
            PlayerPrefs.SetInt("Upgrade6Cost", GetUpgrade6Cost());
            PlayerPrefs.Save();
            txt_costUpgrade6.text = scoreTrackingScript.SimplifyScore(int_costUpgrade6);
            txt_exampleUpgrade6.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS4Multiplier() * constantsScript.flt_initialS4UnlockCost) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS4Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS4UnlockCost);
            stage4Script.PurchaseUpgrade();

            // Play purchase sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_purchaseSound.Play();
            }
        }
    }

    public int GetUpgrade6Cost() {
        return int_costUpgrade6;
    }

    // Double the base earning rate for stage 5.
    public void BuyUpgrade7Clicked()
    {
        if (flt_currentGold >= int_costUpgrade7)
        {
            scoreTrackingScript.MinusGold(int_costUpgrade7);
            scoreTrackingScript.UpgradeS5Multiplier();
            int_costUpgrade7 = (int)Math.Round(int_costUpgrade7 * constantsScript.flt_upgradesCostMultiplier);
            PlayerPrefs.SetInt("Upgrade6Cost", GetUpgrade6Cost());
            PlayerPrefs.Save();
            txt_costUpgrade7.text = scoreTrackingScript.SimplifyScore(int_costUpgrade7);
            txt_exampleUpgrade7.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS5Multiplier() * constantsScript.flt_initialS5UnlockCost) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS5Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS5UnlockCost);
            stage5Script.PurchaseUpgrade();

            // Play purchase sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_purchaseSound.Play();
            }
        }
    }

    public int GetUpgrade7Cost() {
        return int_costUpgrade7;
    }

    // Double the base earning rate for stage 6.
    public void BuyUpgrade8Clicked()
    {
        if (flt_currentGold >= int_costUpgrade8)
        {
            scoreTrackingScript.MinusGold(int_costUpgrade8);
            scoreTrackingScript.UpgradeS6Multiplier();
            int_costUpgrade8 = (int)Math.Round(int_costUpgrade8 * constantsScript.flt_upgradesCostMultiplier);
            PlayerPrefs.SetInt("Upgrade8Cost", GetUpgrade8Cost());
            PlayerPrefs.Save();
            txt_costUpgrade8.text = scoreTrackingScript.SimplifyScore(int_costUpgrade8);
            txt_exampleUpgrade8.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS6Multiplier() * constantsScript.flt_initialS6UnlockCost) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS6Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS6UnlockCost);
            stage6Script.PurchaseUpgrade();

            // Play purchase sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_purchaseSound.Play();
            }
        }
    }

    public int GetUpgrade8Cost() {
        return int_costUpgrade8;
    }

    // Double the base earning rate for stage 7.
    public void BuyUpgrade9Clicked()
    {
        if (flt_currentGold >= int_costUpgrade9)
        {
            scoreTrackingScript.MinusGold(int_costUpgrade9);
            scoreTrackingScript.UpgradeS7Multiplier();
            int_costUpgrade9 = (int)Math.Round(int_costUpgrade9 * constantsScript.flt_upgradesCostMultiplier);
            PlayerPrefs.SetInt("Upgrade9Cost", GetUpgrade9Cost());
            PlayerPrefs.Save();
            txt_costUpgrade9.text = scoreTrackingScript.SimplifyScore(int_costUpgrade6);
            txt_exampleUpgrade9.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS7Multiplier() * constantsScript.flt_initialS7UnlockCost) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS7Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS7UnlockCost);
            stage7Script.PurchaseUpgrade();

            // Play purchase sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_purchaseSound.Play();
            }
        }
    }

    public int GetUpgrade9Cost() {
        return int_costUpgrade9;
    }

    // Double the base earning rate for stage 8.
    public void BuyUpgrade10Clicked()
    {
        if (flt_currentGold >= int_costUpgrade10)
        {
            scoreTrackingScript.MinusGold(int_costUpgrade10);
            scoreTrackingScript.UpgradeS8Multiplier();
            int_costUpgrade6 = (int)Math.Round(int_costUpgrade10 * constantsScript.flt_upgradesCostMultiplier);
            PlayerPrefs.SetInt("Upgrade10Cost", GetUpgrade6Cost());
            PlayerPrefs.Save();
            txt_costUpgrade10.text = scoreTrackingScript.SimplifyScore(int_costUpgrade10);
            txt_exampleUpgrade10.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS8Multiplier() * constantsScript.flt_initialS8UnlockCost) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS8Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS8UnlockCost);
            stage8Script.PurchaseUpgrade();

            // Play purchase sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_purchaseSound.Play();
            }
        }
    }

    public int GetUpgrade10Cost() {
        return int_costUpgrade10;
    }

    // Double the base earning rate for stage 9.
    public void BuyUpgrade11Clicked()
    {
        if (flt_currentGold >= int_costUpgrade11)
        {
            scoreTrackingScript.MinusGold(int_costUpgrade11);
            scoreTrackingScript.UpgradeS9Multiplier();
            int_costUpgrade11 = (int)Math.Round(int_costUpgrade11 * constantsScript.flt_upgradesCostMultiplier);
            PlayerPrefs.SetInt("Upgrade11Cost", GetUpgrade11Cost());
            PlayerPrefs.Save();
            txt_costUpgrade11.text = scoreTrackingScript.SimplifyScore(int_costUpgrade11);
            txt_exampleUpgrade11.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS9Multiplier() * constantsScript.flt_initialS9UnlockCost) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS9Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS9UnlockCost);
            stage9Script.PurchaseUpgrade();

            // Play purchase sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_purchaseSound.Play();
            }
        }
    }

    public int GetUpgrade11Cost() {
        return int_costUpgrade11;
    }

    // Double the base earning rate for stage 10.
    public void BuyUpgrade12Clicked()
    {
        if (flt_currentGold >= int_costUpgrade12)
        {
            scoreTrackingScript.MinusGold(int_costUpgrade12);
            scoreTrackingScript.UpgradeS4Multiplier();
            int_costUpgrade12 = (int)Math.Round(int_costUpgrade12 * constantsScript.flt_upgradesCostMultiplier);
            PlayerPrefs.SetInt("Upgrade12Cost", GetUpgrade12Cost());
            PlayerPrefs.Save();
            txt_costUpgrade12.text = scoreTrackingScript.SimplifyScore(int_costUpgrade12);
            txt_exampleUpgrade12.text = "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS10Multiplier() * constantsScript.flt_initialS10UnlockCost) + " >> " +
            "+" + scoreTrackingScript.SimplifyScore(scoreTrackingScript.GetS10Multiplier() * constantsScript.int_upgradeStageMultiplier * constantsScript.flt_initialS10UnlockCost);
            stage10Script.PurchaseUpgrade();

            // Play purchase sound effect
            if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
                aud_purchaseSound.Play();
            }
        }
    }

    public int GetUpgrade12Cost() {
        return int_costUpgrade12;
    }
}
