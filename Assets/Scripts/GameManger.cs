using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    // Start is called before the first frame update
    public int enemyCount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            Debug.Log("Done");
        }
    }
}
