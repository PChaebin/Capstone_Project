using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public CanvasGroup canvasGroup; 
    public float fadeTime = 2f; 
    public bool isFadeInOnStart = true; 

    private int levelToLoad; 

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            canvasGroup.alpha = 1f; 
        }
        else if (isFadeInOnStart)
        {
            canvasGroup.alpha = 0f; 
            LeanTween.alphaCanvas(canvasGroup, 1f, fadeTime).setEase(LeanTweenType.easeInOutQuad);
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex; 

        LeanTween.alphaCanvas(canvasGroup, 0f, fadeTime).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            SceneManager.LoadScene(levelToLoad); 
        });
    }
}
