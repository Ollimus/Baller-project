using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {

    public GameObject menuScreen;
    public GameObject informationObject;
    private Text informationText;
    private Text completionTimeText;
    private bool isActivated = false;
    private IEnumerator coroutine;

    /*private void Update()
    {
        string test = "Teksti toimii";

        InformationTextUI(test);
    }*/

    private void Start()
    {

        coroutine = BlinkingText(1.0f);

        StartCoroutine(coroutine);
    }

    public void ActivateMenu(string completionTime)
    {
        if (!isActivated)
        {
            try
            {
                isActivated = true;
                menuScreen.transform.SetSiblingIndex(-1);
                completionTimeText = menuScreen.transform.Find("txtCompletionTime").GetComponentInChildren<Text>();
                completionTimeText.text = completionTime;
                Debug.Log(completionTimeText.text);
                menuScreen.SetActive(true);
            }

            catch (Exception e)
            {
                Debug.Log("Error creating victory menu for player. Error: " + e);
            }
        }
    }

    public void ShowInformationText(string inputText)
    {
        if (!informationObject.activeInHierarchy)
        {
            try
            {
                informationObject.SetActive(true);
                informationText = informationObject.GetComponent<Text>();
                informationText.text = inputText;
            }

            catch (Exception e)
            {
                Debug.Log("Error with inputting text to player. Error: " + e);
            }
        }
    }

    private IEnumerator BlinkingText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("?");
    }
}
