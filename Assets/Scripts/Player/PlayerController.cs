using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float rotationSpeed = 5f;
    private float rotation = 0f;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 moveDirection)
    {
        Vector3 move = transform.forward * moveDirection.y + transform.right * moveDirection.x;
        move = move * _moveSpeed * Time.deltaTime;
        _characterController.Move(move);
    }

    public void Look(Vector2 lookDirection)
    {
        rotation += lookDirection.x * rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}