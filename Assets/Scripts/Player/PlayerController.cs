using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    [SerializeField]
    private float _moveSpeed = 5f;

    [SerializeField]
    private float rotationSpeed = 5f;
    private float rotation = 0f;

    [SerializeField]
    private float _speedMultiplier = 12;

    [SerializeField]
    private float _jumpForce = 5f;
    private float _verticalVelocity;

    [SerializeField]
    private float _gravity = 9.81f;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    public void Move(Vector2 moveDirection)
    {
        Vector3 move = transform.forward * moveDirection.y + transform.right * moveDirection.x;
        move = move * _moveSpeed * Time.deltaTime;
        _characterController.Move(move);
        _animator.SetFloat("Speed", move.magnitude * _speedMultiplier);

        ApplyGravity();
    }

    public void Look(Vector2 lookDirection)
    {
        rotation += lookDirection.x * rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
        {
            _verticalVelocity = _jumpForce;
            _animator.SetBool("Jump", true);
        }
    }

    public void ApplyGravity()
    {
        _verticalVelocity -= _gravity * Time.deltaTime;
        _characterController.Move(new Vector3(0, _verticalVelocity, 0) * Time.deltaTime);
        if (_characterController.isGrounded)
        {
            _animator.SetBool("Jump", false);
        }
    }
}
