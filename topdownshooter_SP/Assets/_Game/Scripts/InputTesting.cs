using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputTesting : MonoBehaviour
{

    
    PlayerInputHandler playerInputHandler;

    private void Awake()
    {
        playerInputHandler = new PlayerInputHandler();
        playerInputHandler.PlayerInputActions.Enable();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        Vector2 inputDirection = playerInputHandler.PlayerInputActions.PlayerMovement.ReadValue<Vector2>();
        Debug.Log("x : " + inputDirection.x + "y : " + inputDirection.y);
    }

    private void Movement_Performed(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        //Vector2 inputDirection = context.ReadValue<Vector2>();
        
    }

}



























