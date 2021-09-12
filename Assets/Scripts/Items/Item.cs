using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer field;

    private Bounds bounds;

    private void Awake()
    {
        bounds = field.bounds;
    }

    protected virtual Vector3 RandomPosition()
    {

        float x = Random.Range(bounds.min.x + 1, bounds.max.x - 1);
        float y = Random.Range(bounds.min.y + 1, bounds.max.y - 1);

        return new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
    }
}
