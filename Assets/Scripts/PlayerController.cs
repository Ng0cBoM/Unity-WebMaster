using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 7f;
    private bool isMoving;
    private Vector3 targetPosition;
    private Vector3 targetDirection;

    public GameObject linePrefab;
    private LineRenderer bulletLineRenderer;

    public GameObject origin;
    public GameObject handControll;

    public Animator animator;

    void Start()
    {
        isMoving = false;
        targetPosition = transform.position;
        bulletLineRenderer = linePrefab.GetComponent<LineRenderer>();
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
            if (Physics.Raycast(origin.transform.position, directionMouse, out RaycastHit hitInfo, Mathf.Infinity))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isMoving = true;
                    animator.SetBool("isCliming", false);
                    targetPosition = hitInfo.point;
                    targetDirection = directionMouse;
                    bulletLineRenderer.SetPosition(1, hitInfo.point);
                    //StartCoroutine(MovePlayer(hitInfo.point));
                }
            }
        }
        else
        {
            transform.position += targetDirection * speed * Time.deltaTime;
            bulletLineRenderer.SetPosition(0, transform.position);
        }
        
    }

    //IEnumerator MovePlayer(Vector3 targetPosition)
    //{
    //    Vector3 initialPosition = origin.transform.position;
    //    //float elapsedTime = 0f;
    //    //float duration = 1f;
        
    //    //while (elapsedTime < duration)
    //    //{
    //    //    float t = elapsedTime / duration;
    //    //    transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
    //    //    bulletLineRenderer.SetPosition(0, origin.transform.position);
    //    //    elapsedTime += Time.deltaTime;
    //    //    yield return null;
    //    //}
    //     while (transform.position!=targetPosition)
    //    {

    //        yield return null;
    //    }

    //    bulletLineRenderer.SetPosition(0, transform.position);
    //    transform.position = targetPosition;
    //    isMoving = false;
        
    //}

    private void OnCollisionEnter(Collision collision)
    {
        isMoving = false;
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

    }
}
