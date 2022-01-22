// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player1_Keyboard"",
            ""id"": ""24aae9c1-a008-403f-972f-0e933b9e303e"",
            ""actions"": [
                {
                    ""name"": ""UseItem"",
                    ""type"": ""Button"",
                    ""id"": ""c45caad5-aefd-4201-b8b6-f511834514cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""57045f66-fa2f-4a35-9773-0f5320e64234"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8bb7017a-f45d-4661-89e9-f6ea8f8585b0"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8ed6c4b-a53c-4448-8349-5e4068b0a67f"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58c12846-713c-4c1c-8376-5592b8d56a92"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""cad6150f-9354-4c49-990b-0803f5942c2f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4aae0214-2e26-44af-a4c3-864068b386ee"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""368fb6cf-3bb2-4d47-b706-d9842b97dcf6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""da5cdc9c-8fba-46c6-88c9-f13a4e158f7f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""98fd7e37-3ae6-4c90-aba0-aba2d781c86c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fc38e065-5ad8-432e-bbff-643a0ecc8f79"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6a32936-fd36-44a5-8ee1-fa0b4df10b79"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<HID::Logitech Logitech Dual Action>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player1_Keyboard
        m_Player1_Keyboard = asset.FindActionMap("Player1_Keyboard", throwIfNotFound: true);
        m_Player1_Keyboard_UseItem = m_Player1_Keyboard.FindAction("UseItem", throwIfNotFound: true);
        m_Player1_Keyboard_Movement = m_Player1_Keyboard.FindAction("Movement", throwIfNotFound: true);
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

    // Player1_Keyboard
    private readonly InputActionMap m_Player1_Keyboard;
    private IPlayer1_KeyboardActions m_Player1_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Player1_Keyboard_UseItem;
    private readonly InputAction m_Player1_Keyboard_Movement;
    public struct Player1_KeyboardActions
    {
        private @PlayerInputActions m_Wrapper;
        public Player1_KeyboardActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @UseItem => m_Wrapper.m_Player1_Keyboard_UseItem;
        public InputAction @Movement => m_Wrapper.m_Player1_Keyboard_Movement;
        public InputActionMap Get() { return m_Wrapper.m_Player1_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1_KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IPlayer1_KeyboardActions instance)
        {
            if (m_Wrapper.m_Player1_KeyboardActionsCallbackInterface != null)
            {
                @UseItem.started -= m_Wrapper.m_Player1_KeyboardActionsCallbackInterface.OnUseItem;
                @UseItem.performed -= m_Wrapper.m_Player1_KeyboardActionsCallbackInterface.OnUseItem;
                @UseItem.canceled -= m_Wrapper.m_Player1_KeyboardActionsCallbackInterface.OnUseItem;
                @Movement.started -= m_Wrapper.m_Player1_KeyboardActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_Player1_KeyboardActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_Player1_KeyboardActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_Player1_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @UseItem.started += instance.OnUseItem;
                @UseItem.performed += instance.OnUseItem;
                @UseItem.canceled += instance.OnUseItem;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public Player1_KeyboardActions @Player1_Keyboard => new Player1_KeyboardActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayer1_KeyboardActions
    {
        void OnUseItem(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
    }
}
