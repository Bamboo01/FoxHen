using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Bamboo.UI;

public class PlayerMovement : MonoBehaviour
{

    public float moveDist;
    public float MenuCD;
    private GameObject player;
    private Vector2 moveInputValue;
    //private PlayerInputActions inputActions;

    private void Start()
    {
        //c# delegates
        //note to rmb to generate class from input asset UI
        //inputActions = new PlayerInputActions();
        //inputActions.Player1_Keyboard.Enable();
        //inputActions.Player1_Keyboard.UseItem.performed += UseItem;

        MenuCD = 0f;
        player = gameObject;
        moveInputValue = Vector2.zero;
    }

    private void Update()
    {
        if (GameManager.Instance.currScene == GameManager.sceneNames.MainMenu)
        {
            MainMenuUpdate();
            return;
        }
        TransformUpdate();
    }

    private void TransformUpdate()
    {
        player.transform.position += new Vector3(moveInputValue.x, 0, moveInputValue.y) * moveDist * Time.deltaTime;
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

    public void onUseItem(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Started) //phase start
        {

        }
        else if (context.phase == InputActionPhase.Performed)//phase performing
        {
            //use item
        }
        else if (context.phase == InputActionPhase.Canceled) //phase end
        {

        }
    }

    public void onMoveInput(InputAction.CallbackContext context)
    {
        moveInputValue = context.ReadValue<Vector2>();
    }

    #endregion
}