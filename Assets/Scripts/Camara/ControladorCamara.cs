using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCamara : MonoBehaviour
{
    [SerializeField] GameObject enteEspectador;
    GameObject agenteActual;   
    [SerializeField] InformacionMapa info;

    int agenteId;
    public int AgenteId
    {
        get { return agenteId; }
    }

    public void Start()
    {
        agenteId = -1;
        if (info.tipoPartida == TipoPartida.JVM)
        { enteEspectador.GetComponent<ControlEspectador>().canZoom = false; }
    }

    private void FixedUpdate()
    {
        if (info.tipoPartida == TipoPartida.MVM)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            { 
                agenteActual = null;
                agenteId = -1;
            }
                

            if (agenteActual != null)
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(agenteActual.transform.position.x, agenteActual.transform.position.y, -10), 0.2f);
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, agenteActual.transform.localScale.x + 5.0f, 1.0f * Time.deltaTime);
                enteEspectador.transform.position = Camera.main.transform.position;
                enteEspectador.GetComponent<ControlEspectador>().canZoom = false;
            }
            else
            {
                Camera.main.transform.position = new Vector3(enteEspectador.transform.position.x, enteEspectador.transform.position.y, -10);
                enteEspectador.GetComponent<ControlEspectador>().canZoom = true;
            }
        }
        else
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(agenteActual.transform.position.x, agenteActual.transform.position.y, -10), 0.2f);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, agenteActual.transform.localScale.x + 5.0f, 1.0f * Time.deltaTime);
        }
    }

    public void ActualizarAgenteActual(GameObject nuevoAgente, int nuevoId)
    {
        agenteActual = nuevoAgente;
        agenteId = nuevoId;
    }
}
