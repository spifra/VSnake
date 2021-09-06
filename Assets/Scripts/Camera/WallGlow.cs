using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WallGlow : MonoBehaviour
{
    [SerializeField]
    [Range(0, 2)]
    private float maxIntensity;

    [SerializeField]
    private float glowTime;

    private Light2D light;

    private void Awake()
    {
        light = GetComponent<Light2D>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            light.intensity = Random.Range(1, maxIntensity);
            light.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            yield return new WaitForSeconds(glowTime);
        }
    }
}
