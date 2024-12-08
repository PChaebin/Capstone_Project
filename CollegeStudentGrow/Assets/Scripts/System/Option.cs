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
        optionUI.SetActive(false);

        if (optionButton != null && xButton != null)
        {
            optionButton.onClick.AddListener();
            xButton.onClick.AddListener();
        }
    }

    private void OnOptionButtonClicked()
    {
        optionUI.SetActive(true);
    }

    private void OnXButtonClicked()
    {
        optionUI.SetActive(false);
    }
}
