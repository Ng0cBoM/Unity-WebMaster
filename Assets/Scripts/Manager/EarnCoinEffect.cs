using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EarnCoinEffect : MonoBehaviour
{
    public GameObject pileOfCoins;
    public GameObject coinText;
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

        for (int i = 0; i < pileOfCoins.transform.childCount -1 ; i++)
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
            CountCoins(); 
        }
        else
        {
            bonus = 10 * GameManger.Instance.extraCoin;
            CountCoins();
        }
       
    }

    private void CountCoins()
    {
        pileOfCoins.SetActive(true);
        var delay = 0f;

        for (int i = 0; i < pileOfCoins.transform.childCount -1 ; i++)
        {
            pileOfCoins.transform.GetChild(i).DOScale(1.5f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

            pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(421, 778), 0.8f)
                .SetDelay(delay + 0.1f).SetEase(Ease.InBack);


            pileOfCoins.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.1f)
                .SetEase(Ease.Flash);


            pileOfCoins.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack).OnComplete(CountCoinsByComplete);

            delay += 0.1f;
        }
        StartCoroutine(UpdateCoin());
    }
    private IEnumerator UpdateCoin()
    {
        yield return new WaitForSeconds(3f);
        UIManager.Instance.NextLevel();
    }
    void CountCoinsByComplete()
    {
        GameManger.Instance.UpdateMoney(bonus);
        UIManager.Instance.SetCoinText(); 
    }
}