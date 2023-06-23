using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy_V2 : MonoBehaviour
{
    private Vector3 targetPosition;
    public GameObject weapon;
    public GameObject weaponOnHand;
    public GameObject player;
    public GameObject InstantiatePoint;
    public bool isThrow = false;
    private bool readyThrow = true;
    private bool changeTarget = true;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        weapon.SetActive(false);
    }
    void Throw()
    {
        weapon.transform.position = Vector3.MoveTowards(weapon.transform.position, targetPosition, 5f * Time.deltaTime);
        weapon.transform.Rotate(0, 0, -1000f * Time.deltaTime);
        if (weapon.transform.position == targetPosition) Stop();
    }

    void Stop()
    {
        isThrow = false;
        weapon.SetActive(false);
        weaponOnHand.SetActive(true);
        StartCoroutine(WaitToNextThrow());
    }

    void StartThrow()
    {
        weapon.transform.position = transform.TransformPoint(InstantiatePoint.transform.localPosition);
        weaponOnHand.SetActive(false);
        weapon.SetActive(true);
        isThrow = true;
    }
    void Update()
    {
        if (player)
        {
            if (!isThrow)
            {
                if (Mathf.Abs(player.transform.position.y - transform.position.y) <= 8f)
                {
                    if (readyThrow)
                    {
                        readyThrow = false;
                        if (changeTarget)
                        {
                            targetPosition = player.transform.position;
                            changeTarget = false;
                        }
                        animator.SetTrigger("isThrow");
                    }
                }
            }
            else Throw();

            if (player.transform.position.x >= transform.position.x)
            {
                transform.localScale = new Vector3(0.25f, 0.25f, 1);
                if (!isThrow) weapon.transform.eulerAngles = new Vector3(weapon.transform.eulerAngles.x, 0, weapon.transform.eulerAngles.z);
            }
            else
            {
                transform.localScale = new Vector3(-0.25f, 0.25f, 1);
                if (!isThrow) weapon.transform.eulerAngles = new Vector3(weapon.transform.eulerAngles.x, 180, weapon.transform.eulerAngles.z);
            }
        }
    }
    IEnumerator WaitToNextThrow()
    {
        yield return new WaitForSeconds(5f);
        readyThrow = true;
        changeTarget = true;
    }
}
