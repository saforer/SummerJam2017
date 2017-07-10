using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed;
    public bool right = true;

	// Use this for initialization
	void Start () {
		
	}

    public void Fire()
    {
       rb.AddForce(Vector2.right * (right?1:-1) * speed);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
