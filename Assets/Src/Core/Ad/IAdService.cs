using System;

public interface IAdService
{
    bool _isInit { get; set; }
    public void Init();

    public bool ShowRewardAd(string adName, Action<bool> callback = null);
    public void LoadRewardAd();
    public bool IsRewardAdLoaded();


    public bool ShowInterAd(string adName, Action<bool> callback = null);
    public void LoadInterAd();
    public bool IsInterAdLoaded();


    public bool ShowBannerAd(string adName);
    public void HideBannerAd(string adName);
    public void LoadBannerAd();
    public bool IsBannerAdLoaded();
    public float GetBannerAdHeight();



    public bool IsInited()
    {
        return _isInit;
    }
}
