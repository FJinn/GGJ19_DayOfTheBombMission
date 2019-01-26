using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Tile currentTile;
    Direction myDirection;

    int timer = 2;
    float counter = 0;

    // Update is called once per frame
    void Update()
    {
        if(GameStateManager.Instance.currentGameState == GameState.PLAYER_MOVE)
        {
            OnMove();
        }
    }

    private void OnMove()
    {
        Vector3 boardPos = CalculateGameBoardPosition();
        currentTile = GameBoardManager.Instance.gameBoard[(int)boardPos.x, (int)boardPos.z];

        if(currentTile.blankTile != 0)
        {
            myDirection = currentTile.GetCurrentDirection(this.gameObject.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, currentTile.nextLandPositions[(int)myDirection], 1);

            Timer();
        }
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

    Vector3 CalculateGameBoardPosition()
    {
        Vector3 tempVector = new Vector3(-10,-10,-10);
        for(int i=0; i<10; i++)
        {
            for(int j=0;j<10; j++)
            {
                int xMin = -4 + j;
                int xMax = -4 + j + 1;
                int zMin = -5 + i;
                int zMax = -5 + i + 1;

                if (transform.position.x >= xMin && transform.position.x <= xMax && transform.position.z >= zMin && transform.position.z <= zMax)
                {
                    tempVector = new Vector3(xMin, 1.217f, zMin);
                }
            }
        }

        return tempVector;
    }
}
