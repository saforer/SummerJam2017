using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodScript : MonoBehaviour {
    public PlayerWeaponStates lastMehrioState = PlayerWeaponStates.fireball;
    bool isReal = false;
    bool restarting = false;

    float restartTimer = 10f;
    float restartCount = 0;






    public List<Sprite> levelPic = new List<Sprite>();
    public List<TextAsset> levelDat = new List<TextAsset>();


    int currentLevel = 0;

    PlayerTracker pt;

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
                restarting = false;
                RestartLevel();
            }
        }
    }

    public void GetRoomData(LevelImport li)
    {
        li.dataAsset = (TextAsset) levelDat[currentLevel];
        li.currentSprite = (Sprite) levelPic[currentLevel];
        li.LoadLevel();
        pt = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerTracker>();
        pt.SetRightEdge(li.GetRoomWidth());
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
        RestartCountdown();
    }

}

[System.Serializable]
public class Level
{
    public Sprite levelPic;
    public TextAsset levelData;

    public Level(Sprite s, TextAsset t)
    {
        this.levelPic = s;
        this.levelData = t;
    }
}
