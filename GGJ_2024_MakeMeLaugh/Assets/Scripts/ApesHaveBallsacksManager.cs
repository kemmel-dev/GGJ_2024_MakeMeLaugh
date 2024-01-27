using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ApesHaveBallsacksManager : MonoBehaviour
{

    public Transform Ape;
    public Transform Ballsack;
    public Transform startPosBallsack;
    public Transform startPosApe;
    private Vector3 oldPosition;
    [SerializeField]
    private float minDistance = 10;
    [SerializeField]
    private float startTime = 10;
    public Transform playerArea;

    public UnityEvent m_MyEvent;
    Vector2 screenBounds;
    private float timeRemaining;
    private bool Finished = false;

    public int CountDownTime = 5;
    public int GameTime = 30;
    private MiniGameController miniGameController;

    List<BallsackMovement> ballsackMovements = new List<BallsackMovement>();

    

 /*   public TextMeshProUGUI TimerText;*/

    void Start()
    {
        timeRemaining = startTime;
        Camera mainCamera = Camera.main;
        Vector2 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width *1.5f, Screen.height *1.5f, 1));

        playerArea.localScale = new Vector3(screenBounds.x, screenBounds.y, 1);

        oldPosition = Ape.transform.position;

        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();

        m_MyEvent.AddListener(RespawnApe);

        foreach(BallsackMovement ballsackMovement in FindObjectsOfType<BallsackMovement>())
        {
            ballsackMovements.Add(ballsackMovement);
        }
    }

    public void RespawnApe()
    {
        /*bool far_enough*/
        /* do
         {
             Vector3 newPos = new Vector3(Random.Range(0 - screenBounds.x / 2, 0 + screenBounds.x / 2), Random.Range(0 - screenBounds.y / 2, 0 + screenBounds.y / 2),1);
             float distance = Vector3.Distance(oldPosition, newPos);

             if(distance > minDistance)
         } while{distance < minDistance}*/
        Vector3 newPos = new Vector3(Random.Range(0 - playerArea.transform.localScale.x /2, 0 + playerArea.transform.localScale.x / 2), Random.Range(0 - playerArea.transform.localScale.y / 2, 0 + playerArea.transform.localScale.y / 2), 1);
        Ape.position = newPos;
    }

    private void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        if (timeRemaining < 0)
        {
            Debug.Log("print");
            GameManager.Instance.DeactivateInput();
            Dictionary<PlayerController, int> playerScores = new();
            foreach (BallsackMovement ballsack in ballsackMovements)
            {
                playerScores.Add(ballsack.PlayerControllerReference, ballsack.score);
            }
            GameManager.Instance.SetScorePerPlayer(playerScores);
            SceneManager.LoadScene("ThroneRoom");
        }
    }



    private void OnDestroy()
    {
        m_MyEvent.RemoveListener(RespawnApe);
    }
}

