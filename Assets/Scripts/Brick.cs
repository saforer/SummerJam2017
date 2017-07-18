using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public GameObject debris;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Break()
    {
        GameObject _debris;
        for (int i = 0; i < 4; i++)
        {
            _debris = Instantiate(debris, transform.position, Quaternion.identity);
            _debris.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 10), Random.Range(0, 10)));
        }
        Destroy(gameObject);
    }
}
