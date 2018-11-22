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
            m_Jump = Input.GetKey(KeyCode.Space);
            
        }

        //Place the head down
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_Character.DropHead();
        }

        //Throw head
        if (Input.GetKeyDown(KeyCode.W))
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
        float h = 0.0f;
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            h = 1.0f;
        }
        else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            h = -1.0f;
        }
        // Pass all parameters to the character control script.
        m_Character.Move(h, m_Jump);
        m_Jump = false;
    }
}
