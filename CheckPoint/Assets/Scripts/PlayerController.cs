using System;
using UnityEngine;

[RequireComponent(typeof (PlayerCharacter))]
public class PlayerController : MonoBehaviour
{
    private PlayerCharacter m_Character;
    private bool m_Jump;

    private void Awake()
    {
        m_Character = GetComponent<PlayerCharacter>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            {
                m_Jump = true;
            }
        }

        //Place the head down
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_Character.DropHead();
        }

        //Throw head
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_Character.ThrowHead();
        }
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("PauseMenu").transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }


    private void FixedUpdate()
    {
        // Read the inputs.
        float h = Input.GetAxis("Horizontal");
        
        // Pass all parameters to the character control script.
        m_Character.Move(h, m_Jump);
        m_Jump = false;
    }
}
