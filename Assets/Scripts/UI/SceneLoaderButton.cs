using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderButton : MonoBehaviour {
    public string sceneToLoad;
    
    private void Start() {
        var btn = GetComponent<Button>();
        if (btn != null) {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(LoadTargetScene);
        }
    }

    public void LoadTargetScene() {
        Time.timeScale = 1f; // Always unpause
        SceneManager.LoadScene(sceneToLoad);
    }
}
