using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PingPongCoin : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration = 1f; 
    private float timer = 0f;
    private bool isIncreasing = true;
    public Slider slider;
    public TextMeshProUGUI textCoin;
    private bool isPingPong = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPingPong)
        {
            float variableValue = isIncreasing ? Mathf.PingPong(timer, duration) / duration : 1f - Mathf.PingPong(timer, duration) / duration;
            slider.value = variableValue;
            textCoin.text =(100 * ReturnValuePingPong()+"");
            timer += Time.deltaTime;
            if (timer > duration)
            {
                timer = 0f;
                isIncreasing = !isIncreasing;
            }
        }
    }
    int ReturnValuePingPong()
    {
        float value = slider.value;
        if (value <= 0.08f || value > 0.9f)
        {
            return 2;
        }
        else if ((value > 0.08f && value<=0.265f) || (value > 0.71f && value <= 0.9f))
        {
            return 3;
        }
        else if ((value > 0.265f && value <= 0.425f) || (value > 0.55f && value <= 0.71f))
        {
            return 4;
        }
        else 
        { 
            return 5;
        }
    }

    public void GetValuePingPong()
    {
        isPingPong = false;
        GameManger.Instance.extraCoin = ReturnValuePingPong();
    }
}
