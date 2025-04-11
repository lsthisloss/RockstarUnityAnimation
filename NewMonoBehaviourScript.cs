using UnityEngine;
using TMPro;

public class FadeInAnimation : MonoBehaviour
{
    public TextMeshProUGUI rustyR;
    public float fadeDuration = 1.0f;

    private Color originalColor; 
    public TextMeshProUGUI rustyDots;
    public float dotInterval = 0.5f;

    private string baseText = "";
    private int maxDots = 3;

    void Start()
    {
        try
        {
            rustyDots = FindFirstObjectByType<TextMeshProUGUI>();
            rustyR = GetComponent<TextMeshProUGUI>();
            if (rustyDots == null || rustyR == null)
            {
                Debug.LogError("TextMeshProUGUI component not found in the scene.");
                return;
            }
            Debug.Log("TextMeshProUGUI component successfully assigned.");

            StartCoroutine(AnimateDots());
            StartCoroutine(ShowRustyR());

        }
        catch (System.Exception e)
        {
            Debug.LogError("Error in Start method: " + e.Message);
        }
    }

    private System.Collections.IEnumerator AnimateDots()
    {
        int dotCount = 0;

        while (true) 
        {
            rustyDots.text = baseText + new string('.', dotCount);
            dotCount = (dotCount + 1) % (maxDots + 1);
            yield return new WaitForSeconds(dotInterval);
        }
    }

    private System.Collections.IEnumerator ShowRustyR()
    {

            float elapsedTime = 0f;
            Vector3 originalScale = rustyR.transform.localScale;
            Vector3 targetScale = originalScale * 1.0f; 

            while (elapsedTime < fadeDuration)
            {
                float t = elapsedTime / fadeDuration;
                rustyR.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
                rustyR.color = new Color(rustyR.color.r, rustyR.color.g, rustyR.color.b, t); 
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rustyR.transform.localScale = targetScale;
            rustyR.color = new Color(rustyR.color.r, rustyR.color.g, rustyR.color.b, 1.0f);
            yield return new WaitForSeconds(2.0f);
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            rustyR.transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            rustyR.color = new Color(rustyR.color.r, rustyR.color.g, rustyR.color.b, 1.0f - t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rustyR.transform.localScale = originalScale;
        rustyR.color = new Color(rustyR.color.r, rustyR.color.g, rustyR.color.b, 0.0f);
        Destroy(rustyR);
        Destroy(rustyDots);
        Destroy(this.gameObject);
        Destroy(this);
        Destroy(rustyR);


    }
}