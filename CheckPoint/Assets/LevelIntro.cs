using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntro : MonoBehaviour {

    // Use this for initialization
	void Start () {
        GetComponent<Text>().text = "Level " + (LevelStats.currentLevel + 1).ToString() + " - " + LevelStats.levelIntros[LevelStats.currentLevel];
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level " + (LevelStats.currentLevel + 1).ToString());
        }
    }
}
