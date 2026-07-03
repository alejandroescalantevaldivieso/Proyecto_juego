using UnityEngine;

public class GeneradorBotiquines : MonoBehaviour
{
    public GameObject prefabBotiquin; // La plantilla del botiquín que creamos
    public Transform[] puntosDeAparicion; // Lista de lugares donde saldrán
    public float tiempoEntreApariciones = 15f; // Segundos que tarda en salir uno nuevo

    private float temporizador;

    void Update()
    {
        temporizador += Time.deltaTime;

        if (temporizador >= tiempoEntreApariciones)
        {
            GenerarBotiquin();
            temporizador = 0f; // Reinicia el reloj del generador
        }
    }

    void GenerarBotiquin()
    {
        if (puntosDeAparicion.Length == 0 || prefabBotiquin == null) return;

        // Elige un punto al azar de la lista
        int indiceAleatorio = Random.Range(0, puntosDeAparicion.Length);
        Transform puntoElegido = puntosDeAparicion[indiceAleatorio];

        // Clona el botiquín en el mapa
        Instantiate(prefabBotiquin, puntoElegido.position, puntoElegido.rotation);
        Debug.Log("¡Un nuevo botiquín ha aparecido!");
    }
}