using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {
    [SerializeField] string sceneName = "Level 1";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.transform.Find("TestHead"))
            {
                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                GameObject.Find("FadeImage").GetComponent<FadeScript>().StartFade(sceneName);
            }
        }
    }
}
