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
        transform.localScale = new Vector3(player.position.x > transform.position.x ? 1.0f : -1.0f, transform.localScale.y, transform.localScale.z);
        ghostAudio.Play();
        // get the angle
        //Vector3 norTar = (player.position - transform.position).normalized;
        //float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
        //// rotate to angle
        //Quaternion rotation = new Quaternion();
        //rotation.eulerAngles = new Vector3(0, 0, angle - 90);
        //transform.rotation = rotation;
    }

	// Update is called once per frame
	void Update () {
		if(playerIsDead)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);
            if (transform.position == player.position)
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
