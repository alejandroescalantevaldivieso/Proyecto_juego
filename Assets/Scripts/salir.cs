using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class salir : MonoBehaviour
{
    //Obtener el boton
    public Button btn_salir;
    // Start is called before the first frame update
    void Start()
    {
        btn_salir.onClick.AddListener(salir_juego);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void salir_juego()
    {
        // Verifica si el juego se está ejecutando dentro del editor de Unity
        #if UNITY_EDITOR              
            UnityEditor.EditorApplication.isPlaying = false;

        #else
            // Si el juego está compilado, cierra la aplicación
            Application.Quit();

        #endif
    }
}
