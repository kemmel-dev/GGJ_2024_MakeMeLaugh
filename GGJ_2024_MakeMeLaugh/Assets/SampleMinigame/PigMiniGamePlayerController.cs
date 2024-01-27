using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PigMiniGamePlayerController : MiniGamePlayerController
{
	public int Score = 0;
	private Vector2 _LeftStickInput;
	private Rigidbody2D _Rigidbody;
	private Camera mainCamera;
	private Vector2 screenBounds;
	private float objectWidth;
	private float objectHeight;



	public float PlayerSpeed = 10;

	public BasketBehaviour BasketPrefab;
	public BasketBehaviour Basket;

	public List<PigBehaviour> GrabbablePigs = new();
	public PigBehaviour CurrentPig = null;
	public bool HasPig => CurrentPig != null;

	public override void Initialize(PlayerController playerController)
	{
		base.Initialize(playerController);
		//transform.GetComponent<SpriteRenderer>().color = playerController.PlayerData.color;
		playerController.LeftStick += PlayerControllerOnLeftStick;
		playerController.SouthButton += PlayerControllerOnSouthButton;

		mainCamera = Camera.main;
		screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

		SetupJester();

		Basket = Instantiate(BasketPrefab, Vector3.one * 1000, Quaternion.identity);
		Basket.playerIndex = playerController.PlayerIndex;
		Basket.GetComponent<SpriteRenderer>().color = playerController.PlayerData.color;


		var basketWidth = Basket.GetComponent<SpriteRenderer>().bounds.extents.x; // Extents = size of width / 2
		var basketHeight = Basket.GetComponent<SpriteRenderer>().bounds.extents.y; // Extents = size of height / 2
		switch (playerController.PlayerIndex)
		{
			case 0:
				transform.position = new Vector3(screenBounds.x * -1 + objectWidth, screenBounds.y - objectHeight, 0);
				Basket.transform.position = new Vector3(screenBounds.x * -1 + basketWidth, screenBounds.y - basketHeight, 0);
				break;
			case 1:
				transform.position = new Vector3(screenBounds.x - objectWidth, screenBounds.y - objectHeight, 0);
				Basket.transform.position = new Vector3(screenBounds.x - basketWidth, screenBounds.y - basketHeight, 0);
				break;
			case 2:
				transform.position = new Vector3(screenBounds.x - objectWidth, screenBounds.y * -1 + objectHeight, 0);
				Basket.transform.position = new Vector3(screenBounds.x - basketWidth, screenBounds.y * -1 + basketHeight, 0);
				break;
			case 3:
				transform.position = new Vector3(screenBounds.x * -1 + objectWidth, screenBounds.y * -1 + objectHeight, 0);
				Basket.transform.position = new Vector3(screenBounds.x * -1 + basketWidth, screenBounds.y * -1 + basketHeight, 0);
				break;
		}
	}

	private void SetupJester()
	{
		var jester = Instantiate(PlayerControllerReference.PlayerData.playerModel, transform);
		var coll = jester.GetComponentInChildren<MeshFilter>().AddComponent<BoxCollider2D>();
		objectWidth = coll.bounds.size.x;
		objectHeight = coll.bounds.size.y;
		jester.transform.localPosition = Vector3.zero;
		jester.transform.localScale = Vector3.one * 1.5f;
		jester.transform.localRotation = Quaternion.Euler(0, 180, 0);
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
		GrabbablePigs.Remove(CurrentPig);
		CurrentPig.Collect();
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
		_Rigidbody = GetComponent<Rigidbody2D>();
	}
	public void Update()
	{
		_Rigidbody.velocity = _LeftStickInput * PlayerSpeed;
	}

	void LateUpdate()
	{
		Vector3 viewPos = transform.position;
		viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
		viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
		transform.position = viewPos;
	}

	private void OnDestroy()
	{
		PlayerControllerReference.LeftStick -= PlayerControllerOnLeftStick;
		PlayerControllerReference.SouthButton -= PlayerControllerOnSouthButton;
	}
}
