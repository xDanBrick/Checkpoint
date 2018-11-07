using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpen : MonoBehaviour {

    float gateOpeningDirection = 0.0f;
   
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       float inc = Time.deltaTime * gateOpeningDirection;
        if(inc != 0.0f)
        {
            Transform gateTransform = transform.GetChild(0);
            gateTransform.Translate(0.0f, inc, 0.0f);
            gateTransform.localScale = new Vector3(gateTransform.localScale.x, gateTransform.localScale.y - (inc), gateTransform.localScale.z);
            if ((gateTransform.localScale.y < 0.01f && gateOpeningDirection == 1.0f) || (gateTransform.localScale.y >= 1.0f && gateOpeningDirection == -1.0f))
            {
                gateOpeningDirection = 0.0f;
            }
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Transform gateTransform = transform.GetChild(0);
            //If Gate is up and 
            if (collision.gameObject.transform.Find("TestHead"))
            {
                if (gateTransform.localScale.y >= 0.01f)
                {
                    gateOpeningDirection = 1.0f;
                }
                else
                {
                    gateOpeningDirection = 0.0f;
                }
            }
            else
            {
                if (gateTransform.localScale.y <= 1.0f)
                {
                    gateOpeningDirection = -1.0f;
                }
                else
                {
                    gateOpeningDirection = 0.0f;
                }
            }
        }
    }
}
