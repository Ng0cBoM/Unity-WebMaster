using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager Instance;
    public GameObject startGameUI;
    public GameObject playGameUI;
    public GameObject settingUI;
    public GameObject shoppingUI;
    public GameObject lostUI;
    public TextMeshProUGUI coinText;
    public GameObject winUI;
    public bool isGamePlay;
    public int countTouch;
    void Start()
    {
        startGameUI.SetActive(true);
        lostUI.SetActive(false);
        winUI.SetActive(false);
        shoppingUI.SetActive(false);
        settingUI.SetActive(false);
        playGameUI.SetActive(false);
        isGamePlay = false;
        countTouch = 0;
    }

    // Update is called once per frame
    public void PlayGame()
    {
        isGamePlay=true;
        startGameUI.SetActive(false);
        playGameUI.SetActive(true);
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenSetting()
    {
        settingUI.SetActive(true);
        startGameUI.SetActive(false);

    }
    public void CloseSetting()
    {
        settingUI.SetActive(false);
        startGameUI.SetActive(true);
    }
    public void OpenShopping()
    {
        shoppingUI.SetActive(true);
        startGameUI.SetActive(false);
    }
    public void CloseShopping()
    {
        shoppingUI.SetActive(false);
        startGameUI.SetActive(true);
    }
    public void WinGame()
    {
        isGamePlay=false;
        winUI.SetActive(true);
    }

    public void LostGame()
    {
        lostUI.SetActive(true);
        playGameUI.SetActive(false);
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
