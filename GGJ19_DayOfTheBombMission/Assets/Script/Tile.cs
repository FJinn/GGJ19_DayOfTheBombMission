using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    
    public enum Direction
    {
        UP_LEFT,
        UP_DOWN,
        DOWN_LEFT,
        DOWN_RIGHT,
        LEFT_UP,
        LEFT_DOWN,
        RIGHT_UP,
        RIGHT_DOWN
    }

    Vector3[] landPositions = new Vector3[8];

    private void Awake()
    {
        InitializeLandPosition();
    }

    void InitializeLandPosition()
    {
        landPositions[(int)Direction.UP_LEFT] = new Vector3(transform.position.x - transform.localScale.x / 4, transform.position.y, transform.position.z + transform.localScale.z / 2);
        landPositions[(int)Direction.UP_DOWN] = new Vector3(transform.position.x + transform.localScale.x / 4, transform.position.y, transform.position.z + transform.localScale.z / 2);
        landPositions[(int)Direction.DOWN_LEFT] = new Vector3(transform.position.x - transform.localScale.x / 4, transform.position.y, transform.position.z - transform.localScale.z / 2);
        landPositions[(int)Direction.DOWN_RIGHT] = new Vector3(transform.position.x + transform.localScale.x / 4, transform.position.y, transform.position.z - transform.localScale.z / 2);
        landPositions[(int)Direction.LEFT_UP] = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z + transform.localScale.z / 4);
        landPositions[(int)Direction.LEFT_DOWN] = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z - transform.localScale.z / 4);
        landPositions[(int)Direction.RIGHT_UP] = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z + transform.localScale.z / 4);
        landPositions[(int)Direction.RIGHT_DOWN] = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z - transform.localScale.z / 4);
    }
}
