using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CountdownFreeze : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText; // TextMeshPro for the countdown
    [SerializeField] private TextMeshProUGUI explanationText; // TextMeshPro for the game explanation
    [SerializeField] private string gameExplanation = "This is the game explanation...";
    [SerializeField] private GameObject[] gameObjectsToControl;
    [SerializeField] private Image fadePanel; // UI panel for the fade effect

    private float fadeDuration = 3f; // Duration for the fade effect

    private void Awake()
    {
        // Disable all scripts on the referenced game objects and their children
        SetScriptsActive(false);
        GameManager.Instance.DeactivateInput();

    }

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
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, Mathf.Lerp(1, 0, normalizedTime));
            yield return null;
        }

        fadePanel.gameObject.SetActive(false); // Disable the fade panel after fade out

        // Begin the countdown sequence
        StartCoroutine(StartCountdown());
    }

    // Method to fade the screen to black
    public IEnumerator FadeToBlack()
    {
        fadePanel.gameObject.SetActive(true);
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, Mathf.Lerp(0, 1, normalizedTime));
            yield return null;
        }

    }
        private IEnumerator StartCountdown()
    {
        // Perform the countdown
        countdownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }


        countdownText.text = "Go!";
        yield return new WaitForSeconds(0.3f);
        GameManager.Instance.ActivateInput();

        // Wait for a moment to let players see the 'Go!' message
      

        countdownText.gameObject.SetActive(false); // Hide the countdown text
        explanationText.gameObject.SetActive(false); // Hide the explanation text

        // Reactivate the scripts on the game objects
        SetScriptsActive(true);
    }

    private void SetScriptsActive(bool active)
    {
        foreach (GameObject go in gameObjectsToControl)
        {
            if (go != null)
            {
                // Get all MonoBehaviour components on the GameObject and its children
                MonoBehaviour[] scripts = go.GetComponentsInChildren<MonoBehaviour>(true);
                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = active; // Enable or disable all scripts
                }
            }
        }
    }
}
