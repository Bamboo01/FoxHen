using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using Bamboo.UI;

namespace FoxHen
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerAttributes playerAttrib { get; private set; }
        public float menuCD;
        private GameObject player;
        private Vector2 moveInputValue;
        private PlayerInputActions inputActions;
        private PlayerInventory playerInventory;
        private Rigidbody2D rigidbody;

        private void Start()
        {
            //c# delegates
            //note to rmb to generate class from input asset UI
            inputActions = new PlayerInputActions();
            PlayerInput input = new PlayerInput();
            input.actions = inputActions.asset;
            inputActions.Player1_Keyboard.Enable();
            inputActions.Player1_Keyboard.UseItem.performed += OnUseItem;
            inputActions.Player1_Keyboard.Movement.performed += OnMoveInput;
            inputActions.Player1_Keyboard.Movement.canceled += ResetMovementInput;

            menuCD = 0f;
            player = gameObject;
            moveInputValue = Vector2.zero;
            playerAttrib = GetComponent<PlayerAttributes>();
            playerInventory = GetComponent<PlayerInventory>();
            playerInventory.activateItemDelegate += ActivateItem;
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            TransformUpdate();
            return;
        }

        private void TransformUpdate()
        {
            rigidbody.velocity = new Vector3(moveInputValue.x, moveInputValue.y, 0) * playerAttrib.moveSpeed;
        }

        #region MainMenu

        private void MainMenuUpdate()
        {
            bool Up = false;
            if (Mathf.Abs(moveInputValue.y) > 0.85f)
            {
                if (moveInputValue.y > 0)
                    Up = true;

                MainMenuManagerPLUS.Instance.playerMenuInput(Up, gameObject);
            }
        }

        #endregion

        #region InputCallbacks

        public void OnUseItem(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)//phase performing
            {
                playerInventory.UseItem();
                Debug.Log("HI");
            }
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            moveInputValue = context.ReadValue<Vector2>();
        }

        public void ResetMovementInput(InputAction.CallbackContext context)
        {
            moveInputValue = Vector2.zero;
        }

        #endregion

        public void ActivateItem(ItemType item)
        {
            Debug.Log("Activated" + item.ToString());
        }
        
    }
}