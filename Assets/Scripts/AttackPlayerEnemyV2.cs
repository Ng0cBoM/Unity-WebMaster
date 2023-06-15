using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackPlayerEnemyV2 : MonoBehaviour
{
    public GameObject enemy;
    private bool backDamage = false;

    private void Update()
    {
        if (backDamage)
        {
            BackDamage();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().isMovingphase2)
            {
                backDamage = true;
                enemy.GetComponent<Enemy_V2>().isThrow = false;
            }
            else
            {
                Time.timeScale = 0.2f;
                other.GetComponent<PlayerController>().Beaten();
            } 
        }
        if (other.gameObject.CompareTag("Enemy") && backDamage)
        {
            backDamage = false;
            Destroy(gameObject);
            other.GetComponent<Enemy>().Die();  
        }
    }

    void BackDamage()
    {
        transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, 5f * Time.deltaTime);
        transform.Rotate(0, 0, 1000f * Time.deltaTime);
    }
}
