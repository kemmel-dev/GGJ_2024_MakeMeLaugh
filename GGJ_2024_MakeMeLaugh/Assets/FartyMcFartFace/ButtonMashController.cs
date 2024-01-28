using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonMashController : MiniGamePlayerController
{
    public int amountOfButtonMashes = 0;
    public bool canMash;
    public Rigidbody2D rigidbody;
    public int fartMultiplier;

    private float objectWidth;
    private float objectHeight;

    public ParticleSystem particleSystem;

    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);

        SpriteRenderer spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = playerController.PlayerData.color;
        rigidbody = GetComponent<Rigidbody2D>();

        SetupJester();

        playerController.SouthButton += ButtonMashController_SouthButton;

        particleSystem = GetComponent<ParticleSystem>();
    }

    private void ButtonMashController_SouthButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ButtonMashed();
            FartBuildupInflate();
        }

        if (ctx.canceled)
        {
            FartBuildupDeflate();
        }
    }

    public void ButtonMashed()
    {
        amountOfButtonMashes++;
    }

    public void FartBuildupInflate()
    {
        transform.localScale *= 1.025f;
    }
    
    public void FartBuildupDeflate()
    {
        transform.localScale *= 0.985f;
    }

    public void Fart()
    {
        var fartPower = amountOfButtonMashes * fartMultiplier;

        var main = particleSystem.main;
        main.duration = amountOfButtonMashes * 0.1f;

        if (fartPower != 0)
        {
            particleSystem.Play();
            Animator rumbleAnimator = GetComponentInChildren<Animator>();
            rumbleAnimator.SetTrigger("rumble");
        }

        rigidbody.AddForce(new Vector2(0, fartPower));
    }



    private void OnDestroy()
    {
        PlayerControllerReference.SouthButton -= ButtonMashController_SouthButton;
    }
    private void SetupJester()
    {
        var jester = Instantiate(PlayerControllerReference.PlayerData.playerModel, transform.GetChild(0));
        /*var coll = jester.GetComponentInChildren<MeshFilter>().AddComponent<BoxCollider2D>();
        objectWidth = coll.bounds.size.x;
        objectHeight = coll.bounds.size.y;*/

        jester.transform.localPosition = Vector3.zero;
        jester.transform.localScale = Vector3.one * 1.5f;
        jester.transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
