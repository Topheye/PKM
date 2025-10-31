using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _followTarget;

    [SerializeField]
    private Transform _lookAtTarget;

    [SerializeField]
    private float _followSpeed = 5f;

    private void LateUpdate()
    {
        Vector3 targetPosition = _followTarget.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);
        transform.LookAt(_lookAtTarget);
    }
}
