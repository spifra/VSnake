using GoogleMobileAds.Api;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int score = 0;

    [HideInInspector]
    public inGameMenu menu;

    [SerializeField]
    private GameObject snakePrefab;

    [HideInInspector]
    public Snake snake;

    private bool isAlternativeCommand = false;

    private int scoreMultiplier = 1;

    public void GetSnakeInfo(Snake snake)
    {
        this.snake = snake;
        CommandChange(isAlternativeCommand);
    }

    public void CommandChange(bool value)
    {
        isAlternativeCommand = value;
        snake.alternativeControls = value;
    }

    public int AddScore(int points)
    {
        score += points * scoreMultiplier;
        SoundEffectManager.instance.OnFood();
        menu.OnScore(score);
        return score;
    }

    internal void GoToHome(object sender, EventArgs e)
    {
        if (score != 0)
        {
            Leaderboard.instance.AddScoreToLeaderboard(score);
        }
        SceneManager.LoadScene("Menu");

        score = 0;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        score = 0;
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        menu.GameOver(score);
    }

    internal void RespawnPlayer(object sender, Reward e)
    {
        Instantiate(snakePrefab, new Vector2(0, 0), Quaternion.identity);
        if (score != 0)
        {
            int num = score / 100;
            snake.AddPieces(num, false);
        }
    }

    public void ScoreMultiplier(int multiplier)
    {
        scoreMultiplier = multiplier;
    }
}
