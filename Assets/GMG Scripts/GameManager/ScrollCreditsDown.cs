using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;

public class ScrollCreditsDown : MonoBehaviour
{

    [Header("Movement Settings")]
  
    public RectTransform creditsTransform;
    public float scrollSpeed = 50f;
    public float scrollDuration = 10f;

    private bool isScrolling = true;
    

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //get the recttransform compnenet attacthed to the Text UI element
        StartCoroutine(StopScrollingTimer(scrollDuration));
    }

    // Update is called once per frame
    private void Update()
    {
        if (isScrolling && creditsTransform != null) {
            creditsTransform.anchoredPosition += Vector2.up * (scrollSpeed * Time.deltaTime);
        }
    }

    private IEnumerator StopScrollingTimer(float duration) {
        yield return new WaitForSeconds(duration);

        //stop the movement
        isScrolling = false;
        Debug.Log("Credits scrolling finished");
    }
}
