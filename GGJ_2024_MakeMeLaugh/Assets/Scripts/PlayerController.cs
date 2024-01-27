using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public PlayerData PlayerData;

	public event Action<InputAction.CallbackContext> LeftStick;

	public event Action<InputAction.CallbackContext> NorthButton;
	public event Action<InputAction.CallbackContext> SouthButton;
	public event Action<InputAction.CallbackContext> WestButton;
	public event Action<InputAction.CallbackContext> EastButton;

	public event Action<InputAction.CallbackContext> RightTrigger;
	public event Action<InputAction.CallbackContext> LeftTrigger;
	public event Action ReadyAction;
	public int PlayerIndex => GetComponent<PlayerInput>().playerIndex;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		PlayerData = GetComponent<PlayerData>();
		name = $"{name}{PlayerIndex}";
	}

	public void OnPlayerLeftStick(InputAction.CallbackContext ctx)
	{
		Debug.Log("tets");
		LeftStick?.Invoke(ctx);
	}
	public void OnPlayerNorthButton(InputAction.CallbackContext ctx)
	{
		NorthButton?.Invoke(ctx);
	}
	public void OnPlayerSouthButton(InputAction.CallbackContext ctx)
	{
		SouthButton?.Invoke(ctx);
	}
	public void OnPlayerWestButton(InputAction.CallbackContext ctx)
	{
		WestButton?.Invoke(ctx);
	}
	public void OnPlayerEastButton(InputAction.CallbackContext ctx)
	{
		EastButton?.Invoke(ctx);
	}
	public void OnPlayerRightTrigger(InputAction.CallbackContext ctx)
	{
		RightTrigger?.Invoke(ctx);
	}
	public void OnPlayerLeftTrigger(InputAction.CallbackContext ctx)
	{
		LeftTrigger?.Invoke(ctx);
	}
	public void OnPlayerReady(InputAction.CallbackContext ctx)
	{
		if (!ctx.performed) return;
		ReadyAction?.Invoke();
	}
	public void OnPlayerReady()
	{
		ReadyAction?.Invoke();
	}
}
