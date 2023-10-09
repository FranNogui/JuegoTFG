using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenuOpciones : MonoBehaviour
{
    [Header("Menu de opciones")]
    [SerializeField] GameObject menuOpciones;
    [SerializeField] GameObject menuConfirmacion;
    Animator menuOpcionesAnimator;

    [Header("Contador Entes")]
    [SerializeField] TextMeshProUGUI numeroVivos;

    private void Start()
    {
        menuOpcionesAnimator = menuOpciones.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) menuOpcionesAnimator.SetBool("Abierto", !menuOpcionesAnimator.GetBool("Abierto"));
    }

    public void OpcionesPulsado()
    {
        menuOpcionesAnimator.SetBool("Abierto", true);
    }

    public void CerrarPulsado()
    {
        menuOpcionesAnimator.SetBool("Abierto", false);
    }

    public void CambiarNumeroAgentes(int nuevoNumero)
    { 
        numeroVivos.text = nuevoNumero.ToString();
    }

    public void SalirPulsado()
    {
        menuConfirmacion.SetActive(true);
    }

    public void AceptarPulsado()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void CancelarPulsado()
    {
        menuConfirmacion.SetActive(false);
    }
}
