using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lostConnection : MonoBehaviour
{
    string sceneName;
    Transform tsf_noConnection;
    float minScale, maxScale, pulseSpeed, timeOffset;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "offline") {
            tsf_noConnection = GameObject.Find("img_noConnection").GetComponent<Transform>();
        }

        minScale = 1.0f;
        maxScale = 1.2f;
        pulseSpeed = 1.0f;
        timeOffset = Random.Range(0f, 2 * Mathf.PI);

        StartCoroutine(CheckConnection());
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneName == "offline") {
            float scaleFactor = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(Time.time * pulseSpeed + timeOffset) + 1f) * 0.5f);
            tsf_noConnection.localScale = Vector3.one * scaleFactor;
        }
    }

    IEnumerator CheckConnection()
    {
        while (true) {
            yield return new WaitForSeconds(1f);

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                if (sceneName == "main") {
                    SceneManager.LoadScene("offline");
                }
            }
            else
            {
                if (sceneName == "offline") {
                    SceneManager.LoadScene("main");
                }
            }
        }
    }
}
