using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform followtarget;
    [SerializeField]
    private Transform lookAtTarget;
    [SerializeField]
    private float followSpeed = 5f;
}