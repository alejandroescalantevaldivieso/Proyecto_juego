using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class Inventario_jugador : MonoBehaviour
{   
    
    public TextMeshProUGUI txt_cantidad_tarjetas;
    // Vida
    public TextMeshProUGUI txt_cantidad_vida;
    // Tarjetas
    public int cantidad_tarjetas = 0;
    public int vida = 100;
    // Tiempo
    public float tiempo = 120f;
    public TextMeshProUGUI txt_cantidad_tiempo;
    // Start is called before the first frame update
    void Start()
    {
        txt_cantidad_vida.text = 100.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (tiempo > 0)
        {
             tiempo -= Time.deltaTime;
             txt_cantidad_tiempo.text = Mathf.Ceil(tiempo).ToString();
        }
        else
        {
            tiempo = 0;
            txt_cantidad_tiempo.text = "0";

        }
    }

    public void Agregar_tarjeta()
    {
        cantidad_tarjetas ++;
        txt_cantidad_tarjetas.text = cantidad_tarjetas.ToString();
    }
    public void Recibir_dano(int cantidad)
    {
        vida -= cantidad;
        if(vida < 0)
        {
            vida = 0;
        }
        txt_cantidad_vida.text = vida.ToString();
    }
    public void Recuperar_vida(int cantidad)
    {
        vida += cantidad;

        // Evitar superar la vida máxima
        if (vida > 100)
        {
            vida = 100;
        }

        txt_cantidad_vida.text = vida.ToString();
    }
}
