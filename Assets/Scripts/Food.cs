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

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
        Debug.Log(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RandomPosition();
        }
    }
}
