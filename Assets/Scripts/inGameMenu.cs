using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class inGameMenu : MonoBehaviour
{
    [Tooltip("Panel on pause menu")]
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject pausePopup;

    [SerializeField]
    private GameObject gameOverPopup;

    [SerializeField]
    private GameObject ContinueButton;

    [SerializeField]
    private Text totalScore;

    private bool isAlreadyRewarded = false;

    private void Awake()
    {
        GameManager.instance.menu = this;
    }

    public void OnPause()
    {
        Time.timeScale = 0;

        panel.SetActive(true);
        pausePopup.SetActive(true);
    }

    public void OnHome()
    {
        panel.SetActive(true);
        AdsManager.instance.RequestAndShowInterstitialOnHome();
    }

    public void OnResume()
    {
        panel.SetActive(false);
        pausePopup.SetActive(false);

        Time.timeScale = 1;
    }

    public void OnRestart()
    {
        GameManager.instance.Restart();
    }

    public void OnContinue()
    {
        isAlreadyRewarded = true;
        gameOverPopup.SetActive(false);
        panel.SetActive(false);
        AdsManager.instance.RequestedAndShowRewardedAd();
    }

    public void GameOver(int score)
    {
        totalScore.text = score.ToString();

        panel.SetActive(true);
        if (isAlreadyRewarded)
        {
            ContinueButton.SetActive(false);
        }
        gameOverPopup.SetActive(true);
    }


}