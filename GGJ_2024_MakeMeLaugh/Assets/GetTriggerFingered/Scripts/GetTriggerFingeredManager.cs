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
    Dictionary<PlayerController, int> scores = new();
    public void AddScore(PlayerController player)
    {
        scores.Add(player, scores.Count);
        if(scores.Count == 4)
        {
            GameManager.Instance.SetScorePerPlayer(scores);
            SceneManager.LoadScene("ThroneRoom");
        }
    }

    public void PlaySound()
    {
        audioData.PlayOneShot(audioData.clip, 1f);
    }
}
