using UnityEngine;
using UnityEngine.InputSystem;

#region Woo
public class WooMove : MonoBehaviour
{
    private PlayerInput playerInput;
    private CharacterController characterController;
    private Vector2 input;
    private float speed = 3f;
    private Vector3 velocity; // �߷� ó���� ���� �ӵ�
    private float gravity = -9.81f; // �߷� ��
    private float groundCheckDistance = 0.4f; // ���� üũ�� ���� �Ÿ�
    public Transform groundCheck; // ���� üũ�� ���� Ʈ������ ��ġ
    public LayerMask groundMask; // ������ üũ�� ���̾� ����ũ
    private bool isGrounded; // ���鿡 �ִ��� ����

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 100f; // ���콺 �ΰ���
    public Transform playerCamera; // ī�޶� Ʈ������ (ī�޶� ȸ����ų �� ���)
    private float xRotation = 0f; // ���� ȸ�� ������ ���� ����

    private Animator animator; // Animator ������Ʈ�� ������ ����


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // ���콺 Ŀ�� ���
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        GroundCheck();
        Move();
        ApplyGravity();
    }

    // ���� üũ ó��
    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ���鿡 ���� �� �ӵ��� ����
        }
    }

    // �̵� ó��
    private void Move()
    {
        if (input != Vector2.zero)
        {
            Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;
            characterController.Move(moveDirection * Time.deltaTime * speed);
        }
    }

    // �߷� ó��
    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime); // �߷¿� ���� �Ʒ����� �̵�
    }

    // Move �׼��� �߻����� �� ȣ��Ǵ� �޼���
    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    // Look �׼��� �߻����� �� ȣ��Ǵ� �޼���
    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();

        // ���콺 X, Y �Է¿� ���� ȸ�� ó��
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // ���� ȸ�� (Pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -15f, 40f); // ���� ȸ�� ������ ����

        // ī�޶� ���� ȸ�� ����
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // �¿� ȸ�� (Yaw)
        transform.Rotate(Vector3.up * mouseX);
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Attack Ʈ���Ÿ� �ߵ����� ���� �ִϸ��̼� ���
            animator.SetTrigger("Attack");

            // ���� �ִϸ��̼��̳� ������ ���⼭ ó��
            Debug.Log("Attack Triggered!");

        }
    }
}
#endregion