using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {
    [SerializeField] string sceneName = "Level 1";

    private AudioSource levelCompleteSource;

    void Start()
    {
        levelCompleteSource = GameObject.Find("LevelCompleteAudio").GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.Find("TestHead"))
            {
                if (!collision.isTrigger)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    levelCompleteSource.Play();
                    GameObject.Find("FadeImage").GetComponent<FadeScript>().StartFade(sceneName, 1.0f);
                }
            }
        }
    }
}
