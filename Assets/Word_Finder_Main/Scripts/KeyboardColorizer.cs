using UnityEngine;

public class KeyboardColorizer : MonoBehaviour
{
    [Header("Elements")]
    private KeyboardKey[] keys;

    private void Awake()
    {
        keys = GetComponentsInChildren<KeyboardKey>();
    }
    void Start()
    {
        
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
