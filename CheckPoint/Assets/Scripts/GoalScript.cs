using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {
    private AudioSource levelCompleteSource;

    private bool levelComplete = false;
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
                if (!collision.isTrigger && !levelComplete)
                {
                    LevelStats.levelTime = GameObject.Find("Timer").GetComponent<Timer>().timer;
                    GameObject.Find("Timer").GetComponent<Timer>().enabled = false;
                    if (LevelStats.levelTime < LevelStats.levelTimes[LevelStats.currentLevel])
                    {
                        PlayerPrefs.SetInt("Level " + (LevelStats.currentLevel + 1).ToString() + " Time", 1);
                    }
                    if (PlayerCharacter.hasCollectable)
                    {
                        PlayerPrefs.SetInt("Level " + (LevelStats.currentLevel + 1).ToString() + " Collectable", 1);
                    }
                    PlayerPrefs.SetInt("Level " + (LevelStats.currentLevel + 1).ToString() + " Complete", 1);
                    collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    levelCompleteSource.Play();
                    GameObject.Find("FadeImage").GetComponent<FadeScript>().StartFade("LevelOutro", 1.0f);
                    levelComplete = true;
                }
            }
        }
    }
}
