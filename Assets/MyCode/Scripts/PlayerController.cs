using System.Xml.XPath;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    InputAction moveAction;
    public float moveSpd;
    float gravity = 9.81f;

    InputAction lookAction;
    float sensitivity = 0.1f;
    float angleLock = 90;
    float xRotation;
    float yRotation;
    public Camera camera;


    public CharacterController playerController;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

    }
    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Look();
    }
    


    void Movement()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 moveDir = (transform.right *moveInput.x + transform.forward*moveInput.y) * moveSpd;
        
        playerController.Move(moveDir * Time.deltaTime);

    }

    void Look()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        float XInput = lookInput.x * sensitivity;
        float YInput = lookInput.y * sensitivity;
        xRotation += XInput;
        yRotation -= YInput;
       // Debug.Log(lookInput);

        yRotation = Mathf.Clamp(yRotation, -angleLock, angleLock);

        transform.rotation = Quaternion.Euler(0, xRotation, 0);
        camera.transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
        
        
    }
}
