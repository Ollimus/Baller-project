using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignMessages : MonoBehaviour {

    public string key;
    public TutorialTexts tutorialManager;
    public float fadeTime = 2;
    public int orderInLayer = -1;

    private TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        if (textMesh == null)
        {
            Debug.LogError("No text found.");
        }

        else
            SetUpText();
            
    }

    private void SetUpText()
    {
        textMesh.alpha = 0;

        textMesh.sortingOrder = orderInLayer;

        string text = tutorialManager.GetText(key);

        textMesh.text = text;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || textMesh == null)
            return;

        textMesh.alpha = 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || textMesh == null)
            return;

        StartCoroutine(HideText());
    }

    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(fadeTime);

        textMesh.alpha = 0;
    }
}
