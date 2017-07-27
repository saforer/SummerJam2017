using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour {
    Camera cam;
    public Transform player;
    public float speed;
    public float edge;
    float leftEdge;
    float rightEdge;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        leftEdge = 8.04f;
	}

    public void SetRightEdge(float right)
    {
        rightEdge = right;
        rightEdge *= 1f / 16f;
        rightEdge -= 20f;
        rightEdge += 8.04f;
    }
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(player.position);
            float percentOnScreen = screenPos.x / cam.pixelWidth;
            percentOnScreen -= .50f;
            percentOnScreen *= 2f;

            if (percentOnScreen >= .0f)
            {
                if (transform.position.x < rightEdge)
                {
                    transform.position += new Vector3(speed * percentOnScreen * percentOnScreen * percentOnScreen, 0, 0);
                }
            }
            else if (percentOnScreen <= -edge)
            {   
                if (transform.position.x > leftEdge)
                {
                        transform.position += new Vector3(speed * percentOnScreen * percentOnScreen * percentOnScreen, 0, 0);
                }
            }
        } else
        {
            try
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            } catch (Exception e) { }
        }
	}
}
