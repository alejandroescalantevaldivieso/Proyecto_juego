using UnityEngine;
public class CoinCollectible : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (Level3GameManager.Instance != null) {
                Level3GameManager.Instance.CollectCoin();
            }
            Destroy(gameObject);
        }
    }
}