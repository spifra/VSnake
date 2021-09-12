using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField]
    [Range(1, 5)]
    private int secondsToWait = 5;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(secondsToWait);
        SceneManager.LoadScene("Menu");
    }
}
