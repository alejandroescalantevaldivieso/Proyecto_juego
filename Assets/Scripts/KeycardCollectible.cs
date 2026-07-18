using UnityEngine;
public class KeycardCollectible : MonoBehaviour {
    private bool inRange = false;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            inRange = true;
            if (Level3HUDController.Instance != null) Level3HUDController.Instance.ShowMessage("Presiona 'F' para coger la tarjeta");
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            inRange = false;
        }
    }
    
    private void Update() {
        if (inRange && Input.GetKeyDown(KeyCode.F)) {
            if (Level3GameManager.Instance != null) {
                Level3GameManager.Instance.CollectKeycard();
            }
            gameObject.SetActive(false); // Hide instead of destroy so it can be reused or cleanly removed
        }
    }
}
