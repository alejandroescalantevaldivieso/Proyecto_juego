using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cura : MonoBehaviour
{
    public int cantidad_curacion = 20;
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
            Inventario_jugador inventario_Jugador = other.GetComponent<Inventario_jugador>();

            if(inventario_Jugador != null)
            {
                inventario_Jugador.Recuperar_vida(cantidad_curacion);
            }
            Destroy(gameObject);
        }        
    }
}
