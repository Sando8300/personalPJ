using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPplayer : MonoBehaviour
{

    [Header("Movement")]
    CharacterController characterController;
    float gravityvalue = -9.81f;
    public float maxSpeed = 3f;
    public bool isStop = true;
    public float jumpPower = 10;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction crouchAction;
    private InputAction interactAction;
    Vector3 motion = Vector3.zero;
    public Animator animator;
    float animX;
    float animZ;
    public float sprintSpd = 2;
    bool isRun = false;
    bool isWalk = false;
    bool isCrouch = false;

    [Header("FPSanim")]
    public Animator fpsAnimator;



    [Header("Looking")]
    private InputAction lookAction;
    public CinemachineCamera cinemachineCamera;
    public Transform playerHead;
    public Vector2 lookSensitivity = new Vector2(0.1f, 0.1f);
    float deltaX;
    float deltaY;
    float yaw;
    float pitch;
    public float minPitch = -75f;
    public float maxPitch = 75f;

    [Header("Sound")]
    float timer = 0.5f;
    public AudioSource audioSource;
    public AudioClip[] footstepGeneral;
    public AudioClip[] runningfootstepGeneral;



    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        interactAction = InputSystem.actions.FindAction("interact");

        moveAction?.Enable();
        lookAction?.Enable();
        jumpAction?.Enable();
        sprintAction?.Enable();
        crouchAction?.Enable();
        interactAction?.Enable();

        sprintAction.started += OnSprintStarted;
        sprintAction.canceled += OnSprintCanceled;
        crouchAction.started += OnCrouchStarted;
        crouchAction.canceled += OnCrouchCanceled;
       
    }

    private void OnDisable()
    {
        if (sprintAction != null)
        {
            sprintAction.started -= OnSprintStarted;
            sprintAction.canceled -= OnSprintCanceled;
        }
        if (crouchAction != null)
        {
            crouchAction.started -= OnCrouchStarted;
            crouchAction.canceled -= OnCrouchCanceled;
        }
        moveAction?.Disable();
        lookAction?.Disable();
        jumpAction?.Disable();
        sprintAction?.Disable();
        interactAction?.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        OnMovement();
        OnLooking();
    }


    void OnMovement()
    {

        Vector2 moveDir = moveAction.ReadValue<Vector2>();
        animZ = isRun ? Mathf.Lerp(animZ, moveDir.y * sprintSpd, 0.3f) : Mathf.Lerp(animZ, moveDir.y, 0.1f);
        animX = Mathf.Lerp(animX, moveDir.x, 0.3f);
        if (fpsAnimator != null && isRun && !fpsAnimator.GetCurrentAnimatorStateInfo(0).IsName("root|MC_MP7_run_loop") && !fpsAnimator.IsInTransition(0))
            fpsAnimator.SetBool("isRun", true);
        if (fpsAnimator != null && !isRun) fpsAnimator.SetBool("isRun", false);
        maxSpeed = isRun ? sprintSpd : 3f;
        if (crouchAction.IsPressed())
        {
            animator.SetTrigger("isCrouch");
        }
        if (isCrouch && moveAction.IsPressed())
        {
            animator.SetBool("OnCrouch", true);
        }
        if (isCrouch && !crouchAction.IsPressed())
        {
            animator.SetTrigger("isStanding");
            animator.SetBool("OnCrouch", false);
        }

        animator.SetFloat("Z", animZ);
        animator.SetFloat("X", animX);
        if (fpsAnimator != null && !isRun && moveAction.IsPressed())
        {
            isWalk = true;
            fpsAnimator.SetBool("isWalk", isWalk);
        }
        if (fpsAnimator != null && !isRun && !moveAction.IsPressed())
        {
            isWalk = false;
            fpsAnimator.SetBool("isWalk", isWalk);
        }
        if (!isCrouch && crouchAction.IsPressed())
        {
            animator.SetTrigger("isCrouch");

        }


        Vector3 move = (transform.right * moveDir.x + transform.forward * moveDir.y) * maxSpeed;


        //여기서 y를 항상 0으로 초기화 하고있던것을 기존 motion.y값이 유지되도록 변경

        if (characterController.isGrounded)
        {
            motion.y = -0.5f;
            animator.SetBool("isGround", true);
            if (jumpAction.IsPressed())
            {
                motion.y = jumpPower;
                animator.SetTrigger("Jump");
                animator.SetBool("isGround", false);
            }

            if (Mathf.Abs(animX) >= 0.5f || Mathf.Abs(animZ) >= 0.5f)
            {
                if(Mathf.Abs(animX) >= 3f || Mathf.Abs(animZ) >= 3f)
                {
                    if (timer < Time.time)
                    {
                        audioSource.PlayOneShot(runningfootstepGeneral[Random.Range(0, runningfootstepGeneral.Length)]);
                        timer = Time.time + 0.3f;
                        return;
                    }
                }

                if (timer < Time.time)
                {
                    audioSource.PlayOneShot(footstepGeneral[Random.Range(0, footstepGeneral.Length)]);
                    timer = Time.time + 0.4f;
                }
            }
        }

        else motion.y += gravityvalue * Time.deltaTime;




        motion.x = move.x;
        motion.z = move.z;

        characterController.Move(motion * Time.deltaTime);
    }

    void OnLooking()
    {
        Vector2 look = lookAction.ReadValue<Vector2>();
        deltaX = look.x * lookSensitivity.x;
        deltaY = look.y * lookSensitivity.y;
        yaw += deltaX;
        pitch -= deltaY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(0f, yaw, 0f);




        cinemachineCamera.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        playerHead.rotation = Quaternion.Euler(pitch, 0f, 0f);


    }


    public bool open;


    void OnInteractStarted(InputAction.CallbackContext ctx) => open = true;
    void OnInteractCanceled(InputAction.CallbackContext ctx) => open = false;
    void OnSprintStarted(InputAction.CallbackContext ctx) => isRun = true;
    void OnSprintCanceled(InputAction.CallbackContext ctx) => isRun = false;

    void OnCrouchStarted(InputAction.CallbackContext ctx) => isCrouch = true;
    void OnCrouchCanceled(InputAction.CallbackContext ctx) => isCrouch = false;


}
