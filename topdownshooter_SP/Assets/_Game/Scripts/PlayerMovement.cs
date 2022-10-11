using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Serialized Variables or Components
    [SerializeField] private float playerWalkSpeed = 4f;
    [SerializeField] private float playerRunSpeed = 6f;
    [SerializeField] private float playerRotationSpeed = 10f;

    [SerializeField] private LayerMask mouseWorldLayerMask;
    [SerializeField] private Transform playerOrientation;

    #endregion

    #region Private Variables or Components
    private float playerSpeed;
    private bool isPlayerRunning;
    private float rotationSpeed;


    private PlayerInputHandler playerInputHandler;
    private CharacterController characterController;
    private Camera mainCamera;
    private float turnVelocity = 0.0f;

    #endregion

    #region Events/Delegates
    #endregion

    #region Monobehaviour Callbacks
    
    private void Awake()
    {
        playerInputHandler = new PlayerInputHandler();
        playerInputHandler.PlayerInputActions.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }
    #endregion

    private void PlayerMove()
    {
        Vector2 inputDirection = playerInputHandler.PlayerInputActions.PlayerMovement.ReadValue<Vector2>();
        Vector3 MoveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y).normalized;
        if(MoveDirection.magnitude >= 0.01f)
        {
            // Vector3 playerDirToCamera = (new Vector3(transform.position.x, transform.position.y, transform.position.z)-(Quaternion.Euler(0,mainCamera.transform.eulerAngles.y, 0f) * Vector3.forward)).normalized;
            // // playerOrientation.forward = playerDirToCamera;
            // Vector3 playerDirection = playerDirToCamera * MoveDirection.z + playerDirToCamera * MoveDirection.x;
            // //transform.forward = Vector3.Slerp(transform.forward, playerDirection.normalized, Time.deltaTime);
            // transform.forward = playerDirection;

            float targetAngle = Mathf.Atan2(MoveDirection.x, MoveDirection.z)*Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * playerRotationSpeed);
            transform.rotation = Quaternion.Euler(0, angle, 0f);
            
            Vector3 playerDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            GetComponent<Animator>().SetBool("IsMoving", true);
            characterController.Move(playerDirection * playerWalkSpeed * Time.deltaTime);
        }
        if(MoveDirection.magnitude == 0f)
        {
            GetComponent<Animator>().SetBool("IsMoving", false);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray,out hitInfo, float.MaxValue, mouseWorldLayerMask))
        {
            return hitInfo.point;
        }
        else{
            return Vector3.zero;
        }
    }

    #region Getters
    #endregion

    #region Setters
    #endregion
}
