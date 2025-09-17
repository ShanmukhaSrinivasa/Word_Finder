using System;
using UnityEngine;

public class KeyboardKeyTest : MonoBehaviour
{
    
    void Start()
    {
        KeyboardKey.onKeyPressed += DebugLetter;    
    }

    private void DebugLetter(char letter)
    {
        Debug.Log(letter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
