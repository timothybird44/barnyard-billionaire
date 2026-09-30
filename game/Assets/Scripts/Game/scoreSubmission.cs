using Solana.Unity;
using Solana.Unity.Rpc;
using Solana.Unity.Wallet;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ExtendedNumerics;

public class scoreSubmission : MonoBehaviour
{
    // databaseManager databaseManagerScript;
    constants constantsScript;
    GameObject gmo_submissionCanvas, gmo_menuItems, obj_walletMsg;
    AudioSource aud_buttonSound;
    scoreTracking scoreTrackingScript;
    Color clr_error, clr_success, clr_unavailable, clr_available;
    string str_errMsg, str_submitMsg, str_lowerScoreMsg, str_walletChangeMsg, str_walletNoChangeMsg;
    Text txt_walletMsg, txt_changeWallet, txt_walletSaveCooldown, txt_submitScore, txt_submitScoreCooldown;
    dataManager dataManagerScript;
    Button btn_changeWallet, btn_submitScore;

    TMP_InputField ipt_solanaWallet;

    // Start is called before the first frame update
    void Start()
    {
        constantsScript = GameObject.Find("Score").GetComponent<constants>();
        scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();
        clr_error = scoreTrackingScript.HexToColor("FF6666");
        clr_success = scoreTrackingScript.HexToColor("6DFF66");
        clr_unavailable = scoreTrackingScript.HexToColor("767676");
        clr_available = Color.white;

        gmo_submissionCanvas = GameObject.Find("Score Submit Canvas");
        gmo_menuItems = GameObject.Find("Hamburger Menu Items");
        aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();
        dataManagerScript = GameObject.Find("Data Manager").GetComponent<dataManager>();

        ipt_solanaWallet = GameObject.Find("Solana Wallet Input").GetComponent<TMP_InputField>();
        ipt_solanaWallet.text = PlayerPrefs.GetString("SolanaWallet");
        ipt_solanaWallet.interactable = false;

        btn_submitScore = GameObject.Find("btn_submitCurrentScore").GetComponent<Button>();
        txt_submitScore = GameObject.Find("txt_submitCurrentScore").GetComponent<Text>();
        btn_changeWallet = GameObject.Find("btn_changeWallet").GetComponent<Button>();
        txt_changeWallet = GameObject.Find("txt_changeWallet").GetComponent<Text>();

        txt_walletSaveCooldown = GameObject.Find("txt_walletSaveCooldown").GetComponent<Text>();
        txt_submitScoreCooldown = GameObject.Find("txt_submitCurrentScoreCooldown").GetComponent<Text>();

        str_errMsg = "Please enter a valid wallet address.";
        str_submitMsg = "Your score has been successfully submitted.";
        str_lowerScoreMsg = "Your score is lower than your previous score.";
        str_walletChangeMsg = "Your wallet has been successfully changed.";
        str_walletNoChangeMsg = "Your wallet has not changed.";
        
        obj_walletMsg = GameObject.Find("txt_walletMsg");
        txt_walletMsg = obj_walletMsg.GetComponent<Text>();
        obj_walletMsg.SetActive(false);

        CheckWalletChangeAvailability();
        CheckScoreSubmitAvailability();

        gmo_submissionCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckScoreSubmitAvailability() {
        if (PlayerPrefs.GetFloat("ScoreSaveCooldownRemaining") == 0) {
            btn_submitScore.interactable = true;
            txt_submitScore.color = clr_available;
        }
        else {
            btn_submitScore.interactable = false;
            txt_submitScore.color = clr_unavailable;
        }
    }

    public void CheckWalletChangeAvailability() {
        if (PlayerPrefs.GetString("SolanaWallet") == "") {
            ipt_solanaWallet.interactable = true;
            btn_changeWallet.interactable = true;
            txt_changeWallet.text = "Save Wallet";
            txt_changeWallet.color = clr_available;
        }
        else {
            if (PlayerPrefs.GetFloat("WalletSaveCooldownRemaining") == 0) {
                btn_changeWallet.interactable = true;
                txt_changeWallet.text = "Change Wallet";
                txt_changeWallet.color = clr_available;
            }
            else {
                btn_changeWallet.interactable = false;
                txt_changeWallet.text = "Change Wallet";
                txt_changeWallet.color = clr_unavailable;
            }
        }
    }

    public void SubmissionOpenClicked()
    {
        gmo_submissionCanvas.SetActive(true);
        gmo_menuItems.SetActive(false);
        ipt_solanaWallet.text = PlayerPrefs.GetString("SolanaWallet");
        ipt_solanaWallet.interactable = false;
        CheckWalletChangeAvailability();
        CheckScoreSubmitAvailability();

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    public void SubmissionCloseClicked()
    {
        obj_walletMsg.SetActive(false);
        gmo_submissionCanvas.SetActive(false);

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    public async void SaveScoreClicked()
    {
        txt_walletMsg.text = "Saving Score...";
        txt_walletMsg.color = clr_available;
        obj_walletMsg.SetActive(true);
        string str_walletAddress = ipt_solanaWallet.text;
        BigDecimal dec_submittedScore = scoreTrackingScript.GetScore();

        // Check if the entered wallet is valid
        if (CheckValidAddress(str_walletAddress)) {
            if (PlayerPrefs.GetString("SolanaWallet") != str_walletAddress) {
                PlayerPrefs.SetString("SolanaWallet", str_walletAddress);
                PlayerPrefs.SetFloat("WalletSaveCooldownRemaining", constantsScript.flt_walletSaveCooldown);
                PlayerPrefs.Save();
                txt_changeWallet.text = "Change Wallet";
                scoreTrackingScript.WalletSaveClicked();
            }

            bool bool_scoreSaved = await dataManagerScript.SaveScore();
            if (bool_scoreSaved) {
                txt_walletMsg.text = str_submitMsg;
                txt_walletMsg.color = clr_success;
                obj_walletMsg.SetActive(true);
                PlayerPrefs.SetFloat("ScoreSaveCooldownRemaining", constantsScript.flt_scoreSaveCooldown);
                PlayerPrefs.Save();
                scoreTrackingScript.ScoreSubmitClicked();
            }
            else {
                txt_walletMsg.text = str_lowerScoreMsg;
                txt_walletMsg.color = clr_error;
                obj_walletMsg.SetActive(true);
            }
        }
        else {
            txt_walletMsg.text = str_errMsg;
            txt_walletMsg.color = clr_error;
            obj_walletMsg.SetActive(true);
        }

        CheckScoreSubmitAvailability();
        CheckWalletChangeAvailability();
    }

    bool CheckValidAddress(string str_walletAddress)
    {
        bool bool_isValid = true;

        if (str_walletAddress == "") {
            bool_isValid = false;
        }
        else {
            try {
                // Create a PublicKey object from the input string
                PublicKey publicKey = new PublicKey(str_walletAddress);
                // If the PublicKey object is created successfully, the address is likely valid
            }
            catch {
                bool_isValid = false;
            }
        }

        return bool_isValid;
    }

    public void WalletSaveClicked() {
        if (txt_changeWallet.text == "Change Wallet") {
            ipt_solanaWallet.interactable = true;
            txt_changeWallet.text = "Save Wallet";
        }
        else  if (txt_changeWallet.text == "Save Wallet") {
            txt_walletMsg.text = "Saving Wallet...";
            txt_walletMsg.color = clr_available;
            obj_walletMsg.SetActive(true);
            string str_walletAddress = ipt_solanaWallet.text;

            // Check if the entered wallet is valid
            if (CheckValidAddress(str_walletAddress)) {
                if (PlayerPrefs.GetString("SolanaWallet") != str_walletAddress) {
                    PlayerPrefs.SetString("SolanaWallet", str_walletAddress);
                    PlayerPrefs.SetFloat("WalletSaveCooldownRemaining", constantsScript.flt_walletSaveCooldown);
                    PlayerPrefs.Save();
                    ipt_solanaWallet.interactable = false;
                    CheckWalletChangeAvailability();
                    txt_changeWallet.text = "Change Wallet";
                    scoreTrackingScript.WalletSaveClicked();

                    txt_walletMsg.text = str_walletChangeMsg;
                    txt_walletMsg.color = clr_success;
                    obj_walletMsg.SetActive(true);
                }
                else {
                    ipt_solanaWallet.interactable = false;
                    CheckWalletChangeAvailability();
                    txt_changeWallet.text = "Change Wallet";
                    txt_walletMsg.text = str_walletNoChangeMsg;
                    txt_walletMsg.color = clr_success;
                    obj_walletMsg.SetActive(true);
                }
            }
            else {
                txt_walletMsg.text = str_errMsg;
                txt_walletMsg.color = clr_error;
                obj_walletMsg.SetActive(true);
            }
        }
    }

    public IEnumerator ScoreResetTime(float flt_elapsedTime = 0)
    {
        CheckScoreSubmitAvailability();

        while (flt_elapsedTime < constantsScript.flt_scoreSaveCooldown)
        {
            flt_elapsedTime += Time.deltaTime;
            txt_submitScoreCooldown.text = SimplifyTime(constantsScript.flt_scoreSaveCooldown - flt_elapsedTime);
            PlayerPrefs.SetFloat("ScoreSaveCooldownRemaining", constantsScript.flt_scoreSaveCooldown-flt_elapsedTime);
            PlayerPrefs.Save();

            yield return null;
        }

        PlayerPrefs.SetFloat("ScoreSaveCooldownRemaining", 0);
        PlayerPrefs.Save();
        flt_elapsedTime = 0;
        txt_submitScoreCooldown.text = "";
        CheckScoreSubmitAvailability();
    }

    public IEnumerator WalletResetTime(float flt_elapsedTime = 0)
    {
        CheckWalletChangeAvailability();

        while (flt_elapsedTime < constantsScript.flt_walletSaveCooldown)
        {
            flt_elapsedTime += Time.deltaTime;
            txt_walletSaveCooldown.text = SimplifyTime(constantsScript.flt_walletSaveCooldown - flt_elapsedTime);
            PlayerPrefs.SetFloat("WalletSaveCooldownRemaining", constantsScript.flt_walletSaveCooldown-flt_elapsedTime);
            PlayerPrefs.Save();

            yield return null;
        }

        PlayerPrefs.SetFloat("WalletSaveCooldownRemaining", 0);
        PlayerPrefs.Save();
        flt_elapsedTime = 0;
        txt_walletSaveCooldown.text = "";
        CheckWalletChangeAvailability();
    }

    public void OpenLearnMoreClicked() {
        Application.OpenURL("https://www.barnyardbillionaire.com/leaderboard?how-to-earn=true");
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
        else if (flt_time < 86400)
        {
            int int_hours = (int)(flt_time / 3600);
            int int_mins = (int)((flt_time - (int_hours * 3600)) / 60);
            str_return = int_hours + "h";
            if (int_mins != 0)
            {
                str_return += " " + int_mins + "m";
            }
        }
        else {
            int int_days = (int)(flt_time / 86400);
            int int_hours = (int)((flt_time - (int_days * 86400)) / 3600);
            str_return = int_days + "d";
            if (int_hours != 0)
            {
                str_return += " " + int_hours + "h";
            }
        }

        return str_return;
    }
}
