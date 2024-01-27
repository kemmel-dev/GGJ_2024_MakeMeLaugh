using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTriggerFingeredManager : MonoBehaviour
{
    public List<GTFPlayerBehaviour> players = new List<GTFPlayerBehaviour>();
    public List<GTFPlayerBehaviour> scores = new List<GTFPlayerBehaviour>();

    private void Start()
    {
        //StartCoroutine(LateStart(0.5f));
    }
    public void AddScore(GameObject player)
    {
        scores.Add(player.GetComponent<GTFPlayerBehaviour>());
        if (scores.Count == 4)
        {
            CalcScore();
        }
    }
    private void SelectWinner()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        for (int i = 0; i < scores.Count; i++)
        {

        }
        //gm.Players[0].PlayerData.pointsThisRound = 3; //winner gets 3 points
        //gm.Players[1].PlayerData.pointsThisRound = 2; //second gets 2 points
        //gm.Players[1].PlayerData.pointsThisRound = 1; //third gets 1 point
        //gm.Players[1].PlayerData.pointsThisRound = 0; //fourth gets 0 points

    }
    //IEnumerator LateStart(float waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    for (int i = 0; i < 4; i++)
    //    {
    //        var thing = FindObjectsOfType<GTFPlayerBehaviour>();
    //        players[i] = thing[i];
    //    }
    //}

    public void CalcScore()
    {
        scores.Reverse();
        for (int i = 0; i < scores.Count; i++)
        {
            Debug.Log(scores[i]);
        }
    }
}
