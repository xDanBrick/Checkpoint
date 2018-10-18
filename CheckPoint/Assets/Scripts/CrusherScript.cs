using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherScript : MonoBehaviour {

    private AudioSource buttonSource;
    void Start()
    {
        buttonSource = GameObject.Find("ButtonPressAudio").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.name == "TestHead")
        {
            buttonSource.Play();
            transform.parent.Find("Spikes").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Destroy(gameObject);
        }
    }
}
