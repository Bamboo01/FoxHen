using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using Bamboo.UI;

namespace FoxHen
{
    public class PlayerController : MonoBehaviour
    {
        static int playerIDCounter = 0;

        [SerializeField] PlayerAnimator playerAnimator;
        [SerializeField] SpriteRenderer playerSprite;
        public SpriteRenderer playerIndicatorSprite;
        [SerializeField] public PlayerStatus playerStatus { get; private set; }
        [SerializeField] public PlayerData playerData { get; private set; }
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private Rigidbody2D rigidbody;

        public int playerID { get; private set;}

        void Awake()
        {
            playerID = playerIDCounter;
            playerIDCounter++;
        }

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
            playerStatus = GetComponent<PlayerStatus>();
            playerData = GetComponent<PlayerData>();
            playerInventory = GetComponent<PlayerInventory>();
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
            if(rigidbody.velocity.magnitude > Mathf.Epsilon)
            {
                playerData.lastMoveDirection = rigidbody.velocity.normalized;
            }
            rigidbody.velocity = new Vector3(playerData.moveInputValue.x, playerData.moveInputValue.y, 0) * playerData.moveSpeed;
        }

        #region InputCallbacks

        public void OnUseItem(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)//phase performing
            {
                playerAnimator.PickupItem();
                playerInventory.UseItem();
            }
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
           playerData.moveInputValue = context.ReadValue<Vector2>();
        }

        public void ResetMovementInput(InputAction.CallbackContext context)
        {
            playerData.moveInputValue = Vector2.zero;
        }

        #endregion
    }
}
