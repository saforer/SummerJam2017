using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    PlantState currentState = PlantState.IdleFallen;


    enum PlantState
    {
        Rise,
        IdleRisen,
        Fall,
        IdleFallen
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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


