using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Control_zombie : MonoBehaviour
{
    private AudioSource audio_zombie;
    public AudioClip sonido_reposo;
    public AudioClip sonido_persecucion;
    public AudioClip sonido_ataque;
    private Inventario_jugador inventario_jugador;
    private PlayerHealth playerHealth;
    // Referencia al componente NavMeshAgent
    private NavMeshAgent agente;

    // Referencia al Animator del zombie
    private Animator animador;

    // Referencia al jugador
    public Transform jugador;

    // Velocidad de persecución
    public float velocidad_caminar;
    public float velocidad_correr;


    // Distancia a la que el zombie detecta al jugador
    public float rango;
    public float rango_abandono = 15f;

    // Distancia actual entre zombie y jugador
    public float distancia;

    // Distancia mínima para atacar
    public float distancia_ataque = 1.8f;

    // Radio en el que el zombie buscará puntos aleatorios para patrullar
    public float radioPatrulla = 10f;


    private float tiempoEntreAtaques = 2f;
    private float contadorAtaque = 0f;

    private int estadoActual = -1;

    // Se ejecuta una sola vez al iniciar el juego
    void Start()
    {
        audio_zombie = GetComponent<AudioSource>();
        // Obtener componentes del mismo GameObject
        agente = GetComponent<NavMeshAgent>();
        animador = GetComponent<Animator>();

        inventario_jugador = jugador.GetComponent<Inventario_jugador>();
        playerHealth = jugador.GetComponent<PlayerHealth>();

        // Elegir un primer punto de patrulla
        ElegirNuevoDestino();


        audio_zombie = GetComponent<AudioSource>();
        audio_zombie.pitch = Random.Range(0.9f, 1.1f);

    }

    // Se ejecuta cada frame
    void Update()
    {
        // Calcular la distancia entre el zombie y el jugador
        distancia = Vector3.Distance(transform.position, jugador.position);

        // Si está muy cerca, ataca
        if (distancia <= distancia_ataque)
        {
            Atacar();
        }

        // Si está dentro del rango de detección, persigue
        else if (distancia <= rango || (estadoActual == 1 && distancia <= rango_abandono))
        {
            Perseguir();
        }

        // Si está fuera del rango, patrulla
        else
        {
            Patrullar();
        }
    }

    // Comportamiento de patrulla
    void Patrullar()
    {
        // Asegurar que el agente pueda moverse
        agente.isStopped = false;

        // Velocidad de caminata
        agente.speed = velocidad_caminar;

        // Activar animación de caminar
        animador.SetBool("esta_caminando", true);

        // Desactivar otras animaciones
        animador.SetBool("esta_corriendo", false);
        animador.SetBool("esta_atacando", false);

        // Si ya llegó a su destino actual
        if (!agente.pathPending &&
            agente.remainingDistance <= agente.stoppingDistance)
        {
            // Buscar otro punto aleatorio
            ElegirNuevoDestino();
        }

        if(distancia < 15 && distancia > 10)
        {
            if (estadoActual != 0)
            {           
                estadoActual = 0;

                audio_zombie.Stop();
                audio_zombie.clip = sonido_reposo;
                audio_zombie.time = 0;
                audio_zombie.Play();
            }            
        }else if(distancia > 20)
        {
            audio_zombie.Stop();
            estadoActual = -1;
        }
        
    }

    // Comportamiento de persecución
    void Perseguir()
    {
        // Permitir movimiento
        agente.isStopped = false;

        // Velocidad de carrera
        agente.speed = velocidad_correr;

        // El destino ahora es la posición del jugador
        agente.SetDestination(jugador.position);

        // Activar animación de correr
        animador.SetBool("esta_caminando", false);
        animador.SetBool("esta_corriendo", true);
        animador.SetBool("esta_atacando", false);

        if (estadoActual != 1)
        {
            // Debug.Log("SONANDO: " + sonido_persecucion.name);
            estadoActual = 1;

            audio_zombie.Stop();
            audio_zombie.clip = sonido_persecucion;
            audio_zombie.time = 0;
            audio_zombie.Play();

             // Debug.Log("CLIP ACTUAL: " + audio_zombie.clip.name);
        }
    }

    // Comportamiento de ataque
    void Atacar()
    {
        // Detener movimiento
        agente.isStopped = true;

        // Activar animación de ataque
        animador.SetBool("esta_caminando", false);
        animador.SetBool("esta_corriendo", false);
        animador.SetBool("esta_atacando", true);

        contadorAtaque += Time.deltaTime;

        if (contadorAtaque >= tiempoEntreAtaques)
        {        
             
            if (inventario_jugador != null) inventario_jugador.Recibir_dano(20);
            if (playerHealth != null) playerHealth.TakeDamage(20);
            contadorAtaque = 0f;
        }

        if (estadoActual != 2)
        {
            estadoActual = 2;
                audio_zombie.Stop();
                audio_zombie.clip = sonido_ataque;
                audio_zombie.time = 0;
                audio_zombie.Play();
        }
    }

    // Busca un nuevo punto aleatorio dentro del NavMesh
    void ElegirNuevoDestino()
    {
        // Generar una posición aleatoria alrededor del zombie
        Vector3 posicionAleatoria =
            transform.position + Random.insideUnitSphere * radioPatrulla;

        NavMeshHit hit;

        // Verificar que la posición generada esté dentro del NavMesh
        if (NavMesh.SamplePosition(
            posicionAleatoria,
            out hit,
            radioPatrulla,
            NavMesh.AllAreas))
        {
            // Enviar al zombie hacia ese punto
            agente.SetDestination(hit.position);
        }
    }

    // Dibuja el rango de detección en la escena
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Esfera roja que representa el rango de detección
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}