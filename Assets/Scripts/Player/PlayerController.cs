using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 15f;
    private float speedWeb = 100f;
    private Vector3 targetPosition;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    Quaternion playerRotation;
    private Vector3 spiderWebTarget;

    private string targetTag;
    private GameObject targetCollide;

    public GameObject linePrefab;
    private LineRenderer lineRenderer;

    public GameObject hand;
    public GameObject spiderWeb;
    public GameObject arrow;

    private GameObject enemyPull;

    public Animator animator;

    private bool isMovingphase1;
    public bool isMovingphase2;

    public LayerMask Wall;
    public LayerMask Enemy;
    public ParticleSystem particleSystem;

    private Vector3 particleSystemPosition;

    private Rigidbody rb;

    void Start()
    {
        isMovingphase1 = false;
        isMovingphase2 = false;
        lineRenderer = linePrefab.GetComponent<LineRenderer>();
        linePrefab.SetActive(false);
        spiderWeb.SetActive(false);
        arrow.SetActive(false);
        startRotation = transform.rotation;
        hand.transform.position = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.isGamePlay)
        {
            if (GameManger.Instance.isPlay)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = -Camera.main.transform.position.z;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector3 directionMouse = (worldPosition - transform.position).normalized;
                targetRotation = Quaternion.LookRotation(Vector3.forward, directionMouse);

                if (Physics.Raycast(transform.position, directionMouse, out RaycastHit hitWall, Mathf.Infinity, Wall))
                {
                    Debug.DrawRay(transform.position, directionMouse * hitWall.distance, Color.red);
                    if (Input.GetMouseButton(0) && UIManager.Instance.countTouch > 0)
                    {
                        arrow.SetActive(true);
                        arrow.transform.rotation = targetRotation;
                        hand.transform.position = worldPosition;
                        linePrefab.SetActive(false);
                        spiderWeb.SetActive(false);
                        Time.timeScale = 0.1f;
                        if (isMovingphase2)
                        {
                            isMovingphase2 = false;
                            rb.useGravity = true;
                            rb.isKinematic = false;
                            Falling();
                        }
                    }
                    if (Input.GetMouseButtonUp(0) && UIManager.Instance.countTouch > 0)
                    {
                        spiderWeb.transform.rotation = targetRotation;
                        spiderWeb.SetActive(true);
                        arrow.SetActive(false);
                        Time.timeScale = 1f;
                        if (Physics.Raycast(transform.position, directionMouse, out RaycastHit hitEnemy, Mathf.Infinity, Enemy))
                        {
                            Debug.DrawRay(transform.position, directionMouse * hitEnemy.distance, Color.green);
                            if (hitEnemy.distance < hitWall.distance)
                            {
                                spiderWebTarget = hitEnemy.point;
                                enemyPull = hitEnemy.collider.gameObject;                                
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
                        targetPosition = hitWall.point;
                        targetCollide = hitWall.collider.gameObject;
                        particleSystemPosition = targetPosition;
                        RetransFormTargetPosition();
                        hand.transform.position = targetPosition;

                    }
                    if (isMovingphase1)
                    {
                        lineRenderer.SetPosition(0, transform.position);
                        lineRenderer.SetPosition(1, spiderWeb.transform.position);
                        linePrefab.SetActive(true);
                        MoveSpiderWeb();
                    }
                    if (isMovingphase2)
                    {
                        MoveCharactor();
                    }
                }
            }
            UIManager.Instance.countTouch = 1;
        }  
    }

    void Falling()
    {
        animator.SetTrigger("isFalling");
        animator.SetBool("isFly", false);
        transform.rotation = startRotation;
    }

    void MoveSpiderWeb()
    {
        spiderWeb.transform.position = Vector3.MoveTowards(spiderWeb.transform.position, spiderWebTarget, speedWeb * Time.deltaTime);
        lineRenderer.SetPosition(1, spiderWeb.transform.position);
        if (spiderWeb.transform.position == spiderWebTarget)
        {
            if (targetTag == "Switch")
            {
                Stop();
                targetCollide.GetComponent<Switch>().SwitchState();
            }
            else
            {
                StartCoroutine(StartFly());
            }
            isMovingphase1 = false;
            playerRotation = targetRotation;
        }
    }
    IEnumerator StartFly()
    {
        yield return new WaitForSeconds(0.15f);
        transform.rotation = playerRotation;
        isMovingphase2 = true;
        animator.SetBool("isClimingRight", false);
        animator.SetBool("isClimingLeft", false);
        animator.SetBool("isHang", false);
        animator.SetBool("isFly", true);
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void MoveCharactor()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (enemyPull)
        {
            if (enemyPull.GetComponent<PullObject>().canPull)
            {
                enemyPull.transform.position = Vector3.MoveTowards(enemyPull.transform.position, transform.position, 30 * Time.deltaTime);
                lineRenderer.SetPosition(1, enemyPull.transform.position);
            }
            else
            {
                linePrefab.SetActive(false);
                spiderWeb.SetActive(false);
                enemyPull=null;
            }
        }
        if (transform.position == targetPosition)
        {
            Stop();
            Instantiate(particleSystem, particleSystemPosition, particleSystem.transform.rotation);
        }
        lineRenderer.SetPosition(0, transform.position);
    }

    void RetransFormTargetPosition()
    {
        if (targetTag == "RightWall")
        {
            targetPosition += new Vector3(-0.5f,0,0);
        }
        if (targetTag == "LeftWall")
        {
            targetPosition += new Vector3(0.5f, 0, 0);
        }
        if (targetTag == "UnderWall")
        {
            targetPosition += new Vector3(0, -0.62f, 0);
        }
        if (targetTag == "AboveWall")
        {
            targetPosition += new Vector3(0, 0.62f, 0);
        }
    }

    public void Stop()
    {
        rb.useGravity = false;
        hand.transform.position = transform.position;
        transform.rotation = startRotation;
        isMovingphase2 = false;
        linePrefab.SetActive(false);
        spiderWeb.SetActive(false);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        spiderWeb.transform.position = transform.position;
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

        if (targetTag == "Barrel")
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            Falling();
            rb.AddForce(new Vector3(-1,1,0) * 300f, ForceMode.Impulse);
            targetCollide.GetComponent<Destroy>().DestroyBarrel();
        }
    }

    public void Beaten()
    {
        animator.SetTrigger("isHurt");
        rb.mass = 20;
        Stop();
        rb.useGravity = true;
        rb.isKinematic = false;
        GameManger.Instance.isPlay = false;
    }
}
