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
        target.transform.position = GameBoardManager.instance.gameBoard[row, col].transform.position + new Vector3(0,2f,0);
        
           
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
    //void SelectPosition()
    //{
    //    //float x = TilePos(GameStateManager.Instance.currentPlayer.transform.position.x);
    //    //float y = 0.55f;
    //    //float z = TilePos(GameStateManager.Instance.currentPlayer.transform.position.z);

    //    if (GameStateManager.Instance.currentPlayer.transform.position.z > -5 && Input.GetKeyDown(KeyCode.UpArrow))
    //    {
    //        target.transform.position = new Vector3(x, y, z - 1);
    //    }

    //    if(GameStateManager.Instance.currentPlayer.transform.position.z != 5)
    //    {
    //        if (GameStateManager.Instance.currentPlayer.transform.position.x < 5 && Input.GetKeyDown(KeyCode.LeftArrow))
    //        {
    //            target.transform.position = new Vector3(x + 1, y, z);
    //        }

    //        if (GameStateManager.Instance.currentPlayer.transform.position.x > -4 && Input.GetKeyDown(KeyCode.RightArrow))
    //        {
    //            target.transform.position = new Vector3(x - 1, y,z);
    //        }

    //        if (GameStateManager.Instance.currentPlayer.transform.position.z < 5 && Input.GetKeyDown(KeyCode.DownArrow))
    //        {
    //            target.transform.position = new Vector3(x, y,z + 1);
    //        }
    //    }
    //}

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
        //truePos.x = target.transform.position.x;
        //truePos.y = target.transform.position.y;
        //truePos.z = target.transform.position.z;

        //Tile tempTile = Instantiate(GameStateManager.Instance.currentPlayer.GetComponent<PlayerInventory>().selectedTile, truePos, Quaternion.identity);

        //if(truePos.x + 4 >= 0 && truePos.x + 4 < 10 && truePos.z + 5 >= 0 && truePos.z + 5 < 10)
        //{
        //    gameBoard[(int)truePos.x + 4, (int)truePos.z + 5] = tempTile;

        //    tempTile.InitializeTile();
        //    tempTile.InitializePattern(tempTile.patternID);


        //}

        GameBoardManager.instance.gameBoard[row, col].patternID = GameStateManager.Instance.currentPlayer.GetComponent<PlayerInventory>().selectedTile.patternID;
        GameBoardManager.instance.gameBoard[row, col].InitializePattern(GameBoardManager.instance.gameBoard[row, col].patternID);

    }

}
