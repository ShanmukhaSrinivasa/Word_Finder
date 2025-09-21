using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WordContainer : MonoBehaviour
{
    [Header("Elements")]
    private LetterContainer[] letterContainers;

    [Header("Settings")]
    private int currentLetterContainerIndex;

    private void Awake()
    {
        letterContainers = GetComponentsInChildren<LetterContainer>();
        //Initailize();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Initialize()
    {
        currentLetterContainerIndex = 0;

        for (int i = 0; i < letterContainers.Length; i++)
        {
            letterContainers[i].Initialize();
        }
    }

    public void Add(char letter)
    {
        letterContainers[currentLetterContainerIndex].setLetter(letter);
        currentLetterContainerIndex++;
    }

    public void AddAsHint(int letterIndex, char letter)
    {
        letterContainers[letterIndex].setLetter(letter, true);
    }

    public bool RemoveLetter()
    {
        if(currentLetterContainerIndex <= 0)
        {
            return false;
        }

        currentLetterContainerIndex--;
        letterContainers[currentLetterContainerIndex].Initialize();

        return true;
    }

    public string GetWord()
    {
        string word = "";

        for (int i = 0; i < letterContainers.Length; i++)
        {
            word += letterContainers[i].GetLetter().ToString();
        }

        return word;
    }

    public void ObsoleteColorize(string secretWord)
    {
        List<char> chars = new List<char>(secretWord.Length);

        for (int i = 0; i < secretWord.Length; i++)
        {
            chars.Add(secretWord[i]);
        }

        for (int i = 0; i < letterContainers.Length; i++)
        {
            char letter = letterContainers[i].GetLetter();

            if (letter == secretWord[i])
            {
                //Valid
                letterContainers[i].SetValid();
                chars.Remove(letter);
            }
            else if (chars.Contains(letter))
            {
                //potential
                letterContainers[i].SetPotential();
                chars.Remove(letter);
            }
            else
            {
                //Invalid
                letterContainers[i].SetInvalid();
            }
        }
    }

    public void Colorize(string secretWord)
    {
        List<char> chars = new List<char>(secretWord.Length);

        for (int i = 0; i < secretWord.Length; i++)
        {
            chars.Add(secretWord[i]);
        }

        List<int> coveredIndices = new List<int>();


        //1st Pass
        for (int i = 0; i < letterContainers.Length; i++)
        {
            char letterToCheck = letterContainers[i].GetLetter();

            if (letterToCheck == secretWord[i])
            {
                //Valid
                letterContainers[i].SetValid();
                chars.Remove(letterToCheck);
                coveredIndices.Add(i);
            }
        }

        //2nd Pass
        for (int i = 0; i < letterContainers.Length; i++)
        {
            if (coveredIndices.Contains(i))
            {
                continue;
            }

            char letter = letterContainers[i].GetLetter();

            if (chars.Contains(letter))
            {
                //potential
                letterContainers[i].SetPotential();
                chars.Remove(letter);
            }
            else
            {
                //Invalid
                letterContainers[i].SetInvalid();
            }
        }

    }

    public bool isComplete()
    {
        return currentLetterContainerIndex >= 5;
    }

}
