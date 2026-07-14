public class AdConfig
{
    public static string AppKey => GetAppKey();
    public static string BannerAdUnitId => GetBannerAdUnitId();
    public static string InterstitalAdUnitId => GetInterstitialAdUnitId();
    public static string RewardedVideoAdUnitId => GetRewardedVideoAdUnitId();
    static string GetAppKey()
    {
#if UNITY_ANDROID
        return "233261f05";
#elif UNITY_IPHONE
            return "8545d445";
#else
            return "unexpected_platform";
#endif
    }
    static string GetBannerAdUnitId()
    {
#if UNITY_ANDROID
        return "rpb4l7fiypylbgt1";
#elif UNITY_IPHONE
            return "iep3rxsyp9na3rw8";
#else
            return "unexpected_platform";
#endif
    }
    static string GetInterstitialAdUnitId()
    {
#if UNITY_ANDROID
        return "zvq56o9uk9hm64go";
#elif UNITY_IPHONE
            return "wmgt0712uuux8ju4";
#else
            return "unexpected_platform";
#endif
    }
    static string GetRewardedVideoAdUnitId()
    {
#if UNITY_ANDROID
        return "1w9fbrf9bzu67jqd";
#elif UNITY_IPHONE
            return "qwouvdrkuwivay5q";
#else
            return "unexpected_platform";
#endif
    }
}
