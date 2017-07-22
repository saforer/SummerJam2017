using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWay : MonoBehaviour {
    GameObject p; //Player
    Collider2D coll;

	// Use this for initialization
	void Start () {

        coll = gameObject.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	/*void Update () {
        if (p==null)
        {
            try
            {
                p = (GameObject)GameObject.FindGameObjectsWithTag("Player").GetValue(0);
            } catch (Exception e)
            {

            }
        }

        if (p != null)
        {
            if (gameObject.transform.position.y < (p.transform.position.y - .4f))
            {
                //Solid
                col.enabled = true;
            }
            else
            {
                //Not Solid
                col.enabled = false;
            }
        }
	}*/

    private void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}
