using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdsManager : Singleton<AdsManager>
{

    [HideInInspector]
    public BannerView bannerView;
    public InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    private string appId = "ca-app-pub-2811790606724562~2772193607";

    //This are the real one:
    //private string TESTBannerID = "ca-app-pub-2811790606724562/4841961358";
    //private string TESTInterstitialID = "ca-app-pub-2811790606724562/9146030269";
    //private string TESTRewardedID = "ca-app-pub-2811790606724562/4300897693";

    private string bannerId = "ca-app-pub-3940256099942544/6300978111";
    private string interstitialID = "ca-app-pub-3940256099942544/1033173712";
    private string rewardedID = "ca-app-pub-3940256099942544/5224354917";

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    public void RequestAndShowBanner()
    {

        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a banner at the top of the screen.      
        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Top);

        bannerView.OnAdLoaded += HandleOnAdLoaded;
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        AdRequest request = new AdRequest.Builder().Build();

        this.bannerView.LoadAd(request);
    }

    #region INTERSTITIAL
    /// <summary>
    /// Load Interstitial
    /// </summary>
    public void RequestInterstitial()
    {

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(interstitialID);

        this.interstitial.OnAdLoaded += ShowIntestitial;
        this.interstitial.OnAdClosed += DestroyIntestitial;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    internal void ShowIntestitial(object sender, EventArgs e)
    {
        this.interstitial.Show();
    }

    internal void DestroyIntestitial(object sender, EventArgs e)
    {
        this.interstitial.Destroy();
    }

    #endregion

    #region REWARDED AD

    public void RequesteRewardedAd()
    {

        this.rewardedAd = new RewardedAd(rewardedID);
        
        this.rewardedAd.OnUserEarnedReward += GameManager.instance.RespawnPlayer;

        this.rewardedAd.OnAdLoaded += UserChoseToWatchAd;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    internal void UserChoseToWatchAd(object sender, EventArgs e)
    {
        this.rewardedAd.Show();
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    #endregion

    #region EVENTS
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    #endregion

    /// <summary>
    /// Not used
    /// First ad and banner in the main menu
    /// </summary>
    public void RequestAndShowFirstInterstitialAndBanner()
    {
        RequestInterstitial();
        this.interstitial.OnAdClosed += CallBanner;

    }

    private void CallBanner(object sender, EventArgs e)
    {
        RequestAndShowBanner();
    }


    public void DestroyInterstitial()
    {

        if (this.interstitial != null)
            this.interstitial.Destroy();
    }

    public void RequestedAndShowRewardedAd()
    {
        RequesteRewardedAd();
    }

    public void RequestAndShowInterstitialOnHome()
    {
        RequestInterstitial();

        interstitial.OnAdClosed += GameManager.instance.GoToHome;
    }

    public void RequestAndShowInterstitialOnRestart()
    {
        RequestInterstitial();
        //TODO Restart match
        //interstitial.OnAdClosed += LevelManager.Instance.Restart;
    }

}
