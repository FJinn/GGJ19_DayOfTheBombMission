using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Direction
    {
        TOP_LEFT,
        TOP_DOWN,
        BOTTOM_LEFT,
        BOTTOM_RIGHT,
        LEFT_TOP,
        LEFT_BOTTOM,
        RIGHT_TOP,
        RIGHT_BOTTOM
    }

    Vector3[] landPositions = new Vector3[8];
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
        landPositions[(int)Direction.TOP_DOWN] = new Vector3(transform.position.x + transform.localScale.x / 4, transform.position.y, transform.position.z + transform.localScale.z / 2);
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


}
