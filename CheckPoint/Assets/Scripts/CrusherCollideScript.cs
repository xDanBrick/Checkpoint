using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherCollideScript : MonoBehaviour {

    private AudioSource crusherSource;
    private AudioSource spikeSource;
    // Use this for initialization
    void Start () {
        crusherSource = GameObject.Find("CrusherFallAudio").GetComponent<AudioSource>();
        spikeSource = GameObject.Find("SpikeCrushAudio").GetComponent<AudioSource>(); ;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the player collides with death platform
        if (collision.gameObject.tag == "Ground")
        {
            crusherSource.Play();
            Debug.Log(crusherSource);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        if (collision.gameObject.tag == "Death")
        {
            spikeSource.Play();
            Destroy(collision.gameObject);
        }
    }
}
