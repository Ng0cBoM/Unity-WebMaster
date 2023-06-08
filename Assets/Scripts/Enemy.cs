using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private bool isLive;
    public bool isMove;
    private float startX;
    public GameObject weapon;
    private float originalTimeScale;
    private float originalOrthographicSize;
    public CinemachineVirtualCamera virtualCamera;
    public ParticleSystem particleSystem;
    public GameObject enemyDestroyManager;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>(); 
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_BoxCollider = gameObject.GetComponent<BoxCollider>();
        isLive = true;
        isMove = true;
        startX = transform.position.x;
        originalTimeScale = Time.timeScale;
        originalOrthographicSize = virtualCamera.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            if (isMove)
            {
                transform.position += new Vector3(Mathf.Sign(transform.localScale.x),0,0) * speed * Time.deltaTime;
                if (transform.position.y <= 0) isLive = false;
                if(Mathf.Abs(startX - transform.position.x) >= 0.5) Rotate();
            }
        }
        else
        {
            Destroy(gameObject);
        }        
    }
    void Rotate()
    {
        transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManger.Instance.enemyCount -= 1;
            m_Animator.SetTrigger("die");
            m_BoxCollider.isTrigger = true;
            Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
            enemyDestroyManager.GetComponent<EnemyListDestroyManager>().ShowEnemyDestroy(GameManger.Instance.enemyCount);
            if (GameManger.Instance.enemyCount == 0)
            {
                StartCoroutine(ZoomIn());
            }
            else
            {
                StartCoroutine(SlowMotionCoroutine());
            }
        }
    }
    private IEnumerator SlowMotionCoroutine()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = originalTimeScale;
    }

    private System.Collections.IEnumerator ZoomIn()
    {
        float elapsedTime = 0f;
        Time.timeScale = 0.1f;
        while (elapsedTime < 2f)
        {
            float t = elapsedTime / 2f;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 7, t);
            elapsedTime += Time.deltaTime*(originalTimeScale/Time.timeScale);
            yield return null;
        }
        Time.timeScale = originalTimeScale;
        virtualCamera.m_Lens.OrthographicSize = originalOrthographicSize;
        UIManager.Instance.WinGame();
    }

    void StartAttack()
    {
        weapon.GetComponent<AttackPlayer>().isAttack = true;
    }
    void EndAttack()
    {
        weapon.GetComponent<AttackPlayer>().isAttack = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void Pull(Vector3 directionPull)
    {
        m_Rigidbody.AddForce(directionPull * force, ForceMode.Impulse);
    }
}
