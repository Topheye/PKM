using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField]
    private Camera _playerCamera;

    [SerializeField]
    private LineRenderer _lineRenderer;
    private Animator _animator;

    [SerializeField]
    private float _moveSpeed = 5f;

    [SerializeField]
    private float rotationSpeed = 5f;
    private float rotation = 0f;

    [SerializeField]
    private float _speedMultiplier = 10;

    [SerializeField]
    private float _jumpForce = 5f;
    private float _verticalVelocity;

    [SerializeField]
    private float _gravity = 9.81f;

    [SerializeField]
    private float _dashForce = 5f;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    public void Move(Vector2 moveDirection, bool dash)
    {
        Vector3 move = transform.forward * moveDirection.y + transform.right * moveDirection.x;
        move = move * _moveSpeed * Time.deltaTime;

        if (dash)
        {
            _animator.SetBool("Dash", true);
            move *= 2;
        }
        else
        {
            _animator.SetBool("Dash", false);
        }

        if (Math.Abs(moveDirection.x) > Math.Abs(moveDirection.y))
        {
            if (moveDirection.x > 0)
            {
                _animator.SetBool("Right", true);
                _animator.SetBool("Left", false);
            }
            else
            {
                _animator.SetBool("Left", true);
                _animator.SetBool("Right", false);
            }
        }
        else
        {
            _animator.SetBool("Right", false);
            _animator.SetBool("Left", false);
        }
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
            _animator.SetTrigger("Jump");
        }
    }

    public void Dash(Vector3 dashDirection)
    {
        Look(dashDirection);
        //Move(dashDirection * _dashForce);
        _animator.SetTrigger("Roll");
    }

    public void ApplyGravity()
    {
        _verticalVelocity -= _gravity * Time.deltaTime;
        _characterController.Move(new Vector3(0, _verticalVelocity, 0) * Time.deltaTime);
    }

    public void Shoot(Vector2 lookDirection)
    {
        Vector3 characterPosition = this.transform.position;
        Ray ray = _playerCamera.ScreenPointToRay(lookDirection);
        Vector3 targetPoint = ray.origin + ray.direction * 100;
        Vector3 direction = (targetPoint - characterPosition).normalized;
        StartCoroutine(
            DrawLaser(
                characterPosition + Vector3.up * 1.5f,
                characterPosition + Vector3.up * 1.5f + direction * 100
            )
        );
    }

    private System.Collections.IEnumerator DrawLaser(Vector3 start, Vector3 end)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);

        yield return new WaitForSeconds(1);
        _lineRenderer.enabled = false;
    }
}
