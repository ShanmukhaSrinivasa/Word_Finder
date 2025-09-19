using System;
using UnityEngine;

public enum GameState { Menu, Game, LevelComplete, GameOver, Idle }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameState gameState;

    [Header("Elements")]
    public static Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        OnGameStateChanged?.Invoke(gameState);
    }

    public void NextButtonCallBack()
    {
        SetGameState(GameState.Game);

    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
