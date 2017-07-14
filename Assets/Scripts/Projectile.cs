using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float lengthToStay;
    public Rigidbody2D rb;
    public float speed;
    float count = 0f;

	// Use this for initialization
	void Start () {
		
	}

    public void Fire(bool right)
    {
       rb.AddForce(Vector2.right * (right?1:-1) * speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (count < lengthToStay)
        {
            count += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
