using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return))
        {
            GameObject.Find("FadeImage").GetComponent<FadeScript>().StartFade("EX Level 1", 3.0f);
            GameObject audio = GameObject.Find("ThemeMusic");
            DontDestroyOnLoad(audio);
            audio.GetComponent<AudioSource>().PlayDelayed(0.0f);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
