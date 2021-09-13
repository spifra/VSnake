using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Item
{
    [SerializeField]
    [Header("Minimum seconds to spawn the powerup")]
    private int minSeconds;

    [SerializeField]
    [Header("Maximum seconds to spawn the powerup")]
    private int maxSeconds;

    private float oldSpeed;

    private void Start()
    {
        StartCoroutine(Positioning(Random.Range(minSeconds, maxSeconds + 1)));
    }

    private IEnumerator Timer()
    {
        ChoosePowerUp();

        ChangeStatus(false);
        int seconds = Random.Range(minSeconds, maxSeconds + 1);
        yield return new WaitForSeconds(seconds);
        ChangeStatus(true);

        ResetPowerUp();
        transform.position = RandomPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine("Timer");
        }
    }

    private void ChangeStatus(bool isVisible)
    {
        GetComponent<SpriteRenderer>().enabled = isVisible;
        GetComponent<BoxCollider2D>().enabled = isVisible;
    }

    private void ChoosePowerUp()
    {
        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0:
                GameManager.instance.ScoreMultiplier(2);
                GameManager.instance.menu.OnPowerUp("X2", Color.green, true);
                break;
            case 1:
                GameManager.instance.ScoreMultiplier(3);
                GameManager.instance.menu.OnPowerUp("X3", Color.blue, true);
                break;
            case 2:
                float f = Random.Range(0.8f, 1.5f);
                oldSpeed = GameManager.instance.snake.ChangeSpeed(i);
                GameManager.instance.menu.OnPowerUp("Speed \r\n Change", Color.yellow, f <= 1 ? true : false);
                break;
        }
    }

    private void ResetPowerUp()
    {
        GameManager.instance.ScoreMultiplier(1);
        if (oldSpeed != 0)
            GameManager.instance.snake.speed = oldSpeed;
    }

    private IEnumerator Positioning(float seconds)
    {
        ChangeStatus(false);
        yield return new WaitForSeconds(seconds);
        ChangeStatus(true);
        transform.position = RandomPosition();
    }

}
