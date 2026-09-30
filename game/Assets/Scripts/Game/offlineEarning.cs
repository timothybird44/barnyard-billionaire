using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ExtendedNumerics;

public class offlineEarning : MonoBehaviour
{
    GameObject gmo_offlineCanvas;
    constants constantsScript;
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

    Text txt_offlineHeader, txt_offlineEarning, txt_doubleEarning, txt_offlineTime, txt_doubleEarningsMsg, txt_offlineCollectDouble;
    BigDecimal dec_offlineEarning, dec_earnings, dec_awayTime;
    string str_lastSessionTime, str_currentSessionTime;
    DateTime dte_lastSessionTime, dte_currentSessionTime;
    TimeSpan flt_timeDifference;

    // Start is called before the first frame update
    async Task Start()
    {
        constantsScript = GameObject.Find("Score").GetComponent<constants>();
        scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();
        aud_purchaseSound = GameObject.Find("Purchase Sound").GetComponent<AudioSource>();
        aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();
        gmo_offlineCanvas = GameObject.Find("Offline Canvas");
        txt_offlineHeader = GameObject.Find("txt_offlineHeader").GetComponent<Text>();
        txt_offlineTime = GameObject.Find("txt_offlineTime").GetComponent<Text>();
        txt_offlineEarning = GameObject.Find("txt_offlineEarning").GetComponent<Text>();
        txt_doubleEarning = GameObject.Find("txt_doubleEarnings").GetComponent<Text>();
        txt_doubleEarningsMsg = GameObject.Find("txt_doubleEarningsMsg").GetComponent<Text>();
        txt_offlineCollectDouble = GameObject.Find("txt_offlineCollectDouble").GetComponent<Text>();
        
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

        if (PlayerPrefs.GetString("PlayerName") != "") {
            txt_offlineHeader.text = "Welcome Back,\n" + PlayerPrefs.GetString("PlayerName") + "!";
        }
        else {
            txt_offlineHeader.text = "Welcome Back,\n Farmer!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static async Task<String> GetCurrentDateTimeAsync()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // HttpClient is not supported in WebGL builds (it crashes with "function signature mismatch"),
        // so the browser demo uses the system time
        await Task.Yield();
        return DateTime.Now.ToString();
#endif

        String response;
        int loopCount;
        
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = "https://www.barnyardbillionaire.com/api/get_current_time.php";
            int maxRetries = 2;
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                try
                {
                    response = await client.GetStringAsync(apiUrl);
                    JObject json = JObject.Parse(response);
                    string dateTimeString = json["CURRENT_TIMESTAMP"].ToString();
                    return dateTimeString; // Success, return the result
                }
                catch (HttpRequestException e)
                {
                    retryCount++;
                    if (retryCount < maxRetries)
                    {
                        await Task.Delay(1000); // Wait for 1 second before retrying (adjust as needed)
                    }
                    else
                    {
                        return null; // Or throw the exception if you prefer
                    }
                }
                catch (Newtonsoft.Json.JsonReaderException e)
                {
                    return null; // No point in retrying if the JSON is invalid
                }
                catch (TaskCanceledException e)
                {
                    return null; // Request was cancelled, no point in retrying immediately
                }
                catch (Exception e)
                {
                    retryCount++;
                    if (retryCount < maxRetries)
                    {
                        await Task.Delay(1000);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return null; // Should not reach here under normal circumstances, but added for completeness
        }
    }

    public string SimplifyTime(BigDecimal dec_time) {
        string str_return = "";
        if (dec_time < 0) {
            dec_time = 0;
        }

        if (dec_time < 60)
        {
            str_return = BigDecimal.Round(dec_time, 0) + "s";
        }
        else if (dec_time < 3600)
        {
            BigDecimal int_mins = BigDecimal.Round((dec_time / 60), 0);
            BigDecimal int_secs = BigDecimal.Round((dec_time - (int_mins * 60)), 0);
            str_return = int_mins + "m";
            if (int_secs != 0)
            {
                str_return += " " + int_secs + "s";
            }
        }
        else
        {
            BigDecimal int_hours = BigDecimal.Round((dec_time / 3600), 0);
            BigDecimal int_mins = BigDecimal.Round(((dec_time - (int_hours * 3600)) / 60), 0);
            str_return = int_hours + "h";
            if (int_mins != 0)
            {
                str_return += " " + int_mins + "m";
            }
        }

        return str_return;
    }

    public async void InitiateOfflineCollection() {
        str_lastSessionTime = PlayerPrefs.GetString("LastSessionTime");
        DateTime.TryParse(str_lastSessionTime, out dte_lastSessionTime);
        dec_earnings = BigDecimal.Parse("0");
        str_currentSessionTime = await GetCurrentDateTimeAsync();

        if (str_currentSessionTime == null) {
            SceneManager.LoadScene("offline");
            return;
        }

        DateTime.TryParse(str_currentSessionTime, out dte_currentSessionTime);
        flt_timeDifference = dte_currentSessionTime - dte_lastSessionTime;
        dec_awayTime = (float)flt_timeDifference.TotalSeconds;
        PlayerPrefs.SetString("AwayTime", dec_awayTime.ToString());
        PlayerPrefs.Save();

        if (PlayerPrefs.HasKey("HasPlayedBefore")) {
            dec_offlineEarning = CalculateAwayEarning();
            txt_offlineTime.text = "You were offline for " + SimplifyTime(dec_awayTime) + ".";
            txt_offlineEarning.text = "$" + scoreTrackingScript.SimplifyScore(dec_offlineEarning);
            txt_doubleEarning.text = "$" + scoreTrackingScript.SimplifyScore(dec_offlineEarning * 2);

            if (PlayerPrefs.GetInt("ProvideAds") == 0) {
                txt_doubleEarningsMsg.text = "Collect your Double Earnings";
                txt_offlineCollectDouble.text = "Collect x2";
            }
        }
        else {
            gmo_offlineCanvas.SetActive(false);
            stage1Script.bool_canCollect = true;
            stage2Script.bool_canCollect = true;
            stage3Script.bool_canCollect = true;
            stage4Script.bool_canCollect = true;
            stage5Script.bool_canCollect = true;
            stage6Script.bool_canCollect = true;
            stage7Script.bool_canCollect = true;
            stage8Script.bool_canCollect = true;
            stage9Script.bool_canCollect = true;
            stage10Script.bool_canCollect = true;
        }

        PlayerPrefs.SetString("LastSessionTime", str_currentSessionTime);
        PlayerPrefs.SetInt("HasPlayedBefore", 1);
        PlayerPrefs.SetInt("Loaded", 1);
        PlayerPrefs.Save();
    }

    public void OfflineCollectClicked() {
        scoreTrackingScript.AddScore(dec_offlineEarning);
        gmo_offlineCanvas.SetActive(false);

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    public void OfflineCollectDoubleClicked() {
        scoreTrackingScript.AddScore(dec_offlineEarning * 2);
        gmo_offlineCanvas.SetActive(false);

        // Play purchase sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_purchaseSound.Play();
        }
    }

    public BigDecimal CalculateAwayEarning() {
        float flt_doubleRemaining = PlayerPrefs.GetFloat("EarnTimeRemaining");
        float flt_progress = 0;

        if (dec_awayTime >= flt_doubleRemaining) {
            PlayerPrefs.SetFloat("EarnTimeRemaining", 0);
        }

        if ((stage1Script.GetLevel() != constantsScript.int_intialSubLevel) && (stage1Script.GetManager())) {
            flt_progress = PlayerPrefs.GetFloat("Stage1Progress");
            dec_earnings += (dec_awayTime / stage1Script.GetRewardTime()) * stage1Script.GetRewardAmount();
            // Account for double earnings
            if (flt_doubleRemaining > 0) {
                if (dec_awayTime > flt_doubleRemaining) {
                    dec_earnings += (flt_doubleRemaining / stage1Script.GetRewardTime()) * stage1Script.GetRewardAmount();
                }
                else {
                    dec_earnings += (dec_awayTime / stage1Script.GetRewardTime()) * stage1Script.GetRewardAmount();
                }
            }

            stage1Script.StartRewardCoroutine(((flt_progress + dec_awayTime) % stage1Script.GetRewardTime()).ToString());
        }
        else if ((stage1Script.GetLevel() != constantsScript.int_intialSubLevel) && !stage1Script.GetManager()) {
            flt_progress = PlayerPrefs.GetFloat("Stage1Progress");
            if ((flt_progress != 0) && ((dec_awayTime - (stage1Script.GetRewardTime() - flt_progress)) >= 0)) {
                dec_earnings += stage1Script.GetRewardAmount();
                // Account for double earnings
                if (flt_doubleRemaining > 0) {
                    if (dec_awayTime <= flt_doubleRemaining) {
                        dec_earnings += stage1Script.GetRewardAmount();
                    }
                }

                PlayerPrefs.SetFloat("Stage1Progress", 0);
                stage1Script.bool_canCollect = true;
            }
            else if ((flt_progress != 0) && ((dec_awayTime - (stage1Script.GetRewardTime() - flt_progress)) < 0)) {
                stage1Script.StartRewardCoroutine((flt_progress + dec_awayTime).ToString());
            }
            else {
                stage1Script.bool_canCollect = true;
            }
        }
        if ((stage2Script.GetLevel() != constantsScript.int_intialSubLevel) && (stage2Script.GetManager())) {
            flt_progress = PlayerPrefs.GetFloat("Stage2Progress");
            dec_earnings += (dec_awayTime / stage2Script.GetRewardTime()) * stage2Script.GetRewardAmount();
            // Account for double earnings
            if (flt_doubleRemaining > 0) {
                if (dec_awayTime > flt_doubleRemaining) {
                dec_earnings += (flt_doubleRemaining / stage2Script.GetRewardTime()) * stage2Script.GetRewardAmount();
                }
                else {
                    dec_earnings += (dec_awayTime / stage2Script.GetRewardTime()) * stage2Script.GetRewardAmount();
                }
            }

            stage2Script.StartRewardCoroutine(((flt_progress + dec_awayTime) % stage2Script.GetRewardTime()).ToString());
        }
        else if ((stage2Script.GetLevel() != constantsScript.int_intialSubLevel) && !stage2Script.GetManager()) {
            flt_progress = PlayerPrefs.GetFloat("Stage2Progress");
            if ((flt_progress != 0) && ((dec_awayTime - (stage2Script.GetRewardTime() - flt_progress)) >= 0)) {
                dec_earnings += stage2Script.GetRewardAmount();
                // Account for double earnings
                if (flt_doubleRemaining > 0) {
                    if (dec_awayTime <= flt_doubleRemaining) {
                        dec_earnings += stage2Script.GetRewardAmount();
                    }
                }

                PlayerPrefs.SetFloat("Stage2Progress", 0);
                stage2Script.bool_canCollect = true;
            }
            else if ((flt_progress != 0) && ((dec_awayTime - (stage2Script.GetRewardTime() - flt_progress)) < 0)) {
                stage2Script.StartRewardCoroutine((flt_progress + dec_awayTime).ToString());
            }
            else {
                stage2Script.bool_canCollect = true;
            }
        }
        if ((stage3Script.GetLevel() != constantsScript.int_intialSubLevel) && (stage3Script.GetManager())) {
            flt_progress = PlayerPrefs.GetFloat("Stage3Progress");
            dec_earnings += (dec_awayTime / stage3Script.GetRewardTime()) * stage3Script.GetRewardAmount();
            // Account for double earnings
            if (flt_doubleRemaining > 0) {
                if (dec_awayTime > flt_doubleRemaining) {
                dec_earnings += (flt_doubleRemaining / stage3Script.GetRewardTime()) * stage3Script.GetRewardAmount();
                }
                else {
                    dec_earnings += (dec_awayTime / stage3Script.GetRewardTime()) * stage3Script.GetRewardAmount();
                }
            }

            stage3Script.StartRewardCoroutine(((flt_progress + dec_awayTime) % stage3Script.GetRewardTime()).ToString());
        }
        else if ((stage3Script.GetLevel() != constantsScript.int_intialSubLevel) && !stage3Script.GetManager()) {
            flt_progress = PlayerPrefs.GetFloat("Stage3Progress");
            if ((flt_progress != 0) && ((dec_awayTime - (stage3Script.GetRewardTime() - flt_progress)) >= 0)) {
                dec_earnings += stage3Script.GetRewardAmount();
                // Account for double earnings
                if (flt_doubleRemaining > 0) {
                    if (dec_awayTime <= flt_doubleRemaining) {
                        dec_earnings += stage3Script.GetRewardAmount();
                    }
                }

                PlayerPrefs.SetFloat("Stage3Progress", 0);
                stage3Script.bool_canCollect = true;
            }
            else if ((flt_progress != 0) && ((dec_awayTime - (stage3Script.GetRewardTime() - flt_progress)) < 0)) {
                stage3Script.StartRewardCoroutine((flt_progress + dec_awayTime).ToString());
            }
            else {
                stage3Script.bool_canCollect = true;
            }
        }
        if ((stage4Script.GetLevel() != constantsScript.int_intialSubLevel) && (stage4Script.GetManager())) {
            flt_progress = PlayerPrefs.GetFloat("Stage4Progress");
            dec_earnings += (dec_awayTime / stage4Script.GetRewardTime()) * stage4Script.GetRewardAmount();
            // Account for double earnings
            if (flt_doubleRemaining > 0) {
                if (dec_awayTime > flt_doubleRemaining) {
                dec_earnings += (flt_doubleRemaining / stage4Script.GetRewardTime()) * stage4Script.GetRewardAmount();
                }
                else {
                    dec_earnings += (dec_awayTime / stage4Script.GetRewardTime()) * stage4Script.GetRewardAmount();
                }
            }

            stage4Script.StartRewardCoroutine(((flt_progress + dec_awayTime) % stage4Script.GetRewardTime()).ToString());
        }
        else if ((stage4Script.GetLevel() != constantsScript.int_intialSubLevel) && !stage4Script.GetManager()) {
            flt_progress = PlayerPrefs.GetFloat("Stage4Progress");
            if ((flt_progress != 0) && ((dec_awayTime - (stage4Script.GetRewardTime() - flt_progress)) >= 0)) {
                dec_earnings += stage4Script.GetRewardAmount();
                // Account for double earnings
                if (flt_doubleRemaining > 0) {
                    if (dec_awayTime <= flt_doubleRemaining) {
                        dec_earnings += stage4Script.GetRewardAmount();
                    }
                }

                PlayerPrefs.SetFloat("Stage4Progress", 0);
                stage4Script.bool_canCollect = true;
            }
            else if ((flt_progress != 0) && ((dec_awayTime - (stage4Script.GetRewardTime() - flt_progress)) < 0)) {
                stage4Script.StartRewardCoroutine((flt_progress + dec_awayTime).ToString());
            }
            else {
                stage4Script.bool_canCollect = true;
            }
        }
        if ((stage5Script.GetLevel() != constantsScript.int_intialSubLevel) && (stage5Script.GetManager())) {
            flt_progress = PlayerPrefs.GetFloat("Stage5Progress");
            dec_earnings += (dec_awayTime / stage5Script.GetRewardTime()) * stage5Script.GetRewardAmount();
            // Account for double earnings
            if (flt_doubleRemaining > 0) {
                if (dec_awayTime > flt_doubleRemaining) {
                dec_earnings += (flt_doubleRemaining / stage5Script.GetRewardTime()) * stage5Script.GetRewardAmount();
                }
                else {
                    dec_earnings += (dec_awayTime / stage5Script.GetRewardTime()) * stage5Script.GetRewardAmount();
                }
            }

            stage5Script.StartRewardCoroutine(((flt_progress + dec_awayTime) % stage5Script.GetRewardTime()).ToString());
        }
        else if ((stage5Script.GetLevel() != constantsScript.int_intialSubLevel) && !stage5Script.GetManager()) {
            flt_progress = PlayerPrefs.GetFloat("Stage5Progress");
            if ((flt_progress != 0) && ((dec_awayTime - (stage5Script.GetRewardTime() - flt_progress)) >= 0)) {
                dec_earnings += stage5Script.GetRewardAmount();
                // Account for double earnings
                if (flt_doubleRemaining > 0) {
                    if (dec_awayTime <= flt_doubleRemaining) {
                        dec_earnings += stage5Script.GetRewardAmount();
                    }
                }

                PlayerPrefs.SetFloat("Stage5Progress", 0);
                stage5Script.bool_canCollect = true;
            }
            else if ((flt_progress != 0) && ((dec_awayTime - (stage5Script.GetRewardTime() - flt_progress)) < 0)) {
                stage5Script.StartRewardCoroutine((flt_progress + dec_awayTime).ToString());
            }
            else {
                stage5Script.bool_canCollect = true;
            }
        }
        if ((stage6Script.GetLevel() != constantsScript.int_intialSubLevel) && (stage6Script.GetManager())) {
            flt_progress = PlayerPrefs.GetFloat("Stage6Progress");
            dec_earnings += (dec_awayTime / stage6Script.GetRewardTime()) * stage6Script.GetRewardAmount();
            // Account for double earnings
            if (flt_doubleRemaining > 0) {
                if (dec_awayTime > flt_doubleRemaining) {
                dec_earnings += (flt_doubleRemaining / stage6Script.GetRewardTime()) * stage6Script.GetRewardAmount();
                }
                else {
                    dec_earnings += (dec_awayTime / stage6Script.GetRewardTime()) * stage6Script.GetRewardAmount();
                }
            }

            stage6Script.StartRewardCoroutine(((flt_progress + dec_awayTime) % stage6Script.GetRewardTime()).ToString());
        }
        else if ((stage6Script.GetLevel() != constantsScript.int_intialSubLevel) && !stage6Script.GetManager()) {
            flt_progress = PlayerPrefs.GetFloat("Stage6Progress");
            if ((flt_progress != 0) && ((dec_awayTime - (stage6Script.GetRewardTime() - flt_progress)) >= 0)) {
                dec_earnings += stage6Script.GetRewardAmount();
                // Account for double earnings
                if (flt_doubleRemaining > 0) {
                    if (dec_awayTime <= flt_doubleRemaining) {
                        dec_earnings += stage6Script.GetRewardAmount();
                    }
                }

                PlayerPrefs.SetFloat("Stage6Progress", 0);
                stage6Script.bool_canCollect = true;
            }
            else if ((flt_progress != 0) && ((dec_awayTime - (stage6Script.GetRewardTime() - flt_progress)) < 0)) {
                stage6Script.StartRewardCoroutine((flt_progress + dec_awayTime).ToString());
            }
            else {
                stage6Script.bool_canCollect = true;
            }
        }
        if ((stage7Script.GetLevel() != constantsScript.int_intialSubLevel) && (stage7Script.GetManager())) {
            flt_progress = PlayerPrefs.GetFloat("Stage7Progress");
            dec_earnings += (dec_awayTime / stage7Script.GetRewardTime()) * stage7Script.GetRewardAmount();
            // Account for double earnings
            if (flt_doubleRemaining > 0) {
                if (dec_awayTime > flt_doubleRemaining) {
                dec_earnings += (flt_doubleRemaining / stage7Script.GetRewardTime()) * stage7Script.GetRewardAmount();
                }
                else {
                    dec_earnings += (dec_awayTime / stage7Script.GetRewardTime()) * stage7Script.GetRewardAmount();
                }
            }

            stage7Script.StartRewardCoroutine(((flt_progress + dec_awayTime) % stage7Script.GetRewardTime()).ToString());
        }
        else if ((stage7Script.GetLevel() != constantsScript.int_intialSubLevel) && !stage7Script.GetManager()) {
            flt_progress = PlayerPrefs.GetFloat("Stage7Progress");
            if ((flt_progress != 0) && ((dec_awayTime - (stage7Script.GetRewardTime() - flt_progress)) >= 0)) {
                dec_earnings += stage7Script.GetRewardAmount();
                // Account for double earnings
                if (flt_doubleRemaining > 0) {
                    if (dec_awayTime <= flt_doubleRemaining) {
                        dec_earnings += stage7Script.GetRewardAmount();
                    }
                }

                PlayerPrefs.SetFloat("Stage7Progress", 0);
                stage7Script.bool_canCollect = true;
            }
            else if ((flt_progress != 0) && ((dec_awayTime - (stage7Script.GetRewardTime() - flt_progress)) < 0)) {
                stage7Script.StartRewardCoroutine((flt_progress + dec_awayTime).ToString());
            }
            else {
                stage7Script.bool_canCollect = true;
            }
        }
        if ((stage8Script.GetLevel() != constantsScript.int_intialSubLevel) && (stage8Script.GetManager())) {
            flt_progress = PlayerPrefs.GetFloat("Stage8Progress");
            dec_earnings += (dec_awayTime / stage8Script.GetRewardTime()) * stage8Script.GetRewardAmount();
            // Account for double earnings
            if (flt_doubleRemaining > 0) {
                if (dec_awayTime > flt_doubleRemaining) {
                dec_earnings += (flt_doubleRemaining / stage8Script.GetRewardTime()) * stage8Script.GetRewardAmount();
                }
                else {
                    dec_earnings += (dec_awayTime / stage8Script.GetRewardTime()) * stage8Script.GetRewardAmount();
                }
            }

            stage8Script.StartRewardCoroutine(((flt_progress + dec_awayTime) % stage8Script.GetRewardTime()).ToString());
        }
        else if ((stage8Script.GetLevel() != constantsScript.int_intialSubLevel) && !stage8Script.GetManager()) {
            flt_progress = PlayerPrefs.GetFloat("Stage8Progress");
            if ((flt_progress != 0) && ((dec_awayTime - (stage8Script.GetRewardTime() - flt_progress)) >= 0)) {
                dec_earnings += stage8Script.GetRewardAmount();
                // Account for double earnings
                if (flt_doubleRemaining > 0) {
                    if (dec_awayTime <= flt_doubleRemaining) {
                        dec_earnings += stage8Script.GetRewardAmount();
                    }
                }

                PlayerPrefs.SetFloat("Stage8Progress", 0);
                stage8Script.bool_canCollect = true;
            }
            else if ((flt_progress != 0) && ((dec_awayTime - (stage8Script.GetRewardTime() - flt_progress)) < 0)) {
                stage8Script.StartRewardCoroutine((flt_progress + dec_awayTime).ToString());
            }
            else {
                stage8Script.bool_canCollect = true;
            }
        }
        if ((stage9Script.GetLevel() != constantsScript.int_intialSubLevel) && (stage9Script.GetManager())) {
            flt_progress = PlayerPrefs.GetFloat("Stage9Progress");
            dec_earnings += (dec_awayTime / stage9Script.GetRewardTime()) * stage9Script.GetRewardAmount();
            // Account for double earnings
            if (flt_doubleRemaining > 0) {
                if (dec_awayTime > flt_doubleRemaining) {
                dec_earnings += (flt_doubleRemaining / stage9Script.GetRewardTime()) * stage9Script.GetRewardAmount();
                }
                else {
                    dec_earnings += (dec_awayTime / stage9Script.GetRewardTime()) * stage9Script.GetRewardAmount();
                }
            }

            stage9Script.StartRewardCoroutine(((flt_progress + dec_awayTime) % stage9Script.GetRewardTime()).ToString());
        }
        else if ((stage9Script.GetLevel() != constantsScript.int_intialSubLevel) && !stage9Script.GetManager()) {
            flt_progress = PlayerPrefs.GetFloat("Stage9Progress");
            if ((flt_progress != 0) && ((dec_awayTime - (stage9Script.GetRewardTime() - flt_progress)) >= 0)) {
                dec_earnings += stage9Script.GetRewardAmount();
                // Account for double earnings
                if (flt_doubleRemaining > 0) {
                    if (dec_awayTime <= flt_doubleRemaining) {
                        dec_earnings += stage9Script.GetRewardAmount();
                    }
                }

                PlayerPrefs.SetFloat("Stage9Progress", 0);
                stage9Script.bool_canCollect = true;
            }
            else if ((flt_progress != 0) && ((dec_awayTime - (stage9Script.GetRewardTime() - flt_progress)) < 0)) {
                stage9Script.StartRewardCoroutine((flt_progress + dec_awayTime).ToString());
            }
            else {
                stage9Script.bool_canCollect = true;
            }
        }
        if ((stage10Script.GetLevel() != constantsScript.int_intialSubLevel) && (stage10Script.GetManager())) {
            flt_progress = PlayerPrefs.GetFloat("Stage10Progress");
            dec_earnings += (dec_awayTime / stage10Script.GetRewardTime()) * stage10Script.GetRewardAmount();
            // Account for double earnings
            if (flt_doubleRemaining > 0) {
                if (dec_awayTime > flt_doubleRemaining) {
                dec_earnings += (flt_doubleRemaining / stage10Script.GetRewardTime()) * stage10Script.GetRewardAmount();
                }
                else {
                    dec_earnings += (dec_awayTime / stage10Script.GetRewardTime()) * stage10Script.GetRewardAmount();
                }
            }

            stage10Script.StartRewardCoroutine(((flt_progress + dec_awayTime) % stage10Script.GetRewardTime()).ToString());
        }
        else if ((stage10Script.GetLevel() != constantsScript.int_intialSubLevel) && !stage10Script.GetManager()) {
            flt_progress = PlayerPrefs.GetFloat("Stage10Progress");
            if ((flt_progress != 0) && ((dec_awayTime - (stage10Script.GetRewardTime() - flt_progress)) >= 0)) {
                dec_earnings += stage10Script.GetRewardAmount();
                // Account for double earnings
                if (flt_doubleRemaining > 0) {
                    if (dec_awayTime <= flt_doubleRemaining) {
                        dec_earnings += stage10Script.GetRewardAmount();
                    }
                }

                PlayerPrefs.SetFloat("Stage10Progress", 0);
                stage10Script.bool_canCollect = true;
            }
            else if ((flt_progress != 0) && ((dec_awayTime - (stage10Script.GetRewardTime() - flt_progress)) < 0)) {
                stage10Script.StartRewardCoroutine((flt_progress + dec_awayTime).ToString());
            }
            else {
                stage10Script.bool_canCollect = true;
            }
        }

        PlayerPrefs.Save();
        return dec_earnings;
    }
}
