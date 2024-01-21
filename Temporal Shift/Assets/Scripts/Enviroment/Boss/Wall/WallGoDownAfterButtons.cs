using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGoDownAfterButtons : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float goDownHowFar = 5f;

    public float goDownDuration = 2f;

    ActivateByMultipleButtonsEvent theEventForMulti;
    Vector3 initialPosition;
    Vector3 targetPosition;
    bool isOn;

    private void Awake()
    {
        theEventForMulti = GetComponent<ActivateByMultipleButtonsEvent>();
    }

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition;
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
            Debug.Log("Test");
            StartCoroutine(ActivateUpAndDown());
        }
    }

    private IEnumerator ActivateUpAndDown()
    {
        isOn = true;
        yield return new WaitForSeconds(goDownDuration);
        isOn = false;
    }


    private IEnumerator MoveDown()
    {
        while (transform.position.y > initialPosition.y - goDownHowFar) // Adjust the threshold for when it's considered "down"
        {
            Debug.Log("go down test " + moveSpeed * Time.deltaTime);
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("what is over here");
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

    private void Update()
    {
        if (isOn)
        {
            //transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            targetPosition = initialPosition - new Vector3(0f, goDownHowFar, 0f);
        }
        else
        {
            targetPosition = initialPosition;

        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        //Utils.ClampVector3(transform.position, new Vector3(transform.position.x, transform.position.y + goDownHowFar, transform.position.z), initialPosition);

    }
}
