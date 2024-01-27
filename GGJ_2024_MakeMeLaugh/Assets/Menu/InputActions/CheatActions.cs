//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Menu/InputActions/CheatActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @CheatActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CheatActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CheatActions"",
    ""maps"": [
        {
            ""name"": ""actions"",
            ""id"": ""efeb872e-8cea-4b0d-b8d4-ee0af3ec299b"",
            ""actions"": [
                {
                    ""name"": ""ExtraJoin"",
                    ""type"": ""Button"",
                    ""id"": ""3ff2107e-d126-4467-b36e-084f623df5df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SetAllPlayersToReady"",
                    ""type"": ""Button"",
                    ""id"": ""4a74b2de-53e6-4c2f-be8b-93f943d19b68"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6dc031be-d725-4df6-a270-7c952d785a9b"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExtraJoin"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1984bd30-48d6-49b3-b748-b2025b30555d"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SetAllPlayersToReady"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // actions
        m_actions = asset.FindActionMap("actions", throwIfNotFound: true);
        m_actions_ExtraJoin = m_actions.FindAction("ExtraJoin", throwIfNotFound: true);
        m_actions_SetAllPlayersToReady = m_actions.FindAction("SetAllPlayersToReady", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // actions
    private readonly InputActionMap m_actions;
    private List<IActionsActions> m_ActionsActionsCallbackInterfaces = new List<IActionsActions>();
    private readonly InputAction m_actions_ExtraJoin;
    private readonly InputAction m_actions_SetAllPlayersToReady;
    public struct ActionsActions
    {
        private @CheatActions m_Wrapper;
        public ActionsActions(@CheatActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @ExtraJoin => m_Wrapper.m_actions_ExtraJoin;
        public InputAction @SetAllPlayersToReady => m_Wrapper.m_actions_SetAllPlayersToReady;
        public InputActionMap Get() { return m_Wrapper.m_actions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionsActions set) { return set.Get(); }
        public void AddCallbacks(IActionsActions instance)
        {
            if (instance == null || m_Wrapper.m_ActionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ActionsActionsCallbackInterfaces.Add(instance);
            @ExtraJoin.started += instance.OnExtraJoin;
            @ExtraJoin.performed += instance.OnExtraJoin;
            @ExtraJoin.canceled += instance.OnExtraJoin;
            @SetAllPlayersToReady.started += instance.OnSetAllPlayersToReady;
            @SetAllPlayersToReady.performed += instance.OnSetAllPlayersToReady;
            @SetAllPlayersToReady.canceled += instance.OnSetAllPlayersToReady;
        }

        private void UnregisterCallbacks(IActionsActions instance)
        {
            @ExtraJoin.started -= instance.OnExtraJoin;
            @ExtraJoin.performed -= instance.OnExtraJoin;
            @ExtraJoin.canceled -= instance.OnExtraJoin;
            @SetAllPlayersToReady.started -= instance.OnSetAllPlayersToReady;
            @SetAllPlayersToReady.performed -= instance.OnSetAllPlayersToReady;
            @SetAllPlayersToReady.canceled -= instance.OnSetAllPlayersToReady;
        }

        public void RemoveCallbacks(IActionsActions instance)
        {
            if (m_Wrapper.m_ActionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IActionsActions instance)
        {
            foreach (var item in m_Wrapper.m_ActionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ActionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ActionsActions @actions => new ActionsActions(this);
    public interface IActionsActions
    {
        void OnExtraJoin(InputAction.CallbackContext context);
        void OnSetAllPlayersToReady(InputAction.CallbackContext context);
    }
}
