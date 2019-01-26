using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum SpecialEffect
    {
        BOMB,
        LIFE,
        STUN,
        NONE
    }

    public enum Direction
    {
        TOP_LEFT,
        TOP_RIGHT,
        BOTTOM_LEFT,
        BOTTOM_RIGHT,
        LEFT_TOP,
        LEFT_BOTTOM,
        RIGHT_TOP,
        RIGHT_BOTTOM
    }

    /// <summary>
    /// 0 = no one cross, 1 = owner first crossed, 2 = activate special effect
    /// </summary>
    public int playerCrossed = 0;

    public bool isStunned = false;

    public int patternID;

    [SerializeField] Vector3 currentPosition;
    /// <summary>
    /// put currentDirection into nextLandPostisions as index
    /// </summary>
    Direction currentDirection;
    Vector3[] landPositions = new Vector3[8];
    /// <summary>
    /// index stands for the current position in terms of direction
    /// </summary>
    Vector3[] nextLandPositions = new Vector3[8];
    /// <summary>
    /// 0 = top, 1 = left, 2 = down, 3 = right
    /// </summary>
    Tile[] connectedTiles = new Tile[4];


    private void Awake()
    {
        InitializeLandPosition();
        InitializeConnectedTile();
    }

    void InitializeLandPosition()
    {
        landPositions[(int)Direction.TOP_LEFT] = new Vector3(transform.position.x - transform.localScale.x / 4, transform.position.y, transform.position.z + transform.localScale.z / 2);
        landPositions[(int)Direction.TOP_RIGHT] = new Vector3(transform.position.x + transform.localScale.x / 4, transform.position.y, transform.position.z + transform.localScale.z / 2);
        landPositions[(int)Direction.BOTTOM_LEFT] = new Vector3(transform.position.x - transform.localScale.x / 4, transform.position.y, transform.position.z - transform.localScale.z / 2);
        landPositions[(int)Direction.BOTTOM_RIGHT] = new Vector3(transform.position.x + transform.localScale.x / 4, transform.position.y, transform.position.z - transform.localScale.z / 2);
        landPositions[(int)Direction.LEFT_TOP] = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z + transform.localScale.z / 4);
        landPositions[(int)Direction.LEFT_BOTTOM] = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z - transform.localScale.z / 4);
        landPositions[(int)Direction.RIGHT_TOP] = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z + transform.localScale.z / 4);
        landPositions[(int)Direction.RIGHT_BOTTOM] = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z - transform.localScale.z / 4);
    }

    void InitializeConnectedTile()
    {
        for(int i=0; i<connectedTiles.Length; i++)
        {
            connectedTiles[i] = null;
        }
    }

    public void PlaceTileOnTop(Tile selectedTile)
    {
        connectedTiles[0] = selectedTile;
    }

    public void PlaceTileOnLeft(Tile selectedTile)
    {
        connectedTiles[1] = selectedTile;
    }

    public void PlaceTileOnBottom(Tile selectedTile)
    {
        connectedTiles[2] = selectedTile;
    }

    public void PlaceTileOnRight(Tile selectedTile)
    {
        connectedTiles[3] = selectedTile;
    }

    /// <summary>
    /// get direction for nextLandPositions[(int)currentDirection]
    /// </summary>
    /// <returns></returns>
    Direction GetCurrentDirection()
    {
        for(int i=0; i<landPositions.Length; i++)
        {
            if (currentPosition == landPositions[i])
            {
                currentDirection = (Direction)i;
            }
        }

        return currentDirection;
    }

    /// <summary>
    /// put in the patternID when you call for tile to initialize the pattern
    /// </summary>
    /// <param name="patternID"></param>
    public void InitializePattern(int patternID)
    {
        if(patternID == 1)
        {
            PatternOne();
        }
        else if (patternID == 2)
        {
            PatternTwo();
        }
        else if (patternID == 3)
        {
            PatternThree();
        }
        else if (patternID == 4)
        {
            PatternFour();
        }
        else if (patternID == 5)
        {
            PatternFive();
        }
        else if (patternID == 6)
        {
            PatternSix();
        }
        else if (patternID == 7)
        {
            PatternSeven();
        }
        else if (patternID == 8)
        {
            PatternEight();
        }
        else if (patternID == 9)
        {
            PatternNine();
        }
        else if (patternID == 10)
        {
            PatternTen();
        }
    }

    void PatternOne()
    {
        nextLandPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.BOTTOM_LEFT];
        nextLandPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.BOTTOM_RIGHT];
        nextLandPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.TOP_LEFT];
        nextLandPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.TOP_RIGHT];
        nextLandPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.RIGHT_TOP];
        nextLandPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.RIGHT_BOTTOM];
        nextLandPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.LEFT_TOP];
        nextLandPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.LEFT_BOTTOM];
    }

    void PatternTwo()
    {
        nextLandPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.LEFT_BOTTOM];
        nextLandPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.BOTTOM_LEFT];
        nextLandPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.TOP_RIGHT];
        nextLandPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.RIGHT_TOP];
        nextLandPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.BOTTOM_RIGHT];
        nextLandPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.TOP_LEFT];
        nextLandPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.BOTTOM_RIGHT];
        nextLandPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.LEFT_TOP];
    }

    void PatternThree()
    {
        nextLandPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.RIGHT_TOP];
        nextLandPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.LEFT_TOP];
        nextLandPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.RIGHT_BOTTOM];
        nextLandPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.LEFT_BOTTOM];
        nextLandPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.TOP_RIGHT];
        nextLandPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.BOTTOM_RIGHT];
        nextLandPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.TOP_LEFT];
        nextLandPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.BOTTOM_LEFT];
    }

    void PatternFour()
    {
        nextLandPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.TOP_RIGHT];
        nextLandPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.TOP_LEFT];
        nextLandPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.BOTTOM_RIGHT];
        nextLandPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.BOTTOM_LEFT];
        nextLandPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.RIGHT_TOP];
        nextLandPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.RIGHT_BOTTOM];
        nextLandPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.LEFT_TOP];
        nextLandPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.LEFT_BOTTOM];
    }
    
    void PatternFive()
    {
        nextLandPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.LEFT_TOP];
        nextLandPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.RIGHT_TOP];
        nextLandPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.LEFT_BOTTOM];
        nextLandPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.RIGHT_BOTTOM];
        nextLandPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.TOP_LEFT];
        nextLandPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.BOTTOM_LEFT];
        nextLandPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.TOP_RIGHT];
        nextLandPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.BOTTOM_RIGHT];
    }

    void PatternSix()
    {
        nextLandPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.RIGHT_BOTTOM];
        nextLandPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.LEFT_BOTTOM];
        nextLandPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.LEFT_TOP];
        nextLandPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.RIGHT_TOP];
        nextLandPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.BOTTOM_LEFT];
        nextLandPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.TOP_RIGHT];
        nextLandPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.BOTTOM_RIGHT];
        nextLandPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.TOP_LEFT];
    }

    void PatternSeven()
    {
        nextLandPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.BOTTOM_RIGHT];
        nextLandPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.BOTTOM_LEFT];
        nextLandPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.TOP_RIGHT];
        nextLandPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.TOP_LEFT];
        nextLandPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.RIGHT_BOTTOM];
        nextLandPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.RIGHT_TOP];
        nextLandPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.LEFT_BOTTOM];
        nextLandPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.LEFT_TOP];
    }

    void PatternEight()
    {
        nextLandPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.LEFT_TOP];
        nextLandPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.LEFT_BOTTOM];
        nextLandPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.RIGHT_TOP];
        nextLandPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.RIGHT_BOTTOM];
        nextLandPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.TOP_LEFT];
        nextLandPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.TOP_RIGHT];
        nextLandPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.BOTTOM_LEFT];
        nextLandPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.BOTTOM_RIGHT];
    }

    void PatternNine()
    {
        nextLandPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.RIGHT_TOP];
        nextLandPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.BOTTOM_LEFT];
        nextLandPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.TOP_RIGHT];
        nextLandPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.LEFT_TOP];
        nextLandPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.BOTTOM_RIGHT];
        nextLandPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.RIGHT_BOTTOM];
        nextLandPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.TOP_LEFT];
        nextLandPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.LEFT_BOTTOM];
    }

    void PatternTen()
    {
        nextLandPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.RIGHT_BOTTOM];
        nextLandPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.LEFT_BOTTOM];
        nextLandPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.RIGHT_TOP];
        nextLandPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.LEFT_TOP];
        nextLandPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.BOTTOM_RIGHT];
        nextLandPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.TOP_RIGHT];
        nextLandPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.BOTTOM_LEFT];
        nextLandPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.TOP_LEFT];
    }
}
