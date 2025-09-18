using System;
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

    public bool isComplete()
    {
        return currentLetterContainerIndex >= 5;
    }

}
