using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class TomatoJusticePlayerController : MiniGamePlayerController
{
    [SerializeField] private GameObject tomatoPrefab;
    public int score = 0;
    private PlayerController playerController;
    [SerializeField]
    private AudioSource[] audioSources;
    private TMP_Text scoreText;
    private string color;
 
    private float timeSinceLastShot = 0.0f; // Timer to track time since the last shot
    private const float shootingRate = 1.0f / 3.5f; // 5 shots per second

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
        SetupJester();
        audioSources = this.GetComponents<AudioSource>();
    }

    private void Start()
    {
        getPlayerScoreTextMesh();
        color = PlayerControllerReference.PlayerData.color.ToString();
        Debug.Log("color ===" + color);
    }
   
    private void SetupJester()
    {
        var jester = Instantiate(PlayerControllerReference.PlayerData.playerModel, transform);


        jester.transform.localPosition = Vector3.zero;
        jester.transform.localScale = Vector3.one * 6; // Multiply by 5 to increase size by 5 times
        jester.transform.localRotation = Quaternion.Euler(0, 180, 0);
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
        // Update the timer
        timeSinceLastShot += Time.deltaTime;
    }
    private void OnSouthButtonPressed(InputAction.CallbackContext ctx)
    {
        // Check if the controller button was pressed and enough time has passed since the last shot
        if (ctx.performed && timeSinceLastShot >= shootingRate)
        {
            Debug.Log("shouldshoot ");
            SpawnTomato();
            timeSinceLastShot = 0.0f; // Reset the timer
        }
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
        audioSources[0].Play();
        score += amount;

        scoreText.text = score.ToString();
        Debug.Log("Scoreplus: " + score); // Replace with actual UI update

      
    }

    //
    public void DecreaseScore(int amount)
    {
        score -= amount;
        scoreText.text = score.ToString();
        audioSources[1].Play();
        Debug.Log("Scoreminus: " + score); // Replace with actual UI update
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
      
            PlayerControllerReference.SouthButton -= OnSouthButtonPressed;
      
    }
}
