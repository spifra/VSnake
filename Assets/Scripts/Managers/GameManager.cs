using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Snake snake;

    private bool isAlternativeCommand = false;
    
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
}
