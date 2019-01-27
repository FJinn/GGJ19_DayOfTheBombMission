using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardManager : MonoBehaviour
{
    // 10 x10 game board consists of 100 tiles
    const int boardSize = 10;
    /// <summary>
    /// [j,i]
    /// i = row, j = column
    /// x = column, z = row
    /// ===>>>>[x, z]
    /// </summary>
    public Tile[,] gameBoard;
    public Tile[] tileArray;
    public GameObject tileGO;
    public GameObject target;
    Vector3 truePos;
    public float gridSize;
    public GameObject player;
    private bool isPlaced = false;
    int row = 0;
    int col = 0;

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

        gameBoard = new Tile[boardSize, boardSize];

        // i = row, j = column
        for (int row = 0; row < boardSize; row++)
        {
            for (int column = 0; column < boardSize; column++)
            {
                gameBoard[row, column] = tileArray[column + (row * 10)];
                gameBoard[row, column].blankTile = 0;
                gameBoard[row, column].ConnectTile(gameBoard);
                gameBoard[row, column].x = row;
                gameBoard[row, column].z = column;
            }
        }
    }

    private void Start()
    {
        
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
        target.transform.position = GameBoardManager.instance.gameBoard[row, col].transform.position + new Vector3(0,0f,0);
        
           
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (row > 0) row--;          
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(row <9) row++;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {   
            if(col > 0) col--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(col < 9) col++;
        }
    }

    void PlaceTile()
    {
        Tile tempTile = GameBoardManager.instance.gameBoard[row, col];
        tempTile.patternID = GameStateManager.Instance.currentPlayer.GetComponent<PlayerInventory>().selectedTile.patternID;
        tempTile.InitializePattern(tempTile.patternID);
        tempTile.blankTile = 1; ;

    }

}
