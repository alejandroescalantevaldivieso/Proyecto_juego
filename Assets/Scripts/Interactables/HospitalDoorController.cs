using UnityEngine;

public class HospitalDoorController : MonoBehaviour
{
    public GameObject doorBarrier; // Legacy barrier
    public Transform doorRight;
    public Transform doorLeft;
    
    public float closeSpeed = 2f;
    private bool isClosing = false;

    private Quaternion targetRightRotation;
    private Quaternion targetLeftRotation;

    private void Start() {
        if (doorBarrier != null) doorBarrier.SetActive(false);
        
        if (doorRight != null) targetRightRotation = doorRight.localRotation;
        if (doorLeft != null) targetLeftRotation = doorLeft.localRotation;
    }

    private void Update() {
        if (isClosing) {
            if (doorRight != null) {
                doorRight.localRotation = Quaternion.Slerp(doorRight.localRotation, targetRightRotation, Time.deltaTime * closeSpeed);
            }
            if (doorLeft != null) {
                doorLeft.localRotation = Quaternion.Slerp(doorLeft.localRotation, targetLeftRotation, Time.deltaTime * closeSpeed);
            }
        }
    }

    public void CloseDoor() {
        if (doorBarrier != null) doorBarrier.SetActive(true);
        isClosing = true;
        
        // Find the actual doors in the scene if not assigned
        if (doorRight == null) {
            var dr = GameObject.Find("Main Entrance Door Right 01_445");
            if (dr != null) doorRight = dr.transform;
        }
        if (doorLeft == null) {
            var dl = GameObject.Find("Main Entrance Door Left 01_444");
            if (dl != null) doorLeft = dl.transform;
        }
        
        // Set rotation to closed state (usually 0,0,0 relative to parent)
        if (doorRight != null) {
            targetRightRotation = Quaternion.Euler(0, 0, 0); // Adjust based on axis
        }
        if (doorLeft != null) {
            targetLeftRotation = Quaternion.Euler(0, 0, 0); // Adjust based on axis
        }
    }

    public void OpenDoor() {
        if (doorBarrier != null) doorBarrier.SetActive(false);
        isClosing = false;
    }
}