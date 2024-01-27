using UnityEngine;

public class NPCScript : MonoBehaviour
{
    private bool movingRight = true;
    [SerializeField] private float speed = 5f;
    private bool FriendlyFire;

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
