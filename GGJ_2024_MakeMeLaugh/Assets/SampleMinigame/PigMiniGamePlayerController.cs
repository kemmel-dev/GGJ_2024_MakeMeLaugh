using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PigMiniGamePlayerController : MiniGamePlayerController
{
	public int Score = 0;
	private Vector2 _LeftStickInput;
	private Rigidbody _Rigidbody;
	public List<GameObject> GameObjectToSetLayer = new List<GameObject>();
	public float PlayerSpeed = 10;

	public List<PigBehaviour> GrabbablePigs = new();
	public PigBehaviour CurrentPig = null;
	public bool HasPig => CurrentPig != null;

	public override void Initialize(PlayerController playerController)
	{
		base.Initialize(playerController);
		playerController.LeftStick += PlayerControllerOnLeftStick;
		playerController.SouthButton += PlayerControllerOnSouthButton;

		SetupJester();
	}

	private void SetupJester()
	{
		var jester = Instantiate(PlayerControllerReference.PlayerData.playerModel, transform);
		jester.GetComponentInChildren<MeshFilter>().AddComponent<BoxCollider>();
		var layermask = LayerMask.NameToLayer($"p{PlayerControllerReference.PlayerIndex + 1}");
		transform.gameObject.layer = layermask;

		//can you add all children of this gameobject to a list recusive
		//then set all of them to the layermask
		foreach (var transform in GetComponentsInChildren<Transform>())
		{
			//var layermask = LayerMask.NameToLayer($"p{PlayerControllerReference.PlayerIndex + 1}");
			transform.gameObject.layer = layermask;
		}

		SetLayerMasks(jester.transform);
		jester.layer = layermask;
		jester.transform.localPosition = Vector3.zero;
		jester.transform.localScale = Vector3.one * 1.5f;
		jester.transform.localRotation = Quaternion.Euler(0, 180, 0);
	}

	private void SetLayerMasks(Transform transform)
	{
		foreach (var obj in GameObjectToSetLayer)
		{
			var layermask = LayerMask.NameToLayer($"p{PlayerControllerReference.PlayerIndex + 1}");
			obj.layer = layermask;
			//transform.gameObject.layer = LayerMask.GetMask($"p{PlayerControllerReference.PlayerIndex + 1}");
		}
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
		if (GrabbablePigs.Count <= 0 || HasPig) return;
		CurrentPig = GrabbablePigs[0];
		CurrentPig.transform.SetParent(transform);
		CurrentPig.gameObject.layer = LayerMask.NameToLayer($"p{PlayerControllerReference.PlayerIndex + 1}");
		GrabbablePigs.Remove(CurrentPig);


		CurrentPig.Collect(PlayerControllerReference);
	}

	public void DestroyPig()
	{
		Score += CurrentPig.Points;
		Destroy(CurrentPig.gameObject);
		CurrentPig = null;
		PigSpawner.Instance.RemovePig();
	}

	private void Start()
	{
		_Rigidbody = GetComponent<Rigidbody>();
	}
	public void Update()
	{
		_Rigidbody.velocity = new Vector3(_LeftStickInput.x, 0, _LeftStickInput.y) * PlayerSpeed;
	}

	private void OnDestroy()
	{
		PlayerControllerReference.LeftStick -= PlayerControllerOnLeftStick;
		PlayerControllerReference.SouthButton -= PlayerControllerOnSouthButton;
	}
}
