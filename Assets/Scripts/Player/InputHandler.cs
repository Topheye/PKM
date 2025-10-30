
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    [SerializeField]
    private PlayerController _playerController;

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _jumpAction = InputSystem.actions.FindAction("Jump");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _jumpAction.performed += OnJump;
    }

    void Update()
    {
        Vector2 moveDirection = _moveAction.ReadValue<Vector2>();
        Vector2 lookDirection = _lookAction.ReadValue<Vector2>();

        _playerController.Move(moveDirection);
        _playerController.Look(lookDirection);
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        
    }

    private void OnEnable()
    {
        _jumpAction.performed += OnJump;
    }

    private void OnDisable()
    {
        _jumpAction.performed += OnJump;
    }
}
