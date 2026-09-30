using System;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using ExtendedNumerics;
using Random = UnityEngine.Random;

public class dataManager: MonoBehaviour
{
    public class ScoreTokenValues
    {
        public string playerName;
        public string playerWallet;
        public string score;
        public string scoreString;
    }

    public class SaveScoreRequest
    {
        public string playerName;
        public string playerWallet;
        public string score;
        public string scoreString;
        public string token;
    }

    public class ReferralTokenValues
    {
        public string referralCode;
    }

    public class ValidateReferralRequest
    {
        public string referralCode;
        public string token;
    }

    public class NameTokenValues
    {
        public string playerName;
        public string refereeCode;
        public string referralCode;
    }

    public class SaveNameRequest
    {
        public string playerName;
        public string refereeCode;
        public string referralCode;
        public string token;
    }

    scoreTracking scoreTrackingScript;
    private string SECRET_KEY;

    // Start is called before the first frame update
    void Start()
    {
        SECRET_KEY = Secrets.SERVER_SECRET_KEY; // Defined in Secrets.cs (git-ignored)
        scoreTrackingScript = GameObject.Find("Score").GetComponent<scoreTracking>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async Task<float> GetReferrals(string str_playerName) {
        // for webGL example gameplay, return no referral bonus
        #if UNITY_WEBGL && !UNITY_EDITOR
            return 0;
        #endif

        var data = new ScoreTokenValues
        {
            playerName = str_playerName,
        };

        string token = GenerateToken(JsonUtility.ToJson(data));

        var requestData = new SaveScoreRequest
        {
            playerName = str_playerName,
            token = token
        };

        string requestJson = JsonUtility.ToJson(requestData);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm("https://barnyardbillionaire.com/api/get_player_referrals.php", requestJson))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(requestJson));
            var asyncOperation = www.SendWebRequest();
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                JObject json = JObject.Parse(www.downloadHandler.text);
                float flt_referralCount = (float)(json["referrals"]);
                return flt_referralCount;
            }
            else
            {
                return 0;
            }
        }
    }

    public async Task<bool> SaveScore() {
        #if UNITY_WEBGL && !UNITY_EDITOR
            return false;
        #endif

        BigDecimal dec_score = BigDecimal.Parse(PlayerPrefs.GetString("Score"));
        string str_playerName = PlayerPrefs.GetString("PlayerName");
        string str_playerWallet = PlayerPrefs.GetString("SolanaWallet");

        // Check how many referrals the player has, to multiply their score
        float flt_referralCount = await GetReferrals(str_playerName);
        if (flt_referralCount > 5) {
            flt_referralCount = 5;
        }
        dec_score = dec_score + (dec_score * (flt_referralCount / 10));

        string str_score = dec_score.ToString();
        string str_scoreString = scoreTrackingScript.SimplifyScore(dec_score);

        var data = new ScoreTokenValues
        {
            playerName = str_playerName,
            playerWallet = str_playerWallet,
            score = str_score,
            scoreString = str_scoreString
        };

        string token = GenerateToken(JsonUtility.ToJson(data));

        var requestData = new SaveScoreRequest
        {
            playerName = str_playerName,
            playerWallet = str_playerWallet,
            score = str_score,
            scoreString = str_scoreString,
            token = token
        };

        string requestJson = JsonUtility.ToJson(requestData);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm("https://barnyardbillionaire.com/api/save_score.php", requestJson))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(requestJson));
            var asyncOperation = www.SendWebRequest();
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    async Task<string> CreateReferralCode() {
        const string str_characterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        bool bool_refferalCreated = false;
        StringBuilder strb_referralCode = new StringBuilder();
        string str_referralCode = "";

        while (!bool_refferalCreated) {
            strb_referralCode = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                int randomIndex = Random.Range(0, str_characterSet.Length);
                strb_referralCode.Append(str_characterSet[randomIndex]);
            }

            str_referralCode = strb_referralCode.ToString();

            #if UNITY_WEBGL && !UNITY_EDITOR
                return str_referralCode;
            #endif

            // Check if referral code is already used
            var data = new ReferralTokenValues
            {
                referralCode = str_referralCode
            };

            string token = GenerateToken(JsonUtility.ToJson(data));

            var requestData = new ValidateReferralRequest
            {
                referralCode = str_referralCode,
                token = token
            };

            string requestJson = JsonUtility.ToJson(requestData);

            using (UnityWebRequest www = UnityWebRequest.PostWwwForm("https://barnyardbillionaire.com/api/validate_referral_code.php", requestJson))
            {
                www.SetRequestHeader("Content-Type", "application/json");
                www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(requestJson));
                var asyncOperation = www.SendWebRequest();
                while (!asyncOperation.isDone)
                {
                    await Task.Yield();
                }

                if (www.result == UnityWebRequest.Result.Success)
                {
                    PlayerPrefs.SetString("ReferralCode", str_referralCode);
                    PlayerPrefs.Save();
                    bool_refferalCreated = true;
                }
            }
        }

        return str_referralCode;
    }

    public async Task<bool> SaveName(string str_playerName, string str_refereeCode) {
        // for webGL example gameplay, return true
        #if UNITY_WEBGL && !UNITY_EDITOR
            return true;
        #endif

        string str_referralCode = await CreateReferralCode();

        var data = new NameTokenValues
        {
            playerName = str_playerName,
            refereeCode = str_refereeCode,
            referralCode = str_referralCode
        };

        string token = GenerateToken(JsonUtility.ToJson(data));

        var requestData = new SaveNameRequest
        {
            playerName = str_playerName,
            refereeCode = str_refereeCode,
            referralCode = str_referralCode,
            token = token
        };

        string requestJson = JsonUtility.ToJson(requestData);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm("https://barnyardbillionaire.com/api/save_name.php", requestJson))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(requestJson));
            var asyncOperation = www.SendWebRequest();
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    private string GenerateToken(string data)
    {
        using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SECRET_KEY)))
        {
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return System.BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
