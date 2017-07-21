using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour {

    public Items heldItem;
    bool empty = false;
    public GameObject coin;
    public GameObject fireFlower;
    public GameObject mushroom;


    bool moving;
    bool movingUp;
    public float speed;
    Vector2 startPos;
    public float timeToMove;
    float countToMove;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (moving)
        {
            if (timeToMove > countToMove)
            {
                countToMove += Time.deltaTime;
                transform.position = new Vector2(transform.position.x, transform.position.y + ((movingUp ? 1 : -1)) * (speed * Time.deltaTime));
            } else
            {
                moving = false;
                transform.position = startPos;
            }
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag != "Enemy")
        {
            if (!empty)
            {
                if (col.transform.position.y > transform.position.y)
                {
                    movingUp = false;
                }
                else
                {
                    movingUp = true;
                }

                startPos = transform.position;
                spawnItem(!movingUp);
                anim.SetBool("empty", true);
                moving = true;
                empty = true;
            }
        }
        
    }

    void spawnItem(bool touchedFromTop)
    {
        GameObject toSpawn;
        switch (heldItem)
        {
            case Items.Mushroom:
                toSpawn = mushroom;
                break;
            case Items.FireFlower:
                toSpawn = fireFlower;
                break;
            default:
                toSpawn = coin;
                break;
        }
        float pos;
        if (touchedFromTop)
        {
            pos = -.8f;
        } else
        {
            pos = .8f;
        }

        Instantiate(toSpawn, new Vector2(transform.position.x, transform.position.y + pos), Quaternion.identity);
    }
}

public enum Items
{
    Mushroom,
    FireFlower,
    Coin
}
