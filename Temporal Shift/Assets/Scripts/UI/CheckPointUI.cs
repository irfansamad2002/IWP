using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointUI : MonoBehaviour
{
    [SerializeField] GameObject theUIGO;
    [SerializeField] RectTransform rectTransform;

    float OriginalHeight;
    Vector2 closeRect;
    float lerpDuration = 2;
    CheckPoint toCheckIfSameCheckpoint;

    private void Start()
    {
        OriginalHeight = rectTransform.rect.height;
        closeRect.Set(rectTransform.sizeDelta.x, 0f);
        rectTransform.sizeDelta = closeRect;

    }
    private void OnEnable()
    {
        CheckPoint.OnLatestCheckpointTouch += CheckPoint_OnLatestCheckpointTouch;
    }

    private void OnDisable()
    {
        CheckPoint.OnLatestCheckpointTouch -= CheckPoint_OnLatestCheckpointTouch;

    }

    private void CheckPoint_OnLatestCheckpointTouch(CheckPoint obj)
    {
        if (obj == toCheckIfSameCheckpoint)
            return;
        toCheckIfSameCheckpoint = obj;
        StartAppear();
    }
    [ContextMenu("Start Appear")]
    void StartAppear()
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, OriginalHeight);
        theUIGO.SetActive(true);
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(lerpDuration);

        float timeElapsed = 0;
        while (timeElapsed <= lerpDuration)
        {
            rectTransform.sizeDelta = Vector2.Lerp(rectTransform.sizeDelta, closeRect, timeElapsed / lerpDuration);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        rectTransform.sizeDelta = closeRect;
    }

    
}
