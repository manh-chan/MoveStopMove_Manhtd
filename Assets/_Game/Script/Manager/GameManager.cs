using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state;
    public static event Action<GameState> OnStateChanged;
    public TMP_Text coinTxt;
    public GameObject coin;
    private void Awake()
    {
        DataManager.Instance.Init();
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        UpdateGameState(GameState.StartGame);
    }
    private void Update()
    {
        coinTxt.text = DataManager.Instance.CoinData.ToString();
    }
    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.StartGame:
                HandleStartGame();
                break;
            case GameState.PlayGame:
                HandlePlayGame();
                break;
            case GameState.Victory:
                HandleVictory();
                break;
            case GameState.fail:
                HandleFail();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnStateChanged?.Invoke(newState);
    }
    private void HandleStartGame()
    {
        UIManager.Instance.Open<CanvasMainMenu>();
        coinTxt.text = DataManager.Instance.CoinData.ToString();
    }
    private void HandlePlayGame()
    {
        LevelManager.Instance.StartSpawn();
        coin.SetActive(false);
    }
    private void HandleFail()
    {
        UIManager.Instance.Open<CanvasFail>();
        coin.SetActive(true);
    }
    private void HandleVictory()
    {
        UIManager.Instance.Open<CanvasVictory>();
        coin.SetActive(true);
    }
    public enum GameState
    {
        StartGame,
        PlayGame,
        Victory,
        fail,
    }
}
