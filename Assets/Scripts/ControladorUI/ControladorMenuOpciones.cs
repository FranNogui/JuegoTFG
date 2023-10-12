using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorMenuOpciones : MonoBehaviour
{
    [Header("Menu de opciones")]
    [SerializeField] GameObject menuOpciones;
    [SerializeField] GameObject menuConfirmacion;
    [SerializeField] Slider volumenMusica;
    [SerializeField] Slider volumenSFX;
    [SerializeField] TextMeshProUGUI musicaTexto;
    [SerializeField] TextMeshProUGUI SFXTexto;
    [SerializeField] AudioMixer mixer;
    Animator menuOpcionesAnimator;

    [Header("Contador Entes")]
    [SerializeField] TextMeshProUGUI numeroVivos;

    [Header("Menu Final Partida")]
    [SerializeField] GameObject menuFinalPartida;
    [SerializeField] TextMeshProUGUI textoFinalPartida;

    private void Start()
    {
        menuOpcionesAnimator = menuOpciones.GetComponent<Animator>();

        volumenMusica.value = PlayerPrefs.GetFloat("VolumenMusica");
        mixer.SetFloat("Musica", Mathf.Log10(PlayerPrefs.GetFloat("VolumenMusica")) * 20.0f);
        musicaTexto.text = (int)(PlayerPrefs.GetFloat("VolumenMusica") * 100.0f) + "%";

        volumenSFX.value = PlayerPrefs.GetFloat("VolumenSFX");
        mixer.SetFloat("Sonidos", Mathf.Log10(PlayerPrefs.GetFloat("VolumenSFX")) * 20.0f);
        SFXTexto.text = (int)(PlayerPrefs.GetFloat("VolumenSFX") * 100.0f) + "%";
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

    public void ReiniciarPulsado()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AbrirMenuFinalPartida()
    {
        menuFinalPartida.SetActive(true);
    }

    public void CambiarTextoFinal(string textoFinal, Color color)
    {
        textoFinalPartida.text = textoFinal;
        textoFinalPartida.color = color;
    }

    public void ActualizarBarraAudioMusica(float nuevoValor)
    {
        PlayerPrefs.SetFloat("VolumenMusica", nuevoValor);
        PlayerPrefs.Save();
        mixer.SetFloat("Musica", Mathf.Log10(nuevoValor) * 20.0f);
        musicaTexto.text = (int)(nuevoValor * 100.0f) + "%";
    }

    public void ActualizarBarraAudioSFX(float nuevoValor)
    {
        PlayerPrefs.SetFloat("VolumenSFX", nuevoValor);
        PlayerPrefs.Save();
        mixer.SetFloat("Sonidos", Mathf.Log10(nuevoValor) * 20.0f);
        SFXTexto.text = (int)(nuevoValor * 100.0f) + "%";
    }
}
