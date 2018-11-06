﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

    private int buttonIndex = 0;
    private Transform indicator;
    // Use this for initialization
    void Start () {
        indicator = transform.Find("Indicator");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (buttonIndex < 2)
            {
                RectTransform trans = indicator.GetComponent<RectTransform>();
                trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y - 90, trans.localPosition.z);
                GameObject.Find("Text " + (buttonIndex + 1).ToString()).GetComponent<Text>().color = Color.white;
                ++buttonIndex;
                GameObject.Find("Text " + (buttonIndex + 1).ToString()).GetComponent<Text>().color = Color.red;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && buttonIndex > 0)
        {
            RectTransform trans = indicator.GetComponent<RectTransform>();
                trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y + 90, trans.localPosition.z);
                GameObject.Find("Text " + (buttonIndex + 1).ToString()).GetComponent<Text>().color = Color.white;
                --buttonIndex;
                GameObject.Find("Text " + (buttonIndex + 1).ToString()).GetComponent<Text>().color = Color.red;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            if (buttonIndex == 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
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