using UnityEngine;

public class KeyboardColorizer : MonoBehaviour
{
    [Header("Elements")]
    private KeyboardKey[] keys;

    [Header("Settings")]
    private bool shouldReset;

    private void Awake()
    {
        keys = GetComponentsInChildren<KeyboardKey>();
    }
    void Start()
    {
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

    private void Initialize()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].Initialize();
        }
        shouldReset = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Colorize(string secretWord, string wordToCheck)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            char keyLetter = keys[i].GetLetter();

            for (int j = 0; j < wordToCheck.Length; j++)
            {
                if (keyLetter != wordToCheck[j])
                {
                    continue;
                }

                if (keyLetter == secretWord[j])
                {
                    //valid
                    keys[i].SetValid();
                }
                else if (secretWord.Contains(keyLetter))
                {
                    //potential
                    keys[i].SetPotential();

                }
                else
                {
                    //invalid
                    keys[i].SetInvalid();

                }

            }
        }

    }
}
