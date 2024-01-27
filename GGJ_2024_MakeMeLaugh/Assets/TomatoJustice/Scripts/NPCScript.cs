using UnityEngine;

public class NPCScript : MonoBehaviour
{
    private bool movingRight = true;
    [SerializeField] private float speed = 5f;

    void Update()
    {
        // Move the NPC in the correct direction
        transform.Translate((movingRight ? Vector3.right : Vector3.left) * speed * Time.deltaTime);


        // Destroy if off-screen
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("ProjectileTomJustice"))
        {
            // Increase player score
            PlayerScript.IncreaseScore(1); // Static method in PlayerScript to increase score
            Destroy(other.gameObject); // Destroy the tomato
            Destroy(gameObject); // Destroy the NPC
        }
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
