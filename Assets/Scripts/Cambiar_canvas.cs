using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cambiar_canvas : MonoBehaviour
{
    public GameObject cnv_menu_principal;
    public GameObject cnv_seleccion_personaje;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ocultar_menu_principal()
    {
        cnv_menu_principal.SetActive(false);
        cnv_seleccion_personaje.SetActive(true);
    }
    public void ocultar_seleccion_personaje()
    {
        cnv_seleccion_personaje.SetActive(false);
        cnv_menu_principal.SetActive(true);
    }
}
