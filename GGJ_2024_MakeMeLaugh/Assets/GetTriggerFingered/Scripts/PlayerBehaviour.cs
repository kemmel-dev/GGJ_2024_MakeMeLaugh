using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject leftPoint;
    public GameObject rightPoint;
    public GameObject spawner;
    public GameObject mgManager;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = leftPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }

    }

    public void MoveLeft()
    {
        if (gameObject.transform.position != leftPoint.transform.position)
        {
            gameObject.transform.position = leftPoint.transform.position;
        }
        else return;
    }

    public void MoveRight()
    {
        if (gameObject.transform.position != rightPoint.transform.position)
        {
            gameObject.transform.position = rightPoint.transform.position;
        }
        else return;
    }

    public void Die()
    {

        Destroy(spawner);
        mgManager.GetComponent<GetTriggerFingeredManager>().AddScore(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            Die();
        }
    }
}
