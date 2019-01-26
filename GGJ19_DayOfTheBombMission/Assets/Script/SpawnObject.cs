using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject[] sceneObjects = new GameObject[5];
    public GameObject playerObject;
    public GameObject cubeThing;
    public GameObject boardPos;
    public int riseSpeed;
    Vector3 endPos = new Vector3(0,0,0);

    void Start()
    {
        SpwnObject();
    }

    void Update()
    {
        cubeThing = GameObject.Find("TestCube(Clone)");
        LiftObject();
    }

    void SpwnObject()
    {
        if (playerObject.transform.position.z >= -5)
        {
            Instantiate(sceneObjects[0], new Vector3(playerObject.transform.position.x, playerObject.transform.position.y - 3, playerObject.transform.position.z), Quaternion.identity);
            
        }
        else if(playerObject.transform.position.z >= -3)
        {
            Instantiate(sceneObjects[1], new Vector3(playerObject.transform.position.x, playerObject.transform.position.y - 3, playerObject.transform.position.z), Quaternion.identity);
        }
        else if (playerObject.transform.position.z >= 1)
        {
            Instantiate(sceneObjects[2], new Vector3(playerObject.transform.position.x, playerObject.transform.position.y - 3, playerObject.transform.position.z), Quaternion.identity);
        }
        else if (playerObject.transform.position.z >= 3)
        {
            Instantiate(sceneObjects[3], new Vector3(playerObject.transform.position.x, playerObject.transform.position.y - 3, playerObject.transform.position.z), Quaternion.identity);
        }
        else if (playerObject.transform.position.z == 5)
        {
            Instantiate(sceneObjects[4], new Vector3(playerObject.transform.position.x, playerObject.transform.position.y - 3, playerObject.transform.position.z), Quaternion.identity);
        }
    }

    public void LiftObject()
    {
        if (playerObject.transform.position.z >= -5)
        {
            if (cubeThing.transform.position.y != boardPos.transform.position.y)
            {
                cubeThing.transform.position = Vector3.MoveTowards(cubeThing.transform.position, playerObject.transform.position, riseSpeed * Time.deltaTime);
                
            }
        }
        else if (playerObject.transform.position.z >= -3)
        {
            if (sceneObjects[1].transform.position.y != boardPos.transform.position.y)
            {
                sceneObjects[1].transform.Translate(Vector3.up * riseSpeed, Space.World);
            }
        }
        else if (playerObject.transform.position.z >=  1)
        {
            if (sceneObjects[2].transform.position.y != boardPos.transform.position.y)
            {
                sceneObjects[2].transform.Translate(Vector3.up * riseSpeed, Space.World);
            }
        }
        else if (playerObject.transform.position.z >= 3)
        {
            if (sceneObjects[3].transform.position.y != boardPos.transform.position.y)
            {
                sceneObjects[3].transform.Translate(Vector3.up * riseSpeed, Space.World);
            }
        }
        else if (playerObject.transform.position.z == 5)
        {
            if (sceneObjects[4].transform.position.y != boardPos.transform.position.y)
            {
                sceneObjects[4].transform.Translate(Vector3.up * riseSpeed, Space.World);
            }
        }
    }
}
