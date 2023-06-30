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
    public TextMeshProUGUI levelWin;
    public TextMeshProUGUI levelLost;
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
        /*levelWin.text = "LEVEL " + (LevelManager.Instance.CurrentLevel() + 1);
        levelLost.text = "LEVEL " + (LevelManager.Instance.CurrentLevel() + 1);*/
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
        //LevelManager.Instance.LoadSceneByID(LevelManager.Instance.CurrentLevel());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        /*LevelManager.Instance.NextLevel();
        LevelManager.Instance.LoadSceneByID(LevelManager.Instance.CurrentLevel());*/
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

    public void SetCoinText()
    {
        coinText.text = GameManger.Instance.coin.ToString();
    }
}
