using EgdFoundation;
using System.Collections;
using UnityEngine;


public class PlayerController2 : MonoBehaviour
{
    Fsm<PlayerController2> stateMachine;
    public Animator animator;

    float speedPlayerMove = 15f;
    float speedNinjaWeaponMove = 80f;
    public Vector3 targetPosition;
    public Quaternion targetRotation;
    public string targetTag;
    GameObject targetCollide;
    public Vector3 worldPosition;
    public Vector3 ninjaWeaponPositionTarget;

    public GameObject line;
    public LineRenderer lineRenderer;

    public GameObject hand;
    public GameObject ninjaWeapon;
    public GameObject arrow;
    private GameObject objectCollide;

    public bool isMouseButtonDown = false;
    public bool isMoveWeapon = false;
    public bool isPullTheRope = false;
    public bool isStand = false;
    public bool isFlying = false;

    public LayerMask WallLayer;
    public LayerMask EnemyLayer;
    public ParticleSystem particleInGround;
    public GameObject particleInAir;

    Vector3 particleSystemPosition;

    public Rigidbody rb;
    void Start()
    {
        stateMachine = new Fsm<PlayerController2>(this);

        stateMachine.AddState<PlayerInAirState>();
        stateMachine.AddState<PlayerFallingState>();
        stateMachine.AddState<PlayerStandState>();
        stateMachine.AddState<RopeMove>();
        stateMachine.AddState<PlayerDeathState>();

        lineRenderer = line.GetComponent<LineRenderer>();
        rb = gameObject.GetComponent<Rigidbody>();
        hand.transform.position = transform.position;

        stateMachine.SwitchState<PlayerStandState>();
    }

    void Update()
    {
        if (UIManager.Instance.isGamePlay)
        {
            if (GameManger.Instance.isPlay && UIManager.Instance.countTouch > 0)
            {
                stateMachine.Update();
                Shoot();
            }
            UIManager.Instance.countTouch = 1;
        }
    }

    void Shoot()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionMouse = (worldPosition - transform.position).normalized;
        targetRotation = Quaternion.LookRotation(Vector3.forward, directionMouse);

        if (Physics.Raycast(transform.position, directionMouse, out RaycastHit hitWall, Mathf.Infinity, WallLayer))
        {
            Debug.DrawRay(transform.position, directionMouse * hitWall.distance, Color.red);
            if (Input.GetMouseButton(0))
            {
                DoIfMouseDown();
                isMouseButtonDown = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isMouseButtonDown = false;
                DoIfMouseUp(directionMouse, hitWall);
            }
        }
    }

    public void DoIfMouseDown()
    {
        arrow.SetActive(true);
        arrow.transform.rotation = targetRotation;
        hand.transform.position = worldPosition;
        Time.timeScale = 0.2f;
    }

    public void PlayAnim(string animName)
    {
        animator.CrossFade(animName, 0);
    }

    void DoIfMouseUp(Vector3 directionMouse, RaycastHit hitWall)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, ninjaWeapon.transform.position);
        line.SetActive(true);
        ninjaWeapon.transform.rotation = targetRotation;
        ninjaWeapon.SetActive(true);
        arrow.SetActive(false);
        Time.timeScale = 1f;
        if (Physics.Raycast(transform.position, directionMouse, out RaycastHit hitEnemy, Mathf.Infinity, EnemyLayer))
        {
            Debug.DrawRay(transform.position, directionMouse * hitEnemy.distance, Color.green);
            if (hitEnemy.distance < hitWall.distance)
            {
                ninjaWeaponPositionTarget = hitEnemy.point;
                objectCollide = hitEnemy.collider.gameObject;
            }
            else
            {
                ninjaWeaponPositionTarget = hitWall.point;
            }
        }
        else
        {
            ninjaWeaponPositionTarget = hitWall.point;
        }
        targetTag = hitWall.collider.gameObject.tag;
        targetPosition = hitWall.point;
        targetCollide = hitWall.collider.gameObject;
        particleSystemPosition = targetPosition;
        RetransFormTargetPosition();
        hand.transform.position = targetPosition;
        isMoveWeapon = true;
        Debug.Log(Mathf.Sign(targetPosition.x - transform.position.x));
    }

    void RetransFormTargetPosition()
    {
        if (targetTag == "RightWall")
        {
            targetPosition += new Vector3(-0.5f, 0, 0);
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

    public void MoveNinjaWeapon()
    {
        ninjaWeapon.transform.position = Vector3.MoveTowards
            (ninjaWeapon.transform.position, ninjaWeaponPositionTarget, speedNinjaWeaponMove * Time.deltaTime);
        lineRenderer.SetPosition(1, ninjaWeapon.transform.position);
        if (ninjaWeapon.transform.position == ninjaWeaponPositionTarget)
        {
            isMoveWeapon = false;
            if (targetTag == "Switch")
            {
                targetCollide.GetComponent<Switch>().SwitchState();
                isPullTheRope = true;
            }
            else
            {
                StartCoroutine(StartFly());
            }     
        }
    }

    public void PullTheRope()
    {
        ninjaWeapon.transform.position = Vector3.MoveTowards
           (ninjaWeapon.transform.position, transform.position, speedNinjaWeaponMove * Time.deltaTime);
        lineRenderer.SetPosition(1, ninjaWeapon.transform.position);
        if (ninjaWeapon.transform.position == transform.position)
        {
            isPullTheRope = false;
            hand.transform.position = transform.position;
            line.SetActive(false);
            ninjaWeapon.SetActive(false);
        }
    }

    public void MoveCharactor()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedPlayerMove * Time.deltaTime);
        if (objectCollide)
        {
            if (objectCollide.GetComponent<PullObject>().canPull)
            {
                objectCollide.transform.position = Vector3.MoveTowards(objectCollide.transform.position, transform.position, 30 * Time.deltaTime);
                lineRenderer.SetPosition(1, objectCollide.transform.position);
            }
            else
            {
                line.SetActive(false);
                ninjaWeapon.SetActive(false);
                objectCollide = null;
            }
        }
        lineRenderer.SetPosition(0, transform.position);
        if (transform.position == targetPosition)
        {
            if (targetTag == "Barrel")
            {
                rb.AddForce(new Vector3(-1, 1, 0) * 500f, ForceMode.Impulse);
                targetCollide.GetComponent<Destroy>().DestroyBarrel();
                stateMachine.SwitchState<PlayerFallingState>();
            }
            else stateMachine.SwitchState<PlayerStandState>();
            Instantiate(particleInGround, particleSystemPosition, particleInGround.transform.rotation);
        }
        
    }

    IEnumerator StartFly()
    {
        yield return new WaitForSeconds(0.15f);
        stateMachine.SwitchState<PlayerInAirState>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AboveWall"))
        {
            isStand = true;
        }
    }
    public void Beaten()
    {
        stateMachine.SwitchState<PlayerDeathState>();
        rb.isKinematic = false;
        GameManger.Instance.isPlay = false;
    }
}
