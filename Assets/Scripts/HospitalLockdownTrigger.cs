using UnityEngine;
public class HospitalLockdownTrigger : MonoBehaviour {
    private bool hasTriggered = false;
    private void OnTriggerEnter(Collider other) {
        if (!hasTriggered && other.CompareTag("Player")) {
            hasTriggered = true;
            if (Level3GameManager.Instance != null) {
                Level3GameManager.Instance.ActivateLockdown();
            }
        }
    }
}