using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardManager : MonoBehaviour
{
    // 10 x10 game board consists of 100 tiles
    public int boardSize;
    public int xPos, yPos;
    public int[,]array;
    public GameObject tileGO;
    public GameObject target;
    Vector3 truePos;
    public float gridSize;
    public GameObject player;

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

    private void Update()
    {
        SelectPosition();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceTile();
        }
    }

    void SelectPosition()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            target.transform.position = player.transform.position;
            target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            target.transform.position = player.transform.position;
            target.transform.position = new Vector3(target.transform.position.x+1, target.transform.position.y, target.transform.position.z - 1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            target.transform.position = player.transform.position;
            target.transform.position = new Vector3(target.transform.position.x-1, target.transform.position.y, target.transform.position.z - 1);
        }
    }

    void PlaceTile()
    {
        //if(Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    targetPos = player.transform.position;
        //    targetPos.z -= 40;
        //}

        //else if(Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    targetPos = player.transform.position;
        //    targetPos.
        //}
        truePos.x = Mathf.Floor(target.transform.position.x / gridSize) * gridSize;
        truePos.y = Mathf.Floor(target.transform.position.y / gridSize) * gridSize;
        truePos.z = Mathf.Floor(target.transform.position.z / gridSize) * gridSize;

        Instantiate(tileGO, truePos, Quaternion.identity);
    }

}
