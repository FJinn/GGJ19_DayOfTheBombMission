using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int playerID;

    public int playerLife = 3;

    public bool isStunned = false;

    private void Update()
    {
        CapLifeMax();
        Die();
    }

    void CapLifeMax()
    {
        if(playerLife > 3)
        {
            playerLife = 3;
        }
    }

    void Die()
    {
        if(playerLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
