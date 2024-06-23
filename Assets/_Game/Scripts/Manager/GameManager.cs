using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, GamePlay, Pause}

public class GameManager : Singleton<GameManager>
{
    private GameState gameState;

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    //public void ChangeState(GameState gameState)
    //{
    //    this.gameState = gameState;
    //}

    public void ChangeState(GameState newState)
    {
        if (gameState == newState)
        {
            return;
        }

        gameState = newState;

        Debug.Log($"GameState hien tai: {gameState}");

        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1;
                UIManager.Instance.OpenUI<MainMenu>();
                break;

            case GameState.GamePlay:
                Time.timeScale = 1;
                break;

            case GameState.Pause:
                Time.timeScale = 0;
                break;
        }
    }

    public bool IsState(GameState gameState)
    {
        return this.gameState == gameState;
    }
}
