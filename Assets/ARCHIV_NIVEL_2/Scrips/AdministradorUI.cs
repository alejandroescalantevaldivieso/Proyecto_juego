using UnityEngine;
using UnityEngine.SceneManagement;

public class AdministradorUI : MonoBehaviour
{
    [Header("Pantallas")]
    public GameObject pantallaVictoria;
    public GameObject pantallaDerrota;

    private VidaJugador vidaErika;
    private Inventario_jugador inventarioErika;

    private bool juegoTerminado = false;

    void Start()
    {
        Time.timeScale = 1f;
        juegoTerminado = false;

        if (pantallaVictoria != null) pantallaVictoria.SetActive(false);
        if (pantallaDerrota != null) pantallaDerrota.SetActive(false);

        vidaErika = FindObjectOfType<VidaJugador>();
        inventarioErika = FindObjectOfType<Inventario_jugador>();

        if (vidaErika == null)
        {
            Debug.LogWarning("AdministradorUI: No se encontro a nadie con el script VidaJugador.");
        }
        if (inventarioErika == null)
        {
            Debug.LogWarning("AdministradorUI: No se encontro a nadie con el script Inventario_jugador.");
        }
    }

    void Update()
    {
        if (juegoTerminado) return;

        if (vidaErika != null && vidaErika.vidaActual <= 0)
        {
            MostrarDerrota();
            return;
        }

        if (inventarioErika != null)
        {
            if (inventarioErika.tiempo <= 0 || inventarioErika.cantidad_tarjetas >= 3)
            {
                MostrarVictoria();
                return;
            }
        }
    }

    public void MostrarDerrota()
    {
        juegoTerminado = true;
        if (pantallaDerrota != null) pantallaDerrota.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MostrarVictoria()
    {
        juegoTerminado = true;
        if (pantallaVictoria != null) pantallaVictoria.SetActive(true);
        Time.timeScale = 0f;
    }

    public void BotonReintentar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BotonMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scena01");
    }

    public void BotonSiguienteNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
