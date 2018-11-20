using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour {

    private const float headWakeupDistance = 3.0f;

    private AudioSource squishSource;
    private AudioSource landingSource;
    private AudioSource headRespawningSource;
    private Rigidbody2D headBody;
    private float headRespawn = -1.0f;
    private bool inRangeOfPlayer = false;

    private Transform playerTransform;

    private void ChangeHeadState()
    {
        if (transform.parent != playerTransform && headBody.bodyType == RigidbodyType2D.Static)
        {
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            GetComponent<Animator>().SetBool("HeadSleeping", distance < headWakeupDistance ? false : true);
        }
    }


    // Use this for initialization
    void Start () {
        squishSource = GameObject.Find("SquishAudio").GetComponent<AudioSource>();
        landingSource = GameObject.Find("LandingAudio").GetComponent<AudioSource>();
        headRespawningSource = GameObject.Find("HeadSpawningAudio").GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        headBody = GetComponent<Rigidbody2D>();
        ChangeHeadState();
    }
	
    public void PlayerAndHeadCombined()
    {
        GetComponent<Animator>().SetBool("HeadSleeping", false);
    }

    public bool HeadIsRespawning()
    {
        return headRespawn > 0.0f;
    }

	// Update is called once per frame
	void Update () {
		if(headRespawn > 0.0f)
        {
            headRespawn -= Time.deltaTime;
            if(headRespawn < 0.0f)
            {
                playerTransform.GetComponent<PlayerCharacter>().headRespawing = false;
                
                transform.SetParent(playerTransform);
                transform.position = Vector3.zero;
                transform.localPosition = new Vector3(0.0f, PlayerCharacter.headOffset, 0.0f);
                transform.localScale = new Vector3(1.0f, transform.localScale.y, transform.localScale.z);
                headRespawningSource.Play();
            }
        }

        //If the is on the ground
        ChangeHeadState();
    }

    private void DestroyHead()
    {
        headRespawn = 1.0f;
        playerTransform.GetComponent<PlayerCharacter>().headRespawing = true;
        
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        squishSource.Play();
        GetComponent<Animator>().SetBool("ThrowHead", false);
        GetComponent<Animator>().SetTrigger("Death");
        playerTransform.GetComponent<PlayerCharacter>().HeadDestroyed();
        playerTransform.GetComponent<Animator>().SetBool("HasHead", true);
    }

    private void setHeadSpawnPosition()
    {
        PlayerCharacter.currentSpawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        landingSource.Play();
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Animator>().SetBool("ThrowHead", false);
        playerTransform.GetComponent<PlayerCharacter>().HeadLanded();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
        {
            if (collision.gameObject.tag == "Ground")
            {
                for (int i = 0; i < collision.contacts.Length; ++i)
                {
                    Debug.Log(collision.contacts[i].normal.y);
                    //If the head collides with the top of the platform
                    if (collision.contacts[i].normal.y > 0.9f && collision.contacts[i].normal.y < 1.1f)
                    {
                        setHeadSpawnPosition();
                        return;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the head collides with something tagged Death
        if (collision.gameObject.tag == "Death")
        {
            if (transform.parent != playerTransform)
            {
                DestroyHead();
            }
        }
        //If the head collides with a crusher platform
        else if (collision.gameObject.tag == "Crusher")
        {
            //If the crusher is moving
            if (collision.rigidbody.bodyType == RigidbodyType2D.Dynamic)
            {
                //If the player and the head are attached
                if (transform.parent != playerTransform)
                {
                    DestroyHead();
                }
            }
            else
            {
                for (int i = 0; i < collision.contacts.Length; ++i)
                {
                    //If the head collides with the top of the platform
                    if (collision.contacts[i].normal.y > 0.9f && collision.contacts[i].normal.y < 1.1f)
                    {
                        setHeadSpawnPosition();
                        return;
                    }
                }
            }
        }
        //If the head collides with the ground
        else if (collision.gameObject.tag == "Ground")
        {
            for (int i = 0; i < collision.contacts.Length; ++i)
            {
                Debug.Log(collision.contacts[i].normal.y);
                //If the head collides with the top of the platform
                if (collision.contacts[i].normal.y > 0.9f && collision.contacts[i].normal.y < 1.1f)
                {
                    setHeadSpawnPosition();
                    return;
                }
            }
        }
    }
}
