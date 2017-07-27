using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodScript : MonoBehaviour {
    public PlayerWeaponStates lastMehrioState = PlayerWeaponStates.fireball;
    bool isReal = false;
    bool restarting = false;

    float restartTimer = 3.2f;
    float restartCount = 0;


    public AudioClip endingSong;



    public List<Sprite> levelPic = new List<Sprite>();
    public List<TextAsset> levelDat = new List<TextAsset>();
    public List<AudioClip> musicToUse = new List<AudioClip>();


    public int currentLevel = 0;

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
        gameObject.GetComponent<AudioSource>().clip = musicToUse[currentLevel];
        li.isFirstLevel = currentLevel == 0 ? true : false;
        li.LoadLevel();
        pt = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerTracker>();
        pt.SetRightEdge(li.GetRoomWidth());
        gameObject.GetComponent<AudioSource>().loop = true;
        gameObject.GetComponent<AudioSource>().Play();        
    }

    public void deadStopMusic()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }

    public void RestartCountdown()
    {
        restarting = true;
    }

    public void RestartLevel()
    {
        gameObject.GetComponent<AudioSource>().Stop();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void beatLevelAudio()
    {
        Debug.Log("Played Sound");
        gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<AudioSource>().PlayOneShot(endingSong);
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
