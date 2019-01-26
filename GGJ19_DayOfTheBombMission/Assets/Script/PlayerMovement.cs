using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Tile tile;
    Direction myDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myDirection = tile.GetCurrentDirection(this.gameObject.transform.position);
        this.transform.position = tile.nextLandPositions[(int)myDirection];
    }

    
}
