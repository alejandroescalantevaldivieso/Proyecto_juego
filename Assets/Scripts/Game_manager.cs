using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_manager : MonoBehaviour
{
    public GameObject cnv_victoria;
    public GameObject cnv_derrota;
    public Inventario_jugador inventario_Jugador;

    void Start()
    {
        Time.timeScale = 1;
        if (cnv_victoria != null) cnv_victoria.SetActive(false);
        if (cnv_derrota != null) cnv_derrota.SetActive(false);
    }

    void Update()
    {
        if (inventario_Jugador.vida <= 0)
        {
            Derrota();
        }
        else if (inventario_Jugador.cantidad_tarjetas >= 3 || inventario_Jugador.tiempo <= 0)
        {
            Victoria();
        }
    }

    public void Derrota()
    {
        Time.timeScale = 0;
        if (cnv_derrota != null) cnv_derrota.SetActive(true);
        if (cnv_victoria != null) cnv_victoria.SetActive(false);
    }

    public void Victoria()
    {
        Time.timeScale = 0;
        if (cnv_derrota != null) cnv_derrota.SetActive(false);
        if (cnv_victoria != null) cnv_victoria.SetActive(true);
    }

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
