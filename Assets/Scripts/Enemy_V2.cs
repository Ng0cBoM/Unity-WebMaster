using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy_V2 : MonoBehaviour
{
    private Vector3 weaponPositionFirst;
    private Vector3 targetPosition;
    private Quaternion weaponRotationFirst;
    public GameObject weapon;
    public GameObject weaponOnHand;
    public GameObject player;
    public bool isThrow = false;
    private bool readyThrow = true;
    private bool changeTarget = true;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        weapon.SetActive(false);
        weaponPositionFirst = weapon.transform.position;
        weaponRotationFirst = weapon.transform.rotation;
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
        weapon.transform.position = weaponPositionFirst;
        weapon.transform.rotation = weaponRotationFirst;
        StartCoroutine(WaitToNextThrow());
    }

    void StartThrow()
    {
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
            }
            else
            {
                transform.localScale = new Vector3(-0.25f, 0.25f, 1);
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
