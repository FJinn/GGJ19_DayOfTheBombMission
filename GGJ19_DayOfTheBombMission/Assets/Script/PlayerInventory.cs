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

    bool isDistributed = false;

    private void Start()
    {   
        //selectedTile = tileList[num];
    }

    // Update is called once per frame
    void Update()
    {   
        if(this.gameObject.GetComponent<PlayerStatus>().playerID == GameStateManager.Instance.currentPlayerID)
        {
            if (GameStateManager.Instance.currentGameState == GameState.TILE_DISTRIBUTION)
            {   
                if(!isDistributed)
                {
                    DistributeTiles();
                    isDistributed = true;
                }
                selectedTile = tileList[num];
                if (GameStateManager.Instance.delayTimer < GameStateManager.Instance.delayDuration)
                {
                    GameStateManager.Instance.delayTimer += Time.deltaTime;
                }
                else
                {
                    GameStateManager.Instance.delayTimer = 0;
                    GameStateManager.Instance.currentGameState = GameState.TILE_CHOOSING;
                    isDistributed = false;
                }
            }

            if (GameStateManager.Instance.currentGameState == GameState.TILE_CHOOSING) ChooseTile();
        }      
    }

    void ChooseTile()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (num == 2)
            {
                num = 0;   
            }
            else
            {
                num++;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (num == 0)
            {
                num = 2;
            }
            else
            {
                num--;
            }
        }

        selectedTile = tileList[num];

        if(Input.GetKeyDown(KeyCode.Return))
        {
            tileList.RemoveAt(num);
            GameStateManager.Instance.currentGameState = GameState.TILE_PLACEMENT;
        }
    }

    void DistributeTiles()
    {   
        if(tileList.Count<=0)
        {
            for (int i = 0; i < 3; i++)
            {
                emptyTileHolder = Instantiate(emptyTile, new Vector3(0,-20,0), Quaternion.identity);
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
