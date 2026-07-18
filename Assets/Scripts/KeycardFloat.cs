using UnityEngine;
public class KeycardFloat : MonoBehaviour {
    public float floatSpeed = 2f;
    public float floatHeight = 0.2f;
    public float rotateSpeed = 90f;
    private Vector3 startPos;
    
    private void OnEnable() {
        startPos = transform.position;
    }
    
    private void Update() {
        // Spin
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
        // Float up and down smoothly
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * floatHeight, 0);
    }
}
