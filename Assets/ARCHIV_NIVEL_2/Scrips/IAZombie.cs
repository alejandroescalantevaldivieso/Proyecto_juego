using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IAZombie : MonoBehaviour
{
    [Header("Objetivo y Distancias")]
    public Transform jugador;
    public float distanciaDeteccion = 15f;
    public float radioPatrulla = 12f;
    public float distanciaAtaque = 2.7f;

    [Header("Velocidades")] // --- NUEVO: Control de velocidades ---
    public float velocidadCaminar = 2f;
    public float velocidadCorrer = 5.6f;

    [Header("Ataque")]
    public float tiempoEntreAtaques = 1.3f;

    private NavMeshAgent agente;
    private Animator anim;
    private float cronometroAtaque;
    private bool estaAtacando = false;
    private Vector3 puntoInicio;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        puntoInicio = transform.position;

        if (jugador == null)
        {
            GameObject jugadorObj = GameObject.FindWithTag("Player");
            if (jugadorObj == null) jugadorObj = GameObject.Find("Erika Archer");
            if (jugadorObj != null) jugador = jugadorObj.transform;
        }
    }

    void Update()
    {
        if (jugador == null) return;

        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        if (estaAtacando)
        {
            cronometroAtaque += Time.deltaTime;
            if (cronometroAtaque >= tiempoEntreAtaques)
            {
                estaAtacando = false;
            }
            return;
        }

        if (distanciaAlJugador > distanciaDeteccion)
        {
            Patrullar();
        }
        else if (distanciaAlJugador <= distanciaDeteccion && distanciaAlJugador > distanciaAtaque)
        {
            PerseguirJugador();
        }
        else
        {
            DetenerseYAtacar();
        }
    }

    void Patrullar()
    {
        agente.isStopped = false;
        agente.speed = velocidadCaminar; // --- NUEVO: El NavMeshAgent se vuelve lento ---

        if (!agente.pathPending && agente.remainingDistance <= agente.stoppingDistance)
        {
            Vector3 puntoAleatorio = puntoInicio + Random.insideUnitSphere * radioPatrulla;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(puntoAleatorio, out hit, radioPatrulla, NavMesh.AllAreas))
            {
                agente.SetDestination(hit.position);
            }
        }

        // Le pasamos la velocidad exacta al Blend Tree
        anim.SetFloat("ValorY", agente.velocity.magnitude);
        anim.SetFloat("ValorX", 0f);
    }

    void PerseguirJugador()
    {
        agente.isStopped = false;
        agente.speed = velocidadCorrer; // --- NUEVO: El NavMeshAgent acelera para atraparte ---
        agente.SetDestination(jugador.position);

        // NUEVO: Fuerza la animación a reproducirse al máximo
        if (agente.velocity.magnitude > 0.1f)
        {
            anim.SetFloat("ValorY", agente.speed); // Le manda 2 o 5.6 directamente
        }
        else
        {
            anim.SetFloat("ValorY", 0f); // Si se choca o se detiene, vuelve a Idle
        }
    }

    void DetenerseYAtacar()
    {
        agente.isStopped = true;
        agente.velocity = Vector3.zero;
        anim.SetFloat("ValorY", 0f);

        Vector3 direccion = (jugador.position - transform.position).normalized;
        direccion.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direccion), 0.1f);

        LanzarAtaque();
    }

    void LanzarAtaque()
    {
        estaAtacando = true;
        cronometroAtaque = 0f;
        anim.SetTrigger("atacar");

        VidaJugador scriptVida = jugador.GetComponent<VidaJugador>();
        if (scriptVida != null)
        {
            scriptVida.RecibirDano(1);
        }
    }
}