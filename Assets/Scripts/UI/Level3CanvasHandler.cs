using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level3CanvasHandler : MonoBehaviour {
    private void OnEnable() {
        
        foreach(var btn in GetComponentsInChildren<Button>(true)) {
            var txt = btn.GetComponentInChildren<TMP_Text>(true);
            if (txt != null) {
                string t = txt.text.ToUpper();
                btn.onClick.RemoveAllListeners();
                
                if (t.Contains("VOLVER A JUGAR")) {
                    btn.onClick.AddListener(() => { Time.timeScale = 1f; SceneManager.LoadScene("Scena02"); });
                } else if (t.Contains("REINTENTAR")) {
                    btn.onClick.AddListener(() => { Time.timeScale = 1f; SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
                } else if (t.Contains("MENU")) {
                    btn.onClick.AddListener(() => { Time.timeScale = 1f; SceneManager.LoadScene("Scena01"); });
                }
            }
        }
    }
}
