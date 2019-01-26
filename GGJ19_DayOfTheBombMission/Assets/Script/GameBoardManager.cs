using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardManager : MonoBehaviour
{
    // 10 x10 game board consists of 100 tiles
    public int boardSize;
    public int xPos, yPos;
    public int[,]array;
    public GameObject target;
    public GameObject tile;
    Vector3 truePos;
    public float gridSize;

    static GameBoardManager instance;
    public static GameBoardManager Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        array = new int[boardSize, boardSize];
    }

    void PlaceTile()
    {
        truePos.x = Mathf.Floor(target.transform.position.x / gridSize) * gridSize;
        truePos.y = Mathf.Floor(target.transform.position.y / gridSize) * gridSize;
        truePos.z = Mathf.Floor(target.transform.position.z / gridSize) * gridSize;

        tile.transform.position = truePos;
    }

}
