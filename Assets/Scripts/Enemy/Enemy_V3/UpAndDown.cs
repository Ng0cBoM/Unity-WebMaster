using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpAndDown : MonoBehaviour
{
    public GameObject weaponOnHand;
    private void Start()
    {
    }

    private void Update()
    {
        weaponOnHand.SetActive(false);
    }

    void Up()
    {
        transform.DOMove(new Vector3(5.984f, 17.223f), 0.3f).SetEase(Ease.InOutSine);
    }

    private void Down()
    {
        transform.DOMove(new Vector3(5.856f, 16.061f), 1).SetEase(Ease.InOutSine);
    }


}
