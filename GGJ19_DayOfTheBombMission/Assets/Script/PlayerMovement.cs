using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Tile currentTile;
    Tile firstTile;
    Direction myDirection;

    bool onFirstTile = true;

    int timer = 2;
    float counter = 0;

    // Update is called once per frame
    void Update()
    {
        if(GameStateManager.Instance.currentGameState == GameState.PLAYER_MOVE)
        {
            if (onFirstTile)
            {
                GetFirstTile();
            }
            else
            {
                OnMove();
            }
        }
    }

    void GetFirstTile()
    {
        firstTile = gameObject.GetComponent<PlayerInventory>().selectedTile;
        onFirstTile = false;
            
        myDirection = firstTile.GetCurrentDirection(this.gameObject.transform.position);
        transform.position = firstTile.nextLandPositions[(int)myDirection];
    }

    private void OnMove()
    {
        myDirection = currentTile.GetCurrentDirection(this.gameObject.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, currentTile.nextLandPositions[(int)myDirection], 1);

        Timer();
    }

    void Timer()
    {
        if(counter >= timer)
        {
            counter = 0;

            currentTile.playerCrossed++;
            if (currentTile.ContinueToNextTile(transform.position))
            {
                currentTile = currentTile.GetNextTile();
            }
            else if (!currentTile.ContinueToNextTile(transform.position))
            {
                GameStateManager.Instance.currentGameState = GameState.PLAYER_TURN;
            }
        }
        else
        {
            counter += Time.deltaTime;
        }
    }
}
