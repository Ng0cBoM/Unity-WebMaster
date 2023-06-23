using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFragmentBarrel : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyFrag());
    }

    IEnumerator DestroyFrag()
    {
        yield return new WaitForSeconds(1);
        transform.DOScale(0f, 1f).SetEase(Ease.OutBack).OnComplete(Enable);
    }

    private void Enable()
    {
        gameObject.SetActive(false);
    }
}
