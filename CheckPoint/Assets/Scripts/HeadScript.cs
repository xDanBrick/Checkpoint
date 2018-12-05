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
    private Transform playerTransform;

    public bool disableCheckpoint = false;

    private Rigidbody2D m_RigidBody;
    private Animator m_Animator;

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
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }
	
    public void PlayerAndHeadCombined()
    {
        m_Animator.SetBool("HeadSleeping", false);
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
                m_Animator.SetBool("HeadSleeping", false);
            }
        }

        //If the is on the ground
        ChangeHeadState();
    }

    private void DestroyHead()
    {
        headRespawn = 1.0f;
        playerTransform.GetComponent<PlayerCharacter>().headRespawing = true;

        m_RigidBody.simulated = false;
        if(m_RigidBody.bodyType != RigidbodyType2D.Static)
        {
            m_RigidBody.velocity = Vector2.zero;
        }
        
        squishSource.Play();
        m_Animator.SetBool("ThrowHead", false);
        m_Animator.SetTrigger("Death");
        playerTransform.GetComponent<PlayerCharacter>().HeadDestroyed();
        playerTransform.GetComponent<Animator>().SetBool("HasHead", true);
    }

    private void setHeadSpawnPosition()
    {
        if (!disableCheckpoint)
        {
            PlayerCharacter.currentSpawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            landingSource.Play();
            m_RigidBody.bodyType = RigidbodyType2D.Static;
            m_Animator.SetBool("ThrowHead", false);
            playerTransform.GetComponent<PlayerCharacter>().HeadLanded();
        }
        else
        {
            disableCheckpoint = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
        {
            if (collision.gameObject.tag == "Ground")
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
