using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdScripts : MonoBehaviour
{
    private InterstitialAd interstitial;

    #if DEBUG 
        private string interstitialAdUnity = "ca-app-pub-3940256099942544/1033173712";
        private string appId = "ca-app-pub-4309490894892168~6235420537";
    #else
        #if UNITY_ANDROID
            private string interstitialAdUnity = "ca-app-pub-4309490894892168/5930112867";
            private string appId = "ca-app-pub-4309490894892168~2142501970";
        #elif UNITY_IOS
            private string interstitialAdUnity = "ca-app-pub-4309490894892168/2050910749";
            private string appId = "ca-app-pub-4309490894892168~6235420537";
        #else
            private string interstitialAdUnity = "unexpected_platform";
            private string appId = "unexpected_platform";
        #endif
    #endif
    

    public void Start() {
        MobileAds.Initialize(appId);
        RequestInterstitial();
    }

    public void RequestInterstitial() {
        interstitial = new InterstitialAd(interstitialAdUnity);
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);

        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        interstitial.OnAdClosed += HandleOnAdClosed;
    }

    public void ShowInterstitial() {
        if (!interstitial.IsLoaded()) return;
        interstitial.Show();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        Invoke("RequestInterstitial", 15);
    }

    public void HandleOnAdClosed(object sender, EventArgs args) {
        RequestInterstitial();
    }
}
