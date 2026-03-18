using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    public string textValue;
    public TextMeshProUGUI textElement;

    public float textDelay;
    private string currentText = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textElement.enabled = true;
            StartCoroutine(ShowText());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textElement.enabled = false;
        }
    }

    IEnumerator ShowText()
    {
        for(int i = 0; i <= textValue.Length; i++)
        {
            currentText = textValue.Substring(0,i);
            textElement.text = currentText;
            yield return new WaitForSeconds(textDelay);
        }
    }
}
