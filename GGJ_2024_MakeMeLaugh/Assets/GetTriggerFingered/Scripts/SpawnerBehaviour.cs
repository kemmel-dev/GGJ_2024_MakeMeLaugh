using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    public float ballspeed = 5;
    public GameObject leftPoint;
    public GameObject rightPoint;
    public GameObject ball;
    public float maxBallSpeed = 40f;
    public float acceleration = 1.05f;
    public float minWaitTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Starter()
    {
        StartCoroutine(SpawnBall());
    }

    private IEnumerator SpawnBall()
    {
        float waitTime = 2f;
        
        while (true)
        {
            GameObject spawnpoint;
            
            if (GetRandom() < 0.5)
            {
                spawnpoint = leftPoint;
            }
            else
            {
                spawnpoint = rightPoint;
            }

            var ballSpawned = Instantiate(ball, spawnpoint.transform.position, Quaternion.identity);
            ballSpawned.GetComponent<BallBehaviour>().currentSpeed = ballspeed;
            ballspeed = ballspeed * acceleration;
            if(ballspeed > maxBallSpeed)
            {
                ballspeed = maxBallSpeed;
            }
            waitTime -= 0.05f;
            if(waitTime < minWaitTime)
            {
                waitTime = minWaitTime;
            }
            yield return new WaitForSeconds(waitTime);
        }

        
    }
    private float GetRandom()
    {
        float rdm = Random.Range(0f, 1f);
        return rdm;
    }
}
