using System;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum Validity {None, Valid, Potential, Invalid}

public class KeyboardKey : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image keybg;
    [SerializeField] private TextMeshProUGUI letterText;

    [Header("events")]
    public static Action<char> onKeyPressed;

    [Header("Settings")]
    private Validity validity;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SendKeyPressedEvent);
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SendKeyPressedEvent()
    {
        onKeyPressed?.Invoke(letterText.text[0]);
    }

    public void Initialize()
    {
        keybg.color = Color.white;
        validity = Validity.None;
    }

    public char GetLetter()
    {
        return letterText.text[0];
    }

    public void SetValid()
    {
        keybg.color = Color.green;
        validity = Validity.Valid;
    }

    public void SetPotential()
    {
        if (validity == Validity.Valid)
        {
            return;
        }

        keybg.color = Color.yellow;
        validity = Validity.Potential;
    }

    public void SetInvalid()
    {
        if (validity == Validity.Valid || validity == Validity.Potential)
        {
            return;
        }

        keybg.color = Color.gray;
        validity = Validity.Invalid;
    }
}
