using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour {
    bool goingRight = true;

    Rigidbody2D rb;
    BoxCollider2D turnBox;


	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        turnBox = transform.GetChild(0).GetComponent<BoxCollider2D>();
	}

    // Update is called once per frame
    void Update()
    {
        if (turnBox.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            goingRight = !goingRight;
            //turnBox.gameObject.transform.localPosition = new Vector2((goingRight? .4f : -.4f), 0);

            float x;

            if (goingRight)
            {
                x = .4f;
            } else
            {
                x = -.4f;
            }

            turnBox.gameObject.transform.localPosition = new Vector2(x, 0);
        }
        rb.velocity = new Vector2(2f * (goingRight ? 1 : -1), rb.velocity.y);
    }
}
