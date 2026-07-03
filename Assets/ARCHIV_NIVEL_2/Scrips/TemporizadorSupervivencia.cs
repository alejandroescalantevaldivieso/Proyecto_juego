using UnityEngine;
using TMPro;

public class TemporizadorSupervivencia : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Tiempo en segundos. 120 = 2 minutos")]
    public float tiempoParaGanar = 120f;

    [Tooltip("Arrastra aquí tu texto de UI para ver el tiempo (Opcional)")]
    public TextMeshProUGUI textoTiempo;

    private bool nivelCompletado = false;
    private AdministradorUI adminUI;
    
    void Start()
    {
       
        adminUI = FindObjectOfType<AdministradorUI>();
    }

    void Update()
    {
      
        if (nivelCompletado) return;

        if (tiempoParaGanar > 0)
        {
      
            tiempoParaGanar -= Time.deltaTime;

            
            if (textoTiempo != null)
            {
                int minutos = Mathf.FloorToInt(tiempoParaGanar / 60);
                int segundos = Mathf.FloorToInt(tiempoParaGanar % 60);
                textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
            }
        }
        else
        {
       
            tiempoParaGanar = 0;
            nivelCompletado = true;

            if (textoTiempo != null) textoTiempo.text = "00:00";


            if (adminUI != null)
            {
                adminUI.MostrarVictoria();
            }
        }
    }
}