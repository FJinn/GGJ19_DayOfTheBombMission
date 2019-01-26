using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardManager : MonoBehaviour
{
    // 10 x10 game board consists of 100 tiles
    public int boardSize;
    private int xPos, yPos;
    public int[,]array;
    public GameObject tileGO;
    public GameObject target;
    Vector3 truePos;
    public float gridSize;
    public GameObject player;
    private bool isPlaced = false;
    private bool isInitializedPos = false;

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
        if(GameStateManager.Instance.currentGameState == GameState.TILE_PLACEMENT)
        {
            player = GameStateManager.Instance.currentPlayer;
            if(isInitializedPos == false)
            {
                if (GameStateManager.Instance.currentPlayer.transform.position.z > -5)
                {
                    target.transform.position = GameStateManager.Instance.currentPlayer.transform.position;
                    target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 1);
                }
                else
                {
                    target.transform.position = GameStateManager.Instance.currentPlayer.transform.position;
                    target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z + 1);
                }
                isInitializedPos = true;
            }
            
            
            SelectPosition();
            if (Input.GetKeyDown(KeyCode.Return) && isPlaced == false)
            {
                PlaceTile();
                isInitializedPos = false;
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
            target.transform.position = GameStateManager.Instance.currentPlayer.transform.position;
            target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 1);
        }

        if(GameStateManager.Instance.currentPlayer.transform.position.z != 5)
        {
            if (GameStateManager.Instance.currentPlayer.transform.position.x < 5 && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                target.transform.position = GameStateManager.Instance.currentPlayer.transform.position;
                target.transform.position = new Vector3(target.transform.position.x + 1, target.transform.position.y, target.transform.position.z);
            }

            if (GameStateManager.Instance.currentPlayer.transform.position.x > -4 && Input.GetKeyDown(KeyCode.RightArrow))
            {
                target.transform.position = GameStateManager.Instance.currentPlayer.transform.position;
                target.transform.position = new Vector3(target.transform.position.x - 1, target.transform.position.y, target.transform.position.z);
            }

            if (GameStateManager.Instance.currentPlayer.transform.position.z < 5 && Input.GetKeyDown(KeyCode.DownArrow))
            {
                target.transform.position = GameStateManager.Instance.currentPlayer.transform.position;
                target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z + 1);
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
        truePos.x = Mathf.Floor(target.transform.position.x / gridSize) * gridSize;
        truePos.y = target.transform.position.y;
        truePos.z = Mathf.Floor(target.transform.position.z / gridSize) * gridSize;

        Instantiate(GameStateManager.Instance.currentPlayer.GetComponent<PlayerInventory>().selectedTile, truePos, Quaternion.identity);
    }

}
