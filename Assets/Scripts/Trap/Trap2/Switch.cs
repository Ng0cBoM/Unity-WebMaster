using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject switchControll;
    public string state;
    private void Start()
    {
        state = "On";
    }
    public void SwitchState()
    {
        if (state == "On") Off();
        else if (state == "Off") On();
    }
    private void On()
    {
        switchControll.transform.localEulerAngles = new Vector3(0,0,30);
        state = "On";
    }

    private void Off()
    {
        switchControll.transform.localEulerAngles = new Vector3(0, 0, -30);
        state = "Off";
    }
}
