using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject keyboard;
    private KeyboardKey[] keys;


    [Header("Text Elements")]
    [SerializeField] private TextMeshProUGUI keyboardHintPriceText;
    [SerializeField] private TextMeshProUGUI letterHintPriceText;

    [Header("settings")]
    [SerializeField] private int keyboardHintPrice;
    [SerializeField] private int letterHintPrice;
    private bool shouldReset;

    private void Awake()
    {
        keys = keyboard.GetComponentsInChildren<KeyboardKey>();
    }

    void Start()
    {
        keyboardHintPriceText.text = keyboardHintPrice.ToString();
        letterHintPriceText.text = letterHintPrice.ToString();

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
        if (DataManager.instance.GetCoins() < keyboardHintPrice)
        {
            return;
        }

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

        DataManager.instance.RemoveCoins(keyboardHintPrice);
    }

    List<int> letterHintGivenIndices = new List<int>();
    public void LetterHint()
    {
        if (DataManager.instance.GetCoins() < letterHintPrice)
        {
            return;
        }

        if (letterHintGivenIndices.Count >= 5)
        {
            return;
        }

        WordContainer currentWordContainer = Input_Manager.Instance.GetCurrentWordContainer();
        WordContainer previousWordContainer = Input_Manager.Instance.GetPreviousWordContainer();
        string secretWord = WordManager.instance.GetSecretWord();
        string playerWord = previousWordContainer.GetWord();

        List<int> possibleHintIndices = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            if (!letterHintGivenIndices.Contains(i) && playerWord[i] != secretWord[i])
            {
                possibleHintIndices.Add(i);
            }
        }

        if (possibleHintIndices.Count == 0)
        {
            return;
        }

        int randomKeyIndex = possibleHintIndices[Random.Range(0, possibleHintIndices.Count)];
        letterHintGivenIndices.Add(randomKeyIndex);

        currentWordContainer.AddAsHint(randomKeyIndex, secretWord[randomKeyIndex]);

        DataManager.instance.RemoveCoins(letterHintPrice);
    }
}
