using UnityEngine;
using System.Collections; // Import to use Coroutines

public class NPCScript : MonoBehaviour
{
    private bool movingRight = true;
    [SerializeField] private float speed = 5f;
    private float timer = 0f;

    private bool isStandingStill = false; // New variable to track if the NPC is standing still

    void Update()
    {
        timer += Time.deltaTime;

        // Move the NPC only if it is not standing still
        if (!isStandingStill)
        {
            transform.Translate((movingRight ? Vector3.right : Vector3.left) * speed * Time.deltaTime);
        }

        if (timer > 12f)
        {
            Destroy(gameObject);
        }
    }

    // Method to trigger the stand still behavior
    public void SetStandStill(float waitTime)
    {
        StartCoroutine(StandStillCoroutine(waitTime));
    }

    IEnumerator StandStillCoroutine(float waitTime)
    {
        isStandingStill = true; // Stop the NPC from moving
        yield return new WaitForSeconds(waitTime); // Wait for 2 seconds
        isStandingStill = false; // Resume moving after 2 seconds
    }

    public void SetSpeed(float speedValue)
    {
        speed = speedValue;
    }

    public void SetDirection(bool isMovingRight)
    {
        movingRight = isMovingRight;
    }
}
