using Cinemachine;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator m_Animator;
    private BoxCollider m_BoxCollider;
    private float originalTimeScale;
    private float originalOrthographicSize;
    public CinemachineVirtualCamera virtualCamera;
    public ParticleSystem particleSystem;
    public GameObject enemyDestroyManager;
    public int count = 0;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>(); 

        m_BoxCollider = gameObject.GetComponent<BoxCollider>();
        originalTimeScale = Time.timeScale;
        originalOrthographicSize = virtualCamera.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -5) Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box") ) && count == 0 )
        {
            gameObject.GetComponent<PullObject>().canPull = false;
            Die();
            count++;
            m_BoxCollider.isTrigger = true;
        }
    }
    
    public void Die()
    {
        m_BoxCollider.isTrigger = true;
        GameManger.Instance.enemyCount -= 1;
        m_Animator.SetTrigger("die");
        CountTimeKillEnemy.Instance.SetTimeKillCurrent();
        Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
        enemyDestroyManager.GetComponent<EnemyListDestroyManager>().ShowEnemyDestroy(GameManger.Instance.enemyCount);
        gameObject.GetComponent<FlashEnemy>().Hit();
        if (GameManger.Instance.enemyCount == 0)
        {
            StartCoroutine(ZoomIn());
        }
        else
        {
            StartCoroutine(SlowMotionCoroutine());
        }
    }
    private IEnumerator SlowMotionCoroutine()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = originalTimeScale;
    }
    private IEnumerator ZoomIn()
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
}
