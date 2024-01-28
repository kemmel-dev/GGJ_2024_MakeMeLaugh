using UnityEngine;

public class RotatingAndScalingObject : MonoBehaviour
{
    private Vector3 rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        // Generate a random rotation speed for each axis
        rotationSpeed = new Vector3(
            Random.Range(50f, 90f), // Speed for the x-axis
            Random.Range(50f, 90f), // Speed for the y-axis
            Random.Range(50f, 90f)  // Speed for the z-axis
        );

       
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around each axis independently
        transform.Rotate(
            rotationSpeed.x * Time.deltaTime,
            rotationSpeed.y * Time.deltaTime,
            rotationSpeed.z * Time.deltaTime,
            Space.World // Rotate in world space for a "cooler" effect
        );

       
    }
}
