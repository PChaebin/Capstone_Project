using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Option : MonoBehaviour
{
    [SerializeField] private Button optionButton;
    [SerializeField] private Button xButton;
    [SerializeField] private GameObject optionUI;

    private void Start()
    {
        if (optionButton != null && xButton != null)
        {
            optionButton.onClick.AddListener(OnOptionButtonClicked);
            Debug.Log("Option Button Clicked AddListener");
            xButton.onClick.AddListener(OnXButtonClicked);
            Debug.Log("X Button Clicked AddListener");
        }
    }

    private void OnOptionButtonClicked()
    {
        Debug.Log("Option Button Clicked");
        optionUI.SetActive(true);

        Time.timeScale = 0;

        optionUI.transform.SetAsLastSibling();
    }

    private void OnXButtonClicked()
    {
        Time.timeScale = 1;
        optionUI.SetActive(false);
    }
}
