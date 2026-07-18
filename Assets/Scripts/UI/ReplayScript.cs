using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayScript : MonoBehaviour {
    private void Start() {
        var btn = GetComponent<Button>();
        if (btn != null) {
            btn.onClick.AddListener(ReplayGame);
        }
    }

    public void ReplayGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scena02");
    }
}
