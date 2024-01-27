using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class TomatoJusticePlayerController : MiniGamePlayerController
{
    [SerializeField] private GameObject tomatoPrefab;
    private static int score = 0;
    private PlayerController playerController;
    private List<TMP_Text> textScoreArray = new List<TMP_Text>();
    private TMP_Text scoreText;

    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);
        // Assuming PlayerController is attached to the same GameObject
       

        if (playerController != null)
        {
            playerController.SouthButton += OnSouthButtonPressed;
        }
          else
        {
            Debug.Log(" testnull!");
        }

    
    }

    private void Start()
    {
        getPlayerScoreTextMesh();
    }
    private void getPlayerScoreTextMesh()
    {
       
    
      
       
        if (PlayerControllerReference.PlayerIndex == 0)
        {
            scoreText = GameObject.Find("Player1Score").GetComponent<TMP_Text>();
            Debug.Log(" 00000scoreText =" + scoreText);
        }
        if (PlayerControllerReference.PlayerIndex == 1)
        {
            scoreText = GameObject.Find("Player2Score").GetComponent<TMP_Text>();
            Debug.Log(" 11111scoreText =" + scoreText);
        }
        if (PlayerControllerReference.PlayerIndex == 2)
        {
            scoreText = GameObject.Find("Player3Score").GetComponent<TMP_Text>();
            Debug.Log(" 2222scoreText =" + scoreText);
        }
        if (PlayerControllerReference.PlayerIndex == 3)
        {
            scoreText = GameObject.Find("Player4Score").GetComponent<TMP_Text>();
            Debug.Log(" 3333scoreText =" + scoreText);
        }
        Debug.Log(" scoreText =" + scoreText);
    }

    private void Update()
    {
      
    }

    private void OnSouthButtonPressed(InputAction.CallbackContext ctx)
    {
        // Check if the controller button was pressed
        if (!ctx.performed) return;
        Debug.Log("shouldshoot ");
        SpawnTomato();
        
    }

    void SpawnTomato()
    {
        // Pass the player controller reference to the tomato projectile
        GameObject tomato = Instantiate(tomatoPrefab, transform.position, Quaternion.identity);
        ProjectileMovement tomatoProjectile = tomato.GetComponent<ProjectileMovement>();

        if (tomatoProjectile != null)
        {
            tomatoProjectile.SetPlayerController(this);
        }
    }


    public void IncreaseScore(int amount)
    {
        score += amount;

        scoreText.text = score.ToString();
        Debug.Log("Score: " + score); // Replace with actual UI update
    }

    public void DecreaseScore(int amount)
    {
        score -= amount;
        scoreText.text = score.ToString();
        Debug.Log("Score: " + score); // Replace with actual UI update
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (playerController != null)
        {
            playerController.SouthButton -= OnSouthButtonPressed;
        }
    }
}
