using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Enemy_V1 : MonoBehaviour
{
    public GameObject weapon;
    private float startX;
    private float speed = 0.2f;
    public bool isMove = true;
    private void Start()
    {
        startX = transform.position.x;
    }
    void Update()
    {
        if (isMove)
        {
            transform.position += new Vector3(Mathf.Sign(transform.localScale.x), 0, 0) * speed * Time.deltaTime;
            if (Mathf.Abs(startX - transform.position.x) >= 0.5) Rotate();
        }
    }
    void Rotate()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    void StartAttack()
    {
        weapon.GetComponent<AttackPlayer>().isAttack = true;
    }
    void EndAttack()
    {
        weapon.GetComponent<AttackPlayer>().isAttack = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
