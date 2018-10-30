using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintOnCollide : MonoBehaviour {

    [SerializeField] string hintText = "Hint";
    
    // Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject.Find("Hint Text").GetComponent<Text>().text = hintText;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject.Find("Hint Text").GetComponent<Text>().text = "";
        }
    }
}
