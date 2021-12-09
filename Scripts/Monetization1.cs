using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;


public class Monetization1 : MonoBehaviour, IUnityAdsListener
{
    public static Monetization1 instance;

    private string store_id = "3524445";
    bool testMode = false;

    private string video_ad = "video";
    private string myPlacementId = "rewardedVideo";
    private UIManager _UiManager;





    void Start()
    {
        _UiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        Advertisement.AddListener(this);
        Advertisement.Initialize(store_id, testMode);
    }

    public void RewardedVideoAd()
    {
        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId)
        {
           
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }


    void Update()
    {

    }

}


    




