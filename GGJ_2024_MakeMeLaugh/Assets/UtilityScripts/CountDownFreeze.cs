using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText; // TextMeshPro for the countdown
    [SerializeField] private TextMeshProUGUI explanationText; // TextMeshPro for the game explanation
    [SerializeField] private string gameExplanation = "This is the game explanation...";
    [SerializeField] private GameObject[] gameObjectsToControl;
    [SerializeField] private Image fadePanel; // UI panel for the fade effect

    private float fadeDuration = 2f; // Duration for the fade effect

    private void Start()
    {
        if (countdownText == null || explanationText == null || fadePanel == null)
        {
            Debug.LogError("Required components are not assigned.");
            return;
        }

        // Set the game explanation text
        explanationText.text = gameExplanation;
        // Start the fade-in effect, followed by the countdown
        StartCoroutine(FadeFromBlack());
    }

    private IEnumerator FadeFromBlack()
    {
        // Ensure the fade panel is enabled and fully opaque
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 1);

        // Gradually decrease the alpha of the fade panel to create a fade-in effect
        float startAlpha = 1f;
        float endAlpha = 0f;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, Mathf.Lerp(startAlpha, endAlpha, normalizedTime));
            yield return null;
        }

        // Ensure the alpha is set to 0 to make it fully transparent
        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 0);
        fadePanel.gameObject.SetActive(false); // Disable the fade panel

        // Begin the countdown sequence
        StartCoroutine(StartCountdown());
    }


    private IEnumerator StartCountdown()
    {
        SetGameObjectsActive(false);

        // Set the explanation text active
        explanationText.gameObject.SetActive(true);
        explanationText.text = gameExplanation;

        // Perform the countdown
        countdownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        countdownText.text = "Go!";
        yield return new WaitForSeconds(1);

        // Hide the countdown text but leave the explanation text visible if you want
        countdownText.gameObject.SetActive(false);

        // Reactivate the game objects
        SetGameObjectsActive(true);
    }

    private void SetGameObjectsActive(bool active)
    {
        foreach (GameObject go in gameObjectsToControl)
        {
            if (go != null)
            {
                go.SetActive(active);
            }
        }
    }
}
