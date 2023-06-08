﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManger : MonoBehaviour
{
    // Start is called before the first frame update
    public int enemyCount;
    public static GameManger Instance;
    public int coin;
    public int extraCoin;
    private string filePath;
    
    void Start()
    {
        enemyCount = GameObject.FindObjectsOfType<Enemy>().Length;
        filePath = Path.Combine(Application.dataPath, "Coin.txt");
        coin = ReadMoney();
        UIManager.Instance.coinText.text = coin.ToString();
    }
    private int ReadMoney()
    {
        if (File.Exists(filePath))
        {
            string coinString = File.ReadAllText(filePath);
            int coin;
            if (int.TryParse(coinString, out coin))
            {
                return coin;
            }
        }
        return 0;
    }

    public void WriteMoney(int money)
    {
        File.WriteAllText(filePath, money.ToString());
    }
    // Update is called once per frame
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
