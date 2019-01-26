using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Tile currentTile;
    Direction myDirection;

    int timer = 2;
    float counter = 0;
    Vector3 target;

    bool move = false;

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
        currentTile = GameBoardManager.Instance.gameBoard[(int)boardPos.x, (int)boardPos.z];

        if (currentTile.blankTile != 0)
        {
            myDirection = currentTile.GetCurrentDirection(this.gameObject.transform.position);
            target = currentTile.nextLandPositions[(int)myDirection];
            //   target = new Vector3(Mathf.RoundToInt(target.x), 1.217f, Mathf.RoundToInt(target.z) - 0.5f);
            target = new Vector3(target.x, 2.0f, target.z);
            //transform.position = Vector3.MoveTowards(transform.position, target, 1.0f);
            //transform.position = target;
            move = true;
        }
        if(move)
        Moving();
    }

    void Moving()
    {
        if(transform.position != target)
        {
            if(transform.position.x > target.x)
            {
                transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
            }
            else if(transform.position.x < target.x)
            {
                transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            }
            else if (Mathf.Round(transform.position.x) == Mathf.Round(target.x))
            {
                transform.position = new Vector3(target.x, transform.position.y, transform.position.z);
            }

            if (transform.position.z > target.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
            }
            else if (transform.position.z < target.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
            }
            else if(Mathf.Round(transform.position.z) == Mathf.Round(target.z))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, target.z);
            }

            float dis = Vector3.Distance(transform.position, target);
            if(dis < 0.5f)
            {
                transform.position = target;
            }
        }
        else
        {
            Timer();
            move = false;
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

}
