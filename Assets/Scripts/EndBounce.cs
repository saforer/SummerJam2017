using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBounce : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "BigBouncy")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 13f);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.tag == "BigBouncy")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 13f);
        }
    }
}
