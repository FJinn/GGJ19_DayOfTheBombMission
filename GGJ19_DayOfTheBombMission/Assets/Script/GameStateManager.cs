using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PLAYER_TURN,
    TILE_DISTRIBUTION,
    TILE_PLACEMENT,
    TILE_CHOOSING,
    PLAYER_MOVE,
    BLANK
}

public class GameStateManager : MonoBehaviour
{
    static GameStateManager instance;
    public static GameStateManager Instance { get { return instance; } }

    public int currentPlayerID = 0;
    public GameObject currentPlayer;
    public GameState currentGameState;

    public float delayTimer = 0f;
    public float delayDuration = 0.5f;

    public List<GameObject> playerList = new List<GameObject>();

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }

        if (delayTimer < delayDuration)
        {
            delayTimer += Time.deltaTime;
        }
        else
        {
            delayTimer = 0;
            currentGameState = GameState.PLAYER_TURN;
        }
    }

    private void Start()
    {   
        for(int i=0;i<playerList.Count;i++)
        {
            int num = Random.Range(0, 10);
            int num2 = Random.Range(2, 4);
            playerList[i].transform.position = GameBoardManager.Instance.gameBoard[9, num].landPositions[num2];
        }

        
    }

    private void Update()
    {
        if(currentPlayerID == 4)
        {
            currentGameState = GameState.PLAYER_MOVE;
            currentPlayerID = 0;
        }
    }
}
