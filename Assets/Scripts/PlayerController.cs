using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 13f;
    private Vector3 targetPosition;
    private Quaternion startRotation;
    private string targetTag;
    public GameObject pointStop;

    public GameObject linePrefab;
    private LineRenderer lineRenderer;

    public GameObject handControll;
    public GameObject arrow ;
    public GameObject spiderWeb;

    public Animator animator;

    private bool isMoving;

    public LayerMask Wall;
    public LayerMask Enemy;

    private Enemy enemyForce;

    void Start()
    {
        isMoving = false;
        lineRenderer = linePrefab.GetComponent<LineRenderer>();
        linePrefab.SetActive(false);
        spiderWeb.SetActive(false);
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionMouse = (worldPosition - transform.position).normalized;

        if (isMoving == false)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionMouse);
            handControll.transform.rotation = targetRotation;

            if (Physics.Raycast(transform.position, directionMouse, out RaycastHit hitWall, Mathf.Infinity, Wall))
            {
                Debug.DrawRay(transform.position, directionMouse * hitWall.distance, Color.red);
                if (Input.GetMouseButtonDown(0))
                {
                    transform.rotation = targetRotation;
                    if (Physics.Raycast(transform.position, directionMouse, out RaycastHit hitEnemy, Mathf.Infinity, Enemy))
                    {
                        Debug.DrawRay(transform.position, directionMouse * hitEnemy.distance, Color.green);
                        if (hitEnemy.distance < hitWall.distance)
                        {
                            lineRenderer.SetPosition(1, hitEnemy.point);
                            spiderWeb.transform.position = hitEnemy.point;
                            enemyForce = hitEnemy.collider.GetComponent<Enemy>();
                            enemyForce.Pull(-directionMouse);
                        }
                        else
                        {
                            lineRenderer.SetPosition(1, hitWall.point);
                            spiderWeb.transform.position = hitWall.point;
                        }
                    }
                    else
                    {
                        lineRenderer.SetPosition(1, hitWall.point);
                        spiderWeb.transform.position = hitWall.point;
                    }
                    pointStop.transform.position = hitWall.point;
                    targetTag = hitWall.collider.gameObject.tag;
                    Debug.Log(targetTag);
                    linePrefab.SetActive(true);
                    arrow.SetActive(false);
                    isMoving = true;
                    animator.SetBool("isClimingRight", false);
                    animator.SetBool("isClimingLeft", false);
                    animator.SetBool("isHang", false);
                    animator.SetBool("isFly", true);
                    targetPosition = hitWall.point;
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
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        lineRenderer.SetPosition(0, transform.position);
        
    }
    public void Stop()
    {
        transform.rotation = startRotation;
        isMoving = false;
        linePrefab.SetActive(false);
        arrow.SetActive(true);
        spiderWeb.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Web"))
        {
            Stop();
        }
        if (targetTag == "RightWall")
        {
            animator.SetBool("isClimingRight", true);
            animator.SetBool("isFly", false);
        }
        if (targetTag == "LeftWall")
        {
            animator.SetBool("isClimingLeft", true);
            animator.SetBool("isFly", false);
        }
        if (targetTag == "UnderWall")
        {
            animator.SetBool("isHang", true);
            animator.SetBool("isFly", false);
        }
        if (targetTag == "AboveWall")
        {
            animator.SetBool("isClimingRight", false);
            animator.SetBool("isClimingLeft", false);
            animator.SetBool("isHang", false);
            animator.SetBool("isFly", false);
        }

    }
}
