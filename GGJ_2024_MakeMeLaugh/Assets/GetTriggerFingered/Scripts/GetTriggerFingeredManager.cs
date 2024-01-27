using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetTriggerFingeredManager : MonoBehaviour
{
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

}
