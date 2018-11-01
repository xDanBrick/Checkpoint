using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 10.0f)] float offset = 2.0f;
    [SerializeField] float minimumX = 0.0f;
    [SerializeField] float maximumX = 100.0f;
    private Transform player, ghost, currentFollow;
    public enum TransformFollow { Player, Ghost };

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ghost = GameObject.Find("Ghost").transform;
        currentFollow = player;
    }

    public void setTransformFollow(TransformFollow follow)
    {
        currentFollow = follow == TransformFollow.Player ? player : ghost;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        float x = currentFollow.position.x + offset;
        if(x < minimumX)
        {
            x = minimumX;
        }
        else if(x > maximumX)
        {
            x = maximumX;
        }
        //if(transform.position.x < player.position.x + offset && transform.position.x < maximumX)
        //{
        //    transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, -1.0f);
        //}
        transform.position = new Vector3(x, transform.position.y, -1.0f);
    }
}