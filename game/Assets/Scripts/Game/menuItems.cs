using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuItems : MonoBehaviour
{
    constants constantsScript;
    GameObject gmo_menuItems;
    AudioSource aud_buttonSound;  

    // Start is called before the first frame update
    void Start()
    {
        constantsScript = GameObject.Find("Score").GetComponent<constants>();
        aud_buttonSound = GameObject.Find("Button Sound").GetComponent<AudioSource>();
        gmo_menuItems = GameObject.Find("Hamburger Menu Items");
        
        StartCoroutine(HideMenuItems());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator HideMenuItems()
    {
        yield return new WaitForSeconds(constantsScript.flt_loadTime);
        gmo_menuItems.SetActive(false);
    }

    public void MenuOpenClicked() 
    {
        gmo_menuItems.SetActive(true);

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }

    public void MenuCloseClicked()
    {
        gmo_menuItems.SetActive(false);

        // Play button sound effect
        if ((PlayerPrefs.GetInt("Loaded") == 1) && (PlayerPrefs.GetInt("EffectsAudio") == 1)) {
            aud_buttonSound.Play();
        }
    }
}
