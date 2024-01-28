using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
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
    public int amountDodged = 0;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    private float objectWidth;
    private float objectHeight;

    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);
        playerController.LeftTrigger += PlayerControllerOnLeftTrigger;
        playerController.RightTrigger += PlayerControllerOnRightTrigger;
        body.transform.GetComponent<SpriteRenderer>().color = playerController.PlayerData.color;
        SetupJester();
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
        amountDodged--;
        mgManager.PlayerScoreTexts[PlayerControllerReference.PlayerIndex].text = amountDodged.ToString();
        mgManager.PlaySound(clip, volume);
        spawner.SetActive(false);
        mgManager.GetComponent<GetTriggerFingeredManager>().AddScore(PlayerControllerReference);
        gameObject.SetActive(false);

    }
    public void Dodged()
    {
        amountDodged++;
        mgManager.PlayerScoreTexts[PlayerControllerReference.PlayerIndex].text = amountDodged.ToString();
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
    private void SetupJester()
    {
        var jester = Instantiate(PlayerControllerReference.PlayerData.playerModel, body.transform);
        var coll = jester.GetComponentInChildren<MeshFilter>().AddComponent<BoxCollider2D>();
        objectWidth = coll.bounds.size.x;
        objectHeight = coll.bounds.size.y;
        jester.transform.localPosition = Vector3.zero;
        jester.transform.localScale = Vector3.one * 1.5f;
        jester.transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
