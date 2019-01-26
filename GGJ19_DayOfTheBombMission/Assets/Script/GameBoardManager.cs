using System.Collections;
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
                Tile tempTile = new Tile();
                tempTile.blankTile = 0;
                gameBoard[i,j] = tempTile;
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
            if(GameStateManager.Instance.currentPlayer.transform.position.z > -5)
            {
                target.transform.position = player.transform.position;
                target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 1);
            }
            else
            {
                target.transform.position = player.transform.position;
                target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z + 1);
            }
            
            SelectPosition();
            if (Input.GetKeyDown(KeyCode.Return) && isPlaced == false)
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
        if(GameStateManager.Instance.currentPlayer.transform.position.z > -5 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            target.transform.position = player.transform.position;
            target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 1);
        }

        if (GameStateManager.Instance.currentPlayer.transform.position.x < 5 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            target.transform.position = player.transform.position;
            target.transform.position = new Vector3(target.transform.position.x+1, target.transform.position.y, target.transform.position.z);
        }

        if (GameStateManager.Instance.currentPlayer.transform.position.x > -4 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            target.transform.position = player.transform.position;
            target.transform.position = new Vector3(target.transform.position.x-1, target.transform.position.y, target.transform.position.z);
        }

        if(GameStateManager.Instance.currentPlayer.transform.position.z < 5 && Input.GetKeyDown(KeyCode.DownArrow))
        {
            target.transform.position = player.transform.position;
            target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z + 1);
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

        Tile tempTile = Instantiate(GameStateManager.Instance.currentPlayer.GetComponent<PlayerInventory>().selectedTile, truePos, Quaternion.identity);
       
        gameBoard[(int)truePos.x + 4, (int)truePos.z + +5] = tempTile;

    }

}
