using UnityEngine;

public class Food : Item
{

    private void Start()
    {
        transform.position = RandomPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.position = RandomPosition();
        }
    }
}
