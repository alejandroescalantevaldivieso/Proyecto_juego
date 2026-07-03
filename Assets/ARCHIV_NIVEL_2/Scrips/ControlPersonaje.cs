using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPersonaje : MonoBehaviour
{
    private float mueveX;
    private float mueveY;
    public float speed = 12f;
    public float speedGiro = 180f;

    private Animator ControladorPersonaje;
    private Rigidbody rb;

    public AudioSource sonidoPasos;

    // NUEVA VARIABLE: Controla si el jugador puede moverse y hacer sonido
    public bool estaMuerto = false;

    void Start()
    {
        ControladorPersonaje = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Si el jugador está muerto, detenemos la ejecución de este bloque
        if (estaMuerto) return;

        // 1. LEER LOS CONTROLES Y ANIMACIONES
        mueveX = Input.GetAxisRaw("Horizontal");
        mueveY = Input.GetAxisRaw("Vertical");

        ControladorPersonaje.SetFloat("ValorX", mueveX);
        ControladorPersonaje.SetFloat("ValorY", mueveY);

        // 2. ROTACIÓN (Girar sobre su propio eje)
        transform.Rotate(0, mueveX * Time.deltaTime * speedGiro, 0);

        // 4. LÓGICA DE SONIDO
        bool seEstaMoviendo = mueveX != 0 || mueveY != 0;

        if (seEstaMoviendo)
        {
            if (!sonidoPasos.isPlaying)
            {
                sonidoPasos.Play();
            }
        }
        else
        {
            if (sonidoPasos.isPlaying)
            {
                sonidoPasos.Stop();
            }
        }
    }

    void FixedUpdate()
    {
        // Si el jugador está muerto, nos aseguramos de que no se deslice
        if (estaMuerto)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            return;
        }

        // 3. MOVIMIENTO CON FÍSICAS (Gravedad automática)
        Vector3 movimiento = transform.forward * mueveY * speed;
        rb.velocity = new Vector3(movimiento.x, rb.velocity.y, movimiento.z);
    }

    // NUEVA FUNCIÓN: Detiene todo cuando el jugador muere
    public void Morir()
    {
        estaMuerto = true;

        // Detenemos el sonido de los pasos inmediatamente
        if (sonidoPasos != null && sonidoPasos.isPlaying)
        {
            sonidoPasos.Stop();
        }

        // Si tienes una animación de muerte, podrías llamarla aquí. Ejemplo:
        // ControladorPersonaje.SetTrigger("Muerte");
    }
}