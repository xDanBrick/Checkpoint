using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {

    public bool playerIsDead = false;
    private Transform player;
    [SerializeField] [Range(1.0f, 7.0f)] private float moveSpeed = 3.0f;
    // Use this for initialization

    private AudioSource ghostAudio;
    void Start () {
        player = GameObject.Find("TestHead").transform;
        ghostAudio = GameObject.Find("GhostAudio").GetComponent<AudioSource>();

    }
	
    public void PlayerIsDead()
    {
        playerIsDead = true;
        transform.localScale = new Vector3(player.position.x > transform.position.x ? 1.5f : -1.5f, transform.localScale.y, transform.localScale.z);
        ghostAudio.Play();
    }

	// Update is called once per frame
	void Update () {
		if(playerIsDead)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y - 0.5f, player.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            if (transform.position == targetPosition)
            {
                playerIsDead = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().RespawnBody();
                GetComponent<SpriteRenderer>().enabled = false;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayer>().setTransformFollow(FollowPlayer.TransformFollow.Player);
                ghostAudio.Stop();
            }
        }
	}
}
