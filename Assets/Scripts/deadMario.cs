using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadMario : MonoBehaviour {

    public GodScript gs;

    float timer = 3.5f;


    private void Start()
    {
        gs.deadStopMusic();
    }

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            gs.RestartLevel();
            Destroy(gameObject);
        }
	}
}
