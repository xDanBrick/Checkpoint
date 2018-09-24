using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            GetComponent<Rigidbody2D>().simulated = false;
            transform.SetParent(player);
            transform.position = Vector3.zero;
            transform.localPosition = new Vector3(0.0f, 0.3f, 0.0f);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            PlayerCharacter.currentSpawnPosition = transform.position;
        }
    }
}
