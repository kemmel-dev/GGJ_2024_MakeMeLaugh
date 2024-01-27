using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    Vector2 screenBounds;
    public float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * currentSpeed * Time.deltaTime);

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
        bool isVisible = viewportPosition.x >= -1.5 && viewportPosition.x <= 1.5 && viewportPosition.y >= -0.2 && viewportPosition.y <= 1.5;
        return isVisible;
    }
}

