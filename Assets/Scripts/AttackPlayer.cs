using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private Animator enemyAnimator;
    public GameObject enemy;
    public GameObject player;
    public bool isAttack;
    private BoxCollider boxCollider;
    private void Start()
    {
        enemyAnimator = enemy.GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        isAttack = false;
    }
    private void Update()
    {
        if (Mathf.Abs(player.transform.position.y - enemy.transform.position.y) <= 1)
        {
            if (Mathf.Abs(player.transform.position.x - enemy.transform.position.x) <= 2)
            {
                if (isAttack == false)
                {
                    enemyAnimator.SetBool("isAttack", true);
                }
                if (Mathf.Sign(enemy.transform.localScale.x) != Mathf.Sign((player.transform.position - enemy.transform.position).normalized.x))
                {
                    enemy.transform.localScale = new Vector3(-1 * enemy.transform.localScale.x, enemy.transform.localScale.y, enemy.transform.localScale.z);
                }
                enemy.GetComponent<Enemy>().isMove = false;
            }
            else
            {
                enemy.GetComponent<Enemy>().isMove = true;
                enemyAnimator.SetBool("isAttack", false);
            }
        }
        else
        {
            enemy.GetComponent<Enemy>().isMove = true;
            enemyAnimator.SetBool("isAttack", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isAttack ) Destroy(other.gameObject);
        }
    }


}
