using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private BoxCollider m_BoxCollider;
    private float force = 10f;
    private float speed = 0.2f;
    private Vector3 direction;
    private int range;
    private bool isLive;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>(); 
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_BoxCollider = gameObject.GetComponent<BoxCollider>();
        direction = Vector3.left;
        range = 0;
        isLive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            range += 1;
            transform.position += direction * speed * Time.deltaTime;
            if (transform.position.y <= 0) isLive = false;
            if (range == 330)
            {
                Rotate();
            }
         
        }
        else
        {
            Destroy(gameObject);
        }       
        
       
    }
    void Rotate()
    {
        range = 0;
        if (direction == Vector3.left)
        {
            direction = Vector3.right;
        }
        else
        {
            direction = Vector3.left;
        }
        transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_Animator.SetTrigger("die");
            m_BoxCollider.isTrigger = true;
            
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
