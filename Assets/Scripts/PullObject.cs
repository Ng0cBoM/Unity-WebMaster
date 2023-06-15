using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private float force = 10f;
    private void Start()
    {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    public void Pull(Vector3 directionPull)
    {
        m_Rigidbody.AddForce(directionPull * force, ForceMode.Impulse);
    }
}
