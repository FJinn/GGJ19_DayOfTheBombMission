using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Tile currentTile;
    Direction myDirection;

    int timer = 2;
    float counter = 0;
    Vector3 target;

    int tileX, tileZ;

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
        tileX = currentTile.x;
        tileZ = currentTile.z;

        if(tileX > 10 || tileX < 0 || tileZ > 10 || tileZ < 0)
        {
            GameStateManager.Instance.playerList.Remove(this.gameObject);
            Destroy(gameObject);
        }

        if (currentTile.blankTile != 0)
        {
            myDirection = currentTile.GetCurrentDirection(this.gameObject.transform.position);
            target = currentTile.nextLandPositions[(int)myDirection];
            target = new Vector3(target.x, 2.0f, target.z);
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
            if(transform.position.x <= target.x + 0.3f && transform.position.x >= target.x - 0.3f)
            {
                transform.position = new Vector3(target.x, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > target.x)
            {
                transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
            }
            else if(transform.position.x < target.x)
            {
                transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            }

            if (transform.position.z <= target.z + 0.3f && transform.position.z >= target.z - 0.3f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, target.z);
            }
            else if (transform.position.z > target.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
            }
            else if (transform.position.z < target.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
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
