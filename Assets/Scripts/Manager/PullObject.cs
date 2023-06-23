using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private float force = 100f;
    public bool canPull = true;
    /*private void Start()
    {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    public void Pull(Vector3 directionPull, Vector3 spideweb)
    {
        m_Rigidbody.AddForce(directionPull * force, ForceMode.Impulse);
    }*/
}
