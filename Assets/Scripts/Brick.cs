using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public GameObject debris;
    BoxCollider2D brCollider;
    bool isQuitting = false;
    GameObject godObject;

	// Use this for initialization
	void Start () {
        godObject = GameObject.FindGameObjectWithTag("GodObject");
        brCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public void OnDestroy()
    {
        if (!isQuitting)
        {
            if (!godObject.GetComponent<GodScript>().restarting)
            {
                GameObject _debris;
                for (int i = 0; i < 4; i++)
                {
                    _debris = Instantiate(debris, transform.position, Quaternion.identity);
                    _debris.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 10), Random.Range(0, 10)));
                }
            }
        }
    }
}
