using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using Bamboo.UI;

namespace FoxHen
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] PlayerAnimator playerAnimator;
        [SerializeField] SpriteRenderer playerSprite;
        [SerializeField] public PlayerStatus playerStatus { get; private set; }
        [SerializeField] public PlayerData playerData { get; private set; }

        private GameObject player;
        private Vector2 moveInputValue;
        private PlayerInventory playerInventory;
        private Rigidbody2D rigidbody;

        private void Start()
        {
            //c# delegates
            ////note to rmb to generate class from input asset UI
            //inputActions = new PlayerInputActions();
            //PlayerInput input = new PlayerInput();
            //input.actions = inputActions.asset;
            //inputActions.Player1_Keyboard.Enable();
            //inputActions.Player1_Keyboard.UseItem.performed += OnUseItem;
            //inputActions.Player1_Keyboard.Movement.performed += OnMoveInput;
            //inputActions.Player1_Keyboard.Movement.canceled += ResetMovementInput;

            DontDestroyOnLoad(gameObject);
            player = gameObject;
            moveInputValue = Vector2.zero;
            playerStatus = GetComponent<PlayerStatus>();
            playerData = GetComponent<PlayerData>();
            playerInventory = GetComponent<PlayerInventory>();
            playerInventory.activateItemDelegate += ActivateItem;
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            TransformUpdate();
            playerAnimator.IsRunning(rigidbody.velocity.magnitude > 0.01f);
            Vector3 myScale = playerSprite.transform.localScale;

            if (Mathf.Abs(rigidbody.velocity.x) >= Mathf.Epsilon)
                myScale.x = (rigidbody.velocity.x > 0) ? Mathf.Abs(myScale.x) : Mathf.Abs(myScale.x) * -1.0f;

            playerSprite.transform.localScale = myScale;
            return;
        }

        private void TransformUpdate()
        {
            rigidbody.velocity = new Vector3(moveInputValue.x, moveInputValue.y, 0) * playerData.moveSpeed;
        }

        #region InputCallbacks

        public void OnUseItem(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)//phase performing
            {
                playerAnimator.PickupItem();
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
