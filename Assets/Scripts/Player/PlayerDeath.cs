using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject playerParent;
    // Start is called before the first frame update
    void Death()
    {
        Time.timeScale = 1f;
        UIManager.Instance.LostGame();
    }
   /* void ReallyDeath()
    {
        Destroy(gameObject);
    }*/

    void ReallyReallyDeath()
    {
        Destroy(playerParent);
    }
}
