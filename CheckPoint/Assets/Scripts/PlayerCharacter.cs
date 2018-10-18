using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    const string headName = "TestHead";

    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
    [SerializeField] private float dropDistance = 0.5f;
    [SerializeField] private float throwDistance = 400.0f;

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = 0.2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    public static Vector3 currentSpawnPosition;
    private Transform m_PlayerHead;
    bool canPutdownHead = true;
    private AudioSource jumpSource;
    private AudioSource bodySquishSource;
    private AudioSource throwSource;

    private float throwDelay = -1.0f;
    private float bodyRespawnDelay = -1.0f;
    bool canMovePlayer = true;

    private void Awake()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
       
        m_PlayerHead = GameObject.Find(headName).transform;
        currentSpawnPosition = new Vector3(m_PlayerHead.transform.position.x, m_PlayerHead.transform.position.y + 0.5f, m_PlayerHead.transform.position.z);
        jumpSource = GameObject.Find("JumpAudio").GetComponent<AudioSource>();
        throwSource = GameObject.Find("ThrowAudio").GetComponent<AudioSource>();
        bodySquishSource = GameObject.Find("BodySquishAudio").GetComponent<AudioSource>();
    }


    private void FixedUpdate()
    {
        m_Grounded = false;

        //// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        //// This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        { 
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        
    }

    private void Update()
    {
        if (throwDelay >= 0.0f)
        {
            throwDelay -= Time.deltaTime;
            if (throwDelay < 0.0f)
            {
                m_PlayerHead.Translate(1.0f, 0.0f, 0.0f);
                m_PlayerHead.SetParent(null);
                Rigidbody2D body = m_PlayerHead.GetComponent<Rigidbody2D>();
                body.bodyType = RigidbodyType2D.Dynamic;
                m_PlayerHead.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
                m_PlayerHead.GetComponent<Rigidbody2D>().simulated = true;
                body.AddRelativeForce(new Vector2(transform.localScale.x > 0.0f ? throwDistance : -throwDistance, 200.0f));
                throwSource.Play();
                throwDelay = -1.0f;
                //BoxCollider2D collider = GetComponent<BoxCollider2D>();
                //collider.size = new Vector2(0.4f, 0.8f);
            }
        }
        if (bodyRespawnDelay >= 0.0f)
        {
            bodyRespawnDelay -= Time.deltaTime;
            if (bodyRespawnDelay < 0.0f)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                m_PlayerHead.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                m_PlayerHead.GetComponent<Rigidbody2D>().simulated = false;
                m_PlayerHead.transform.SetParent(transform);
                m_PlayerHead.transform.position = Vector3.zero;
                m_PlayerHead.transform.localPosition = new Vector3(0.0f, 0.65f, 0.0f);
                canMovePlayer = true;
            }
        }
    }

    public void Move(float move, bool jump)
    {
        if (canMovePlayer)
        {
            if (m_Rigidbody2D.bodyType == RigidbodyType2D.Dynamic)
            {
                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("WalkSpeed", Math.Abs(move));

                if (m_PlayerHead.parent == transform)
                {
                    m_PlayerHead.GetComponent<Animator>().SetFloat("WalkSpeed", Math.Abs(move));
                }

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // If the player should jump...
                if (m_Grounded && jump) // m_Anim.SetTrigger(0);
                {
                    // Add a vertical force to the player.
                    m_Grounded = false;
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                    jumpSource.Play();
                    m_Anim.SetTrigger("Jump");
                }
            }
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the player collides with death platform
        if (collision.gameObject.tag == "Death")
        {
            //If the head exists and is not attached to the player
            if (m_PlayerHead.parent == transform || m_PlayerHead.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            {
                //Reset the players position
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                GameObject.Find("FadeImage").GetComponent<FadeScript>().StartFade(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, 3.0f);
            }
            else
            {
                bodyRespawnDelay = 2.0f;
                canMovePlayer = false;
                transform.position = currentSpawnPosition;
                GetComponent<SpriteRenderer>().enabled = false;
            }
            bodySquishSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canPutdownHead = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canPutdownHead = true;
        }
    }
    public void DropHead()
    {
        if (m_PlayerHead.parent == transform)
        {
            if(canPutdownHead)
            {
                //Place the head down in from of what ever way the player is facing
                m_PlayerHead.Translate(dropDistance, 0.0f, 0.0f);
                m_PlayerHead.SetParent(null);
                m_PlayerHead.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                m_PlayerHead.GetComponent<Rigidbody2D>().simulated = true;
            }
        }
        else if(Mathf.Abs(transform.position.x - m_PlayerHead.position.x) < 1.3f && Mathf.Abs(transform.position.y - m_PlayerHead.position.y) < 1.3f)
        {
            m_PlayerHead.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            m_PlayerHead.GetComponent<Rigidbody2D>().simulated = false;
            m_PlayerHead.transform.SetParent(transform);
            m_PlayerHead.transform.position = Vector3.zero;
            m_PlayerHead.transform.localPosition = new Vector3(0.0f, 0.65f, 0.0f);
        }
    }

    public void ThrowHead()
    {
        if (m_PlayerHead.parent == transform)
        {
            if (canPutdownHead)
            {
                throwDelay = 0.25f;
                m_Anim.SetTrigger("ThrowHead");
            }
        }
    }
}