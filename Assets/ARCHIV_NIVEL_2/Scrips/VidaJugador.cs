using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 20;
    public int vidaActual;

    [Header("Componentes de Interfaz de Usuario")]
    public Slider barraVida;
    public GameObject textoMisionFallida;

    [Header("Audio y Sonidos")]
    public AudioSource musicaFondo;
    public AudioSource reproductorEfectos;
    public AudioClip sonidoDolor;

    void Start()
    {
        Time.timeScale = 1f;
        vidaActual = vidaMaxima;

        if (barraVida != null)
        {
            barraVida.maxValue = vidaMaxima;
            barraVida.value = vidaMaxima;
        }

        if (textoMisionFallida != null)
        {
            textoMisionFallida.SetActive(false);
        }
    }

    public void RecibirDano(int cantidadDano)
    {
        vidaActual -= cantidadDano;

        if (barraVida != null)
        {
            barraVida.value = vidaActual;
        }

        Debug.Log("¡Auch! Vida restante del jugador: " + vidaActual);

        // --- SOLUCIÓN PARA QUE NO SE SUPERPONGAN LOS GRITOS ---
        if (reproductorEfectos != null && sonidoDolor != null)
        {
            // Verificamos si NO está reproduciendo ya un sonido
            if (!reproductorEfectos.isPlaying)
            {
                // Le asignamos el clip de dolor y le damos a Play normal
                reproductorEfectos.clip = sonidoDolor;
                reproductorEfectos.Play();
            }
        }
        // ------------------------------------------------------

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    public void Curar(int cantidadCura)
    {
        if (vidaActual <= 0) return;

        vidaActual += cantidadCura;

        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima;
        }

        if (barraVida != null)
        {
            barraVida.value = vidaActual;
        }

        Debug.Log("¡Botiquín usado! Vida actual: " + vidaActual);
    }

    void Morir()
    {
        Debug.Log("¡EL JUGADOR HA SIDO ELIMINADO!");

        if (textoMisionFallida != null)
        {
            textoMisionFallida.SetActive(true);
        }

        if (GetComponent<ControlPersonaje>() != null)
        {
            GetComponent<ControlPersonaje>().Morir();
            GetComponent<ControlPersonaje>().enabled = false;
        }

        // --- DETENER TODOS LOS SONIDOS DEL JUEGO ---
        AudioSource[] todosLosAudios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in todosLosAudios)
        {
            audio.Stop();
        }
        // -------------------------------------------

        Time.timeScale = 0f;
    }
}