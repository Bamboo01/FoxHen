using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public GameObject player;
    public float moveDist;
    private PlayerInputActions inputActions;

    private void Start()
    {
        //c# delegates
        //note to rmb to generate class from input asset UI
        inputActions = new PlayerInputActions();
        inputActions.Player1_Keyboard.Enable();
        inputActions.Player1_Keyboard.UseItem.performed += UseItem;
    }

    private void Update()
    {
        Vector2 inputvalue = inputActions.Player1_Keyboard.Movement.ReadValue<Vector2>();
        player.transform.position += new Vector3(inputvalue.x, 0, inputvalue.y) * moveDist * Time.deltaTime;
    }

    public void UseItem(InputAction.CallbackContext context)
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
}