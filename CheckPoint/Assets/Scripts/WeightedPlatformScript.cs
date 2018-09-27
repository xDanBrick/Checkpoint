using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedPlatformScript : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.transform.Find("TestHead"))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.Find("TestHead"))
            {
                Destroy(gameObject);
            }
        }
    }
}
