using UnityEngine;

public class KeycardPickup : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") || other.GetComponent<PlayerMovement>() != null || other.GetComponent<CharacterController>() != null) {
            Level3GameManager gm = FindObjectOfType<Level3GameManager>();
            if (gm != null) {
                gm.CollectKeycard();
                gameObject.SetActive(false); // Desaparecer la tarjeta
            }
        }
    }
}