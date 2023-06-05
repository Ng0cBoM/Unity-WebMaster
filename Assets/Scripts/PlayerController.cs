using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 15f;
    private float speedWeb = 100f;
    private Vector3 targetPosition;
    private float distancePlayer;
    private Quaternion startRotation;
    private Vector3 spiderWebTarget;

    private string targetTag;
    public GameObject pointStop;

    public GameObject linePrefab;
    private LineRenderer lineRenderer;

    public GameObject handControll;
    public GameObject arrow;
    public GameObject spiderWeb;

    public Animator animator;

    private bool isMovingphase1;
    private bool isMovingphase2;

    public LayerMask Wall;
    public LayerMask Enemy;

    private Enemy enemyForce;
    public ParticleSystem particleSystem;

    void Start()
    {
        isMovingphase1 = false;
        isMovingphase2 = false;
        lineRenderer = linePrefab.GetComponent<LineRenderer>();
        linePrefab.SetActive(false);
        spiderWeb.SetActive(false);
        arrow.SetActive(false);
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionMouse = (worldPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionMouse);

        if (Physics.Raycast(transform.position, directionMouse, out RaycastHit hitWall, Mathf.Infinity, Wall))
        {
            Debug.DrawRay(transform.position, directionMouse * hitWall.distance, Color.red);
            if (Input.GetMouseButton(0))
            {
                Time.timeScale = 0.1f;
                arrow.SetActive(true);
                linePrefab.SetActive(false);
                spiderWeb.SetActive(false);
                handControll.transform.rotation = targetRotation;
                lineRenderer.SetPosition(1,transform.position);
            }
            if (Input.GetMouseButtonUp(0))
            { 
                Time.timeScale = 1f;
                arrow.SetActive(false);
                transform.rotation = targetRotation;
                if (Physics.Raycast(transform.position, directionMouse, out RaycastHit hitEnemy, Mathf.Infinity, Enemy))
                {
                    Debug.DrawRay(transform.position, directionMouse * hitEnemy.distance, Color.green);
                    if (hitEnemy.distance < hitWall.distance)
                    {
                        spiderWebTarget = hitEnemy.point;
                        enemyForce = hitEnemy.collider.GetComponent<Enemy>();
                        enemyForce.Pull(-directionMouse);
                    }
                    else
                    {
                        spiderWebTarget = hitWall.point;
                    }
                }
                else
                {
                    spiderWebTarget = hitWall.point;
                }
                targetTag = hitWall.collider.gameObject.tag;
                isMovingphase1 = true;
                animator.SetBool("isClimingRight", false);
                animator.SetBool("isClimingLeft", false);
                animator.SetBool("isHang", false);
                animator.SetBool("isFly", true);
                targetPosition = hitWall.point;
                pointStop.transform.position = targetPosition;
                distancePlayer = Vector3.Distance(transform.position, targetPosition);

            }
            if (isMovingphase1)
            {
                linePrefab.SetActive(true);
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, spiderWeb.transform.position);
                MoveSpiderWeb();
            }
            if (isMovingphase2)
            {
                MoveCharactor();
            }
        }
    }

    void MoveSpiderWeb()
    {
        spiderWeb.transform.position = Vector3.MoveTowards(spiderWeb.transform.position, spiderWebTarget, speedWeb * Time.deltaTime);
        lineRenderer.SetPosition(1, spiderWeb.transform.position);
        if (spiderWeb.transform.position == spiderWebTarget)
        {
            spiderWeb.SetActive(true);
            isMovingphase1 = false;
            isMovingphase2 = true;
        }
    }

    void MoveCharactor()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) <= distancePlayer * (9f / 10f))
        {
            speed = 25f;
        }
        lineRenderer.SetPosition(0, transform.position);
    }
    public void Stop()
    {
        speed = 15f;
        transform.rotation = startRotation;
        isMovingphase2 = false;
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
            particleSystem.Play();
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
