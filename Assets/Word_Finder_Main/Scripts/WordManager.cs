using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager instance;

    [Header("Elements")]
    [SerializeField] public string secretWord;
    [SerializeField] private TextAsset wordsText;
    private string words;

    [Header("Settings")]
    bool shouldReset;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        words = wordsText.text;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetNewSecretWord();
        GameManager.OnGameStateChanged += GameStateChangedCallBack;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallBack;
    }

    private void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                
                break;

            case GameState.Game:

                if (shouldReset)
                {
                    SetNewSecretWord();
                }
                break;

            case GameState.LevelComplete:
                shouldReset = true;
                break;

            case GameState.GameOver:
                shouldReset = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetSecretWord()
    {
        return secretWord.ToUpper();
    }

    private void SetNewSecretWord()
    {
        Debug.Log("Word Length is: " + words.Length);

        int wordCount = (words.Length + 2) / 7;

        int wordIndex = Random.Range(0, wordCount);

        int wordStartIndex = wordIndex * 7;

        secretWord = words.Substring(wordStartIndex, 5);

        shouldReset = false;
    }
}
