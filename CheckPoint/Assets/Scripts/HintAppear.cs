using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintAppear : MonoBehaviour {

    [SerializeField] Transform hintSprite;
    private bool hintIsActive = false;
    float startPosition = 0.0f;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if(hintIsActive)
        {
            if(startPosition < 1.0f)
            {
                startPosition += 0.005f;
                hintSprite.Translate(new Vector3(0.0f, 0.005f, 0.0f));
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!hintSprite.GetComponent<SpriteRenderer>().enabled)
            {
                hintSprite.GetComponent<SpriteRenderer>().enabled = true;
                hintIsActive = true;
            }
        }
    }
}
