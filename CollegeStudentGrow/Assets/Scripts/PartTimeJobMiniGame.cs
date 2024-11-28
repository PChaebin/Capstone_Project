using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartTimeJobMiniGame : MonoBehaviour
{
    

    

    private void Start()
    {
        LoadRecipesToJson();
    }

    

    public void gameStatus()
    {
        startButton.gameObject.SetActive(false);
        recipeUI.SetActive(true);

        StartCoroutine(DisplayRecipes());
    }

        
}
