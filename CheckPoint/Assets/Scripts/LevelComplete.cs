using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour {

    [SerializeField] Sprite completeSprite;
    // Use this for initialization
    void Start () {
        string levelName = "Level " + (LevelStats.currentLevel + 1).ToString();
        int complete = PlayerPrefs.GetInt(levelName + " Complete");
        int time = PlayerPrefs.GetInt(levelName + " Time");
        int collectable = PlayerPrefs.GetInt(levelName + " Collectable");
        if (complete != 0)
        {
            transform.Find("Star 1").GetComponent<Image>().sprite = completeSprite;
        }
        if (time != 0)
        {
            transform.Find("Star 2").GetComponent<Image>().sprite = completeSprite;
        }
        if (collectable != 0)
        {
            transform.Find("Star 3").GetComponent<Image>().sprite = completeSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ++LevelStats.currentLevel;
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelIntro");
        }
    }
}
