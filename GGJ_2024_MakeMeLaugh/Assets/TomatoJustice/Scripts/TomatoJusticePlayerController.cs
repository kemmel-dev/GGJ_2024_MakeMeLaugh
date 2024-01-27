using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class TomatoJusticePlayerController : MiniGamePlayerController
{
    [SerializeField] private GameObject tomatoPrefab;
    private static int score = 0;
    private PlayerController playerController;

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
        Instantiate(tomatoPrefab, transform.position, Quaternion.identity);
    }

    public static void IncreaseScore(int amount)
    {
        score += amount;
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
