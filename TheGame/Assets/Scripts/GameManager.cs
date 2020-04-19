using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int enemiesCount;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("called start");
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesCount = enemies?.Length ?? 0;
    }
    
    public int EnemiesCount()
    {
        return enemiesCount;
    }

    public void EnemyKilled()
    {
        enemiesCount--;
    }
}
