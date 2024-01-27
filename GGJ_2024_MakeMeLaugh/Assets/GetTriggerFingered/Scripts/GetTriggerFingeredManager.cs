using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTriggerFingeredManager : MonoBehaviour
{
    public GameObject[] players = new GameObject[4];
    public List<Transform> scores = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(scores.Count == 4)
        {
            CalcScore();
            for (int i = 0; i < scores.Count; i++)
            {
                Debug.Log(scores[i]);
            }
        }
    }

    public void AddScore(GameObject player)
    {
        
        Debug.Log(player.transform.parent);
        scores.Add(player.transform.parent);
    }

    public void CalcScore()
    {
        scores.Reverse();
    }
}
