using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EarnCoinEffect : MonoBehaviour
{
    public GameObject pileOfCoins;
    public TextMeshProUGUI counter;
    private Vector2[] initialPos;
    private Quaternion[] initialRotation;
    private int coinsAmount;
    private int bonus;
    void Start()
    {

        if (coinsAmount == 0)
            coinsAmount = 10;

        initialPos = new Vector2[coinsAmount];
        initialRotation = new Quaternion[coinsAmount];

        for (int i = 0; i < pileOfCoins.transform.childCount; i++)
        {
            initialPos[i] = pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
            initialRotation[i] = pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().rotation;
        }
    }

    public void EarnCoin(int id)
    {
        if (id == 0)
        {
            bonus = 10;
           // pileOfCoins.transform.position = new Vector3(-11, -220, pileOfCoins.transform.position.z);
            CountCoins(); 
        }
        else
        {
            bonus = 10 * GameManger.Instance.extraCoin;
            //pileOfCoins.transform.position = new Vector3(46, -81, pileOfCoins.transform.position.z);
            CountCoins();
        }
       
    }

    private void CountCoins()
    {
        pileOfCoins.SetActive(true);
        var delay = 0f;

        for (int i = 0; i < pileOfCoins.transform.childCount; i++)
        {
            pileOfCoins.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

            pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(157, 290), 0.8f)
                .SetDelay(delay + 0.1f).SetEase(Ease.InBack);


            pileOfCoins.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.1f)
                .SetEase(Ease.Flash);


            pileOfCoins.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack).OnComplete(CountCoinsByComplete);

            delay += 0.1f;

            counter.transform.parent.GetChild(0).transform.DOScale(1.1f, 0.1f).SetLoops(10, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(1.2f);
        }
        StartCoroutine(UpdateCoin());
    }
    private IEnumerator UpdateCoin()
    {
        yield return new WaitForSeconds(3f);
        GameManger.Instance.WriteMoney(int.Parse(counter.text));
        UIManager.Instance.NextLevel();
    }
    void CountCoinsByComplete()
    {
        counter.text = (int.Parse(counter.text) + bonus).ToString();
        
    }
}