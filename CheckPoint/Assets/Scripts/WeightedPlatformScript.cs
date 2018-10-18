using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedPlatformScript : MonoBehaviour {

    private AudioSource creakSource, breakSource;
    private bool headColliding = false;
    private bool bodyColliding = false;
    void Start()
    {
        creakSource = GameObject.Find("BridgeCreakAudio").GetComponent<AudioSource>();
        breakSource = GameObject.Find("BridgeBreakAudio").GetComponent<AudioSource>();
    }

    private void BreakPlatform()
    {
        breakSource.Play();
        Destroy(gameObject);
        GameObject.Find("TestHead").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.Find("TestHead") || headColliding)
            {
                BreakPlatform();
            }
            else
            {
                creakSource.Play();
            }
            bodyColliding = true;
        }
        else if(collision.gameObject.name == "TestHead")
        {
            headColliding = true;
            if (bodyColliding)
            {
                BreakPlatform();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.Find("TestHead"))
            {
                BreakPlatform();
                creakSource.Stop();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            creakSource.Stop();
            bodyColliding = false;
        }
        else if (collision.gameObject.name == "TestHead")
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
            {
                headColliding = false;
            }
        }
    }
}
