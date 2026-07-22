using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;//need to load scenes

public class MainMenu : MonoBehaviour
{
    [Header("UI Fade settings")]
    public CanvasGroup logoCanvasGroup;
    public float fadeDuration = 2.0f;

    [Header("Scene transition.")]// this will hold the scene name selected from the inspector dropdown
    
    public string sceneToLoad; //type scene name here in the inspector


    private void Start()
    {
        //syart the logo as completely transparent
        if (logoCanvasGroup != null) {
            logoCanvasGroup.alpha = 0f;
            StartCoroutine(FadeInLogo());
        }
    }

    private IEnumerator FadeInLogo() {
        float timer = 0f;
        while (timer < fadeDuration) {
            timer += Time.deltaTime;
            //smoothly transition alpha from 0 to 1
            logoCanvasGroup.alpha = Mathf.Clamp01(timer / fadeDuration);
            yield return null; //with until the next frame
        }
    }

    public void PlayGame()
    {
        //load thje next scene in build settinsg q
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("No scene selected in Main Menu script inspector!");
        }

    }

    public void QuitGame() {
        Debug.Log("Quit Game Pressed!");
        Application.Quit();
    }
}
    
