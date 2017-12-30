using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {

    public GameObject menuScreen;
    public GameObject informationObject;
    public GameObject touchControls;
    private Text informationText;
    private Text completionTimeText;
    private bool isActivated = false;
    private IEnumerator coroutine;

    void Start()
    {
        if (touchControls != null)
        {
            string operatingSystemCheck;
            operatingSystemCheck = SystemInfo.operatingSystem;

            if (operatingSystemCheck.StartsWith("Windows") || !operatingSystemCheck.StartsWith("Mac"))
            {
                touchControls.SetActive(false);
            }

            else
            {
                touchControls.SetActive(true);
            }
        }
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
                informationText = informationObject.GetComponent<Text>();
                informationText.text = inputText;
                informationObject.SetActive(true);
                coroutine = FlashUIText(2.0f);
                StartCoroutine(coroutine);


            }

            catch (Exception e)
            {
                Debug.Log("Error with inputting text to player. Error: " + e);
            }
        }
    }

    private IEnumerator FlashUIText(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        informationObject.SetActive(false);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void DisableTouchControl()
    {
        touchControls.SetActive(false);
    }

    public void PlaceHolderText()
    {
        String placeholderText = "Functionality under construction!";

        ShowInformationText(placeholderText);
    }
}
