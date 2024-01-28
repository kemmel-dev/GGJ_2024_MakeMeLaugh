using UnityEngine;

public class NPCScript : MonoBehaviour
{
    private bool movingRight = true;
    [SerializeField] private float speed = 5f;
    private bool FriendlyFire;

    private float timer = 0f; // Timer to track the time elapsed

    void Update()
    {
        // Increment the timer by the time elapsed since last frame
        timer += Time.deltaTime;

        // Move the NPC in the correct direction
        transform.Translate((movingRight ? Vector3.right : Vector3.left) * speed * Time.deltaTime);

        // Check if the timer exceeds 7 seconds and destroy the game object if so
        if (timer > 12f)
        {
            Destroy(gameObject);
        }

        //previous off-screen destroy logic can remain here if needed
        // if (!GetComponent<Renderer>().isVisible)
        // {
        //     Destroy(gameObject);
        // }
    }

    public void SetSpeed(float speedValue)
    {
        speed = speedValue;
    }

    // New method to set the direction
    public void SetDirection(bool isMovingRight)
    {
        movingRight = isMovingRight;
    }
}
