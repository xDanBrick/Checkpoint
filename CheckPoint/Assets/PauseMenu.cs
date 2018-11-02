using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    private Transform indicator;
    private int buttonIndex = 0;
	// Use this for initialization
	void Start () {
        indicator = transform.Find("Indicator");
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.DownArrow) && buttonIndex < 2)
        {
            ++buttonIndex;
            RectTransform trans = indicator.GetComponent<RectTransform>();
            trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y - 30, trans.localPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && buttonIndex > 0)
        {
            RectTransform trans = indicator.GetComponent<RectTransform>();
            trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y + 30, trans.localPosition.z);
            --buttonIndex;
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(buttonIndex == 0)
            {
                gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else if(buttonIndex == 1)
            {
                gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                GameObject.Find("FadeImage").GetComponent<FadeScript>().StartFade(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, 3.0f);
            }
            else if(buttonIndex == 2)
            {
                gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
            }
        }
    }
}
