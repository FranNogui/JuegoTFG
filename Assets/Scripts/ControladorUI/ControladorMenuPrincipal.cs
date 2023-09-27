using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControladorMenuPrincipal : MonoBehaviour
{
    [Header("Menu de juego")]
    [SerializeField] GameObject menuJugar;
    Animator   menuJugarAnimator;
    GameObject menuJugarOpciones1;
    GameObject menuJugarOpciones2;

    [SerializeField] TextMeshProUGUI numeroJugadores;

    [SerializeField] Image  imagenElegido;
    [SerializeField] Sprite imagenHumano;
    [SerializeField] Sprite imagenComputador;

    [Header("Menu de opciones")]
    [SerializeField] GameObject menuOpciones;
    Animator menuOpcionesAnimator;



    private void Start()
    {
        menuJugarAnimator  = menuJugar.GetComponent<Animator>();
        menuJugarOpciones1 = menuJugar.transform.Find("Opciones1").gameObject;
        menuJugarOpciones2 = menuJugar.transform.Find("Opciones2").gameObject;

        menuOpcionesAnimator = menuOpciones.GetComponent<Animator>();
    }

    public void JugarPulsado()
    {
        menuJugarAnimator.SetBool("Abierto", true);
        menuJugarOpciones1.SetActive(true);
        menuJugarOpciones2.SetActive(false);
    }

    public void OpcionesPulsado()
    {
        menuOpcionesAnimator.SetBool("Abierto", true);
    }

    public void SalirPulsado()
    {

    }

    public void CerrarMenuOpcionesPulsado()
    {
        menuOpcionesAnimator.SetBool("Abierto", false);
    }

    public void CerrarMenuJugarPulsado()
    {
        menuJugarAnimator.SetBool("Abierto", false);
    }

    public void JugadorContraMaquinaPulsado()
    {
        menuJugarOpciones1.SetActive(false);
        menuJugarOpciones2.SetActive(true);
        imagenElegido.sprite = imagenHumano;
    }

    public void MaquinaContraMaquinaPulsado()
    {
        menuJugarOpciones1.SetActive(false);
        menuJugarOpciones2.SetActive(true);
        imagenElegido.sprite = imagenComputador;
    }

    public void ActualizarNumeroJugadores(float nuevoNumero)
    {
        numeroJugadores.text = nuevoNumero.ToString();
    }

    public void CancelarPartidaPusado()
    {
        menuJugarOpciones1.SetActive(true);
        menuJugarOpciones2.SetActive(false);
    }

    public void AceptarPartidaPulsado()
    {

    }
}
