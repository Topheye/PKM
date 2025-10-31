using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private InputAction _dashAction;
    private InputAction _rollAction;

    [SerializeField]
    private PlayerController _playerController;

    private float _moveThreshold = 0.01f;

    void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _dashAction = InputSystem.actions.FindAction("Dash");
        _rollAction = InputSystem.actions.FindAction("Roll");

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 moveDirection = _moveAction.ReadValue<Vector2>();
        Vector2 lookDirection = _lookAction.ReadValue<Vector2>();
        bool dash = false;
        
        if (_dashAction.IsPressed())
        {
            dash = true;
        }
        _playerController.Move(moveDirection, dash);
        _playerController.Look(lookDirection);
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        _playerController.Jump();
    }

    private void OnRoll(InputAction.CallbackContext ctx)
    {
        if (_moveAction.ReadValue<Vector2>().magnitude > _moveThreshold)
        {
            _playerController.Dash(_moveAction.ReadValue<Vector2>());
        }
    }

    private void OnEnable()
    {
        _jumpAction.performed += OnJump;
        _rollAction.performed += OnRoll;
    }

    private void OnDisable()
    {
        _jumpAction.performed -= OnJump;
        _rollAction.performed -= OnRoll;
    }
}
