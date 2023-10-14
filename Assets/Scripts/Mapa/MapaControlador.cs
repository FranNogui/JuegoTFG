using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MapaControlador : MonoBehaviour
{
    [SerializeField] InformacionMapa info;
    [SerializeField] GameObject bordesPequenyo;
    [SerializeField] GameObject bordesMediano;
    [SerializeField] GameObject bordesGrande;
    [SerializeField] Transform limiteMinPequenyo;
    [SerializeField] Transform limiteMaxPequenyo;
    [SerializeField] Transform limiteMinMediano;
    [SerializeField] Transform limiteMaxMediano;
    [SerializeField] Transform limiteMinGrande;
    [SerializeField] Transform limiteMaxGrande;

    [SerializeField] AlimentoSpawner alimentoSpawner;
    [SerializeField] ControladorAgentes controladorAgentes;
    [SerializeField] SpriteRenderer fondo;

    [SerializeField] TextMeshProUGUI contadorTexto;
    float contador;

    private void Awake()
    {
        contador = 0.0f;
        switch (info.tamanyo)
        {
            case TamanyoMapa.Pequenyo:
                bordesMediano.SetActive(false);
                bordesGrande.SetActive(false);
                alimentoSpawner.MinPos = limiteMinPequenyo.position;
                alimentoSpawner.MaxPos = limiteMaxPequenyo.position;
                alimentoSpawner.maxAlimentos = 1000;
                controladorAgentes.MinPos = limiteMinPequenyo.position;
                controladorAgentes.MaxPos = limiteMaxPequenyo.position;
                fondo.size = limiteMaxPequenyo.position - limiteMinPequenyo.position;
                break;
            case TamanyoMapa.Mediano:
                bordesPequenyo.SetActive(false);
                bordesGrande.SetActive(false);
                alimentoSpawner.MinPos = limiteMinMediano.position;
                alimentoSpawner.MaxPos = limiteMaxMediano.position;
                alimentoSpawner.maxAlimentos = 4000;
                controladorAgentes.MinPos = limiteMinMediano.position;
                controladorAgentes.MaxPos = limiteMaxMediano.position;
                fondo.size = limiteMaxMediano.position - limiteMinMediano.position;
                break;
            case TamanyoMapa.Grande:
                bordesPequenyo.SetActive(false);
                bordesMediano.SetActive(false);
                alimentoSpawner.MinPos = limiteMinGrande.position;
                alimentoSpawner.MaxPos = limiteMaxGrande.position;
                alimentoSpawner.maxAlimentos = 8000;
                controladorAgentes.MinPos = limiteMinGrande.position;
                controladorAgentes.MaxPos = limiteMaxGrande.position;
                fondo.size = limiteMaxGrande.position - limiteMinGrande.position;
                break;
        }
    }

    private void Update()
    {
        contador += Time.deltaTime;
        contadorTexto.text = ((int)(contador / 60.0f)).ToString("00") + ":" + ((int)(contador % 60.0f)).ToString("00");
    }
}
