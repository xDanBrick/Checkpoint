using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour {

    private AudioSource squishSource;
    private AudioSource landingSource;
    private float headRespawn = -1.0f;
    // Use this for initialization
    void Start () {
        squishSource = GameObject.Find("SquishAudio").GetComponent<AudioSource>();
        landingSource = GameObject.Find("LandingAudio").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if(headRespawn > 0.0f)
        {
            headRespawn -= Time.deltaTime;
            if(headRespawn < 0.0f)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                Transform player = GameObject.FindGameObjectWithTag("Player").transform;
                transform.SetParent(player);
                transform.position = Vector3.zero;
                transform.localPosition = new Vector3(0.0f, PlayerCharacter.headOffset, 0.0f);
                transform.localScale = new Vector3(1.0f, transform.localScale.y, transform.localScale.z);
                player.GetComponent<Animator>().SetBool("HasHead", true);
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            if (transform.parent != player.transform)
            {
                headRespawn = 2.0f;
                GetComponent<Rigidbody2D>().simulated = false;
                squishSource.Play();
                GetComponent<Animator>().SetTrigger("HeadHitGround");
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else if (collision.gameObject.tag == "Crusher")
        {
            Debug.Log(collision.relativeVelocity.y);
            if (collision.rigidbody.bodyType == RigidbodyType2D.Dynamic)
            {
                Transform player = GameObject.FindGameObjectWithTag("Player").transform;
                if (transform.parent != player.transform)
                {
                    headRespawn = 2.0f;
                    GetComponent<Rigidbody2D>().simulated = false;
                    squishSource.Play();
                    GetComponent<Animator>().SetTrigger("HeadHitGround");
                    GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            else
            {
                for (int i = 0; i < collision.contacts.Length; ++i)
                {
                    if (collision.contacts[i].normal.y > 0.9f && collision.contacts[i].normal.y < 1.1f)
                    {
                        PlayerCharacter.currentSpawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                        landingSource.Play();
                        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                        GetComponent<Animator>().SetTrigger("HeadHitGround");
                        return;
                    }
                }
            }
        }
        else if (collision.gameObject.tag == "Ground")
        {

            for (int i = 0; i < collision.contacts.Length; ++i)
            {
                if (collision.contacts[i].normal.y > 0.9f && collision.contacts[i].normal.y < 1.1f)
                {
                    PlayerCharacter.currentSpawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                    landingSource.Play();
                    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    GetComponent<Animator>().SetTrigger("HeadHitGround");
                    return;
                }
            }
        }
    }
}
