using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static Cinemachine.CinemachineImpulseManager.ImpulseEvent;

public class TrapLaserController : MonoBehaviour
{
    public GameObject switchControll;
    private string state;
    private LineRenderer lineRenderer;
    public LayerMask layerMask;
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    void LateUpdate()
    {
        state = switchControll.GetComponent<Switch>().state;
        if (state == "On")
        {
            if (Physics.Raycast(transform.position, Vector3.right, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, Vector3.right * hit.distance, Color.red);
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    hit.collider.gameObject.GetComponent<PlayerController>().Beaten();
                }
            }
        }
        else if (state == "Off")
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}
