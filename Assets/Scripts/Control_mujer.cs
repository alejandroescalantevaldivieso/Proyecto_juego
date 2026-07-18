using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Control_mujer : MonoBehaviour
{

    private float mueve_x;
    private float mueve_y;
    public float velocidad_caminar = 3;
    private float velocidad_caminar_giro = 120;
    private Animator controler;

    public float velocidad_inicial;
    public float velocidad_agachado;

    public float velocidad_correr= 8;
    

    public Rigidbody rb;
    public float fuerza_salto= 8f;
    public bool puedo_saltar;

    

    // Start is called before the first frame update
    void Start()
    {
        // cuando inicar el juego no puedo saltar
        puedo_saltar = false;
        
        controler = GetComponent<Animator>();

        velocidad_inicial = velocidad_caminar;
        velocidad_agachado = velocidad_caminar * 0.5f;
    }

    // Se ejecuta en intervalos fijos y se usa para lógica de física (Rigidbody, fuerzas y movimiento físico)
    void FixedUpdate()
    {
        transform.Translate(0,0,mueve_y*Time.deltaTime*velocidad_caminar);
        transform.Rotate(0,mueve_x*Time.deltaTime*velocidad_caminar_giro,0);
    }

    // Update is called once per frame
    void Update()
    {
    mueve_x = Input.GetAxis("Horizontal");
        mueve_y = Input.GetAxis("Vertical");

    controler.SetFloat("valor_x", mueve_x);

    // velocidad base
    velocidad_caminar = velocidad_inicial;

    float velocidad_animacion = 0;

    // caminar adelante
    if (mueve_y > 0)
    {
        velocidad_animacion = 0.5f;

        // correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidad_caminar = velocidad_correr;
            velocidad_animacion = 1f;
        }
    }
    // caminar atrás
    else if (mueve_y < 0)
    {
        velocidad_animacion = -0.5f;
    }

    // agacharse
    if (Input.GetKey(KeyCode.LeftControl))
    {
        controler.SetBool("agachado", true);
        velocidad_caminar = velocidad_agachado;
    }
    else
    {
        controler.SetBool("agachado", false);
    }

    controler.SetFloat("valor_y", velocidad_animacion, 0.1f, Time.deltaTime);

    // salto
    if (puedo_saltar)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controler.SetBool("salte", true);
            rb.AddForce(Vector3.up * fuerza_salto, ForceMode.Impulse);
        }

        controler.SetBool("tocar_piso", true);
    }
    else
    {
        estoy_cayendo();
    }



    }

    public void estoy_cayendo()
    {
        controler.SetBool("tocar_piso",false);
        controler.SetBool("salte",false);
    }

}
