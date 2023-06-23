using System.Collections;
using UnityEngine;

public class InstantiateMoney : MonoBehaviour
{
    public GameObject player;
    private bool isEarn = false;
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 20f, ForceMode.Impulse);
        StartCoroutine(WaitToEarnMoney());
    }

    private void Update()
    {
        if (isEarn)
        {
            EarnMoney();
        }
    }
    IEnumerator WaitToEarnMoney()
    {
        yield return new WaitForSeconds(1f);
        isEarn = true;
    }
    void EarnMoney()
    {
        if (player)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 30f * Time.deltaTime);
            if (transform.position == player.transform.position)
            {
                GameManger.Instance.UpdateMoney(10);
                UIManager.Instance.SetCoinText();
                gameObject.SetActive(false);
            }
        }
        else gameObject.SetActive(false);

    }
}
