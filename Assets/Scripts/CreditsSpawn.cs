using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsSpawn : MonoBehaviour {
    public GameObject bottomBlock;
    public List<GameObject> creditsList = new List<GameObject>();


    /*
     * Additional Levels
     * Flarnith
     * 
     * Playtesters
     * Tann
     * Krillininthename
     * Crayon
     * Materant
     * Xenav
     * Magica
     * LucidDream
     * 
     * 
     * 
     * 
     * Thanks to
     * Spriters Resource
     * My parents
     * For putting up with my shit
     * #SAGameDev
     * For beinga  perpetual source of mentorship
     * Poemdexter
     * Mido
     * Tann
     * For getting me into this shit in the first place
     * & Saintly patience
     * 
     * Press Jump

     */

    public float currentTimer = 0f;
    float inStateAmount = 1f;
    float betweenStateAmount = 3f;
    CreditsState currentCreditsState = CreditsState.AdditionalLevels;
    public int additionalAfterTitle = 1;
    public bool shouldSpawnTitle = false;
    public bool clearingScreen = false;
    public bool clearedScreen = false;
    bool breakEverything = false;


	void Update () {

        if (Input.GetButtonDown("Jump"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("OpeningScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }




        if (!breakEverything) currentTimer -= Time.deltaTime;
		if (currentTimer <= 0f)
        {
            if (!shouldSpawnTitle)
            {
                //spawn additionalAfterTitleThing
                int toSpawn = 0;
                switch (currentCreditsState)
                {
                    default:
                    case CreditsState.AdditionalLevels:
                        toSpawn = 0;
                        break;
                    case CreditsState.Playtesters:
                        toSpawn = 2;
                        break;
                    case CreditsState.Thanksto:
                        toSpawn = 11;
                        break;
                }

                toSpawn += additionalAfterTitle;

                GameObject o = Instantiate(getObjectViaNumber(toSpawn), randomPos(getObjectViaNumber(toSpawn)), Quaternion.identity);
                o.transform.parent = gameObject.transform;
                additionalAfterTitle--;
                if (additionalAfterTitle == 0)
                {
                    shouldSpawnTitle = true;
                }

                currentTimer = inStateAmount;
            } else if (shouldSpawnTitle && !clearingScreen) 
            {
                int toSpawn = 0;
                switch (currentCreditsState)
                {
                    default:
                    case CreditsState.AdditionalLevels:
                        toSpawn = 0;
                        break;
                    case CreditsState.Playtesters:
                        toSpawn = 2;
                        break;
                    case CreditsState.Thanksto:
                        toSpawn = 11;
                        break;
                    case CreditsState.PressJump:
                        toSpawn = 22;
                        break;
                }
                GameObject o = Instantiate(getObjectViaNumber(toSpawn), randomPos(getObjectViaNumber(toSpawn)), Quaternion.identity);
                o.transform.parent = gameObject.transform;
                currentTimer = betweenStateAmount;
                clearingScreen = true;
            } else if (clearingScreen && !clearedScreen)
            {
                if (currentCreditsState != CreditsState.Thanksto)
                {
                    bottomBlock.GetComponent<BoxCollider2D>().enabled = false;
                    currentTimer = betweenStateAmount;
                    clearedScreen = true;
                } else
                {
                    if (!breakEverything)
                    {
                        Instantiate(getObjectViaNumber(22), randomPos(getObjectViaNumber(22)), Quaternion.identity);
                        breakEverything = true;
                    }
                }
            } else if (clearedScreen)
            {
                bottomBlock.GetComponent<BoxCollider2D>().enabled = true;
                currentCreditsState = Next(currentCreditsState);
                currentTimer = betweenStateAmount;
                shouldSpawnTitle = false;
                clearingScreen = false;
                clearedScreen = false;
                //Remove children from screen so they don't infinitely fall
                foreach (Transform o in gameObject.transform)
                {
                    Destroy(o.gameObject);
                }
            }
        }
	}

    float getWidth (GameObject g)
    {
        return (g.GetComponent<BoxCollider2D>().size.x * g.transform.localScale.x);
    }

    Vector2 randomPos(GameObject g)
    {
        float width = getWidth(g);
        float x = 0f;
        float min = -6.6f + (width/2);
        float max = 6.6f - (width/2);
        if (min >= 0) x = 0;
        x = Random.Range(min, max);

        return new Vector2(x, gameObject.transform.position.y);
    }

    GameObject getObjectViaNumber(int num)
    {
        return creditsList[num];
    }

    CreditsState Next(CreditsState current)
    {
        switch (current)
        {
            default:
            case CreditsState.AdditionalLevels:
                additionalAfterTitle = 8;
                return CreditsState.Playtesters;
            case CreditsState.Playtesters:
                additionalAfterTitle = 10;
                return CreditsState.Thanksto;
            case CreditsState.Thanksto:
                return CreditsState.PressJump;
        }
    }
}

public enum CreditsState
{
    AdditionalLevels,
    Playtesters,
    Thanksto,
    PressJump
}