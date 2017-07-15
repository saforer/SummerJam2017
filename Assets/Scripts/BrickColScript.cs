using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickColScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Player p = col.GetComponent<Player>();
            if ((p.currentMehrio == PlayerWeaponStates.big) || (p.currentMehrio == PlayerWeaponStates.fireball))
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}
