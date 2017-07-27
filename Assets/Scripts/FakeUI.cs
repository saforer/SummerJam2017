using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeUI : MonoBehaviour {

    GameObject mainCamera;
	// Use this for initialization
	void Start () {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (mainCamera.transform.position.x >= 9f)
        {
            gameObject.GetComponent<Rigidbody2D>().WakeUp();
            Destroy(this);
        }
	}
}
