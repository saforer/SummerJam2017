using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    public PlantState currentState = PlantState.IdleFallen;
    GameObject p;
    public bool cantGoUp = false;
    float leeway = .8f;

    public float stateChangeCountdown = 0f;
    float timeBetweenStates = 2f;
    Vector2 risenPosition;
    Vector2 fallenPosition;


    // Use this for initialization
    void Start () {
        risenPosition = transform.position;
        transform.position = new Vector2(transform.position.x, transform.position.y - 1.4f);
        fallenPosition = transform.position;
        try
        {
            p = (GameObject)GameObject.FindGameObjectsWithTag("Player").GetValue(0);
        } catch (Exception e) {}
    }
	
	// Update is called once per frame
	void Update () {
        setCantGoUp();


        stateChangeCountdown -= Time.deltaTime;

        if (stateChangeCountdown <= 0)
        {
            //Change the state to the next one
            if (!((currentState == PlantState.IdleFallen) && cantGoUp))
            {
                currentState = Next(currentState);
                stateChangeCountdown = timeBetweenStates;
            }
        } else
        {
            //Animate current state
            if (currentState == PlantState.Rise)
            {
                float percentBetweenState = stateChangeCountdown / timeBetweenStates;
                float y = Mathf.Lerp(risenPosition.y, fallenPosition.y, percentBetweenState);
                transform.position = new Vector2(transform.position.x, y);
            } else if (currentState == PlantState.Fall)
            {
                float percentBetweenState = stateChangeCountdown / timeBetweenStates;
                float y = Mathf.Lerp(fallenPosition.y, risenPosition.y, percentBetweenState);
                transform.position = new Vector2(transform.position.x, y);
            }
        }
	}

    PlantState Next(PlantState current)
    {
        switch (current)
        {
            case PlantState.Fall:
                return (PlantState.IdleFallen);
            default:
            case PlantState.IdleFallen:
                return (PlantState.Rise);
            case PlantState.Rise:
                return (PlantState.IdleRisen);
            case PlantState.IdleRisen:
                return (PlantState.Fall);
        }
    }

    void setCantGoUp()
    {
        if (p == null)
        {
            try
            {
                p = (GameObject)GameObject.FindGameObjectsWithTag("Player").GetValue(0);
            }
            catch (Exception e) { }
        }
        else
        {
            if ((p.transform.position.x < transform.position.x + leeway) && (p.transform.position.x > transform.position.x - leeway))
            {
                cantGoUp = true;
            } else
            {
                cantGoUp = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            Destroy(gameObject);
        }
    }
}


public enum PlantState
{
    Rise,
    IdleRisen,
    Fall,
    IdleFallen
}