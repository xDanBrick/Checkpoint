using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherScript : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            transform.parent.Find("Spikes").GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        }
    }
}
