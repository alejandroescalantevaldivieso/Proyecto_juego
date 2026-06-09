using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarjeta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventario_jugador inventario_jugador = other.GetComponent<Inventario_jugador>();
            if (inventario_jugador != null)
            {
                inventario_jugador.Agregar_tarjeta();
            }

            Destroy(gameObject);
        }

        
    }
}
