using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    int hp = 1;
    public Camera cam;


	// Use this for initialization
	void Start () {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

    public void TakeDamage()
    {
        hp--;
        if (hp <= 1)
        {
            Destroy(gameObject);
        }
    }

    public bool amIOnCamera()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(gameObject.transform.position);
        float percentOnScreen = screenPos.x / cam.pixelWidth;
        percentOnScreen -= .50f;
        percentOnScreen *= 2f;

        if ((percentOnScreen > 1f) || (percentOnScreen < -1f))    return false;
        return true;
    }

    public float cameraPosition()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(gameObject.transform.position);
        float percentOnScreen = screenPos.x / cam.pixelWidth;
        percentOnScreen -= .50f;
        percentOnScreen *= 2f;
        return percentOnScreen;
    }
}
