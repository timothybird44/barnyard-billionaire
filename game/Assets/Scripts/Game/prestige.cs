using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExtendedNumerics;

public class prestige : MonoBehaviour
{
    BigDecimal int_prestigeCost, dec_currentScore;
    scoreTracking scoreTrackingScript;
    AudioSource aud_purchaseSound, aud_buttonSound;  
    upgrades upgradesScript;
    managers managersScript;
    Text txt_prestigeCost, txt_description, txt_prestigeYes;
    GameObject gmo_prestigeCanvas, gmo_menuItems;
    Button btn_prestigeYes;
    Color clr_unavailable, clr_available;

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
        scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();
        upgradesScript = GameObject.Find("Upgrades Canvas").GetComponent<upgrades>();
        managersScript = GameObject.Find("Managers Canvas").GetComponent<managers>();
        aud_purchaseSound = GameObject.Find("Purchase Sound").GetComponent<AudioSource>();
        aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();
        gmo_menuItems = GameObject.Find("Hamburger Menu Items");

        clr_unavailable = scoreTrackingScript.HexToColor("767676");
        clr_available = Color.white;

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

        gmo_prestigeCanvas = GameObject.Find("Prestige Canvas");
        btn_prestigeYes = GameObject.Find("btn_prestigeYes").GetComponent<Button>();
        txt_prestigeYes = GameObject.Find("txt_prestigeYes").GetComponent<Text>();
        txt_prestigeCost = GameObject.Find("txt_prestigeCost").GetComponent<Text>();
        txt_description = GameObject.Find("txt_prestigeDescription").GetComponent<Text>();

        gmo_prestigeCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreTrackingScript != null)
        {
            dec_currentScore = scoreTrackingScript.GetScore();

            if (dec_currentScore >= int_prestigeCost)
            {
                btn_prestigeYes.interactable = true;
                txt_prestigeYes.color = clr_available;
            }
            else
            {
                btn_prestigeYes.interactable = false;
                txt_prestigeYes.color = clr_unavailable;
            }
        }
    }

    public void PrestigeOpenClicked()
    {
        int_prestigeCost = scoreTrackingScript.GetPrestigeCost();
        txt_prestigeCost.text = "Cost: $" + scoreTrackingScript.SimplifyScore(int_prestigeCost);
        txt_description.text = "By prestiging, you will reset your farm and remove all farmers. You will receive " +
            PlayerPrefs.GetInt("GoldBonus") + " gold and gain +" + PlayerPrefs.GetFloat("PrestigeBonus") + "% on all your earnings.";

        gmo_prestigeCanvas.SetActive(true);
        gmo_menuItems.SetActive(false);
        

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    public void PrestigeNoClicked()
    {
        gmo_prestigeCanvas.SetActive(false);

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    public void PrestigeYesClicked()
    {
        gmo_prestigeCanvas.SetActive(false);
        upgradesScript.gmo_upgradesCanvas.SetActive(true);
        managersScript.gmo_managersCanvas.SetActive(true);
        managersScript.Prestige();
        stage1Script.Prestige();
        stage2Script.Prestige();
        stage3Script.Prestige();
        stage4Script.Prestige();
        stage5Script.Prestige();
        stage6Script.Prestige();
        stage7Script.Prestige();
        stage8Script.Prestige();
        stage9Script.Prestige();
        stage10Script.Prestige();
        upgradesScript.gmo_upgradesCanvas.SetActive(false);
        managersScript.gmo_managersCanvas.SetActive(false);
        scoreTrackingScript.BuyPrestige();

        // Play purchase sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_purchaseSound.Play();
        }
    }
}
