using System.Collections;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private Animator enemyAnimator;
    public GameObject enemy;
    public GameObject player;
    public bool nearPlayer;
    public bool isAttack;
    private void Start()
    {
        enemyAnimator = enemy.GetComponent<Animator>();
        isAttack = false;
    }
    private void Update()
    {
        if (player)
        {
            if (Mathf.Abs(player.transform.position.y - enemy.transform.position.y) <= 1)
            {
                if (Mathf.Abs(player.transform.position.x - enemy.transform.position.x) <= 1.2)
                {
                    nearPlayer = true;
                    StartCoroutine(Attack());
                }
                else
                {
                    nearPlayer = false;
                    enemy.GetComponent<Enemy_V1>().isMove = true;
                    enemyAnimator.SetBool("isAttack", false);
                    enemyAnimator.SetBool("isReadyAttack", false);
                }
            }
            else
            {
                nearPlayer = false;
                enemy.GetComponent<Enemy_V1>().isMove = true;
                enemyAnimator.SetBool("isAttack", false);
                enemyAnimator.SetBool("isReadyAttack", false);
            }
        }
    }

    private IEnumerator Attack()
    {
        if (Mathf.Sign(enemy.transform.localScale.x) != Mathf.Sign((player.transform.position - enemy.transform.position).normalized.x))
        {
            enemy.transform.localScale = new Vector3(-1 * enemy.transform.localScale.x, enemy.transform.localScale.y, enemy.transform.localScale.z);
        }
        enemy.GetComponent<Enemy_V1>().isMove = false;
        enemyAnimator.SetBool("isReadyAttack",true);

        yield return new WaitForSeconds(2f);

        if (isAttack == false && nearPlayer == true)
        {
            enemyAnimator.SetBool("isReadyAttack", false);
            enemyAnimator.SetBool("isAttack", true);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isAttack && GameManger.Instance.isPlay)
            {
                Time.timeScale = 0.2f;
                nearPlayer = false;
                other.GetComponent<PlayerController2>().Beaten();
            }
        }
    }
}
