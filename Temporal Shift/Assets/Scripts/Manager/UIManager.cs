using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text leftClickUI;
    [SerializeField] private TMP_Text rightClickUI;
    [SerializeField] private TMP_Text rewindingTextUI;
    [SerializeField] private GameObject RightClickGO;
    [SerializeField] private CanvasGroup canvasGroup;


    [SerializeField] private GameObject HealthParent;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private HideMouseOnFocus hideMouseOnFocus;
    [SerializeField] private Transform defaultLocaltionForMouse;

    [SerializeField] Volume postProcessingVolume;

    [SerializeField] private GameObject inGameMenuGO;

    public static UIManager Instance;

    private int lastDeactivatedChildIndex = -1;

    [SerializeField] HideMouseOnFocus hideMouse;

    public bool isPausing;


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
        PlayerHealth.OnDeath += PlayerHealth_OnDeath;

        RespawnManager.OnRespawnEvent += RespawnManager_OnRespawnEvent;

        InputReader.InGameMenuEvent += InputReader_InGameMenuEvent;

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
        PlayerHealth.OnDeath -= PlayerHealth_OnDeath;

        RespawnManager.OnRespawnEvent -= RespawnManager_OnRespawnEvent;

        InputReader.InGameMenuEvent -= InputReader_InGameMenuEvent;
    }

    #region inGameMenu
    private void InputReader_InGameMenuEvent()
    {
        inGameMenuGO.SetActive(true);
        hideMouse.ShowCursor();

        // Stop World Time
        Time.timeScale = 0f;

        MakeBackgroundBlur();

        isPausing = true;

    }

    public void CloseInGameMenu()
    {
        inGameMenuGO.SetActive(false);
        hideMouse.HideCursor();

        // Stop World Time
        Time.timeScale = 1f;

        MakeBackgroundUnBlur();

        isPausing = false;
    }

    #endregion

    #region player On Death

    private void RespawnManager_OnRespawnEvent()
    {
        GameOverScreen.SetActive(false);

        Time.timeScale = 1f;

        hideMouseOnFocus.HideCursor();

        MakeBackgroundUnBlur();

        ResetHealthChildren();


    }

    private void PlayerHealth_OnDeath()
    {
        //ShowGameOverOption
        GameOverScreen.SetActive(true);

        StopCoroutine(FadeBloodOut());
        canvasGroup.alpha = 0f;


        //Stop World Time
        Time.timeScale = 0f;

        //Enable Mouse
        hideMouseOnFocus.ShowCursor();

        MakeBackgroundBlur();

    }

    [ContextMenu("blur")]
    private void MakeBackgroundBlur()
    {
        postProcessingVolume.profile.components[3].active = true;
    }

    [ContextMenu("unblur")]
    private void MakeBackgroundUnBlur()
    {
        postProcessingVolume.profile.components[3].active = false;
    }




    private void ResetHealthChildren()
    {
        Transform healthParentTransform = HealthParent.transform;

        for (int i = 0; i < healthParentTransform.childCount; i++)
        {
            Transform child = healthParentTransform.GetChild(i);
            child.gameObject.SetActive(true);
        }
    }
    #endregion

    private void PlayerHealth_OnHit()
    {


        DeactivateNextChild();
        canvasGroup.alpha = 1f;
        StopCoroutine(FadeBloodOut());

        StartCoroutine(FadeBloodOut());
        
    }

    private void DeactivateNextChild()
    {
        Transform healthParentTransform = HealthParent.transform;

        if (lastDeactivatedChildIndex < healthParentTransform.childCount - 1)
        {
            lastDeactivatedChildIndex++;
            Transform child = healthParentTransform.GetChild(lastDeactivatedChildIndex);
            child.gameObject.SetActive(false);
        }
        else
        {
            // All children are deactivated, reset the index
            lastDeactivatedChildIndex = -1;
        }
    }

    private IEnumerator FadeBloodOut()
    {
        float elapsedTime = 0f;
        float fadeDuration = 2f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.unscaledDeltaTime;
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
