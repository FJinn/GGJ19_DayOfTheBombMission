using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int playerID;

    public int playerLife = 3;

    public bool isStunned = false;

    private int num = 0;

    private void Update()
    {
        CapLifeMax();
        Die();
        if(GameStateManager.Instance.currentGameState == GameState.PLAYER_TURN)
        {   
            for(int i=0;i<GameStateManager.Instance.playerList.Count;i++)
            {
                GameStateManager.Instance.playerList[i].GetComponent<PlayerStatus>().playerID = num;
                num++;
            }
            num = 0;
            if (GameStateManager.Instance.currentPlayerID == playerID)
            {
                GameStateManager.Instance.currentPlayer = this.gameObject;
                if(GameStateManager.Instance.delayTimer < GameStateManager.Instance.delayDuration)
                {
                    GameStateManager.Instance.delayTimer += Time.deltaTime;
                }
                else
                {
                    GameStateManager.Instance.delayTimer = 0;
                    GameStateManager.Instance.currentGameState = GameState.TILE_DISTRIBUTION;
                }              
            }
        }
        
    }

    void CapLifeMax()
    {
        if(playerLife > 3)
        {
            playerLife = 3;
        }
    }

    void Die()
    {
        if(playerLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerLife--;
            other.gameObject.GetComponent<PlayerStatus>().playerLife--;
        }
    }
}
