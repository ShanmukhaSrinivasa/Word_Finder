using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Input_Manager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private KeyboardColorizer KeyboardColorizer;

    [Header("Settings")]
    private int currentWordContainerIndex;
    private bool canAddLetters = true;

    void Start()
    {
        Initialize();

        KeyboardKey.onKeyPressed += KeyPressedCallBack;
    }


    void Update()
    {
        
    }

    private void Initialize()
    {
        for (int i = 0; i < wordContainers.Length; i++)
        {
            wordContainers[i].Initialize();
        }
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
            //CheckWord();
            //currentWordContainerIndex++;
        }
    }

    public void CheckWord()
    {
        string WordToCheck = wordContainers[currentWordContainerIndex].GetWord();
        string secretWord = WordManager.instance.GetSecretWord();

        wordContainers[currentWordContainerIndex].Colorize(secretWord);
        KeyboardColorizer.Colorize(secretWord, WordToCheck);

        if (secretWord == WordToCheck)
        {
            Debug.Log("Correct Word");
        }
        else
        {
            Debug.Log("Wrong Word");
            canAddLetters = true;
            DisableTryButton();
            currentWordContainerIndex++;
        }
    }

    public void BackspaceKeyCallback()
    {
        bool removedLetter = wordContainers[currentWordContainerIndex].RemoveLetter();
        canAddLetters = true;

        if (removedLetter)
        {
            DisableTryButton();
        }
    }

    private void EnableTryButton()
    {
        tryButton.interactable = true;
    }

    private void DisableTryButton()
    {
        tryButton.interactable = false;
    }
}
