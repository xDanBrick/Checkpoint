using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour {

    private AudioSource squishSource;
    private AudioSource landingSource;
    // Use this for initialization
    void Start () {
        squishSource = GameObject.Find("SquishAudio").GetComponent<AudioSource>();
        landingSource = GameObject.Find("LandingAudio").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crusher" || collision.gameObject.tag == "Death")
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            if(transform.parent != player.transform)
            {
                GetComponent<Rigidbody2D>().simulated = false;
                transform.SetParent(player);
                transform.position = Vector3.zero;
                transform.localPosition = new Vector3(0.0f, 0.3f, 0.0f);
                squishSource.Play();
            }
        }
        else if (collision.gameObject.tag == "Ground")
        {
            PlayerCharacter.currentSpawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            landingSource.Play();
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
