using System;
using UnityEngine;

public class AdManager : MonoBehaviourSingleton<AdManager>, IInitable
{
    public bool debugNoReward = false;
    public bool debugNoInter = false;

    IAdService _adService;

    [HideInInspector]
    public bool _isInited { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        ((IInitable)this).Init();
        ((IInitable)this).MarkInitEnd();
    }

    public void Init()
    {
        // _adService = new MaxAdService();
        // _adService.Init();
    }

    //reward
    public bool ShowRewardAd(string adName, Action<bool> callback = null)
    {
        Tools.Log("ShowRewardAd adName:" + adName);
        return _adService.ShowRewardAd(adName, callback);
    }

    public bool IsRewardAdLoaded()
    {
        return _adService.IsRewardAdLoaded();
    }
    
    //inter
    public bool ShowInterAd(string adName, Action<bool> callback = null)
    {
        Tools.Log("ShowInterAd adName:" + adName);
        return _adService.ShowInterAd(adName, callback);
    }

    public bool IsInterAdLoaded()
    {
        return _adService.IsInterAdLoaded();
    }

    //banner
    public bool ShowBannerAd(string adName)
    {
        Tools.Log("ShowBannerAd adName:" + adName);
        return _adService.ShowBannerAd(adName);
    }

    public void HideBannerAd(string adName)
    {
        Tools.Log("HideBannerAd adName:" + adName);
        _adService.HideBannerAd(adName);
    }
    
    public float GetBannerAdHeight()
    {
        return _adService.GetBannerAdHeight();
    }

}
