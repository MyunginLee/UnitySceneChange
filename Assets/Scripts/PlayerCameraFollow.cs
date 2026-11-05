using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -8f);
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0f, 2f, 0f);

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // World-space offsets keep character rotation from swinging the camera.
        transform.position = target.position + offset;
        Vector3 lookDirection = target.position + lookAtOffset - transform.position;
        if (lookDirection.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }
    }
}
