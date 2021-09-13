using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Snake : MonoBehaviour
{
    public bool isDebug = true;

    [Range(0, 10)]
    public float speed;

    [SerializeField]
    [Header("Speed to add to the Snake every time we add a piece")]
    [Range(0, 10)]
    private float speedPerPiece;

    [SerializeField]
    private bool isMenu = false;

    [SerializeField]
    private Transform piecePrefab;

    public bool alternativeControls = true;

    private List<Transform> pieces = new List<Transform>();

    private GameObject bodyParent;

    private bool isDead = false;

    private void Awake()
    {
        if (GameManager.instance != null)
            GameManager.instance.GetSnakeInfo(this);
        pieces.Add(this.transform);
    }

    private void Update()
    {
        if (isDebug)
        {
            MouseBehaviour();
        }

        Movement();
    }

    private void MouseBehaviour()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (alternativeControls == true)
            {
                if (Mouse.current.position.ReadValue().x < Screen.width / 2)
                {
                    switch (transform.rotation.eulerAngles.z)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0, 0, 90);
                            break;
                        case 90:
                            transform.rotation = Quaternion.Euler(0, 0, 180);
                            break;
                        case 180:
                            transform.rotation = Quaternion.Euler(0, 0, 90);
                            break;
                        case 270:
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            break;
                    }
                }
                else if (Mouse.current.position.ReadValue().x > Screen.width / 2)
                {
                    switch (transform.rotation.eulerAngles.z)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0, 0, -90);
                            break;
                        case 90:
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            break;
                        case 180:
                            transform.rotation = Quaternion.Euler(0, 0, -90);
                            break;
                        case 270:
                            transform.rotation = Quaternion.Euler(0, 0, 180);
                            break;
                    }
                }
            }
            else
            {
                if (Mouse.current.position.ReadValue().x < Screen.width / 2)
                {
                    transform.rotation *= Quaternion.Euler(0, 0, 90);
                }
                else if (Mouse.current.position.ReadValue().x > Screen.width / 2)
                {
                    transform.rotation *= Quaternion.Euler(0, 0, -90);
                }
            }
        }
    }
    public void TouchBehaviour(Vector2 position)
    {
        Debug.Log("qui" + position);
        if (alternativeControls == true)
        {
            if (position.x < Screen.width / 2)
            {
                switch (transform.rotation.eulerAngles.z)
                {
                    case 0:
                        transform.rotation = Quaternion.Euler(0, 0, 90);
                        break;
                    case 90:
                        transform.rotation = Quaternion.Euler(0, 0, 180);
                        break;
                    case 180:
                        transform.rotation = Quaternion.Euler(0, 0, 90);
                        break;
                    case 270:
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                }
            }
            else if (position.x > Screen.width / 2)
            {
                switch (transform.rotation.eulerAngles.z)
                {
                    case 0:
                        transform.rotation = Quaternion.Euler(0, 0, -90);
                        break;
                    case 90:
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case 180:
                        transform.rotation = Quaternion.Euler(0, 0, -90);
                        break;
                    case 270:
                        transform.rotation = Quaternion.Euler(0, 0, 180);
                        break;
                }
            }
        }
        else
        {
            if (position.x < Screen.width / 2)
            {
                transform.rotation *= Quaternion.Euler(0, 0, 90);
            }
            else if (position.x > Screen.width / 2)
            {
                transform.rotation *= Quaternion.Euler(0, 0, -90);
            }
        }
    }

    public void OnPause()
    {
        if (!isMenu)
            GameManager.instance.menu.OnPause();
    }

    //Position
    private void Movement()
    {
        if (!isDead)
        {
            // Move body
            for (int i = pieces.Count - 1; i > 0; i--)
            {
                pieces[i].position = Vector3.Lerp(pieces[i].position, pieces[i - 1].position, speed * Time.deltaTime);
            }

            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    public void AddPieces(int num, bool addScore)
    {
        if (bodyParent == null)
        {
            bodyParent = new GameObject("Body");
            bodyParent.transform.position = new Vector3(0, 0, 0);
        }
        for (int i = 0; i < num; i++)
        {
            Transform piece = Instantiate(piecePrefab, bodyParent.transform);
            piece.position = pieces[pieces.Count - 1].position;
            pieces.Add(piece);
            speed += speedPerPiece;
        }
        if (addScore)
        {
            GameManager.instance.AddScore(100);
        }
    }
    private void GameOver()
    {
        isDead = true;
        StartCoroutine("DestroyBody");
    }

    //Destroy the body and the head
    private IEnumerator DestroyBody()
    {
        if (bodyParent != null)
        {
            for (int i = pieces.Count - 1; i > 0; i--)
            {
                Destroy(pieces[i].gameObject);
                yield return new WaitForSeconds(0.2f);
            }
            Destroy(bodyParent);
        }
        SoundEffectManager.instance.OnGameOver();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            AddPieces(1, true);
        }
        else if (collision.tag == "Walls")
        {
            GameOver();
        }
    }

    private void OnDestroy()
    {
        if (!isMenu)
            GameManager.instance.GameOver();
    }

    public float ChangeSpeed(float multiplier)
    {
        float oldSpeed = speed;
        speed *= multiplier;
        return oldSpeed;
    }
}
