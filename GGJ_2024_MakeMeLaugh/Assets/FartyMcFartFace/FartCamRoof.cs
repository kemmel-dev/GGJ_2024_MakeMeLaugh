using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartCamRoof : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (transform.position.y <= collision.transform.position.y)
        {
            transform.position = new Vector2(transform.position.x, collision.transform.position.y);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y + 10, Camera.main.transform.position.z);
        }
    }
}
