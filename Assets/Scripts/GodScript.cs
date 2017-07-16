using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodScript : MonoBehaviour {
    public PlayerWeaponStates lastMehrioState = PlayerWeaponStates.fireball;
    bool isReal = false;
    public bool restarting = false;

    float restartTimer = 5f;
    float restartCount = 0;

    public void Start()
    {
        if (GameObject.FindGameObjectsWithTag("GodObject").Length>1)
        {
            if (!isReal)
            {
                Destroy(gameObject);
            }
        }

        isReal = true;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (restarting)
        {
            if (restartCount < restartTimer)
            {
                restartCount += Time.deltaTime;
            }
            else
            {
                restartCount = 0f;
                RestartLevel();
            }
        }
    }

    public void RestartCountdown()
    {
        restarting = true;
    }

    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    private void OnLevelWasLoaded(int level)
    {
        restarting = false;
    }

    public void NextLevel()
    {
        RestartLevel();
    }

}
