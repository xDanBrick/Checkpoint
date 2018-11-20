using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSystem : MonoBehaviour {

    // Use this for initialization
	void Start () {
        int complete = PlayerPrefs.GetInt(gameObject.name + " Complete");
        int time = PlayerPrefs.GetInt(gameObject.name + " Time");
        int collectable = PlayerPrefs.GetInt(gameObject.name + " Collectable");
        if(complete != 0)
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
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
