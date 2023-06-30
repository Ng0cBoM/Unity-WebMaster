using EgdFoundation;
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
            if (other.GetComponent<PlayerController2>().isFlying)
            {
                backDamage = true;
                enemy.GetComponent<Enemy_V2>().isThrow = false;
            }
            else if (!backDamage && GameManger.Instance.isPlay)
            {
                Time.timeScale = 0.2f;
                other.GetComponent<PlayerController2>().Beaten();
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
        transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, 10f * Time.deltaTime);
        transform.Rotate(0, 0, 1000f * Time.deltaTime);
    }
}
