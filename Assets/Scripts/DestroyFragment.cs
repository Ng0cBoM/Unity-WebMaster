using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using TMPro;

public class DestroyFragment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyFrag());
    }

    IEnumerator DestroyFrag()
    {
        yield return new WaitForSeconds(3f);
        transform.DOScale(0f, 1f).SetEase(Ease.OutBack).OnComplete(Enable);
    }

    private void Enable()
    {
        gameObject.SetActive(false);
    }


}
