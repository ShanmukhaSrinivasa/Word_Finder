using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager Instance;

    [Header("Elements")]
    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private KeyboardColorizer KeyboardColorizer;

    [Header("Settings")]
    private int currentWordContainerIndex;
    private bool canAddLetters = true;
    private bool shouldReset;

    [Header("Events")]
    public static Action onLetterAdded;
    public static Action onLetterRemoved;

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

    void Start()
    {
        Initialize();

        KeyboardKey.onKeyPressed += KeyPressedCallBack;
        GameManager.OnGameStateChanged += GameStateChangedCallBack;
    }

    private void OnDestroy()
    {
        KeyboardKey.onKeyPressed -= KeyPressedCallBack;
        GameManager.OnGameStateChanged -= GameStateChangedCallBack;
    }

    private void GameStateChangedCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Game:
                if (shouldReset)
                {
                    Initialize();
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

    void Update()
    {
        
    }

    private void Initialize()
    {
        currentWordContainerIndex = 0;
        canAddLetters = true;

        DisableTryButton();

        for (int i = 0; i < wordContainers.Length; i++)
        {
            wordContainers[i].Initialize();
        }

        shouldReset = false;
    }
    private void KeyPressedCallBack(char letter)
    {
        if (!canAddLetters)
        {
            return;
        }

        wordContainers[currentWordContainerIndex].Add(letter);

        if (wordContainers[currentWordContainerIndex].isComplete())
        {
            canAddLetters = false;
            EnableTryButton();
            
        }

        onLetterAdded?.Invoke();
    }

    public void CheckWord()
    {
        string WordToCheck = wordContainers[currentWordContainerIndex].GetWord();
        string secretWord = WordManager.instance.GetSecretWord();

        wordContainers[currentWordContainerIndex].Colorize(secretWord);
        KeyboardColorizer.Colorize(secretWord, WordToCheck);

        if (secretWord == WordToCheck)
        {
            SetLevelComplete();
            
        }
        else
        {
            Debug.Log("Wrong Word");
            currentWordContainerIndex++;
            DisableTryButton();

            if (currentWordContainerIndex >= wordContainers.Length)
            {
                DataManager.instance.ResetScore();
                GameManager.Instance.SetGameState(GameState.GameOver);
            }
            else
            {
                canAddLetters = true;
            }

        }
    }

    private void SetLevelComplete()
    {
        UpdateData();

        GameManager.Instance.SetGameState(GameState.LevelComplete);
    }

    private void UpdateData()
    {
        int scoreToAdd = (6 - currentWordContainerIndex) * 5;

        DataManager.instance.IncreaseScore(scoreToAdd);
        DataManager.instance.AddCoins(scoreToAdd);
    }

    public void BackspaceKeyCallback()
    {
        if (!GameManager.Instance.IsGameState())
        {
            return;
        }

        bool removedLetter = wordContainers[currentWordContainerIndex].RemoveLetter();

        if (removedLetter)
        {
            DisableTryButton();
        }

        canAddLetters = true;

        onLetterRemoved?.Invoke();
    }

    private void EnableTryButton()
    {
        tryButton.interactable = true;
    }

    private void DisableTryButton()
    {
        tryButton.interactable = false;
    }

    public WordContainer GetCurrentWordContainer()
    {
        return wordContainers[currentWordContainerIndex];
    }
}
