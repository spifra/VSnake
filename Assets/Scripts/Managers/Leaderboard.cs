using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class Leaderboard : Singleton<Leaderboard>
{
    private void Start()
    {
        try
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();

            SignIn();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void SignIn()
    {
        Social.localUser.Authenticate(success => { });
    }

    public void AddScoreToLeaderboard(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, "CgkIk7rjk_wGEAIQAA", success => { });
        }
    }

    public void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }
}
