using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountTimeKillEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public static CountTimeKillEnemy Instance;
    private float timeKillLatest = 0;
    private float timeKillCurrent = 0;
    private int countKill = -1;
    public GameObject textCountKill;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
        if (timeKillLatest>0)
        {
            float duration = timeKillCurrent - timeKillLatest;
            if (duration>0 && duration < 2f)
            {
                countKill++;
                CheckCountKill();
            }
            else if (duration >= 2f)
            {
                countKill = -1;
            }
        }
        timeKillLatest = timeKillCurrent;
    }

    public void SetTimeKillCurrent()
    {
        timeKillCurrent = Time.time;
    }
    void CheckCountKill()
    {
        if (countKill < 4)
        {
            textCountKill.transform.GetChild(countKill).gameObject.SetActive(true);
            StartCoroutine(HideText(countKill));
        }
        else textCountKill.transform.GetChild(4).gameObject.SetActive(true);
    }
    IEnumerator HideText(int count)
    {
        yield return new WaitForSeconds(1f);
        textCountKill.transform.GetChild(count).gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
