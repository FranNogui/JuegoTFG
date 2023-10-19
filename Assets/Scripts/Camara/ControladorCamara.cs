using UnityEngine;

/// <summary>
/// Clase encargada de controlar el comportamiento global de la c�mara durante una partida.
/// </summary>
public class ControladorCamara : MonoBehaviour
{
    [Header("Elementos Necesarios")]

    [Tooltip("Objeto controlado mientras se especta la partida.")]
    [SerializeField] Transform enteEspectador;

    [Tooltip("ScriptableObject con la informaci�n de la partida.")]
    [SerializeField] InformacionMapa informacionMapa;

    Transform agenteActual;
    ControlEspectador controlEspectador;
    Camera camara;
    Transform camaraPosicion;
    Vector3 posicionAgente;

    int agenteId;
    
    public void Start()
    {
        agenteId = -1;
        controlEspectador = enteEspectador.gameObject.GetComponent<ControlEspectador>();
        camara = Camera.main;
        camaraPosicion = camara.transform;
        posicionAgente = Vector3.zero;
        if (informacionMapa.tipoPartida == TipoPartida.JugadorVSMaquina)
        { controlEspectador.ZoomHabilitado = false; }
    }

    void FixedUpdate()
    {
        if (informacionMapa.tipoPartida == TipoPartida.MaquinaVSMaquina)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            { 
                agenteActual = null;
                agenteId = -1;
            }
            if (agenteActual != null)
            {
                CamaraAAgente();
                enteEspectador.position = camaraPosicion.position;
                controlEspectador.ZoomHabilitado = false;
            }
            else
            {
                camaraPosicion.position = enteEspectador.position;
                controlEspectador.ZoomHabilitado = true;
            }
        }
        else CamaraAAgente();
    }

    /// <summary>
    /// M�todo que mueve la c�mara hacia la posici�n del agente de forma interpolada.
    /// </summary>
    void CamaraAAgente()
    {
        posicionAgente = agenteActual.position;
        posicionAgente.z = -10;
        camaraPosicion.position = Vector3.Lerp(camaraPosicion.position, posicionAgente, 0.2f);
        camara.orthographicSize = Mathf.Lerp(camara.orthographicSize, agenteActual.localScale.x + 5.0f, 1.0f * Time.deltaTime);
    }

    /// <summary>
    /// M�todo para establecer el agente que seguir� la c�mara.
    /// </summary>
    /// <param name="nuevoAgente">Transform del agente a seguir.</param>
    /// <param name="nuevoId">Identificador del agente dentro del Controlador de Agentes.</param>
    public void ActualizarAgenteActual(Transform nuevoAgente, int nuevoId)
    { agenteActual = nuevoAgente; agenteId = nuevoId; }

    //** Getters y Setters **//

    /// <summary>ID del agente que sigue la c�mara.</summary>
    public int AgenteId
    { get { return agenteId; } }

    //** FIN Getters y Setters **//
}