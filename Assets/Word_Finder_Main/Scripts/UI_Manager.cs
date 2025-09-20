using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    [Header("Elements")]
    [SerializeField] private CanvasGroup menuCG;
    [SerializeField] private CanvasGroup gameCG;
    [SerializeField] private CanvasGroup levelCompleteCG;
    [SerializeField] private CanvasGroup gameOverCG;

    [Header("Menu Elements")]
    [SerializeField] private TextMeshProUGUI menuCoins;
    [SerializeField] private TextMeshProUGUI menuBestScore;

    [Header("Level Complete ELements")]
    [SerializeField] private TextMeshProUGUI levelCompleteCoins;
    [SerializeField] private TextMeshProUGUI levelCompleteScore;
    [SerializeField] private TextMeshProUGUI levelCompleteSecretWord;
    [SerializeField] private TextMeshProUGUI levelCompletebestScore;

    [Header("Game Elements")]
    [SerializeField] private TextMeshProUGUI gameCoins;
    [SerializeField] private TextMeshProUGUI gameScore;

    [Header("Game Over Elements")]
    [SerializeField] private TextMeshProUGUI gameOverCoins;
    [SerializeField] private TextMeshProUGUI gameOverSecretWord;
    [SerializeField] private TextMeshProUGUI gameOverBestScore;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ShowGame();
        ShowMenu();
        HideGame();
        HideLevelComplete();
        HideGameOver();

        GameManager.OnGameStateChanged += GameStateChangedCallBack;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallBack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                ShowMenu();
                HideGame();
                break;

            case GameState.Game:
                ShowGame();
                HideMenu();
                HideLevelComplete();
                HideGameOver();
                break;

            case GameState.LevelComplete:
                ShowLevelComplete();
                HideGame();
                break;

            case GameState.GameOver:
                ShowGameOver();
                HideGame();
                break;
        }
    }

    private void ShowMenu()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        menuBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(menuCG);
    }

    private void HideMenu()
    {
        HideCG(menuCG);
    }

    private void ShowGame()
    {
        gameCoins.text = DataManager.instance.GetCoins().ToString();
        gameScore.text = DataManager.instance.GetScore().ToString();

        ShowCG(gameCG);
    }

    private void HideGame()
    {
        HideCG(gameCG);
    }

    private void ShowLevelComplete()
    {
        levelCompleteCoins.text = DataManager.instance.GetCoins().ToString();
        levelCompleteScore.text = DataManager.instance.GetScore().ToString();
        levelCompletebestScore.text = DataManager.instance.GetBestScore().ToString();
        levelCompleteSecretWord.text = WordManager.instance.GetSecretWord();

        ShowCG(levelCompleteCG);
    }

    private void HideLevelComplete()
    {
        HideCG(levelCompleteCG);
    }

    private void ShowGameOver()
    {
        gameOverCoins.text = DataManager.instance.GetCoins().ToString();
        gameOverBestScore.text = DataManager.instance.GetBestScore().ToString();
        gameOverSecretWord.text = WordManager.instance.GetSecretWord();

        ShowCG(gameOverCG);

    }

    private void HideGameOver()
    {
        HideCG(gameOverCG);
    }

    private void ShowCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    private void HideCG(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
