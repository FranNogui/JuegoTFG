using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
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
    [SerializeField] Slider volumenMusica;
    [SerializeField] Slider volumenSFX;
    [SerializeField] TextMeshProUGUI musicaTexto;
    [SerializeField] TextMeshProUGUI SFXTexto;
    [SerializeField] AudioMixer mixer;
    Animator menuOpcionesAnimator;

    [Header("Informacion del mapa")]
    [SerializeField] InformacionMapa info;

    [Header("Opciones de partida")]
    [SerializeField] Slider sliderJugadores;
    [SerializeField] Toggle pequenyo;
    [SerializeField] Toggle mediano;
    [SerializeField] Toggle grande;


    private void Start()
    {
        menuJugarAnimator  = menuJugar.GetComponent<Animator>();
        menuJugarOpciones1 = menuJugar.transform.Find("Opciones1").gameObject;
        menuJugarOpciones2 = menuJugar.transform.Find("Opciones2").gameObject;
        volumenMusica.value = PlayerPrefs.GetFloat("VolumenMusica");
        volumenSFX.value = PlayerPrefs.GetFloat("VolumenSFX");

        volumenMusica.value = PlayerPrefs.GetFloat("VolumenMusica");
        mixer.SetFloat("Musica", Mathf.Log10(PlayerPrefs.GetFloat("VolumenMusica")) * 20.0f);
        musicaTexto.text = (int)(PlayerPrefs.GetFloat("VolumenMusica") * 100.0f) + "%";

        volumenSFX.value = PlayerPrefs.GetFloat("VolumenSFX");
        mixer.SetFloat("Sonidos", Mathf.Log10(PlayerPrefs.GetFloat("VolumenSFX")) * 20.0f);
        SFXTexto.text = (int)(PlayerPrefs.GetFloat("VolumenSFX") * 100.0f) + "%";

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
        info.tipoPartida = TipoPartida.JVM;
    }

    public void MaquinaContraMaquinaPulsado()
    {
        menuJugarOpciones1.SetActive(false);
        menuJugarOpciones2.SetActive(true);
        imagenElegido.sprite = imagenComputador;
        info.tipoPartida = TipoPartida.MVM;
    }

    public void ActualizarNumeroJugadores(float nuevoNumero)
    {
        numeroJugadores.text = nuevoNumero.ToString();
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

    public void CancelarPartidaPusado()
    {
        menuJugarOpciones1.SetActive(true);
        menuJugarOpciones2.SetActive(false);
    }

    public void AceptarPartidaPulsado()
    {
        if      (pequenyo.isOn) info.tamanyo = TamanyoMapa.Pequenyo;
        else if (mediano.isOn)  info.tamanyo = TamanyoMapa.Mediano;
        else if (grande.isOn)   info.tamanyo = TamanyoMapa.Grande;

        info.numJugadores = (int) sliderJugadores.value;

        SceneManager.LoadScene("EscenaJuego");
    }
}
