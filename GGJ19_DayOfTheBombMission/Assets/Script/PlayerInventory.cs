using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int playerID;

    public List<Tile> tileList = new List<Tile>();

    private Tile selectedTile;

    int num = 0;

    private void Start()
    {
        selectedTile = tileList[num];
    }

    // Update is called once per frame
    void Update()
    {
        ChooseTile();
    }



    void ChooseTile()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (num == 2)
            {
                selectedTile = tileList[0];
            }
            else
            {
                selectedTile = tileList[num + 1];
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (num == 0)
            {
                selectedTile = tileList[2];
            }
            else
            {
                selectedTile = tileList[num - 1];
            }
        }
    }
}
