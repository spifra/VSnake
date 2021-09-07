using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        AdsManager.instance.RequestAndShowBanner();
        slider = GetComponentInChildren<Slider>();
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnHighscores()
    {
        Leaderboard.instance.ShowLeaderboard();
    }

    public void OnCommandsChange()
    {
        if (slider.value == 1)
        {
            GameManager.instance.CommandChange(true);
        }
        else
        {
            GameManager.instance.CommandChange(false);
        }
    }
}
