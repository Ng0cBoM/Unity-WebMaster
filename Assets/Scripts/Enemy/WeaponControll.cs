using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponControll : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody weaponRb;
    private Vector3 weaponPositionFirst;
    private Quaternion weaponRotationFirst;
    private float maxTorque = 10;
    public GameObject weapon;
    public GameObject player;
    public Animator animator;
    private bool isThrow;
    
    void Start()
    {
        weaponPositionFirst = weapon.transform.position;
        weaponRotationFirst = weapon.transform.rotation;
        weaponRb = weapon.GetComponent<Rigidbody>();
        animator.SetTrigger("isThrow");
        isThrow = false;
    }

    void Throw()
    {
        weaponRb.AddForce(Force(), ForceMode.Impulse);
        weaponRb.useGravity = true;
        isThrow = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (weapon.transform.position.y <= -14f)
        {
            weaponRb.Sleep();
            weaponRb.velocity = Vector3.zero;
            weaponRb.useGravity = false;
            isThrow = false;
            weapon.transform.position = weaponPositionFirst;
            weapon.transform.rotation = weaponRotationFirst;
        }
        if (isThrow)
        {
            weapon.transform.Rotate(0,0, 1000f * Time.deltaTime);
        }
    }
    void WaitToNextThrow()
    {
        StartCoroutine(WaitToThrow());
    }

    private IEnumerator WaitToThrow()
    {
            yield return new WaitForSeconds(10);
            animator.SetTrigger("isThrow");
    }
    Vector3 Force()
    {
        return (player.transform.position - transform.position).normalized * 7f;
    }
}
