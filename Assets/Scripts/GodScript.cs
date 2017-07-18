using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodScript : MonoBehaviour {
    public PlayerWeaponStates lastMehrioState = PlayerWeaponStates.fireball;
    bool isReal = false;
    bool restarting = false;

    float restartTimer = 5f;
    float restartCount = 0;

    

    public List<Sprite> levelPic = new List<Sprite>();
    public List<TextAsset> levelData = new List<TextAsset>();
    int currentLevel = 0;

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

    public void GetRoomData(LevelImport li)
    {
        li.dataAsset = levelData[currentLevel];
        li.currentSprite = levelPic[currentLevel];
    }

    public void RestartCountdown()
    {
        restarting = true;
    }

    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        currentLevel++;
        RestartLevel();
    }

}
