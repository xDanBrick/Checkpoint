using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            //Place the head down
            if (Input.GetKeyDown(KeyCode.Return))
            {
                m_Character.DropHead();
            }

            //Throw head
            if (Input.GetKeyDown(KeyCode.T))
            {
                Transform head = transform.Find("TestHead");
                if (head)
                {
                    head.Translate(1.0f, 0.0f, 0.0f);
                    head.SetParent(null);
                    Rigidbody2D body = head.GetComponent<Rigidbody2D>();
                    body.simulated = true;
                    body.AddForce(new Vector2(transform.localScale.x > 0.0f ? 600.0f : -600.0f, 200.0f));
                }
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
}
