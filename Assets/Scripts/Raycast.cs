using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    private float speed = 7f;
    private bool isMoving;

    void Start()
    {
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = (worldPosition - transform.position).normalized;
        if (Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, direction * hitInfo.distance, Color.red);
            if (Input.GetMouseButtonDown(0) && isMoving == false)
            {
                isMoving = true;
                StartCoroutine(MovePlayer(hitInfo.point));
            }
        }
    }

    IEnumerator MovePlayer(Vector3 targetPosition)
    {
        Vector3 initialPosition = transform.position;
        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        isMoving = false;
    }
}
