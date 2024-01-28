using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    public GameObject parent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            Destroy(collision.gameObject);
            parent.GetComponent<GTFPlayerBehaviour>().Die();
        }
    }
}
