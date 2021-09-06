using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer field;

    private void Start()
    {
        RandomPosition();
    }

    private void RandomPosition()
    {
        Bounds bounds = field.bounds;

        float x = Random.Range(bounds.min.x + 0.5f, bounds.max.x - 0.5f);
        float y = Random.Range(bounds.min.y + 0.5f, bounds.max.y - 0.5f);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RandomPosition();
        }
    }
}
