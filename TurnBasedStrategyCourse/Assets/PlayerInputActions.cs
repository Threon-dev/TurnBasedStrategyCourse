//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""3109e875-5424-485c-91de-c7ec58dc8502"",
            ""actions"": [
                {
                    ""name"": ""CameraMovement"",
                    ""type"": ""Value"",
                    ""id"": ""28dc82e2-ebd2-47d6-b27d-575dbb061984"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraRotate"",
                    ""type"": ""Value"",
                    ""id"": ""eb0fac9e-dc79-49fd-9aa4-9cfaac1ad693"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraZoom"",
                    ""type"": ""Value"",
                    ""id"": ""7863fd99-a958-4421-991e-091d968c598a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""c1b9297a-99b0-46b7-9b64-1401d43661d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b7b7900b-b2c1-40f5-a79e-290950aab370"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f65b37e1-f1b1-480b-acf5-cd219de1c9c4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""95b84e6e-bcf6-4748-a4c5-cf6e7f92643a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""dc2b5394-1570-4031-87a1-e8f45dc75589"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bf8b50c4-45f6-4f4b-b241-8abcc194b606"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""5974b406-8c8e-426c-90bc-6fa1784d8a2d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e34a31b4-69f1-4451-bbb6-a970d8758020"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3cd9914e-72c9-4f38-b581-f0bb0612b38a"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""15e1e90e-d3bf-4328-9594-7b57963efe0d"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Invert,Clamp(min=-1,max=1)"",
                    ""groups"": """",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd18118f-79da-423f-a23f-ab3e0b23a0c0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_CameraMovement = m_Player.FindAction("CameraMovement", throwIfNotFound: true);
        m_Player_CameraRotate = m_Player.FindAction("CameraRotate", throwIfNotFound: true);
        m_Player_CameraZoom = m_Player.FindAction("CameraZoom", throwIfNotFound: true);
        m_Player_Click = m_Player.FindAction("Click", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_CameraMovement;
    private readonly InputAction m_Player_CameraRotate;
    private readonly InputAction m_Player_CameraZoom;
    private readonly InputAction m_Player_Click;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @CameraMovement => m_Wrapper.m_Player_CameraMovement;
        public InputAction @CameraRotate => m_Wrapper.m_Player_CameraRotate;
        public InputAction @CameraZoom => m_Wrapper.m_Player_CameraZoom;
        public InputAction @Click => m_Wrapper.m_Player_Click;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @CameraMovement.started += instance.OnCameraMovement;
            @CameraMovement.performed += instance.OnCameraMovement;
            @CameraMovement.canceled += instance.OnCameraMovement;
            @CameraRotate.started += instance.OnCameraRotate;
            @CameraRotate.performed += instance.OnCameraRotate;
            @CameraRotate.canceled += instance.OnCameraRotate;
            @CameraZoom.started += instance.OnCameraZoom;
            @CameraZoom.performed += instance.OnCameraZoom;
            @CameraZoom.canceled += instance.OnCameraZoom;
            @Click.started += instance.OnClick;
            @Click.performed += instance.OnClick;
            @Click.canceled += instance.OnClick;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @CameraMovement.started -= instance.OnCameraMovement;
            @CameraMovement.performed -= instance.OnCameraMovement;
            @CameraMovement.canceled -= instance.OnCameraMovement;
            @CameraRotate.started -= instance.OnCameraRotate;
            @CameraRotate.performed -= instance.OnCameraRotate;
            @CameraRotate.canceled -= instance.OnCameraRotate;
            @CameraZoom.started -= instance.OnCameraZoom;
            @CameraZoom.performed -= instance.OnCameraZoom;
            @CameraZoom.canceled -= instance.OnCameraZoom;
            @Click.started -= instance.OnClick;
            @Click.performed -= instance.OnClick;
            @Click.canceled -= instance.OnClick;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnCameraMovement(InputAction.CallbackContext context);
        void OnCameraRotate(InputAction.CallbackContext context);
        void OnCameraZoom(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
    }
}