using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {

    public bool playerIsDead = false;
    private Transform player;
    [SerializeField] [Range(1.0f, 7.0f)] private float moveSpeed = 3.0f;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("TestHead").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerIsDead)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);
            if(transform.position == player.position)
            {
                playerIsDead = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().RespawnBody();
                GetComponent<SpriteRenderer>().enabled = false;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayer>().setTransformFollow(FollowPlayer.TransformFollow.Player);
            }
        }
	}
}
