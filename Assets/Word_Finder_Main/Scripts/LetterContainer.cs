using TMPro;
using UnityEngine;

public class LetterContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshPro letter;
    [SerializeField] private SpriteRenderer letterContainer;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        letter.text = "";
        letterContainer.color = Color.white;

    }

    public void setLetter(char letter)
    {
        this.letter.text = letter.ToString();
        letterContainer.color = Color.white;
    }

    public void SetValid()
    {
        letterContainer.color = Color.green;
    }

    public void SetPotential()
    {
        letterContainer.color = Color.yellow;
    }

    public void SetInvalid()
    {
        letterContainer.color = Color.gray;
    }

    public char GetLetter()
    {
        return letter.text[0];
    }
}
