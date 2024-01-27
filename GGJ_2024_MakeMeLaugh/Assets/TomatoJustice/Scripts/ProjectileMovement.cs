using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private TomatoJusticePlayerController playerController;

    // Add a method to set the player controller reference
    public void SetPlayerController(TomatoJusticePlayerController controller)
    {
        playerController = controller;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Destroy if off-screen
        if (!IsVisibleFromCamera())
        {
            Destroy(gameObject);
        }
    }

    private bool IsVisibleFromCamera()
    {
        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        bool isVisible = viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1;
        return isVisible;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);

        // Check if the entering object has a name that starts with "Enemy"
        if (other.gameObject.name.StartsWith("Enemy"))
        {
            // Increase player's score using the stored player controller reference
            if (playerController != null)
            {
                playerController.IncreaseScore(1); // You should have a method to increase the player's score in PlayerController
            }
        }
        // Check if the entering object has a name that starts with "Enemy"
        if (other.gameObject.name.StartsWith("Friend"))
        {
            // Increase player's score using the stored player controller reference
            if (playerController != null)
            {
                playerController.DecreaseScore(1); // You should have a method to increase the player's score in PlayerController
            }
        }
        // Always destroy the tomato
        Destroy(gameObject);
    }
}