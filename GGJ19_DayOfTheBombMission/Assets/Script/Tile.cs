using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class Tile : MonoBehaviour
{
    public enum SpecialEffect
    {
        BOMB,
        LIFE,
        STUN,
        NONE
    }

    /// <summary>
    /// 0 = blank tile, 1 = real tile
    /// </summary>
    public int blankTile = 0;
    
    public int x, z;

    SpecialEffect myEffect;
    /// <summary>
    /// 0 = no one cross, 1 = owner first crossed, 2 = activate special effect
    /// </summary>
    public int playerCrossed = 0;
    
    public int patternID;

    /// <summary>
    /// 0 = top, 1 = left, 2 = bottom, 3 = right
    /// </summary>
    [SerializeField] int currentSide;

    [SerializeField] Vector3 currentPosition;
    /// <summary>
    /// put currentDirection into nextLandPostisions as index
    /// </summary>
    Direction currentDirection;
    Vector3[] landPositions = new Vector3[8];
    /// <summary>
    /// index stands for the current position in terms of direction
    /// </summary>
    public Vector3[] nextLandPositions = new Vector3[8];
    /// <summary>
    /// 0 = top, 1 = left, 2 = down, 3 = right
    /// </summary>
    Tile[] connectedTiles = new Tile[4];

  //  int currentDegree = 0;


    public void InitializeTile()
    {
        InitializeLandPosition();
        InitializeConnectedTile();
    }

    void InitializeLandPosition()
    {
        landPositions[(int)Direction.TOP_LEFT] = new Vector3(Mathf.RoundToInt(transform.position.x) + 0.25f, transform.position.y, Mathf.RoundToInt(transform.position.z) - 0.5f);
        landPositions[(int)Direction.TOP_RIGHT] = new Vector3(Mathf.RoundToInt(transform.position.x) - 0.25f, transform.position.y, Mathf.RoundToInt(transform.position.z) - 0.5f);
        landPositions[(int)Direction.BOTTOM_LEFT] = new Vector3(Mathf.RoundToInt(transform.position.x) + 0.25f, transform.position.y, Mathf.RoundToInt(transform.position.z) + 0.5f);
        landPositions[(int)Direction.BOTTOM_RIGHT] = new Vector3(Mathf.RoundToInt(transform.position.x) - 0.25f, transform.position.y, Mathf.RoundToInt(transform.position.z) + 0.5f);
        landPositions[(int)Direction.LEFT_TOP] = new Vector3(Mathf.RoundToInt(transform.position.x) + 0.5f, transform.position.y, Mathf.RoundToInt(transform.position.z) - 0.25f);
        landPositions[(int)Direction.LEFT_BOTTOM] = new Vector3(Mathf.RoundToInt(transform.position.x) + 0.5f, transform.position.y, Mathf.RoundToInt(transform.position.z) + 0.25f);
        landPositions[(int)Direction.RIGHT_TOP] = new Vector3(Mathf.RoundToInt(transform.position.x) - 0.5f, transform.position.y, Mathf.RoundToInt(transform.position.z) - 0.25f);
        landPositions[(int)Direction.RIGHT_BOTTOM] = new Vector3(Mathf.RoundToInt(transform.position.x) - 0.5f, transform.position.y, Mathf.RoundToInt(transform.position.z) + 0.25f);

    //    Debug.Log(transform.position);

    //    Debug.Log(gameObject.name + " : " + Direction.TOP_LEFT + " : " + landPositions[(int)Direction.TOP_LEFT]);
    //    Debug.Log(gameObject.name + " : " + Direction.TOP_RIGHT + " : " + landPositions[(int)Direction.TOP_RIGHT]);
    //    Debug.Log(gameObject.name + " : " + Direction.BOTTOM_LEFT + " : " + landPositions[(int)Direction.BOTTOM_LEFT]);
    //    Debug.Log(gameObject.name + " : " + Direction.BOTTOM_RIGHT + " : " + landPositions[(int)Direction.BOTTOM_RIGHT]);
    //    Debug.Log(gameObject.name + " : " + Direction.LEFT_TOP + " : " + landPositions[(int)Direction.LEFT_TOP]);
    //    Debug.Log(gameObject.name + " : " + Direction.LEFT_BOTTOM + " : " + landPositions[(int)Direction.LEFT_BOTTOM]);
    //    Debug.Log(gameObject.name + " : " + Direction.RIGHT_TOP + " : " + landPositions[(int)Direction.RIGHT_TOP]);
    //    Debug.Log(gameObject.name + " : " + Direction.RIGHT_BOTTOM + " : " + landPositions[(int)Direction.RIGHT_BOTTOM]);
    }

    void InitializeConnectedTile()
    {
        for(int i=0; i<connectedTiles.Length; i++)
        {
            connectedTiles[i] = null;
        }
    }

    void CalculateCurrentSide(Vector3 playerPosition)
    {
        if((playerPosition.x > transform.position.x && playerPosition.z < transform.position.z) ||
           (playerPosition.x < transform.position.x && playerPosition.z < transform.position.z) )
        {
            // top
            currentSide = 0;
        }
        else if((playerPosition.x > transform.position.x && playerPosition.z < transform.position.z) ||
                (playerPosition.x > transform.position.x && playerPosition.z > transform.position.z) )
        {
            // left
            currentSide = 1;
        }
        else if((playerPosition.x < transform.position.x && playerPosition.z > transform.position.z) ||
                (playerPosition.x > transform.position.x && playerPosition.z > transform.position.z) )
        {
            // bottom
            currentSide = 2;
        }
        else if ((playerPosition.x < transform.position.x && playerPosition.z < transform.position.z) ||
                 (playerPosition.x < transform.position.x && playerPosition.z > transform.position.z) )
        {
            // right
            currentSide = 3;
        }
        else
        {
            currentSide = 4;
        }
    }

    // for player keep moving
    public bool ContinueToNextTile(Vector3 playerPosition)
    {
        CalculateCurrentSide(playerPosition);

        if (currentSide >= 0 && currentSide <= 3 && connectedTiles[currentSide] != null)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// use ContinueToNextTile() first, to check true or false
    /// </summary>
    /// <returns></returns>
    public Tile GetNextTile()
    {
        return connectedTiles[currentSide];
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
    public Direction GetCurrentDirection(Vector3 currentLocation)
    {
        CalculateCurrentSide(currentLocation);
        currentPosition = currentLocation;

        if (currentLocation.x > transform.position.x && currentLocation.z < transform.position.z && currentSide == 0)
        {
            // top left
            currentDirection = Direction.TOP_LEFT;
        }
        else if (currentLocation.x < transform.position.x && currentLocation.z < transform.position.z && currentSide == 0)
        {
            // top right
            currentDirection = Direction.TOP_RIGHT;
        }
        else if (currentLocation.x > transform.position.x && currentLocation.z < transform.position.z && currentSide == 1)
        {
            // left top
            currentDirection = Direction.LEFT_TOP;
        }
        else if(currentLocation.x > transform.position.x && currentLocation.z > transform.position.z && currentSide == 1)
        {
            // left bottom
            currentDirection = Direction.LEFT_BOTTOM;
        }
        else if (currentLocation.x < transform.position.x && currentLocation.z > transform.position.z && currentSide == 2)
        {
            // bottom right
            currentDirection = Direction.BOTTOM_RIGHT;
        }
        else if(currentLocation.x > transform.position.x && currentLocation.z > transform.position.z && currentSide == 2)
        {
            // bottom left
            currentDirection = Direction.BOTTOM_LEFT;
        }
        else if (currentLocation.x < transform.position.x && currentLocation.z < transform.position.z && currentSide == 3)
        {
            // right top
            currentDirection = Direction.RIGHT_TOP;
        }
        else if(currentLocation.x < transform.position.x && currentLocation.z > transform.position.z && currentSide == 3)
        {
            // right bottom
            currentDirection = Direction.RIGHT_BOTTOM;
        }

        return currentDirection;
    }

    /// <summary>
    /// Call whenever tile is rotated
    /// </summary>
    public void TileRotated()
    {

        landPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.RIGHT_TOP];
        landPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.RIGHT_BOTTOM];
        landPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.LEFT_TOP];
        landPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.LEFT_BOTTOM];
        landPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.TOP_RIGHT];
        landPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.TOP_LEFT];
        landPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.BOTTOM_RIGHT];
        landPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.BOTTOM_LEFT];

        /* since player only can rotate 90 at once, so it will do it job 
        currentDegree += 90;

        if(currentDegree >= 360 || currentDegree <= 0)
        {
            currentDegree = 0;
        }

        if(currentDegree == 0)
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
        else if (currentDegree == 90)
        {
            landPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.RIGHT_TOP];
            landPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.RIGHT_BOTTOM];
            landPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.LEFT_TOP];
            landPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.LEFT_BOTTOM];
            landPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.TOP_RIGHT];
            landPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.TOP_LEFT];
            landPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.BOTTOM_RIGHT];
            landPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.BOTTOM_LEFT];
        }
        else if (currentDegree == 180)
        {
            landPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.RIGHT_TOP];
            landPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.RIGHT_BOTTOM];
            landPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.LEFT_TOP];
            landPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.LEFT_BOTTOM];
            landPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.TOP_RIGHT];
            landPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.TOP_LEFT];
            landPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.BOTTOM_RIGHT];
            landPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.BOTTOM_LEFT];
        }
        else if (currentDegree == 270)
        {
            landPositions[(int)Direction.TOP_LEFT] = landPositions[(int)Direction.RIGHT_TOP];
            landPositions[(int)Direction.TOP_RIGHT] = landPositions[(int)Direction.RIGHT_BOTTOM];
            landPositions[(int)Direction.BOTTOM_LEFT] = landPositions[(int)Direction.LEFT_TOP];
            landPositions[(int)Direction.BOTTOM_RIGHT] = landPositions[(int)Direction.LEFT_BOTTOM];
            landPositions[(int)Direction.LEFT_TOP] = landPositions[(int)Direction.TOP_RIGHT];
            landPositions[(int)Direction.LEFT_BOTTOM] = landPositions[(int)Direction.TOP_LEFT];
            landPositions[(int)Direction.RIGHT_TOP] = landPositions[(int)Direction.BOTTOM_RIGHT];
            landPositions[(int)Direction.RIGHT_BOTTOM] = landPositions[(int)Direction.BOTTOM_LEFT];
        }
        */
    }

    /// <summary>
    /// put in the patternID when you call for tile to initialize the pattern
    /// </summary>
    /// <param name="patternID"></param>
    public void InitializePattern(int patternID)
    {
        this.patternID = patternID;
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

        InitializeSpecialEffect();
        blankTile = 1;
    }

    // initialize special effect
    void InitializeSpecialEffect()
    {
        float value = Random.value;

        if(value <= 0.25)
        {
            myEffect = SpecialEffect.BOMB;
        }
        else if(value > 0.25 && value <= 0.5)
        {
            myEffect = SpecialEffect.LIFE;
        }
        else if (value > 0.5 && value <= 0.75)
        {
            myEffect = SpecialEffect.STUN;
        }
        else if (value > 0.75)
        {
            myEffect = SpecialEffect.NONE;
        }
    }

    // use this function before move
    public void DoSpecialEffect(PlayerStatus ps)
    {
        if(playerCrossed >= 2)
        {
            if (myEffect == SpecialEffect.BOMB)
            {
                ps.playerLife--;
                DestroyTile();
            }
            else if(myEffect == SpecialEffect.LIFE)
            {
                ps.playerLife++;
            }
            else if (myEffect == SpecialEffect.STUN)
            {
                //
            }
        }
    }

    void DestroyTile()
    {
        for(int i=0; i<connectedTiles.Length; i++)
        {
            if(connectedTiles[i] != null)
            {
                int tempNum = -1;

                if(i == 0)
                {
                    tempNum = 2;
                }
                else if(i == 1)
                {
                    tempNum = 3;
                }
                else if (i == 2)
                {
                    tempNum = 0;
                }
                else if (i == 3)
                {
                    tempNum = 1;
                }

                connectedTiles[i].connectedTiles[tempNum] = null;
            }
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
