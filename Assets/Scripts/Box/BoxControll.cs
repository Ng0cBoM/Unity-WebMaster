using UnityEngine;

public class BoxControll : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private float force = 30f;
    private bool isReadyBroken = false ;
    public GameObject[] fragments;
    public GameObject money;
    public float forceExplo = 10f;
    private GameObject player;

    private void Start()
    {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_Rigidbody.AddForce((-collision.gameObject.transform.position + transform.position).normalized * force, ForceMode.Impulse);
            isReadyBroken = true;
            player = collision.gameObject;
            gameObject.GetComponent<PullObject>().canPull = false;
        }
        else if (isReadyBroken)
        {
            gameObject.SetActive(false);
            foreach(GameObject fragment in fragments)
            {
                Instantiate(fragment, transform.position, fragment.transform.rotation);
                fragment.GetComponent<Rigidbody>().AddForce(Vector3.up * forceExplo,ForceMode.Impulse);
            }
            for (int i=0; i<5; i++)
            {
                Instantiate(money, transform.position, money.transform.rotation);
                money.GetComponent<InstantiateMoney>().player = player;
            }
        }
    }
}
