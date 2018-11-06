using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class LevelStats
{
    const int LevelCount = 5;
    public static string[] levelIntros = new string[LevelCount] {
        "Get your Head in the Game",
        "Spiky Climb",
        "Precarious Pass",
        "",
        ""
    };

    public static float[] levelTimes = new float[LevelCount]
    {
        20.0f,
        20.0f,
        20.0f,
        20.0f,
        20.0f,
    };
    public static int currentLevel = 0;
}

public class LevelSelect : MonoBehaviour {

    Transform indicator;
    int buttonIndex = 0;
    bool mainMenu = false;
	// Use this for initialization
	void Start () {
        indicator = transform.Find("Indicator");

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(buttonIndex < 4)
            {
                RectTransform trans = indicator.GetComponent<RectTransform>();
                trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y - 60, trans.localPosition.z);
                GameObject.Find("Level " + (buttonIndex + 1).ToString() + " Text").GetComponent<Text>().color = Color.black;
                ++buttonIndex;
                GameObject.Find("Level " + (buttonIndex + 1).ToString() + " Text").GetComponent<Text>().color = Color.red;
            }
            else if(!mainMenu)
            {
                mainMenu = true;
                GameObject.Find("Level " + (buttonIndex + 1).ToString() + " Text").GetComponent<Text>().color = Color.black;
                RectTransform trans = indicator.GetComponent<RectTransform>();
                trans.localPosition = new Vector3(-200.0f, -345.0f, trans.localPosition.z);
                GameObject.Find("Menu Text").GetComponent<Text>().color = Color.red;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && buttonIndex > 0)
        {
            if (mainMenu)
            {
                RectTransform trans = indicator.GetComponent<RectTransform>();
                trans.localPosition = new Vector3(-300.0f, -280.0f, trans.localPosition.z);
                GameObject.Find("Menu Text").GetComponent<Text>().color = Color.black;
                GameObject.Find("Level " + (buttonIndex + 1).ToString() + " Text").GetComponent<Text>().color = Color.red;
                mainMenu = false;
            }
            else
            {
                RectTransform trans = indicator.GetComponent<RectTransform>();
                trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y + 60, trans.localPosition.z);
                GameObject.Find("Level " + (buttonIndex + 1).ToString() + " Text").GetComponent<Text>().color = Color.black;
                --buttonIndex;
                GameObject.Find("Level " + (buttonIndex + 1).ToString() + " Text").GetComponent<Text>().color = Color.red;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(mainMenu)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
            }
            else
            {
                LevelStats.currentLevel = buttonIndex;
                UnityEngine.SceneManagement.SceneManager.LoadScene("LevelIntro");
            }
        }
    }
}
