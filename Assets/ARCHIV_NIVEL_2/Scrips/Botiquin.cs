using UnityEngine;

using UnityEngine;

public class Botiquin : MonoBehaviour
{
    public int cantidadCura = 5; // Cuántos puntos de vida recupera

    // OnTriggerEnter detecta automáticamente cuando alguien atraviesa este objeto
    private void OnTriggerEnter(Collider otro)
    {
        // Verificamos si el objeto que lo atravesó tiene la etiqueta del jugador
        if (otro.CompareTag("Player"))
        {
            VidaJugador scriptVida = otro.GetComponent<VidaJugador>();

            if (scriptVida != null)
            {
                scriptVida.Curar(cantidadCura); // Llama a la función que creamos antes
                Destroy(gameObject); // Destruye el botiquín de la escena
            }
        }
    }
}