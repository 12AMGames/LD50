//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/_Scripts/Input/Input.inputactions
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

public partial class @Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""normal"",
            ""id"": ""c0113c2f-24e7-43a2-90fd-d923d3e62a2b"",
            ""actions"": [
                {
                    ""name"": ""MousePos"",
                    ""type"": ""Value"",
                    ""id"": ""46196ae6-47eb-4e14-b9f2-445135d3b550"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""8fa477d9-d8ef-4ba9-a837-8c5384a993d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""779edc9d-a51f-49a9-b029-c6cb7135468c"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4448fcc7-c0e7-453c-ad86-3b685bc59988"",
                    ""path"": ""<VirtualMouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6319d83-ea52-484f-a98c-3c8ae0624b96"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // normal
        m_normal = asset.FindActionMap("normal", throwIfNotFound: true);
        m_normal_MousePos = m_normal.FindAction("MousePos", throwIfNotFound: true);
        m_normal_Click = m_normal.FindAction("Click", throwIfNotFound: true);
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

    // normal
    private readonly InputActionMap m_normal;
    private INormalActions m_NormalActionsCallbackInterface;
    private readonly InputAction m_normal_MousePos;
    private readonly InputAction m_normal_Click;
    public struct NormalActions
    {
        private @Controls m_Wrapper;
        public NormalActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePos => m_Wrapper.m_normal_MousePos;
        public InputAction @Click => m_Wrapper.m_normal_Click;
        public InputActionMap Get() { return m_Wrapper.m_normal; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NormalActions set) { return set.Get(); }
        public void SetCallbacks(INormalActions instance)
        {
            if (m_Wrapper.m_NormalActionsCallbackInterface != null)
            {
                @MousePos.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnMousePos;
                @MousePos.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnMousePos;
                @MousePos.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnMousePos;
                @Click.started -= m_Wrapper.m_NormalActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_NormalActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_NormalActionsCallbackInterface.OnClick;
            }
            m_Wrapper.m_NormalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePos.started += instance.OnMousePos;
                @MousePos.performed += instance.OnMousePos;
                @MousePos.canceled += instance.OnMousePos;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
            }
        }
    }
    public NormalActions @normal => new NormalActions(this);
    public interface INormalActions
    {
        void OnMousePos(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
    }
}
