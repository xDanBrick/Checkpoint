using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Text timerText;
    public float timer = 0.0f;


	// Use this for initialization
	void Start () {
        timerText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("f1");
    }
}
