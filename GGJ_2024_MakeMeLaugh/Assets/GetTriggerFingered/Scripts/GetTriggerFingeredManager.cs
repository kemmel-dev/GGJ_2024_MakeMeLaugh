using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GetTriggerFingeredManager : MonoBehaviour
{
    
    public AudioSource audioData;
    private void Start()
    {
        audioData = GetComponent<AudioSource>();
    }
    public TextMeshProUGUI timerText;
    public List<TextMeshProUGUI> PlayerScoreTexts;
    public TextMeshProUGUI winnerText;
    Dictionary<PlayerController, int> scores = new();
    public void AddScore(PlayerController player)
    {
        scores.Add(player, scores.Count);
        if(scores.Count == 4)
        {
            winnerText.gameObject.SetActive(true);
            winnerText.text = "Player " + (player.PlayerIndex + 1) + " won!";
            GameManager.Instance.SetScorePerPlayer(scores);

            foreach(TextMeshProUGUI text in PlayerScoreTexts)
            {
                text.gameObject.SetActive(false);
            }
            StartCoroutine(endScreen());
        }
    }

    private IEnumerator endScreen()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("ThroneRoom");
    }

    Dictionary<PlayerController, int> amountDodged = new();
 
    public void PlaySound(AudioClip clip, float volume)
    {
        audioData.PlayOneShot(clip, volume);
    }
}
