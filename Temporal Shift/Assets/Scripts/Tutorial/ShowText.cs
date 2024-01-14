using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    [SerializeField] GameObject tutorialTextGo;
    [SerializeField] GameObject playerGo;



    private void Start()
    {
        tutorialTextGo.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialTextGo.GetComponentInChildren<TMP_Text>().text = " Interect with the console, you can left click to spawn a freeze orb";
            tutorialTextGo.SetActive(true);
            playerGo.GetComponent<FreezeTime>().enabled = true;
            
        }

    }
    

}
