using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GTFPlayerBehaviour : MiniGamePlayerController
{
    public TextMeshProUGUI timerText;
    public GameObject leftPoint;
    public GameObject rightPoint;
    public GameObject spawner;
    public GetTriggerFingeredManager mgManager;
    public GameObject body;
    public int CountDownTime = 5;
    public AudioSource audioData;

    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);
        playerController.LeftTrigger += PlayerControllerOnLeftTrigger;
        playerController.RightTrigger += PlayerControllerOnRightTrigger;
        body.transform.GetComponent<SpriteRenderer>().color = playerController.PlayerData.color;

    }
    void Start()
    {
        
        body.transform.position = leftPoint.transform.position;
        mgManager = GameObject.FindObjectOfType<GetTriggerFingeredManager>();
        timerText = mgManager.timerText;
        timerText.text = CountDownTime.ToString();
        StartCoroutine(CountDown());
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

        mgManager.PlaySound();
        spawner.SetActive(false);
        mgManager.GetComponent<GetTriggerFingeredManager>().AddScore(PlayerControllerReference);
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
    private IEnumerator CountDown()
    {
        for (int i = 0; i < CountDownTime; i++)
        {
            yield return new WaitForSeconds(1);
            timerText.text = (CountDownTime - i).ToString();
        }
        yield return new WaitForSeconds(1);
        timerText.text = "GO";
        yield return new WaitForSeconds(1);
        GameManager.Instance.ActivateInput();
        spawner.GetComponent<SpawnerBehaviour>().Starter();
        timerText.gameObject.SetActive(false);
        StopCoroutine(CountDown());
    }
}
