using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Tile> tileList = new List<Tile>();
    public GameObject emptyTile;
    private GameObject emptyTileHolder;

    public Tile selectedTile;

    int num = 0;

    private void Start()
    {
        selectedTile = tileList[num];
    }

    // Update is called once per frame
    void Update()
    {   
        if(this.gameObject == GameStateManager.Instance.currentPlayer)
        {
            if (GameStateManager.Instance.currentGameState == gameState.TILE_DISTRIBUTION)
            {
                DistributeTiles();
                GameStateManager.Instance.currentGameState = gameState.TILE_CHOOSING;
            }

            if (GameStateManager.Instance.currentGameState == gameState.TILE_CHOOSING) ChooseTile();
        }      
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

        if(Input.GetKeyDown(KeyCode.Return))
        {
            GameStateManager.Instance.currentGameState = gameState.TILE_PLACEMENT;
        }
    }

    void DistributeTiles()
    {   
        if(tileList.Count<=0)
        {
            for (int i = 0; i < 3; i++)
            {
                emptyTileHolder = Instantiate(emptyTile, transform.position, Quaternion.identity);
                int j = Random.Range(1, 11);
                emptyTileHolder.GetComponent<Tile>().InitializePattern(j);
                tileList.Add(emptyTileHolder.GetComponent<Tile>());
            }
        }
        else
        {
            emptyTileHolder = Instantiate(emptyTile, transform.position, Quaternion.identity);
            int j = Random.Range(1, 11);
            emptyTileHolder.GetComponent<Tile>().InitializePattern(j);
            tileList.Add(emptyTileHolder.GetComponent<Tile>());
        }
    }
}
