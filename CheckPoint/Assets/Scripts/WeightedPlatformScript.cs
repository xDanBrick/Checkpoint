using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedPlatformScript : MonoBehaviour {

    private AudioSource creakSource, breakSource;
    void Start()
    {
        creakSource = GameObject.Find("BridgeCreakAudio").GetComponent<AudioSource>();
        breakSource = GameObject.Find("BridgeBreakAudio").GetComponent<AudioSource>(); ;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.Find("TestHead"))
            {
                breakSource.Play();
                Destroy(gameObject);
            }
            else
            {
                creakSource.Play();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            creakSource.Stop();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.Find("TestHead"))
            {
                breakSource.Play();
                Destroy(gameObject);
            }
        }
    }
}
