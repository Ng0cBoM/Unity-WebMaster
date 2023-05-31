using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 13f;
 
    private Vector3 targetDirection;

    public GameObject linePrefab;
    private LineRenderer bulletLineRenderer;

    public GameObject origin;
    public GameObject handControll;
    public GameObject arrow ;
    public GameObject spiderWeb;

    public Animator animator;

    private bool isMoving;
    private string targetTag;

    public LayerMask Wall;
    public LayerMask Enemy;

    private Enemy enemyForce;

    void Start()
    {
        isMoving = false;
        bulletLineRenderer = linePrefab.GetComponent<LineRenderer>();
        linePrefab.SetActive(false);
        spiderWeb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionMouse = (worldPosition - origin.transform.position).normalized;

        if (isMoving == false)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionMouse);
            handControll.transform.rotation = targetRotation;

            if (Physics.Raycast(origin.transform.position, directionMouse, out RaycastHit hitWall, Mathf.Infinity, Wall))
            {
                Debug.DrawRay(origin.transform.position, directionMouse * hitWall.distance, Color.red);
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(origin.transform.position, directionMouse, out RaycastHit hitEnemy, Mathf.Infinity, Enemy))
                    {
                        Debug.DrawRay(origin.transform.position, directionMouse * hitEnemy.distance, Color.green);
                        if (hitEnemy.distance < hitWall.distance)
                        {
                            bulletLineRenderer.SetPosition(1, hitEnemy.point);
                            spiderWeb.transform.position = hitEnemy.point;
                            enemyForce = hitEnemy.collider.GetComponent<Enemy>();
                            enemyForce.Pull(-directionMouse);
                        }
                    }
                    else
                    {
                        bulletLineRenderer.SetPosition(1, hitWall.point);
                        spiderWeb.transform.position = hitWall.point;
                    }
                    targetTag = hitWall.collider.gameObject.name;
                    linePrefab.SetActive(true);
                    arrow.SetActive(false);
                    isMoving = true;
                    animator.SetBool("isCliming", false);
                    animator.SetBool("isHang", false);
                    targetDirection = directionMouse;
                    spiderWeb.SetActive(true);
                    
                }
            }
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        transform.position += targetDirection * speed * Time.deltaTime;
        bulletLineRenderer.SetPosition(0, origin.transform.position);
        
    }
    public void Stop()
    {
        isMoving = false;
        linePrefab.SetActive(false);
        arrow.SetActive(true);
        spiderWeb.SetActive(false);
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == targetTag)
        {
            Stop();
            if (other.gameObject.CompareTag("RightWall"))
            {
                transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
                animator.SetBool("isCliming", true);
            }
            if (other.gameObject.CompareTag("LeftWall"))
            {
                transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
                animator.SetBool("isCliming", true);
            }
            if (other.gameObject.CompareTag("UnderWall"))
            {
                transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
                animator.SetBool("isHang", true);
            }
        }
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == targetTag)
        {
            Stop();
            if (collision.gameObject.CompareTag("RightWall"))
            {
                transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
                animator.SetBool("isCliming", true);
            }
            if (collision.gameObject.CompareTag("LeftWall"))
            {
                transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
                animator.SetBool("isCliming", true);
            }
            if (collision.gameObject.CompareTag("UnderWall"))
            {
                transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
                animator.SetBool("isHang", true);
            }
        }
    }
}
