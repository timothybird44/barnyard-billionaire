using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerNameManager : MonoBehaviour
{
    scoreTracking scoreTrackingScript;
    dataManager dataManagerScript;
    Color clr_error, clr_success, clr_unavailable, clr_available;

    GameObject gmo_playerNameSubmission, gmo_submitMsg;
    TMP_InputField ipt_playerName, ipt_referralCode;
    Text txt_submitMsg;
    // Start is called before the first frame update
    void Start()
    {
        scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();
        dataManagerScript = GameObject.Find("Data Manager").GetComponent<dataManager>();
        clr_error = scoreTrackingScript.HexToColor("FF6666");
        clr_success = scoreTrackingScript.HexToColor("6DFF66");
        clr_unavailable = scoreTrackingScript.HexToColor("767676");
        clr_available = Color.white;
            
        gmo_playerNameSubmission = GameObject.Find("Player Name Submission");
        ipt_playerName = GameObject.Find("Name Input").GetComponent<TMP_InputField>();
        ipt_referralCode = GameObject.Find("Referral Input").GetComponent<TMP_InputField>();
        gmo_submitMsg = GameObject.Find("txt_submitNameMsg");
        txt_submitMsg = gmo_submitMsg.GetComponent<Text>();
        gmo_submitMsg.SetActive(false);
        
        // Hide player name popup
        if ((PlayerPrefs.HasKey("HasPlayedBefore")) && (PlayerPrefs.GetString("PlayerName") != ""))
        {
            gmo_playerNameSubmission.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ipt_playerName.text = Regex.Replace(ipt_playerName.text, "[^a-zA-Z0-9]", "");
        ipt_referralCode.text = Regex.Replace(ipt_referralCode.text, "[^a-zA-Z0-9]", "");

        if (ipt_playerName.text.Length > 15) {
            ipt_playerName.text = ipt_playerName.text.Substring(0, 15);
        }
        if (ipt_referralCode.text.Length > 8) {
            ipt_referralCode.text = ipt_referralCode.text.Substring(0, 8);
        }
    }

    public async void NameConfirmClicked() 
    {
        string str_playerName = ipt_playerName.text;

        if (str_playerName == "") {
            txt_submitMsg.text = "Please enter a name.";
            txt_submitMsg.color = clr_error;
            gmo_submitMsg.SetActive(true);
        }
        else {
            txt_submitMsg.text = "Saving Name...";
            txt_submitMsg.color = clr_available;
            gmo_submitMsg.SetActive(true);
        
            bool bool_nameSaved = await dataManagerScript.SaveName(ipt_playerName.text, ipt_referralCode.text);
            if (bool_nameSaved) {
                PlayerPrefs.SetString("PlayerName", str_playerName);
                PlayerPrefs.Save();
                gmo_playerNameSubmission.SetActive(false);
            }
            else {
                txt_submitMsg.text = "That name is already taken.";
                txt_submitMsg.color = clr_error;
                gmo_submitMsg.SetActive(true);
            }
        }
    }
}
