using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2.5f, -4f);
    public float mouseSensitivity = 2f;
    private float pitch = 15f;

    void LateUpdate()
    {
        if (target != null)
        {
            // Vertical mouse look controls pitch (tilt up/down)
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -10f, 60f);

            // Calculate rotation and position
            Quaternion rotation = target.rotation * Quaternion.Euler(pitch, 0, 0);
            Vector3 desiredPosition = target.position + rotation * offset;

            transform.position = desiredPosition;
            transform.LookAt(target.position + Vector3.up * 1.5f);
        }
    }
}