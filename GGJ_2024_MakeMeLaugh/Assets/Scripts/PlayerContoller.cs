using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : MonoBehaviour
{
	public bool ready = false;

	public event Action<InputAction.CallbackContext> LeftStick;

	public event Action<InputAction.CallbackContext> NorthButton;
	public event Action<InputAction.CallbackContext> SouthButton;
	public event Action<InputAction.CallbackContext> WestButton;
	public event Action<InputAction.CallbackContext> EastButton;

	public event Action<InputAction.CallbackContext> RightTrigger;
	public event Action<InputAction.CallbackContext> LeftTrigger;

	public int PlayerIndex => GetComponent<PlayerInput>().playerIndex;

	private void Awake()
	{
		name = $"{name}{PlayerIndex}";
		LeftStick += (ctx) => Debug.Log($"{name}, leftStick");
		NorthButton += (ctx) => Debug.Log($"{name}, NorthButton");
		SouthButton += (ctx) => Debug.Log($"{name}, SouthButton");
		WestButton += (ctx) => Debug.Log($"{name}, WestButton");
		EastButton += (ctx) => Debug.Log($"{name}, EastButton");
		RightTrigger += (ctx) => Debug.Log($"{name}, RightTrigger");
		LeftTrigger += (ctx) => Debug.Log($"{name}, LeftTrigger");
	}


	public void OnPlayerLeftStick(InputAction.CallbackContext ctx)
	{
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

	private void OnDestroy()
	{
	}
}
