using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGoDownAfterButtons : MonoBehaviour
{
    private Vector3 initialPosition;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float goDownHowFar = 5f;

    public float goDownForHowLong = 2f;

    ActivateByMultipleButtonsEvent theEventForMulti;
    bool isOn;

    private void Awake()
    {
        theEventForMulti = GetComponent<ActivateByMultipleButtonsEvent>();
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        theEventForMulti.OnActivation += TheEventForMulti_OnActivation;
    }

    private void OnDisable()
    {
        theEventForMulti.OnActivation -= TheEventForMulti_OnActivation;

    }
    private void TheEventForMulti_OnActivation()
    {
        if (!isOn)
        {
            StartCoroutine(ActivateUpAndDown());
        }
    }

    private IEnumerator ActivateUpAndDown()
    {
        GoDown();
        yield return new WaitForSeconds(goDownForHowLong);
        GoUp();
    }

    [ContextMenu("GoDown")]
    public void GoDown()
    {
        StartCoroutine(MoveDown());
    }
    [ContextMenu("GoUp")]
    public void GoUp()
    {
        StartCoroutine(MoveUp());

    }

    private IEnumerator MoveDown()
    {
        while (transform.position.y > initialPosition.y - goDownHowFar) // Adjust the threshold for when it's considered "down"
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MoveUp()
    {
        while (transform.position.y < initialPosition.y)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            yield return null;
        }
        isOn = false;
    }
}
