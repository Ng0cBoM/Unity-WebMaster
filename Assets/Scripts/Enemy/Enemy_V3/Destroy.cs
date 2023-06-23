using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] fragments;
    public GameObject enemyFirst;
    public GameObject enemySecond;

    public void DestroyBarrel()
    {
        enemyFirst.SetActive(false);
        gameObject.SetActive(false);
        enemySecond.SetActive(true);
        foreach (GameObject fragment in fragments)
        {
            fragment.SetActive(true);
            fragment.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);
        }
    }
}
