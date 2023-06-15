using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class InstantiateMoney : MonoBehaviour
{
    public GameObject player;
    //public TextMeshProUGUI counter;
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
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 30f * Time.deltaTime);
        if (transform.position == player.transform.position)
        {
           // counter.transform.parent.GetChild(0).transform.DOScale(1.1f, 0.1f).SetLoops(10, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(1.2f);
            gameObject.SetActive(false);
        }
    }
}
