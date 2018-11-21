using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

    private int buttonIndex = 0;
    private Transform indicator;
    // Use this for initialization
    private AudioSource menuBeepSource;
    private AudioSource menuConfirmSource;
    void Start () {
        indicator = transform.Find("Indicator");
        menuConfirmSource = GameObject.Find("MenuConfirmAudio").GetComponent<AudioSource>();
        menuBeepSource = GameObject.Find("MenuBeepAudio").GetComponent<AudioSource>();
        DontDestroyOnLoad(GameObject.Find("MenuMusic"));
        //Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
        {
            if (buttonIndex < 2)
            {
                RectTransform trans = indicator.GetComponent<RectTransform>();
                trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y - 90, trans.localPosition.z);
                GameObject.Find("Text " + (buttonIndex + 1).ToString()).GetComponent<Text>().color = Color.white;
                ++buttonIndex;
                GameObject.Find("Text " + (buttonIndex + 1).ToString()).GetComponent<Text>().color = Color.red;
                menuBeepSource.Play();
            }
        }
        else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && buttonIndex > 0)
        {
            RectTransform trans = indicator.GetComponent<RectTransform>();
                trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y + 90, trans.localPosition.z);
                GameObject.Find("Text " + (buttonIndex + 1).ToString()).GetComponent<Text>().color = Color.white;
                --buttonIndex;
                GameObject.Find("Text " + (buttonIndex + 1).ToString()).GetComponent<Text>().color = Color.red;
            menuBeepSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            menuConfirmSource.Play();
            if (buttonIndex == 0)
            {
                LevelStats.currentLevel = 0;
                PlayerPrefs.DeleteAll();
                UnityEngine.SceneManagement.SceneManager.LoadScene("LevelIntro");
            }
            else if (buttonIndex == 1)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
            }
            else if (buttonIndex == 2)
            {
                Application.Quit();
            }
            
        }
    }
}
