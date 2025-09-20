using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject keyboard;
    private KeyboardKey[] keys;

    [Header("settings")]
    private bool shouldReset;

    private void Awake()
    {
        keys = keyboard.GetComponentsInChildren<KeyboardKey>();
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
            case GameState.Menu:
                
                break;

            case GameState.Game:
                if (shouldReset)
                {
                    letterHintGivenIndices.Clear();
                    shouldReset = false;
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

    public void KeyboardHint()
    {
        string secretWord = WordManager.instance.GetSecretWord();

        List<KeyboardKey> untouchedKeys = new List<KeyboardKey>();

        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].IsUntouched())
            {
                untouchedKeys.Add(keys[i]);
            }
        }

        //At this point we have a list of all the untouched keys
        //now let's remove the ones that are in the secret word

        List<KeyboardKey> temp_untouchedKeys = new List<KeyboardKey>(untouchedKeys);

        for (int i = 0; i < untouchedKeys.Count; i++)
        {
            if (secretWord.Contains(untouchedKeys[i].GetLetter()))
            {
                temp_untouchedKeys.Remove(untouchedKeys[i]);
            }
        }

        //At this point we have all the Untouched keys without containing the secret word

        if (temp_untouchedKeys.Count <= 0)
        {
            return;
        }

        int randomKeyIndex = Random.Range(0, temp_untouchedKeys.Count);
        temp_untouchedKeys[randomKeyIndex].SetInvalid();
    }

    List<int> letterHintGivenIndices = new List<int>();
    public void LetterHint()
    {
        if (letterHintGivenIndices.Count >= 5)
        {
            return;
        }

        List<int> letterHintNotGivenIndices = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            if (!letterHintGivenIndices.Contains(i))
            {
                letterHintNotGivenIndices.Add(i);
            }
        }

        WordContainer currentWordContainer = Input_Manager.Instance.GetCurrentWordContainer();

        string secretWord = WordManager.instance.GetSecretWord();

        int randomKeyIndex = letterHintNotGivenIndices[Random.Range(0, letterHintNotGivenIndices.Count)];
        letterHintGivenIndices.Add(randomKeyIndex);

        currentWordContainer.AddAsHint(randomKeyIndex, secretWord[randomKeyIndex]);
    }
}
