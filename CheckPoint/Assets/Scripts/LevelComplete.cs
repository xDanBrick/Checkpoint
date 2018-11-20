using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour {

    // Use this for initialization
    void Start () {
        string levelName = "Level " + (LevelStats.currentLevel + 1).ToString();
        int complete = PlayerPrefs.GetInt(levelName + " Complete");
        int time = PlayerPrefs.GetInt(levelName + " Time");
        int collectable = PlayerPrefs.GetInt(levelName + " Collectable");
        if (complete != 0)
        {
            transform.Find("Star 1").GetComponent<Image>().color = Color.white;
        }
        if (time != 0)
        {
            transform.Find("Star 2").GetComponent<Image>().color = Color.white;
        }
        if (collectable != 0)
        {
            transform.Find("Star 3").GetComponent<Image>().color = Color.white;
        }
        transform.Find("Level Complete Number").GetComponent<Text>().text = (LevelStats.currentLevel + 1).ToString();
        transform.Find("TargetTime").GetComponent<Text>().text = "Target Time: " + (LevelStats.levelTimes[LevelStats.currentLevel]).ToString("f1");
        transform.Find("TimeText").GetComponent<Text>().text = "Your time is: " + LevelStats.levelTime.ToString("f1");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (LevelStats.currentLevel < 4)
            {
                ++LevelStats.currentLevel;
                UnityEngine.SceneManagement.SceneManager.LoadScene("LevelIntro");
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
            }
        }
    }
}
