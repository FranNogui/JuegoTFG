using System.Collections;
using TMPro;
using UnityEngine;

public class ControladorAgentes : MonoBehaviour
{
    [SerializeField] Transform almacenAgentes;
    [SerializeField] InformacionMapa info;
    [SerializeField] GameObject[] tiposDeAgente;
    [SerializeField] ControladorMenuOpciones controladorMenuOpciones;
    [SerializeField] ControladorCamara controladorCamara;
    [SerializeField] GameObject[] topLista;
    [SerializeField] GameObject topNick;
    GameObject[] topNicks;
    float[] tamanyosActuales;
    int[] topPos;

    int numAgentes;
    float velocidadInicial;
    GameObject[] agentes;

    Vector2 minPos;
    Vector2 maxPos; 

    public Vector2 MinPos { set { minPos = value; } }
    public Vector2 MaxPos { set { maxPos = value; } }

    void LateUpdate()
    {
        ActualizarTop();
        AjustarVelocidades();
    }

    void ActualizarTop()
    {
        for (int i = 0; i < info.numeroJugadores; i++)
        {
            tamanyosActuales[i] = agentes[i].GetComponent<ControladorAgente>().Volumen;
        }

        for (int i = 0; i < info.numeroJugadores; i++)
        {
            int j = i;
            while (j > 0 && tamanyosActuales[topPos[j]] > tamanyosActuales[topPos[j - 1]])
            {
                int aux = topPos[j];
                topPos[j] = topPos[j - 1];
                topPos[j - 1] = aux;
            }
        }

        for (int i = 0; i < info.numeroJugadores; i++)
        {
            topNicks[topPos[i]].SetActive(true);
            topNicks[topPos[i]].transform.SetParent(topLista[Mathf.Min(i, topLista.Length - 1)].transform, true);
            topNicks[topPos[i]].GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            topNicks[topPos[i]].GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            topNicks[topPos[i]].GetComponent<TextMeshProUGUI>().text = System.String.Format("{0,-15} : {1:.00}", agentes[topPos[i]].GetComponent<ControladorNombre>().Nombre(), tamanyosActuales[topPos[i]].ToString(".00"));
            if (agentes[topPos[i]].GetComponent<ControladorAgente>().Eliminado) topNicks[topPos[i]].GetComponent<TextMeshProUGUI>().color = Color.red;
            if (i >= topLista.Length - 1) topNicks[topPos[i]].SetActive(false);
        }
    }

    void Start()
    {
        topNicks = new GameObject[info.numeroJugadores];
        tamanyosActuales = new float[info.numeroJugadores];
        topPos = new int[info.numeroJugadores];
        agentes = new GameObject[info.numeroJugadores];
        for (int i = 0; i < agentes.Length; i++)
        {
            agentes[i] = Instantiate(tiposDeAgente[Random.Range(0, tiposDeAgente.Length)]);
            agentes[i].GetComponent<ControladorAgente>().RandomizarColor();
            agentes[i].GetComponent<ControladorAgente>().ID = i;
            agentes[i].GetComponent<ControladorAgente>().ControladorAgentes = this;
            agentes[i].GetComponent<ControladorNombre>().EstablecerNombre("Bot " + i);
            topNicks[i] = Instantiate(topNick);
            topNicks[i].transform.SetParent(topLista[Mathf.Min(i, topLista.Length - 1)].transform, true);
            topNicks[i].GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            topNicks[i].GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            topNicks[i].GetComponent<TextMeshProUGUI>().text = agentes[i].GetComponent<ControladorNombre>().Nombre() + ": 1,00";
            topNicks[i].transform.localScale = Vector3.one;
            topPos[i] = i;
            if (i >= topLista.Length - 1) topNicks[i].SetActive(false);
            agentes[i].transform.parent = almacenAgentes.transform;
        }
        if (info.tipoPartida == TipoPartida.JugadorVSMaquina)
        { 
            agentes[0].GetComponent<ModuloMovimiento>().CambiarA(TipoAgente.Jugador);
            agentes[0].GetComponent<ControladorNombre>().EstablecerNombre(PlayerPrefs.GetString("NombreJugador"));
            controladorCamara.ActualizarAgenteActual(agentes[0].transform, 0);
        }
        
        for (int i = 0; i < info.numeroJugadores; i++) 
        {
            bool valido = false;
            while (!valido)
            {
                agentes[i].transform.position = new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y), -1.0f);
                valido = true;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (((agentes[i].transform.position - agentes[j].transform.position).magnitude) < 2.0f)
                    { valido = false; break; }
                }
            }
        }

        velocidadInicial = agentes[0].GetComponent<ControladorAgente>().Velocidad;
  
        numAgentes = info.numeroJugadores;
        controladorMenuOpciones.CambiarNumeroAgentes(numAgentes);
    }

    public void EliminarAgente(int idEliminado, int idEliminador)
    {
        controladorMenuOpciones.CambiarNumeroAgentes(--numAgentes);

        if ((idEliminado == 0 && info.tipoPartida == TipoPartida.JugadorVSMaquina) || numAgentes == 1)
        {
            if (info.tipoPartida == TipoPartida.JugadorVSMaquina) 
            {
                if (idEliminado == 0)
                {
                    controladorMenuOpciones.CambiarTextoFinal("¡¡Derrota!!", Color.red);
                }
                else
                {
                    controladorMenuOpciones.CambiarTextoFinal("¡¡Victoria!!", Color.green);
                }
            }
            else
            {
                controladorMenuOpciones.CambiarTextoFinal("Fin de la partida", Color.white);
            }


            controladorMenuOpciones.AbrirMenuFinalPartida();
        }

        if (idEliminado == controladorCamara.AgenteId)
        {
            controladorCamara.ActualizarAgenteActual(agentes[idEliminador].transform, idEliminador);
        }
        StartCoroutine(EliminarAgenteCorutina(idEliminado, idEliminador)); 
    }

    IEnumerator EliminarAgenteCorutina(int idEliminado, int idEliminador)
    {
        float distancia = (agentes[idEliminado].transform.position - agentes[idEliminador].transform.position).magnitude;
        while (true)
        {
            if (agentes[idEliminado].transform.localScale.x > 0.5f)
            {
                agentes[idEliminado].transform.localScale = Vector3.Lerp(agentes[idEliminado].transform.localScale, Vector3.zero, 1.0f * Time.deltaTime);
                distancia = distancia - 2.0f * Time.deltaTime;
                float distanciaNueva = (agentes[idEliminado].transform.position - agentes[idEliminador].transform.position).magnitude;
                if (distanciaNueva <= distancia)
                {
                    distancia = distanciaNueva;
                }
                else
                {
                    if (distancia < 0.0f) distancia = 0.0f;
                    Vector2 vec = (agentes[idEliminado].transform.position - agentes[idEliminador].transform.position).normalized * distancia;
                    agentes[idEliminado].transform.position = new Vector3(agentes[idEliminador].transform.position.x + vec.x, agentes[idEliminador].transform.position.y + vec.y, -1.0f);
                }
            }
            else
            {
                agentes[idEliminado].SetActive(false);
                break;
            }
            yield return null;
        }
        yield return null;
    }

    void AjustarVelocidades()
    {
        float maxVelocidadProporcional = 0.0f;
        float velocidadActual;

        foreach (var agente in agentes)
        {
            if (agente.activeSelf)
            {
                ControladorAgente ag = agente.GetComponent<ControladorAgente>();
                maxVelocidadProporcional = Mathf.Max(maxVelocidadProporcional, ag.VelocidadProporcional);
            }
        }

        velocidadActual = velocidadInicial / (maxVelocidadProporcional / velocidadInicial);
        foreach (var agente in agentes) agente.GetComponent<ControladorAgente>().Velocidad = velocidadActual;
    }

    public void SeleccionarAgente(int idAgente)
    {
        if (info.tipoPartida == TipoPartida.MaquinaVSMaquina)
            controladorCamara.ActualizarAgenteActual(agentes[idAgente].transform, idAgente);
    }

    public void SeleccionarAgenteTOP(int top)
    {
        int agente = topPos[top];

        if (info.tipoPartida == TipoPartida.MaquinaVSMaquina && !agentes[agente].GetComponent<ControladorAgente>().Eliminado)
        {
            SeleccionarAgente(agente);
        }
    }
}