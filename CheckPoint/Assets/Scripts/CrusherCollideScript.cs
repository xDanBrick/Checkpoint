using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherCollideScript : MonoBehaviour {

    private AudioSource crusherSource;
    // Use this for initialization
    void Start () {
        crusherSource = GameObject.Find("CrusherFallAudio").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the player collides with death platform
        if (collision.gameObject.tag == "Ground")
        {
            crusherSource.Play();
            Debug.Log(crusherSource);
        }
        if (collision.gameObject.tag == "Death")
        {
            Destroy(collision.gameObject);
        }
    }
}
