using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallsackMovement : MonoBehaviour
{
    [SerializeField]
    private int speed = 10;
    public Transform player;
    public ApesHaveBallsacksManager manager;
    private bool inBallsackArea = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Transform>().parent.transform == manager.Ape.transform)
        {
            inBallsackArea = true;
            Debug.Log("in");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Transform>().parent.transform == manager.Ape.transform)
        {
            inBallsackArea = false;
            Debug.Log("gone");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inBallsackArea)
            {
                manager.m_MyEvent.Invoke();
            }
        }
    }

    public void Move()
    {

        Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"),  Input.GetAxis("Vertical"), 0);

        player.transform.position += Movement * speed * Time.deltaTime;
    }
}
