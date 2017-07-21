using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelImport : MonoBehaviour
{
    int _roomWidth;
    public int _tileSize;
    public Vector2 MinimapRoomCoordinates;

    public TextAsset dataAsset;
    public Sprite currentSprite;
    string roomName;
    string roomFile;
    public List<BoxCollider2D> collisionBoxList = new List<BoxCollider2D>();


    GodScript gs;
    SpriteRenderer sr;
    
    public GameObject coin;
    public GameObject coinBox;
    public GameObject goal;
    public GameObject player;
    public GameObject brick;
    public GameObject bouncy;
    public GameObject oneWay;
    public GameObject peach;
    public GameObject toad;
    public GameObject goomba;

    private void Start()
    {
        gs = GameObject.FindGameObjectWithTag("GodObject").GetComponent<GodScript>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        GetLevel();
    }


    void GetLevel()
    {
        gs.GetRoomData(this);
    }


    public void LoadLevel()
    {

        //First we need the sprite to be right
        sr.sprite = currentSprite;

        //Then we need the right objects
        if (!dataAsset) Debug.Log("No room file!");

        Dictionary<string, object> hash = dataAsset.text.dictionaryFromJson();
        
        roomFile = dataAsset.ToString();

        _roomWidth = int.Parse(hash["width"].ToString());
        _tileSize = int.Parse(hash["tilewidth"].ToString());

        List<object> layersList = (List<object>)hash["layers"];

        for (int i = 0; i < layersList.Count; i++)
        {

            Dictionary<string, object> layerHash = (Dictionary<string, object>)layersList[i];


            if (layerHash["name"].ToString().Equals("ObjectJson"))
            {
                
                List<object> objectList = (List<object>)layerHash["objects"];

                for (int j = 0; j < objectList.Count; j++)
                {

                    Dictionary<string, object> objHash = (Dictionary<string, object>)objectList[j];


                    if (objHash["type"].Equals("Ground") || objHash["type"].Equals(""))
                    {
                        //Debug.Log(objHash["id"] + " " + objHash["type"] + " " + objHash["x"] + " " + objHash["y"] + " " + objHash["width"] + " " + objHash["height"]);
                        GameObject levelBox = new GameObject(objHash["type"].ToString());
                        levelBox.name = objHash["id"].ToString();

                        float xInTiles = float.Parse(objHash["x"].ToString());
                        float yInTiles = float.Parse(objHash["y"].ToString());
                        float widthInTiles = float.Parse(objHash["width"].ToString());
                        float heightInTiles = float.Parse(objHash["height"].ToString());

                        xInTiles += widthInTiles / 2;
                        yInTiles += heightInTiles / 2;

                        yInTiles *= -1f;

                        xInTiles *= 1.0f / 16.0f;
                        yInTiles *= 1.0f / 16.0f;
                        widthInTiles *= 1.0f / 16.0f;
                        heightInTiles *= 1.0f / 16.0f;

                        xInTiles *= .8f;
                        yInTiles *= .8f;
                        widthInTiles *= .8f;
                        heightInTiles *= .8f;

                        yInTiles += 13f;

                        yInTiles -= .2f;


                        levelBox.transform.position = new Vector3(xInTiles, yInTiles);
                        levelBox.AddComponent<BoxCollider2D>();
                        levelBox.GetComponent<BoxCollider2D>().size = new Vector2(widthInTiles, heightInTiles);
                        levelBox.layer = 10;
                        levelBox.transform.SetParent(this.transform);
                        levelBox.layer = 9;
                    }

                    if (objHash["type"].Equals("Coin"))
                    {
                        float xInTiles = float.Parse(objHash["x"].ToString());
                        float yInTiles = float.Parse(objHash["y"].ToString());

                        yInTiles *= -1f;

                        xInTiles *= 1.0f / 16.0f;
                        yInTiles *= 1.0f / 16.0f;
                        xInTiles *= .8f;
                        yInTiles *= .8f;

                        yInTiles += 13f;
                        yInTiles -= .65f;
                        xInTiles += .4f;


                        GameObject _coin = Instantiate(coin, new Vector2(xInTiles, yInTiles), Quaternion.identity);
                        _coin.transform.parent = this.transform;
                    }

                    if (objHash["type"].Equals("CoinBox"))
                    {
                        float xInTiles = float.Parse(objHash["x"].ToString());
                        float yInTiles = float.Parse(objHash["y"].ToString());

                        yInTiles *= -1f;

                        xInTiles *= 1.0f / 16.0f;
                        yInTiles *= 1.0f / 16.0f;
                        xInTiles *= .8f;
                        yInTiles *= .8f;

                        yInTiles += 13f;
                        yInTiles -= .65f;

                        xInTiles += .4f;


                        GameObject _coinBox = Instantiate(coinBox, new Vector2(xInTiles, yInTiles), Quaternion.identity);
                        _coinBox.transform.SetParent(this.transform);
                        switch (objHash["name"].ToString())
                        {
                            case "InvisCoin":
                                _coinBox.GetComponent<CoinBlock>().heldItem = Items.Coin;
                                _coinBox.GetComponent<Animator>().SetBool("invis", true);
                                break;
                            case "FireFlower":
                                _coinBox.GetComponent<CoinBlock>().heldItem = Items.FireFlower;
                                break;
                            default:
                            case "Coin":
                                _coinBox.GetComponent<CoinBlock>().heldItem = Items.Coin;
                                break;
                            case "Mushroom":
                                _coinBox.GetComponent<CoinBlock>().heldItem = Items.Mushroom;
                                break;
                        }
                    }

                    if (objHash["type"].Equals("Brick"))
                    {
                        float xInTiles = float.Parse(objHash["x"].ToString());
                        float yInTiles = float.Parse(objHash["y"].ToString());

                        yInTiles *= -1f;

                        xInTiles *= 1.0f / 16.0f;
                        yInTiles *= 1.0f / 16.0f;
                        xInTiles *= .8f;
                        yInTiles *= .8f;

                        yInTiles += 13f;
                        yInTiles -= .65f;

                        xInTiles += .4f;


                        GameObject _brick = Instantiate(brick, new Vector2(xInTiles, yInTiles), Quaternion.identity);
                        _brick.transform.parent = this.transform;
                    }

                    if (objHash["type"].Equals("Peach"))
                    {
                        float xInTiles = float.Parse(objHash["x"].ToString());
                        float yInTiles = float.Parse(objHash["y"].ToString());

                        yInTiles *= -1f;

                        xInTiles *= 1.0f / 16.0f;
                        yInTiles *= 1.0f / 16.0f;
                        xInTiles *= .8f;
                        yInTiles *= .8f;

                        yInTiles += 13f;
                        yInTiles -= .65f;

                        xInTiles += .4f;


                        GameObject _peach = Instantiate(peach, new Vector2(xInTiles, yInTiles), Quaternion.identity);
                        _peach.transform.parent = this.transform;
                    }

                    if (objHash["type"].Equals("Toad"))
                    {
                        float xInTiles = float.Parse(objHash["x"].ToString());
                        float yInTiles = float.Parse(objHash["y"].ToString());

                        yInTiles *= -1f;

                        xInTiles *= 1.0f / 16.0f;
                        yInTiles *= 1.0f / 16.0f;
                        xInTiles *= .8f;
                        yInTiles *= .8f;

                        yInTiles += 13f;
                        yInTiles -= .65f;

                        xInTiles += .4f;

                        //come to see the
                        GameObject _toad = Instantiate(toad, new Vector2(xInTiles, yInTiles), Quaternion.identity);
                        _toad.transform.parent = this.transform;
                    }

                    if (objHash["type"].Equals("Goal"))
                    {
                        float xInTiles = float.Parse(objHash["x"].ToString());
                        float yInTiles = float.Parse(objHash["y"].ToString());

                        yInTiles *= -1f;

                        xInTiles *= 1.0f / 16.0f;
                        yInTiles *= 1.0f / 16.0f;
                        xInTiles *= .8f;
                        yInTiles *= .8f;

                        yInTiles += 13f;
                        yInTiles -= .65f;
                        yInTiles -= 4.75f;

                        xInTiles += .4f;


                        Instantiate(goal, new Vector2(xInTiles, yInTiles), Quaternion.identity);
                    }

                    if (objHash["type"].Equals("Bouncy"))
                    {
                        float xInTiles = float.Parse(objHash["x"].ToString());
                        float yInTiles = float.Parse(objHash["y"].ToString());
                        float widthInTiles = float.Parse(objHash["width"].ToString());
                        float heightInTiles = float.Parse(objHash["height"].ToString());

                        xInTiles += widthInTiles / 2;
                        yInTiles += heightInTiles / 2;

                        yInTiles *= -1f;

                        xInTiles *= 1.0f / 16.0f;
                        yInTiles *= 1.0f / 16.0f;
                        widthInTiles *= 1.0f / 16.0f;
                        heightInTiles *= 1.0f / 16.0f;

                        xInTiles *= .8f;
                        yInTiles *= .8f;
                        widthInTiles *= .8f;
                        heightInTiles *= .8f;

                        yInTiles += 13f;

                        yInTiles -= .2f;

                        GameObject _bouncy = Instantiate(bouncy, new Vector2(xInTiles, yInTiles), Quaternion.identity);
                        _bouncy.transform.parent = this.transform;

                        _bouncy.transform.position = new Vector3(xInTiles, yInTiles);
                        _bouncy.GetComponent<BoxCollider2D>().size = new Vector2(widthInTiles, heightInTiles);
                        _bouncy.name = objHash["id"].ToString();
                    }


                    if (objHash["type"].Equals("BigBouncy"))
                    {
                        float xInTiles = float.Parse(objHash["x"].ToString());
                        float yInTiles = float.Parse(objHash["y"].ToString());
                        float widthInTiles = float.Parse(objHash["width"].ToString());
                        float heightInTiles = float.Parse(objHash["height"].ToString());

                        xInTiles += widthInTiles / 2;
                        yInTiles += heightInTiles / 2;

                        yInTiles *= -1f;

                        xInTiles *= 1.0f / 16.0f;
                        yInTiles *= 1.0f / 16.0f;
                        widthInTiles *= 1.0f / 16.0f;
                        heightInTiles *= 1.0f / 16.0f;

                        xInTiles *= .8f;
                        yInTiles *= .8f;
                        widthInTiles *= .8f;
                        heightInTiles *= .8f;

                        yInTiles += 13f;

                        yInTiles -= .2f;

                        GameObject _bouncy = Instantiate(bouncy, new Vector2(xInTiles, yInTiles), Quaternion.identity);
                        _bouncy.transform.parent = this.transform;
                        _bouncy.transform.tag = "BigBouncy";

                        _bouncy.transform.position = new Vector3(xInTiles, yInTiles);
                        _bouncy.GetComponent<BoxCollider2D>().size = new Vector2(widthInTiles, heightInTiles);
                        _bouncy.name = objHash["id"].ToString();
                    }

                    if (objHash["type"].Equals("OneWay"))
                    {
                        GameObject _oneWay;
                        

                        float xInTiles = float.Parse(objHash["x"].ToString());
                        float yInTiles = float.Parse(objHash["y"].ToString());
                        float widthInTiles = float.Parse(objHash["width"].ToString());
                        float heightInTiles = float.Parse(objHash["height"].ToString());

                        xInTiles += widthInTiles / 2;
                        yInTiles += heightInTiles / 2;

                        yInTiles *= -1f;

                        xInTiles *= 1.0f / 16.0f;
                        yInTiles *= 1.0f / 16.0f;
                        widthInTiles *= 1.0f / 16.0f;
                        heightInTiles *= 1.0f / 16.0f;

                        xInTiles *= .8f;
                        yInTiles *= .8f;
                        widthInTiles *= .8f;
                        heightInTiles *= .4f;

                        yInTiles += 13f;

                        yInTiles -= .2f;

                        _oneWay = Instantiate(oneWay, new Vector2(xInTiles, yInTiles), Quaternion.identity);
                        _oneWay.transform.parent = this.transform;

                        _oneWay.transform.position = new Vector3(xInTiles, yInTiles);
                        _oneWay.GetComponent<BoxCollider2D>().size = new Vector2(widthInTiles, heightInTiles);
                        _oneWay.name = objHash["id"].ToString();
                    }

                    if (objHash["type"].Equals("Goomba"))
                    {
                        float xInTiles = float.Parse(objHash["x"].ToString());
                        float yInTiles = float.Parse(objHash["y"].ToString());

                        yInTiles *= -1f;

                        xInTiles *= 1.0f / 16.0f;
                        yInTiles *= 1.0f / 16.0f;
                        xInTiles *= .8f;
                        yInTiles *= .8f;

                        yInTiles += 13f;
                        yInTiles -= .65f;
                        xInTiles += .4f;


                        GameObject _goomba = Instantiate(goomba, new Vector2(xInTiles, yInTiles), Quaternion.identity);
                        _goomba.transform.parent = this.transform;
                    }
                }
            }
        }


        GameObject p = Instantiate(player, new Vector2(1f, 2.9f), Quaternion.identity);
        p.transform.SetParent(gameObject.transform);

    }

    public int GetRoomWidth()
    {
        return _roomWidth * _tileSize;
    }

}