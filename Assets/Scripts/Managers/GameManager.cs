using GoogleMobileAds.Api;
using System;
using System.Collections;
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

    private int bodyPieces = 0;

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
        bodyPieces = 0;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        score = 0;
        bodyPieces = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
    }

    public void GameOver(int pieces)
    {
        bodyPieces = pieces;
        menu.GameOver(score);
    }

    private IEnumerator IRespawnPlayer()
    {
        menu.OnResume();
        menu.SetGetReady(true);
        yield return new WaitForSeconds(2f);
        menu.SetGetReady(false);
        snake = Instantiate(snakePrefab, new Vector2(0, 0), Quaternion.identity).GetComponent<Snake>();
        if (score != 0)
        {
            snake.AddPieces(bodyPieces, false);
        }
    }

    public void ScoreMultiplier(int multiplier)
    {
        scoreMultiplier = multiplier;
    }

    public void GiveSnakeDirection(Vector2 direction)
    {
        if (snake != null)
        {
            if (snake.isDebug)
                snake.MouseBehaviour();
            else
                snake.TouchBehaviour(direction);
        }
    }
    internal void RespawnPlayer(object sender, EventArgs e)
    {
        StartCoroutine("IRespawnPlayer");
    }
}
