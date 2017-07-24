using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSpawner : MonoBehaviour {
    public List<GameObject> mehrioTitles = new List<GameObject>();
    public GameObject studioName;
    public GameObject jamName;
    public GameObject jamLogo;
    public GameObject titleText;
    public GameObject creditText;
    public float titleCountdown;
    public float titleTimer;
    public int stage = 0;
    bool gameObj = true;
    

    // Update is called once per frame
    void Update () {
        if (stage <= 20)
        {
            titleTimer += Time.deltaTime;
            if (titleTimer >= titleCountdown)
            {
                switch (stage)
                {
                    case 0:
                        Instantiate(studioName, randomPos(5f), Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(jamName, randomPos(5.13f), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(jamLogo, randomPos(8.9f), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(randomMehrioTitle(), randomPos(8.8f), Quaternion.identity);
                        break;
                    default:
                    case 10:
                        if (gameObj == true)
                        {
                            Instantiate(titleText, new Vector2(0, -4f), Quaternion.identity);
                        } else
                        {
                            Instantiate(creditText, new Vector2(0, -4f), Quaternion.identity);
                        }
                        gameObj = !gameObj;
                        break;
                }
                stage++;
                titleTimer = 0f;
            }
        }

        //Check to see if they pressed jump
        if (Input.GetButtonDown("Jump"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game", UnityEngine.SceneManagement.LoadSceneMode.Single);
        } else if (Input.GetButtonDown("Fire"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("CreditsScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
	}

    Vector2 randomPos(float width)
    {
        float x = 0f;
        float min = -6.6f + (width / 2);
        float max = 6.6f - (width / 2);

        x = Random.Range(min, max);
        
        return new Vector2(x, gameObject.transform.position.y);
    }

    GameObject randomMehrioTitle()
    {
        return mehrioTitles[Random.Range(0, mehrioTitles.Count)];
    }
}
