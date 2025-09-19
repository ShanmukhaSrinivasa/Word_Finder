using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager instance;

    [Header("Elements")]
    [SerializeField] public string secretWord;

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

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetSecretWord()
    {
        return secretWord.ToUpper();
    }

    
}
