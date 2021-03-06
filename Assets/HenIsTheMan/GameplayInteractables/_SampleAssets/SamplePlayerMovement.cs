namespace FoxHen {
	using UniRx;
	using UnityEngine;
    using UnityEngine.InputSystem;

    namespace FoxHen {
        public class SamplePlayerMovement: MonoBehaviour {
            public float moveDist;
            public float menuCD;
            private Vector2 moveInputValue;
            private PlayerInputActions inputActions;

            [SerializeField]
            private Transform playerTransform;

            [SerializeField]
            private ParticleSystem lightningParticleSystem; //Idk where to put this lol

            private float savedSign = 1.0f;

            private void Start() {
                //c# delegates
                //note to rmb to generate class from input asset UI
                inputActions = new PlayerInputActions();
                inputActions.Player1_Keyboard.Enable();
                inputActions.Player1_Keyboard.UseItem.performed += OnUseItem;
                inputActions.Player1_Keyboard.Movement.performed += OnMoveInput;
                inputActions.Player1_Keyboard.Movement.canceled += ResetMovementInput;

                menuCD = 0f;
                moveInputValue = Vector2.zero;

                _ = Observable.EveryUpdate()
                    .Subscribe(_ => {
                        playerTransform.position += new Vector3(moveInputValue.x, moveInputValue.y, 0) * moveDist * Time.deltaTime;

                        if(!Mathf.Approximately(0.0f, moveInputValue.x)) {
                            savedSign = Mathf.Sign(moveInputValue.x);
                        }

                        lightningParticleSystem.gameObject.SetActive(
                            !Mathf.Approximately(0.0f, moveInputValue.x)
                            || !Mathf.Approximately(0.0f, moveInputValue.y)
                        );

                        playerTransform.localScale = new Vector3(
                            Mathf.Abs(playerTransform.localScale.x) * savedSign,
                            playerTransform.localScale.y,
                            playerTransform.localScale.z
                        );
                    })
                    .AddTo(this);
            }


            #region InputCallbacks

            public void OnUseItem(InputAction.CallbackContext context) {

                if(context.phase == InputActionPhase.Started) //phase start
                {

                } else if(context.phase == InputActionPhase.Performed)//phase performing
                  {
                    //use item
                } else if(context.phase == InputActionPhase.Canceled) //phase end
                  {

                }
            }

            public void OnMoveInput(InputAction.CallbackContext context) {
                moveInputValue = context.ReadValue<Vector2>();
            }

            public void ResetMovementInput(InputAction.CallbackContext context) {
                moveInputValue = Vector2.zero;
            }

            #endregion
        }
    }
}