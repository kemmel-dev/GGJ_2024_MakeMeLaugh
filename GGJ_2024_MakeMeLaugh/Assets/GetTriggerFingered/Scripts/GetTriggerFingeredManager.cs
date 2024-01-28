using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GetTriggerFingeredManager : MonoBehaviour
{
    
    public AudioSource audioData;
    private void Start()
    {
        audioData = GetComponent<AudioSource>();
        PlayerScoreTexts[0].color = FindObjectOfType<PlayerData>().Colors[0];
        PlayerScoreTexts[1].color = FindObjectOfType<PlayerData>().Colors[1];
        PlayerScoreTexts[2].color = FindObjectOfType<PlayerData>().Colors[2];
        PlayerScoreTexts[3].color = FindObjectOfType<PlayerData>().Colors[3];

    }
    public TextMeshProUGUI timerText;
    public List<TextMeshProUGUI> PlayerScoreTexts;
    public TextMeshProUGUI winnerText;

    public GameObject Lt;
    public GameObject rt;
    Dictionary<PlayerController, int> scores = new();
    public void AddScore(PlayerController player)
    {
        scores.TryAdd(player, scores.Count);
        if(scores.Count == 4)
        {
            //winnerText.gameObject.SetActive(true);
            //winnerText.text = "Player " + (player.PlayerIndex + 1) + " won!";
            GameManager.Instance.SetScorePerPlayer(scores);

            //foreach (TextMeshProUGUI text in PlayerScoreTexts)
            //{
            //    text.gameObject.SetActive(false);
            //}
            StartCoroutine(endScreen());
        }
    }

    public void TurnOffTutorial()
    {
        rt.SetActive(false);
        Lt.SetActive(false);
    }

    private IEnumerator endScreen()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("ThroneRoom");
    }

    Dictionary<PlayerController, int> amountDodged = new();
 
    public void PlaySound(AudioClip clip, float volume)
    {
        audioData.PlayOneShot(clip, volume);
    }
}
