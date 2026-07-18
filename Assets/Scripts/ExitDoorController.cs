using UnityEngine;

public class ExitDoorController : MonoBehaviour {
    
    private bool promptShown = false;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player") && Level3GameManager.Instance != null && Level3GameManager.Instance.lockdownActive) {
            
            
            if (!promptShown) {
                promptShown = true;
                if (Level3HUDController.Instance != null) {
                    Level3HUDController.Instance.ShowMessage("Presiona 'F' para usar la puerta");
                }
            }
            
            if (Input.GetKeyDown(KeyCode.F)) {
                Level3GameManager.Instance.TryExit();
            }
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            
            promptShown = false;
        }
    }
}
