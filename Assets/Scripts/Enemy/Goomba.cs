using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : Enemy {
    public float speed;

	// Use this for initialization
	void Start () {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        if (amIOnCamera())
        {
            gameObject.GetComponent<Rigidbody2D>().IsAwake();
        } else
        {
            gameObject.GetComponent<Rigidbody2D>().Sleep();
        }
	}
}
