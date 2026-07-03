using UnityEngine;
using TMPro; // Vital para usar TextMeshPro

public class Temporizador : MonoBehaviour
{
    [Header("Configuración")]
    public float tiempoSobrevivencia = 60f; // Los segundos que debe durar la partida
    private bool temporizadorActivo = true;

    [Header("Interfaz y Audio")]
    public TextMeshProUGUI textoTemporizador; // El texto amarillo del reloj
    public GameObject textoMisionCumplida;    // El nuevo cartel verde
    public AudioSource musicaFondo;           // Para detener la música al ganar

    void Start()
    {
        // Nos aseguramos de que el cartel de victoria esté oculto al iniciar
        if (textoMisionCumplida != null)
        {
            textoMisionCumplida.SetActive(false);
        }
    }

    void Update()
    {
        if (temporizadorActivo)
        {
            // Restar el tiempo basándonos en el reloj de la vida real
            tiempoSobrevivencia -= Time.deltaTime;

            // Mostrarlo en formato de reloj (Minutos:Segundos)
            int minutos = Mathf.FloorToInt(tiempoSobrevivencia / 60);
            int segundos = Mathf.FloorToInt(tiempoSobrevivencia % 60);
            textoTemporizador.text = string.Format("{0:00}:{1:00}", minutos, segundos);

            // ¿El tiempo llegó a Cero?
            if (tiempoSobrevivencia <= 0)
            {
                tiempoSobrevivencia = 0;
                textoTemporizador.text = "00:00";
                temporizadorActivo = false;
                GanarPartida(); // Dispara la victoria
            }
        }
    }

    void GanarPartida()
    {
        Debug.Log("¡HAS SOBREVIVIDO! Misión Cumplida.");

        // 1. Mostrar el cartel verde
        if (textoMisionCumplida != null)
        {
            textoMisionCumplida.SetActive(true);
        }

        // 2. Apagar la música
        if (musicaFondo != null)
        {
            musicaFondo.Stop();
        }

        // 3. Buscar al jugador y bloquear sus controles
        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null && jugador.GetComponent<ControlPersonaje>() != null)
        {
            jugador.GetComponent<ControlPersonaje>().enabled = false;
        }

        // 4. Congelar el universo entero (los zombies se quedan estáticos)
        Time.timeScale = 0f;
    }
}