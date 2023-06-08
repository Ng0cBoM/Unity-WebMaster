using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyListDestroyManager : MonoBehaviour
{
    // Start is called before the first frame update
    // public GameObject enemy;
    public GameObject enemyDestroy;
    //private GameObject[] enemyList;
    private GameObject[] enemyListDestroy;
    void Start()
    {
        //enemyList = new GameObject[enemy.transform.childCount];
        enemyListDestroy = new GameObject[enemyDestroy.transform.childCount];
        for (int i = 0; i < enemyDestroy.transform.childCount; i++)
        {
            //enemyList[i] = enemy.transform.GetChild(i).gameObject;
            enemyListDestroy[i] = enemyDestroy.transform.GetChild(i).gameObject;
            enemyListDestroy[i].SetActive(false);
        }
    }

    public void ShowEnemyDestroy(int i)
    {
        enemyListDestroy[i].SetActive(true);
    }
}
