using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text leftClickUI;
    [SerializeField] private TMP_Text rightClickUI;
    [SerializeField] private TMP_Text rewindingTextUI;
    [SerializeField] private GameObject RightClickGO;
    [SerializeField] private CanvasGroup canvasGroup;


    public static UIManager Instance;

    private void Start()
    {
        RightClickGO.SetActive(false);
        rewindingTextUI.gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        FreezeTime.OnSpawnFreezeOrb += FreezeTime_OnSpawnFreezeOrb;
        FreezeOrb.OnDestroyOrb += FreezeTime_OnDestroyOrb;
        FreezeOrb.OnStopOrb += FreezeTime_OnStopOrb;
        TimeRewindController.OnNotLookingAtRewindObjectUI += TimeRewindController_OnHideUI;
        TimeRewindController.OnRewindingObjectUI += TimeRewindController_OnShowCancelUI;
        TimeRewindController.OnAbleToRewindObjectUI += TimeRewindController_OnShowHoldUI;

        PlayerHealth.OnHit += PlayerHealth_OnHit;
        
    }

    

    private void OnDisable()
    {
        FreezeTime.OnSpawnFreezeOrb -= FreezeTime_OnSpawnFreezeOrb;
        FreezeOrb.OnDestroyOrb -= FreezeTime_OnDestroyOrb;
        FreezeOrb.OnStopOrb -= FreezeTime_OnStopOrb;
        TimeRewindController.OnNotLookingAtRewindObjectUI -= TimeRewindController_OnHideUI;
        TimeRewindController.OnRewindingObjectUI -= TimeRewindController_OnShowCancelUI;
        TimeRewindController.OnAbleToRewindObjectUI -= TimeRewindController_OnShowHoldUI;

        PlayerHealth.OnHit -= PlayerHealth_OnHit;

    }

    private void PlayerHealth_OnHit()
    {
        canvasGroup.alpha = 1f;
        StartCoroutine(FadeBloodOut());
    }

    private IEnumerator FadeBloodOut()
    {
        float elapsedTime = 0f;
        float fadeDuration = 2f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f; 
    }

    private void TimeRewindController_OnShowHoldUI()
    {
        RightHoldAbleToRewind();
    }

    private void TimeRewindController_OnShowCancelUI()
    {
        RightHoldRewinding();
    }

    private void TimeRewindController_OnHideUI()
    {
        RightClickGO.SetActive(false);
        rewindingTextUI.gameObject.SetActive(false);

    }


    private void FreezeTime_OnStopOrb()
    {
        LeftClickDespawnOrbUI();
    }

    private void FreezeTime_OnDestroyOrb()
    {
        LeftClickSpawnFreezeOrbUI();
    }

    private void FreezeTime_OnSpawnFreezeOrb()
    {
        LeftClickStopOrbMovementUI();
    }

    private void LeftClickSpawnFreezeOrbUI()
    {
        leftClickUI.text = "Freeze Orb";
    }

    private void LeftClickStopOrbMovementUI()
    {
        leftClickUI.text = "Stop Orb";
    }

    private void LeftClickDespawnOrbUI()
    {
        leftClickUI.text = "Cancel";
    }

    private void RightHoldAbleToRewind()
    {

        RightClickGO.SetActive(true);
        rightClickUI.text = "Hold To Rewind";

    }

    private void RightHoldRewinding()
    {
        RightClickGO.SetActive(true);
        rightClickUI.text = "Release To Cancel";
        rewindingTextUI.gameObject.SetActive(true);

    }

}
