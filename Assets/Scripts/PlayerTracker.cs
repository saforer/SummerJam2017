using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour {
    Camera cam;
    public Transform player;
    public float speed;
    public float edge;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 screenPos = cam.WorldToScreenPoint(player.position);
        float percentOnScreen = screenPos.x / cam.pixelWidth;
        percentOnScreen -= .50f;
        percentOnScreen *= 2f;
        Debug.Log("target is " + percentOnScreen + "%");



        if (percentOnScreen >= edge)
        {
            transform.position += new Vector3(speed * percentOnScreen, 0, 0);
        } else if (percentOnScreen <= -edge)
        {
            if (transform.position.x > 1.18f) transform.position += new Vector3(speed * percentOnScreen * percentOnScreen * percentOnScreen, 0, 0);
        }
        
	}
}
