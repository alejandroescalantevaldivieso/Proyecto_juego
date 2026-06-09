using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_manager : MonoBehaviour
{
    public GameObject cnv_victoria;
    public GameObject cnv_derrota;
    public Inventario_jugador inventario_Jugador;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        cnv_victoria.SetActive(false);
        cnv_derrota.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(inventario_Jugador.vida == 0)
        {
            Derrota();
        }

        if(inventario_Jugador.cantidad_tarjetas == 3 || inventario_Jugador.tiempo == 0)
        {
            Victoria();
        }
    }

    public void Derrota()
    {
        Time.timeScale = 0;
        cnv_derrota.SetActive(true);
        cnv_victoria.SetActive(false);
    }
    public void Victoria()
    {
        Time.timeScale = 0;
        cnv_derrota.SetActive(false);
        cnv_victoria.SetActive(true);
    }

    // Ir al siguiente nivel 
    public void Continuar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scena03");
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scena01");
    }

    public void Reintentar()
    {
        Time.timeScale = 1;
        Scene scenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scenaActual.name);
    }
}
