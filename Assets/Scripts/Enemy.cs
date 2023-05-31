using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private float force = 10f;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>(); 
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_Animator.SetTrigger("die");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_Animator.SetTrigger("die");
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void Pull(Vector3 directionPull)
    {
        Debug.Log(directionPull);
        m_Rigidbody.AddForce(directionPull * force, ForceMode.Impulse);
    }
}
