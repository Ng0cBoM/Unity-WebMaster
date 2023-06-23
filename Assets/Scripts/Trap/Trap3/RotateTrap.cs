using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateTrap : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startPoint;
    public GameObject endPoint;
    void Start()    {
        gameObject.transform.position = startPoint.transform.position;
        transform.DOMove(endPoint.transform.position, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(0, 0, 360), 2, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
}
