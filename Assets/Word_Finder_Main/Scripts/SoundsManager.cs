using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;

    [Header("Sounds")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource letterAddedSound;
    [SerializeField] private AudioSource letterRemovedSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameOverSound;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        Input_Manager.onLetterAdded += OnLetterAddedCallback;
        Input_Manager.onLetterRemoved += OnLetterRemovedCallback;
        GameManager.OnGameStateChanged += OnGameStateChangedCallback;
    }

    private void OnDestroy()
    {
        Input_Manager.onLetterAdded -= OnLetterAddedCallback;
        Input_Manager.onLetterRemoved -= OnLetterRemovedCallback;
        GameManager.OnGameStateChanged -= OnGameStateChangedCallback;
    }

    private void OnGameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.LevelComplete:
                levelCompleteSound.Play();
                break;

            case GameState.GameOver:
                gameOverSound.Play();
                break;
        }
    }

    private void OnLetterAddedCallback()
    {
        letterAddedSound.Play();
    }

    private void OnLetterRemovedCallback()
    {
        letterRemovedSound.Play();
    }


    void Update()
    {
        
    }

    public void PlayButtonSound()
    {
        buttonSound.Play();
    }

    public void EnableSounds()
    {
        buttonSound.volume = 1;
        letterAddedSound.volume = 1;
        letterRemovedSound.volume = 1;
        levelCompleteSound.volume = 1;
        gameOverSound.volume = 1;
    }

    public void DisableSounds()
    {
        buttonSound.volume = 0;
        letterAddedSound.volume = 0;
        letterRemovedSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameOverSound.volume = 0;
    }
}
