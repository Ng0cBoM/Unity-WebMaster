using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyListDestroyManager : MonoBehaviour
{
    public GameObject enemyDestroy;
    private GameObject[] enemyListDestroy;
    void Start()
    {
        enemyListDestroy = new GameObject[enemyDestroy.transform.childCount];
        for (int i = 0; i < enemyDestroy.transform.childCount; i++)
        {
            enemyListDestroy[i] = enemyDestroy.transform.GetChild(i).gameObject;
            enemyListDestroy[i].SetActive(false);
        }
    }

    public void ShowEnemyDestroy(int i)
    {
        enemyListDestroy[i].SetActive(true);
    }
}
