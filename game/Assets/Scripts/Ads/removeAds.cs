using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeAds : MonoBehaviour
{
    AudioSource aud_purchaseSound, aud_buttonSound;  
    GameObject gmo_removeAdsCanvas, gmo_noAdsButton;

    Transform tsf_removeAds;
    float minScale, maxScale, pulseSpeed, timeOffset;

    // Start is called before the first frame update
    void Start()
    {
        aud_purchaseSound = GameObject.Find("Purchase Sound").GetComponent<AudioSource>();
        aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();
        
        gmo_removeAdsCanvas = GameObject.Find("Remove Ads Canvas");
        gmo_removeAdsCanvas.SetActive(false);

        tsf_removeAds = GameObject.Find("btn_noAds").GetComponent<Transform>();
        gmo_noAdsButton = GameObject.Find("btn_noAds");
        
        minScale = 1.0f;
        maxScale = 1.1f;
        pulseSpeed = 1.5f;
        timeOffset = Random.Range(0f, 2 * Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("ProvideAds") == 0) {
            gmo_noAdsButton.SetActive(false);
        }
        else {
            gmo_noAdsButton.SetActive(true);
        }
        
        float scaleFactor = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(Time.time * pulseSpeed + timeOffset) + 1f) * 0.5f);
        tsf_removeAds.localScale = Vector3.one * scaleFactor;
    }

    public void RemoveAdsOpenClicked() {
        gmo_removeAdsCanvas.SetActive(true);

        if ((PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    public void RemoveAdsCloseClicked() {
        gmo_removeAdsCanvas.SetActive(false);

        if ((PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    public void RemoveAdsPurchaseClicked() {
        PlayerPrefs.SetInt("ProvideAds", 0);

        if ((PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_purchaseSound.Play();
        }

        StartCoroutine(RemoveAds());
    }

    IEnumerator RemoveAds() {
        yield return new WaitForSeconds(0.5f);

        gmo_removeAdsCanvas.SetActive(false);
        gmo_noAdsButton.SetActive(false);
    }
}
