using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private TomatoJusticePlayerController playerController;
    private bool goingUp = true;
    [SerializeField]
    private AudioSource hitSound;
    private bool canCheckVisibility = false;
    void Start()
    {
        StartCoroutine(DelayVisibilityCheck()); // Add this line
    }
    // Add this coroutine method
    private IEnumerator DelayVisibilityCheck()
    {
        yield return new WaitForSeconds(2f);
        canCheckVisibility = true;
    }
    // Add a method to set the player controller reference
    public void SetPlayerController(TomatoJusticePlayerController controller)
    {
        playerController = controller;
        Debug.Log("SetGoingDown0???" + goingUp);
    }

    void FixedUpdate()
    {
        Debug.Log("FixedUpdate called. Going up? " + goingUp);
        if (goingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            Debug.Log("goingup");
        }
        else if (!goingUp) 
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            Debug.Log("goingDOWN");
        }
        // Destroy if off-screen
        if (canCheckVisibility && !IsVisibleFromCamera())
        {
            Destroy(gameObject);
        }

        Debug.Log("SetFixedUpdate" + goingUp);
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
            // Always destroy the tomato
            Destroy(gameObject);
        }
        // Check if the entering object has a name that starts with "Enemy"
        if (other.gameObject.name.StartsWith("Friend"))
        {
            Debug.Log("ERRRORRRSETGOINGDOWN!!");
            hitSound.Play(); // plays whoosh throwing sound!
            if (playerController != null)
            {
                goingUp = false;
             
                
            }
        }
        if (other.CompareTag("Player") && !goingUp)
        {
           
            if (playerController != null)
            {
                playerController.DecreaseScore(1);
            }
            // Always destroy the tomato
            Destroy(gameObject);
        }
       
     
    }
}