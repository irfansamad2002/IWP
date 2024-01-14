using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InteractConsole : MonoBehaviour, IInteractable
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject tutorialTextGo;
    [SerializeField] float shootForce = 500f;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Interacted()
    {
        SpawnTheBall();
        PlaySFX();
        StartCoroutine(ShowTutorialText());
    }
    [ContextMenu("SpawnTheBall")]
    void SpawnTheBall()
    {
        GameObject go = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);

        Rigidbody rb = go.GetComponent<Rigidbody>();

        go.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shootForce);
    }
    
    void PlaySFX()
    {
        audioSource.Play();
    }

   //void ShowTutorialText()
   //{
   //     tutorialTextGo.GetComponentInChildren<TMP_Text>().text = "Hit the ball with it";
   //     tutorialTextGo.SetActive(true);
   //}

    IEnumerator ShowTutorialText()
    {
        tutorialTextGo.GetComponentInChildren<TMP_Text>().text = "Hit the ball with it";
        tutorialTextGo.SetActive(true);

        yield return new WaitForSeconds(4f);
        tutorialTextGo.SetActive(false);
        //tutorialTextGo.GetComponentInChildren<TMP_Text>().text = "You can stop the ball from moving by the first click";

        //yield return new WaitForSeconds(6f);
        //tutorialTextGo.GetComponentInChildren<TMP_Text>().text = "Press again to despawn the orb";

        yield return null;
    }

    void CloseTutorialText()
    {
        tutorialTextGo.SetActive(false);
    }
}
