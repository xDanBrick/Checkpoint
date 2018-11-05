using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSystem : MonoBehaviour {

    [SerializeField] Sprite completeSprite;

	// Use this for initialization
	void Start () {
        int complete = PlayerPrefs.GetInt(gameObject.name + " Complete");
        int time = PlayerPrefs.GetInt(gameObject.name + " Time");
        int collectable = PlayerPrefs.GetInt(gameObject.name + " Collectable");
        if(complete != 0)
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
	void Update () {
		
	}
}
