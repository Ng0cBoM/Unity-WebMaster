using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControll : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private float force = 20f;
    private bool isReadyBroken = false ;
    public GameObject[] fragments;
    public GameObject money;
    public float forceExplo = 50f;
    private GameObject player;

    private void Start()
    {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_Rigidbody.AddForce((-collision.gameObject.transform.position + transform.position).normalized * force, ForceMode.Impulse);
            isReadyBroken = true;
            player = collision.gameObject;
        }
        else if (isReadyBroken)
        {
            gameObject.SetActive(false);
            foreach(GameObject fragment in fragments)
            {
                Instantiate(fragment, transform.position, fragment.transform.rotation);
                //AddExplosionForce(fragment.GetComponent<Rigidbody>(), forceExplo, transform.position, radius, upliftModifer);
                fragment.GetComponent<Rigidbody>().AddForce(Vector3.up * forceExplo,ForceMode.Impulse);
            }
            for (int i=0; i<6; i++)
            {
                Instantiate(money, transform.position, money.transform.rotation);
                money.GetComponent<InstantiateMoney>().player = player;
            }
        }
    }
    /*private void AddExplosionForce(Rigidbody body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier = 0)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir.normalized * explosionForce * wearoff;
        baseForce.z = 0;
        body.AddForce(baseForce);

        if (upliftModifer != 0)
        {
            float upliftWearoff = 1 - upliftModifier / explosionRadius;
            Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
            upliftForce.z = 0;
            body.AddForce(upliftForce);
        }

    }*/
}
