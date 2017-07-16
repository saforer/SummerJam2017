using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadMario : MonoBehaviour {

    public GodScript gs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -1f)
        {
            gs.RestartCountdown();
            Destroy(gameObject);
        }
	}
}
