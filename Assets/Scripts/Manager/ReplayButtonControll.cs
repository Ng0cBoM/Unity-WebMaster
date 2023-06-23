using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButtonControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Replay()
    {
        UIManager.Instance.ReplayGame();
    }
}
