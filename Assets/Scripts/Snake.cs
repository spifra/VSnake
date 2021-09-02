using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    public bool isDebug = true;

    [SerializeField]
    [Range(0, 5)]
    private float speed;

    [SerializeField]
    private Transform piecePrefab;

    private List<Transform> pieces = new List<Transform>();

    private void Start()
    {
        pieces.Add(this.transform);
    }

    private void Update()
    {
        if (isDebug)
        {
            MouseBehaviour();
        }
        else
        {
            TouchBehaviour();
        }

        Movement();
    }


    private void MouseBehaviour()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
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

    //Rotation
    private void TouchBehaviour()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).position.x < Screen.width / 2)
            {
                transform.rotation *= Quaternion.Euler(0, 0, 90);
            }
            else if (Input.GetTouch(0).position.x > Screen.width / 2)
            {
                transform.rotation *= Quaternion.Euler(0, 0, -90);
            }
        }
    }
    //Position
    private void Movement()
    {
        // Move body
        for (int i = pieces.Count - 1; i > 0; i--)
        {
            pieces[i].position = Vector3.Lerp(pieces[i].position, pieces[i - 1].position, 0.05f);
        }

        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void AddPiece()
    {
        Transform piece = Instantiate(piecePrefab);
        piece.position = pieces[pieces.Count - 1].position;

        pieces.Add(piece);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            AddPiece();
        }
    }
}
