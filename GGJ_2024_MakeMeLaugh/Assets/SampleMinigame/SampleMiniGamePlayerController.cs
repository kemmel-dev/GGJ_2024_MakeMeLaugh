using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SampleMiniGamePlayerController : MiniGamePlayerController
{
	private Vector2 _LeftStickInput;
	private Rigidbody2D _Rigidbody;

	public List<PigBehaviour> GrabbablePigs = new();

	public override void Initialize(PlayerController playerController)
	{
		base.Initialize(playerController);

		playerController.LeftStick += PlayerControllerOnLeftStick;
		playerController.SouthButton += PlayerControllerOnSouthButton;
	}

	private void PlayerControllerOnLeftStick(InputAction.CallbackContext ctx)
	{
		if (ctx.started || ctx.performed)
		{
			_LeftStickInput = ctx.ReadValue<Vector2>();
		}

		if (ctx.canceled)
		{
			_LeftStickInput = Vector2.zero;
		}
	}

	private void PlayerControllerOnSouthButton(InputAction.CallbackContext ctx)
	{
		if (!ctx.performed) return;
		Debug.Log($"collect");
		if (GrabbablePigs.Count <= 0) return;
		GrabbablePigs[0].transform.SetParent(transform);
		GrabbablePigs[0].Collect();
	}

	private void Start()
	{
		_Rigidbody = GetComponent<Rigidbody2D>();
	}


	public void Update()
	{
		_Rigidbody.velocity = _LeftStickInput;
	}

	private void OnDestroy()
	{
		PlayerControllerReference.LeftStick -= PlayerControllerOnLeftStick;
		PlayerControllerReference.SouthButton -= PlayerControllerOnSouthButton;
	}
}
