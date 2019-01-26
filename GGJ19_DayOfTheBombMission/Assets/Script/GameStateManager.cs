using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    static GameStateManager instance;
    public static GameStateManager Instance { get { return instance; } }

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
    }

    public enum gameState
    {
        PLAYER_TURN,
        TILE_DISTRIBUTION,
        TILE_PLACEMENT,
        PLAYER_MOVE,
        BLANK
    }
}
