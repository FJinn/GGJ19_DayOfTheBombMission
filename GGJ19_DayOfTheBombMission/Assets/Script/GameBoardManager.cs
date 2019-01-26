﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardManager : MonoBehaviour
{
    // 10 x10 game board consists of 100 tiles
    public int boardSize;
    /// <summary>
    /// [j,i]
    /// i = row, j = column
    /// x = column, z = row
    /// ===>>>>[x, z]
    /// </summary>
    public Tile[,] gameBoard;
    public GameObject tileGO;
    public GameObject target;
    Vector3 truePos;
    public float gridSize;
    public GameObject player;
    private bool isPlaced = false;

    public Tile emptyTile;

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
        gameBoard = new Tile[boardSize, boardSize];

        // i = row, j = column
        for(int i=0; i<boardSize; i++)
        {
            for(int j=0; j<boardSize; j++)
            {
                emptyTile.blankTile = 0;
                gameBoard[i,j] = emptyTile;
                // x = column, z = row
                gameBoard[i, j].x = 5 - j;
                gameBoard[i, j].z = -5 + i;
            }
        }
    }

    private void Update()
    {   
        if(GameStateManager.Instance.currentGameState == GameState.TILE_PLACEMENT)
        {
            player = GameStateManager.Instance.currentPlayer;
            
            SelectPosition();
            if (Input.GetKeyDown(KeyCode.Space) && isPlaced == false)
            {
                PlaceTile();
                isPlaced = true;
            }
        } 

        if(isPlaced == true)
        {
            if (GameStateManager.Instance.delayTimer < GameStateManager.Instance.delayDuration)
            {
                GameStateManager.Instance.delayTimer += Time.deltaTime;
            }
            else
            {
                GameStateManager.Instance.delayTimer = 0;
                GameStateManager.Instance.currentGameState = GameState.PLAYER_TURN;
                GameStateManager.Instance.currentPlayerID++;
                isPlaced = false;
            }
        }
    }

    void SelectPosition()
    {
        int x = Mathf.RoundToInt(GameStateManager.Instance.currentPlayer.transform.position.x / 1);
        int y = (int)GameStateManager.Instance.currentPlayer.transform.position.y;
        int z = Mathf.RoundToInt(GameStateManager.Instance.currentPlayer.transform.position.z / 1);

        if (GameStateManager.Instance.currentPlayer.transform.position.z > -5 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            target.transform.position = new Vector3(x, y, z - 1);
        }

        if(GameStateManager.Instance.currentPlayer.transform.position.z != 5)
        {
            if (GameStateManager.Instance.currentPlayer.transform.position.x < 5 && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                target.transform.position = new Vector3(x + 1, y, z);
            }

            if (GameStateManager.Instance.currentPlayer.transform.position.x > -4 && Input.GetKeyDown(KeyCode.RightArrow))
            {
                target.transform.position = new Vector3(x - 1, y,z);
            }

            if (GameStateManager.Instance.currentPlayer.transform.position.z < 5 && Input.GetKeyDown(KeyCode.DownArrow))
            {
                target.transform.position = new Vector3(x, y,z + 1);
            }
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
        truePos.x = target.transform.position.x;
        truePos.y = target.transform.position.y;
        truePos.z = target.transform.position.z;

        Tile tempTile = Instantiate(GameStateManager.Instance.currentPlayer.GetComponent<PlayerInventory>().selectedTile, truePos, Quaternion.identity);
       
        gameBoard[(int)truePos.x + 4, (int)truePos.z + +5] = tempTile;

    }

}
