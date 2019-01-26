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
    bool runTime = false;

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

        if(boardPos.x >10 || boardPos.x < 0 || boardPos.z >10 || boardPos.z < 0)
        {
            GameStateManager.Instance.playerList.Remove(this.gameObject);
            Destroy(gameObject);
        }

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

        if (move)
            Moving();
        else if(runTime)
            Timer();
    }

    void Moving()
    {
        if(transform.position != target)
        {
            if (transform.position.x > target.x)
            {
                transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
            }
            else if(transform.position.x < target.x)
            {
                transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            }

            if (transform.position.z > target.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
            }
            else if (transform.position.z < target.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
            }

            float dis = Vector3.Distance(transform.position, target);
            if (dis < 0.5f)
            {
                transform.position = target;
            }
        }
        else
        {
            move = false;
            runTime = true;
        }
    }

    void Timer()
    {
        if(counter >= timer)
        {
            counter = 0;
            runTime = false;
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
