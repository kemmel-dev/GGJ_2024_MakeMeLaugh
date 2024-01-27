using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GTFPlayerBehaviour : MiniGamePlayerController
{
    public GameObject leftPoint;
    public GameObject rightPoint;
    public GameObject spawner;
    public GetTriggerFingeredManager mgManager;
    public GameObject body;

    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);
        playerController.LeftTrigger += PlayerControllerOnLeftTrigger;
        playerController.RightTrigger += PlayerControllerOnRightTrigger;
    }
    void Start()
    {
        GameManager.Instance.ActivateInput();
        body.transform.position = leftPoint.transform.position;
        mgManager = GameObject.FindObjectOfType<GetTriggerFingeredManager>();
    }

    public void MoveLeft()
    {

        if (body.transform.position != leftPoint.transform.position)
        {
            body.transform.position = leftPoint.transform.position;
        }
        else return;
    }

    public void MoveRight()
    {
        if (body.transform.position != rightPoint.transform.position)
        {
            body.transform.position = rightPoint.transform.position;
        }
        else return;
    }

    public void Die()
    {
        spawner.SetActive(false);
        //Destroy(spawner);
        mgManager.GetComponent<GetTriggerFingeredManager>().AddScore(PlayerControllerReference);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    private void PlayerControllerOnLeftTrigger(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            MoveLeft();
        }
        
    }

    private void PlayerControllerOnRightTrigger(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            MoveRight();
        }
        
    }

    private void OnDestroy()
    {
        PlayerControllerReference.LeftTrigger -= PlayerControllerOnLeftTrigger;
        PlayerControllerReference.RightTrigger -= PlayerControllerOnRightTrigger;
    }
}
