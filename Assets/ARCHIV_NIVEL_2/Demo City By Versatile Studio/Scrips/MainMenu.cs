using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Función para el botón "Iniciar Juego"
    public void IniciarJuego()
    {
        // Asegúrate de que el nombre entre comillas sea exactamente el de tu escena
        SceneManager.LoadScene("demo_city_night");
    }

    // Función para el botón "Salir"
    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");

        // Si estamos probando el juego dentro del Editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        // Si el juego ya está compilado (.exe, .apk, etc.)
#else
            Application.Quit();
#endif
    }
}