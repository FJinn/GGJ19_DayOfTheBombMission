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
    public float delayDuration = 2f;

    public GameObject[] playerList;

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
        for(int i=0;i<playerList.Length;i++)
        {   
            Randomize:
                int num = Random.Range(-4, 5);
                int num2 = Random.Range(0, 2);
                if(num2 == 0)
                {
                    playerList[i].transform.position = new Vector3(num + 0.25f, playerList[i].transform.position.y, playerList[i].transform.position.z);
                }
                else
                {
                    playerList[i].transform.position = new Vector3(num - 0.25f, playerList[i].transform.position.y, playerList[i].transform.position.z);
                }

                for(int j=0;j<playerList.Length;j++)
                {
                    if(i!=j)
                    {
                        if(playerList[i].transform.position.x == playerList[j].transform.position.x)
                        {
                            goto Randomize;
                        }
                    }
                }
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
