﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelImport : MonoBehaviour
{
    public int _roomWidth;
    public int _roomHeight;
    public int _tileSize;
    public string roomName;
    public Vector2 MinimapRoomCoordinates;
    public string roomFile;
    public TextAsset dataAsset;
    public List<BoxCollider2D> collisionBoxList = new List<BoxCollider2D>();



    public GameObject coin;
    public GameObject coinBox;
    public GameObject goal;
    public GameObject player;
    public GameObject brick;

    // Use this for initialization
    void Start()
    {
        //TextAsset dataAsset = (TextAsset) Resources.Load (roomFile, typeof(TextAsset));

        if (!dataAsset) Debug.Log("No room file!");

        Dictionary<string, object> hash = dataAsset.text.dictionaryFromJson();

        _roomWidth = int.Parse(hash["width"].ToString());
        _roomHeight = int.Parse(hash["height"].ToString());
        _tileSize = int.Parse(hash["tilewidth"].ToString());

        string[] pathSplit = roomFile.Split(new char[] { '/' });
        roomName = pathSplit[pathSplit.Length - 1];

        List<object> layersList = (List<object>)hash["layers"];

        for (int i = 0; i < layersList.Count; i++)
        {

            Dictionary<string, object> layerHash = (Dictionary<string, object>)layersList[i];


            if (layerHash["name"].ToString().Equals("ObjectJson"))
            {

                // Load object data if it exists...
                List<object> objectList = (List<object>)layerHash["objects"];

                for (int j = 0; j < objectList.Count; j++)
                {

                    Dictionary<string, object> objHash = (Dictionary<string, object>)objectList[j];

                    
                    if (objHash["type"].Equals("Ground") || objHash["type"].Equals(""))
                    {
                        //Debug.Log(objHash["id"] + " " + objHash["type"] + " " + objHash["x"] + " " + objHash["y"] + " " + objHash["width"] + " " + objHash["height"]);
                        GameObject levelBox = new GameObject(objHash["type"].ToString());
                        levelBox.name = objHash["id"].ToString();
                        Vector3 levelCenter = new Vector3(0, 0, 0);
                        Vector3 levelSize = new Vector3(1, 1, 1);
                        Vector3 levelBoxSize = new Vector2(1, 1);

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
                        levelBox.transform.localScale = levelSize;
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
                        switch(objHash["name"].ToString())
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


                }
            }
        }

        GameObject p = Instantiate(player, new Vector2(1f, 2.9f), Quaternion.identity);


    }

    public int GetRoomHeight()
    {
        return _roomHeight * _tileSize;
    }

    public int GetRoomWidth()
    {
        return _roomWidth * _tileSize;
    }

}