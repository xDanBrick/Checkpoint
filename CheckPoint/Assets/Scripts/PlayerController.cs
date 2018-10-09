using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            
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
            Application.Quit();
        }
    }


    private void FixedUpdate()
    {
        // Read the inputs.
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        // Pass all parameters to the character control script.
        m_Character.Move(h, m_Jump);
        m_Jump = false;
    }
}
