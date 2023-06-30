using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private int count = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && count == 0)
        {
            count++;
            Debug.Log("Die");
            collision.gameObject.GetComponent<PlayerController2>().Beaten();
        }
    }
/*    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && count == 0)
        {
            Debug.Log("Die");
            other.gameObject.GetComponent<PlayerController>().Beaten();
            count++;
        }
    }*/

}
