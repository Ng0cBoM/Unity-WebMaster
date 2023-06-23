using DG.Tweening;
using UnityEngine;

public class MoveLaserTrap : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveY(8f, 3).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}

