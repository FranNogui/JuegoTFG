using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControladorAgentes : MonoBehaviour
{
    [SerializeField] GameObject almacenAgentes;
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

    public void Update()
    {
        ActualizarTop();
    }

    void ActualizarTop()
    {
        for (int i = 0; i < info.numJugadores; i++)
        {
            tamanyosActuales[i] = agentes[i].GetComponent<AgenteControlador>().Volumen;
        }

        for (int i = 0; i < info.numJugadores; i++)
        {
            int j = i;
            while (j > 0 && tamanyosActuales[topPos[j]] > tamanyosActuales[topPos[j - 1]])
            {
                int aux = topPos[j];
                topPos[j] = topPos[j - 1];
                topPos[j - 1] = aux;
            }
        }

        for (int i = 0; i < info.numJugadores; i++)
        {
            topNicks[topPos[i]].SetActive(true);
            topNicks[topPos[i]].transform.SetParent(topLista[Mathf.Min(i, topLista.Length - 1)].transform, true);
            topNicks[topPos[i]].GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            topNicks[topPos[i]].GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            topNicks[topPos[i]].GetComponent<TextMeshProUGUI>().text = System.String.Format("{0,-15} : {1:.00}", agentes[topPos[i]].GetComponent<NombreControlador>().Nombre(), tamanyosActuales[topPos[i]].ToString(".00"));
            if (agentes[topPos[i]].GetComponent<AgenteControlador>().Eliminado) topNicks[topPos[i]].GetComponent<TextMeshProUGUI>().color = Color.red;
            if (i >= topLista.Length - 1) topNicks[topPos[i]].SetActive(false);
        }
    }

    private void Start()
    {
        topNicks = new GameObject[info.numJugadores];
        tamanyosActuales = new float[info.numJugadores];
        topPos = new int[info.numJugadores];
        agentes = new GameObject[info.numJugadores];
        for (int i = 0; i < agentes.Length; i++)
        {
            agentes[i] = Instantiate(tiposDeAgente[Random.Range(0, tiposDeAgente.Length)]);
            agentes[i].GetComponent<AgenteControlador>().RandomizarColor();
            agentes[i].GetComponent<AgenteControlador>().ID = i;
            agentes[i].GetComponent<AgenteControlador>().ControladorAgentes = this;
            agentes[i].GetComponent<NombreControlador>().EstablecerNombre("Bot " + i);
            topNicks[i] = Instantiate(topNick);
            topNicks[i].transform.SetParent(topLista[Mathf.Min(i, topLista.Length - 1)].transform, true);
            topNicks[i].GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            topNicks[i].GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            topNicks[i].GetComponent<TextMeshProUGUI>().text = agentes[i].GetComponent<NombreControlador>().Nombre() + ": 1,00";
            topNicks[i].transform.localScale = Vector3.one;
            topPos[i] = i;
            if (i >= topLista.Length - 1) topNicks[i].SetActive(false);
            agentes[i].transform.parent = almacenAgentes.transform;
        }
        if (info.tipoPartida == TipoPartida.JVM)
        { 
            agentes[0].GetComponent<ModuloMovimiento>().CambiarA(TipoAgente.Jugador);
            agentes[0].GetComponent<NombreControlador>().EstablecerNombre(PlayerPrefs.GetString("NombreJugador"));
            controladorCamara.ActualizarAgenteActual(agentes[0], 0);
        }
        
        for (int i = 0; i < info.numJugadores; i++) 
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

        velocidadInicial = agentes[0].GetComponent<AgenteControlador>().Velocidad;
  
        numAgentes = info.numJugadores;
        controladorMenuOpciones.CambiarNumeroAgentes(numAgentes);
    }

    public void EliminarAgente(int idEliminado, int idEliminador)
    {
        controladorMenuOpciones.CambiarNumeroAgentes(--numAgentes);

        if ((idEliminado == 0 && info.tipoPartida == TipoPartida.JVM) || numAgentes == 1)
        {
            if (info.tipoPartida == TipoPartida.JVM) 
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
            controladorCamara.ActualizarAgenteActual(agentes[idEliminador], idEliminador);
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
                AjustarVelocidades();
                break;
            }
            yield return null;
        }
        yield return null;
    }

    private void AjustarVelocidades()
    {
        float minVelocidadProporcional = velocidadInicial;
        float velocidadActual;
        foreach (var agente in agentes)
        {
            if (agente.gameObject.activeSelf)
            {
                AgenteControlador ag = agente.GetComponent<AgenteControlador>();
                minVelocidadProporcional = Mathf.Min(minVelocidadProporcional, ag.VelocidadProporcional);
            }
        }

        velocidadActual = velocidadInicial / (minVelocidadProporcional / velocidadInicial);
        foreach (var agente in agentes) agente.GetComponent<AgenteControlador>().Velocidad = velocidadActual;
    }

    public void SeleccionarAgente(int idAgente)
    {
        if (info.tipoPartida == TipoPartida.MVM)
            controladorCamara.ActualizarAgenteActual(agentes[idAgente], idAgente);
    }

    public void SeleccionarAgenteTOP(int top)
    {
        int agente = topPos[top];

        if (info.tipoPartida == TipoPartida.MVM && !agentes[agente].GetComponent<AgenteControlador>().Eliminado)
        {
            SeleccionarAgente(agente);
        }
    }
}
