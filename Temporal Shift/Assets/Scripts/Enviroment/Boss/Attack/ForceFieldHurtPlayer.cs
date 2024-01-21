using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldHurtPlayer : MonoBehaviour
{
    [SerializeField] float wallMovementSpeed = 4f;
    [SerializeField] float wallHeight = 40f;
    [SerializeField] float getReadySpeed = 5f;
    [SerializeField] float wallLifetime = 4f;

    private bool hitPlayer;
    private float currentWallSpeed;
    private float targetWallHeight;

    float checkHeightOffset = 2f;
    private void Start()
    {
        currentWallSpeed = 0f;
        targetWallHeight = 1f;
    }

    

    private void OnTriggerEnter(Collider other)
    {
            
        IHitable iHitable = other.gameObject.GetComponent<IHitable>();

        if (iHitable != null && !hitPlayer)
        {

            iHitable.Hit();
            hitPlayer = true;
        }


    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        //currentWallSpeed = wallMovementSpeed;
        targetWallHeight = wallHeight;
        Debug.Log("target Wall Height" + targetWallHeight);
    }

    private void Update()
    {

        Vector3 newScale = new Vector3(transform.localScale.x, targetWallHeight, transform.localScale.z);

        transform.localScale = Vector3.Lerp(transform.localScale, newScale, getReadySpeed * Time.deltaTime);

        //if(transform.localScale.y is around targetwallheight is 2f offset, so it will become tru when transform.localScale.y - 2)
        if (transform.localScale.y >= wallHeight - checkHeightOffset)
        {
            currentWallSpeed = wallMovementSpeed;
            Destroy(gameObject, wallLifetime);
        }

        transform.Translate(Vector3.forward * (currentWallSpeed * Time.deltaTime));

        //Debug.Log("test");
        //float height;
        //height = Mathf.Lerp(currentWallSpeed, targetWallHeight, 0.2f * Time.deltaTime);
        //transform.localScale = newScale;



    }
}
