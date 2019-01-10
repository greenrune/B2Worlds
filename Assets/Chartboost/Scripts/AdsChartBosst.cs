using ChartboostSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsChartBosst : MonoBehaviour {

    private CBInPlay inPlayAd;

    void Start()
    {
        Chartboost.setAutoCacheAds(true);
        Chartboost.isInitialized();
    }
    void OnEnable()
    {
        // Listen to all impression-related events
        Chartboost.didFailToLoadInterstitial += didFailToLoadInterstitial;
        Chartboost.didDismissInterstitial += didDismissInterstitial;
        Chartboost.didCloseInterstitial += didCloseInterstitial;
        Chartboost.didClickInterstitial += didClickInterstitial;
        Chartboost.didCacheInterstitial += didCacheInterstitial;
        Chartboost.shouldDisplayInterstitial += shouldDisplayInterstitial;
        Chartboost.didDisplayInterstitial += didDisplayInterstitial;
        Chartboost.didFailToLoadMoreApps += didFailToLoadMoreApps;
        Chartboost.didDismissMoreApps += didDismissMoreApps;
        Chartboost.didCloseMoreApps += didCloseMoreApps;
        Chartboost.didClickMoreApps += didClickMoreApps;
        Chartboost.didCacheMoreApps += didCacheMoreApps;
        Chartboost.shouldDisplayMoreApps += shouldDisplayMoreApps;
        Chartboost.didDisplayMoreApps += didDisplayMoreApps;
        Chartboost.didFailToLoadRewardedVideo += didFailToLoadRewardedVideo;
        Chartboost.didDismissRewardedVideo += didDismissRewardedVideo;
        Chartboost.didCloseRewardedVideo += didCloseRewardedVideo;
        Chartboost.didClickRewardedVideo += didClickRewardedVideo;
        Chartboost.didCacheRewardedVideo += didCacheRewardedVideo;
        Chartboost.shouldDisplayRewardedVideo += shouldDisplayRewardedVideo;
        Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
        Chartboost.didDisplayRewardedVideo += didDisplayRewardedVideo;
        Chartboost.didCacheInPlay += didCacheInPlay;
        Chartboost.didFailToLoadInPlay += didFailToLoadInPlay;
        Chartboost.didPauseClickForConfirmation += didPauseClickForConfirmation;
        Chartboost.willDisplayVideo += willDisplayVideo;
#if UNITY_IPHONE
		Chartboost.didCompleteAppStoreSheetFlow += didCompleteAppStoreSheetFlow;
#endif
    }

    public void ShowAds()
    {
        Chartboost.showRewardedVideo(CBLocation.Default);
    }

    void OnDisable()
    {
        // Remove event handlers
        Chartboost.didFailToLoadInterstitial -= didFailToLoadInterstitial;
        Chartboost.didDismissInterstitial -= didDismissInterstitial;
        Chartboost.didCloseInterstitial -= didCloseInterstitial;
        Chartboost.didClickInterstitial -= didClickInterstitial;
        Chartboost.didCacheInterstitial -= didCacheInterstitial;
        Chartboost.shouldDisplayInterstitial -= shouldDisplayInterstitial;
        Chartboost.didDisplayInterstitial -= didDisplayInterstitial;
        Chartboost.didFailToLoadMoreApps -= didFailToLoadMoreApps;
        Chartboost.didDismissMoreApps -= didDismissMoreApps;
        Chartboost.didCloseMoreApps -= didCloseMoreApps;
        Chartboost.didClickMoreApps -= didClickMoreApps;
        Chartboost.didCacheMoreApps -= didCacheMoreApps;
        Chartboost.shouldDisplayMoreApps -= shouldDisplayMoreApps;
        Chartboost.didDisplayMoreApps -= didDisplayMoreApps;
        Chartboost.didFailToLoadRewardedVideo -= didFailToLoadRewardedVideo;
        Chartboost.didDismissRewardedVideo -= didDismissRewardedVideo;
        Chartboost.didCloseRewardedVideo -= didCloseRewardedVideo;
        Chartboost.didClickRewardedVideo -= didClickRewardedVideo;
        Chartboost.didCacheRewardedVideo -= didCacheRewardedVideo;
        Chartboost.shouldDisplayRewardedVideo -= shouldDisplayRewardedVideo;
        Chartboost.didCompleteRewardedVideo -= didCompleteRewardedVideo;
        Chartboost.didDisplayRewardedVideo -= didDisplayRewardedVideo;
        Chartboost.didCacheInPlay -= didCacheInPlay;
        Chartboost.didFailToLoadInPlay -= didFailToLoadInPlay;
        Chartboost.didPauseClickForConfirmation -= didPauseClickForConfirmation;
        Chartboost.willDisplayVideo -= willDisplayVideo;
#if UNITY_IPHONE
		Chartboost.didCompleteAppStoreSheetFlow -= didCompleteAppStoreSheetFlow;
#endif
    }



    void didFailToLoadInterstitial(CBLocation location, CBImpressionError error)
    {
        Debug.Log(string.Format("didFailToLoadInterstitial: {0} at location {1}", error, location));
    }

    void didDismissInterstitial(CBLocation location)
    {
        Debug.Log("didDismissInterstitial: " + location);
    }

    void didCloseInterstitial(CBLocation location)
    {
        Debug.Log("didCloseInterstitial: " + location);
    }

    void didClickInterstitial(CBLocation location)
    {
        Debug.Log("didClickInterstitial: " + location);
    }

    void didCacheInterstitial(CBLocation location)
    {
        Debug.Log("didCacheInterstitial: " + location);
    }

    bool shouldDisplayInterstitial(CBLocation location)
    {
        Debug.Log("shouldDisplayInterstitial: " + location);
        return true;
    }

    void didDisplayInterstitial(CBLocation location)
    {
        Debug.Log("didDisplayInterstitial: " + location);
    }

    void didFailToLoadMoreApps(CBLocation location, CBImpressionError error)
    {
        Debug.Log(string.Format("didFailToLoadMoreApps: {0} at location: {1}", error, location));
    }

    void didDismissMoreApps(CBLocation location)
    {
        Debug.Log(string.Format("didDismissMoreApps at location: {0}", location));
    }

    void didCloseMoreApps(CBLocation location)
    {
        Debug.Log(string.Format("didCloseMoreApps at location: {0}", location));
    }

    void didClickMoreApps(CBLocation location)
    {
        Debug.Log(string.Format("didClickMoreApps at location: {0}", location));
    }

    void didCacheMoreApps(CBLocation location)
    {
        Debug.Log(string.Format("didCacheMoreApps at location: {0}", location));
    }

    bool shouldDisplayMoreApps(CBLocation location)
    {
        Debug.Log(string.Format("shouldDisplayMoreApps at location: {0}", location));
        return true;
    }

    void didDisplayMoreApps(CBLocation location)
    {
        Debug.Log("didDisplayMoreApps: " + location);
    }

    void didFailToRecordClick(CBLocation location, CBImpressionError error)
    {
        Debug.Log(string.Format("didFailToRecordClick: {0} at location: {1}", error, location));
    }

    void didFailToLoadRewardedVideo(CBLocation location, CBImpressionError error)
    {
        Debug.Log(string.Format("didFailToLoadRewardedVideo: {0} at location {1}", error, location));
    }

    void didDismissRewardedVideo(CBLocation location)
    {
        Debug.Log("didDismissRewardedVideo: " + location);
    }

    void didCloseRewardedVideo(CBLocation location)
    {
        Debug.Log("didCloseRewardedVideo: " + location);
    }

    void didClickRewardedVideo(CBLocation location)
    {
        Debug.Log("didClickRewardedVideo: " + location);
    }

    void didCacheRewardedVideo(CBLocation location)
    {
        Debug.Log("didCacheRewardedVideo: " + location);
    }

    bool shouldDisplayRewardedVideo(CBLocation location)
    {
        Debug.Log("shouldDisplayRewardedVideo: " + location);
        return true;
    }

    void didCompleteRewardedVideo(CBLocation location, int reward)
    {
        Debug.Log(string.Format("didCompleteRewardedVideo: reward {0} at location {1}", reward, location));
    }

    void didDisplayRewardedVideo(CBLocation location)
    {
        Debug.Log("didDisplayRewardedVideo: " + location);
    }

    void didCacheInPlay(CBLocation location)
    {
        Debug.Log("didCacheInPlay called: " + location);
    }

    void didFailToLoadInPlay(CBLocation location, CBImpressionError error)
    {
        Debug.Log(string.Format("didFailToLoadInPlay: {0} at location: {1}", error, location));
    }

    void didPauseClickForConfirmation()
    {
        Debug.Log("didPauseClickForConfirmation called");
    }

    void willDisplayVideo(CBLocation location)
    {
        Debug.Log("willDisplayVideo: " + location);
    }

#if UNITY_IPHONE
#elif UNITY_ANDROID
    void TrackIAP()
    {
        Debug.Log("TrackIAP");
        // title, description, price, currency, productID, purchaseData, purchaseSignature
        // This data should be sent after handling the results from the google store.
        // This is fake data and doesn't represent a real or even imaginary purchase
        Chartboost.trackInAppGooglePlayPurchaseEvent("SampleItem", "TestPurchase", "0.99", "USD", "ProductID", "PurchaseData", "PurchaseSignature");

        // If you are using the Amazon store...		
        //Chartboost.trackInAppAmazonStorePurchaseEvent("SampleItem", "TestPurchase", "0.99", "ProductID", "UserId", "PurchaseToken");
    }
#else
    void TrackIAP()
    {
        Debug.Log("TrackIAP on unsupported platform");
    }
#endif
}
