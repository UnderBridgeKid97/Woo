using UnityEngine;
using UnityEngine.InputSystem;

#region Woo
public class WooMove : MonoBehaviour
{
    private PlayerInput playerInput;
    private CharacterController characterController;
    private Vector2 input;
    private float speed = 3f;
    private Vector3 velocity; // 중력 처리를 위한 속도
    private float gravity = -9.81f; // 중력 값
    private float groundCheckDistance = 0.4f; // 지면 체크를 위한 거리
    public Transform groundCheck; // 지면 체크를 위한 트랜스폼 위치
    public LayerMask groundMask; // 지면을 체크할 레이어 마스크
    private bool isGrounded; // 지면에 있는지 여부

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 100f; // 마우스 민감도
    public Transform playerCamera; // 카메라 트랜스폼 (카메라를 회전시킬 때 사용)
    private float xRotation = 0f; // 상하 회전 제한을 위한 변수

    private Animator animator; // Animator 컴포넌트를 참조할 변수


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // 마우스 커서 잠금
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        GroundCheck();
        Move();
        ApplyGravity();
    }

    // 지면 체크 처리
    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 지면에 있을 때 속도를 리셋
        }
    }

    // 이동 처리
    private void Move()
    {
        if (input != Vector2.zero)
        {
            Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;
            characterController.Move(moveDirection * Time.deltaTime * speed);
        }
    }

    // 중력 처리
    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime); // 중력에 의한 아래로의 이동
    }

    // Move 액션이 발생했을 때 호출되는 메서드
    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    // Look 액션이 발생했을 때 호출되는 메서드
    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();

        // 마우스 X, Y 입력에 따른 회전 처리
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // 상하 회전 (Pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -15f, 40f); // 상하 회전 각도를 제한

        // 카메라 상하 회전 적용
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 좌우 회전 (Yaw)
        transform.Rotate(Vector3.up * mouseX);
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Attack 트리거를 발동시켜 공격 애니메이션 재생
            animator.SetTrigger("Attack");

            // 공격 애니메이션이나 동작을 여기서 처리
            Debug.Log("Attack Triggered!");

        }
    }
}
#endregion