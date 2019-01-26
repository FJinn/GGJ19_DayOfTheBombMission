﻿using System.Collections;
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
        Vector3 boardPos = new Vector3(Mathf.RoundToInt(transform.position.x) + 4, 0, Mathf.RoundToInt(transform.position.z) +4);
        Debug.Log("BoardPos: " + boardPos);
        currentTile = GameBoardManager.Instance.gameBoard[(int)boardPos.x, (int)boardPos.z];
        Debug.Log("CurrentTile: " + currentTile);

        if (currentTile.blankTile != 0)
        {
            myDirection = currentTile.GetCurrentDirection(this.gameObject.transform.position);
            Debug.Log("MyDirection: " + myDirection);
            Vector3 target = currentTile.nextLandPositions[(int)myDirection];
            Debug.Log("OldTarget: " + target);
            target = new Vector3(Mathf.RoundToInt(target.x), 1.217f, Mathf.RoundToInt(target.z) - 0.5f);
            Debug.Log("NewTarget: " + target);
            transform.position = Vector3.MoveTowards(transform.position, target, 0.5f);
            Debug.Log("position: " + transform.position);

        }
        //Timer();
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
