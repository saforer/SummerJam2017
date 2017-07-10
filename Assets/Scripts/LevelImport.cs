using UnityEngine;
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

                    
                    if (objHash["type"].Equals("Ground"))
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

                        xInTiles -= .4f;
                        yInTiles -= .2f;


                        levelBox.transform.position = new Vector3(xInTiles, yInTiles);
                        levelBox.transform.localScale = levelSize;
                        levelBox.AddComponent<BoxCollider2D>();
                        levelBox.GetComponent<BoxCollider2D>().size = new Vector2(widthInTiles, heightInTiles);
                        levelBox.layer = 10;
                        levelBox.transform.SetParent(this.transform);
                        levelBox.layer = 9;
                    }
                }
            }
        }
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