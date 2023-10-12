using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerControlador : MonoBehaviour
{
    int valorMaxCapa = 32767;
    [SerializeField] int NumCapas = 4;
    [SerializeField] int LayerBase;
    [SerializeField] int LayerOutline;
    [SerializeField] int LayerOjo;
    [SerializeField] int LayerOjoOutline;
    [SerializeField] int LayerPupila;
    [SerializeField] SpriteRenderer Base;
    [SerializeField] SpriteRenderer Outline;
    [SerializeField] SpriteRenderer[] Ojos;
    [SerializeField] SpriteRenderer[] OjosOutline;
    [SerializeField] SpriteRenderer[] Pupilas;
    [SerializeField] TamanyoControlador tamanyo;

    private void Update()
    {
        int extra = (int)(tamanyo.TamanyoActual * 10.0f);
        Base.sortingOrder = (LayerBase + extra) % valorMaxCapa;
        Base.sortingLayerName = "Agentes" + Mathf.Min((LayerBase + extra) / valorMaxCapa, 4);
        Outline.sortingOrder = (LayerOutline + extra) % valorMaxCapa;
        Outline.sortingLayerName = "Agentes" + Mathf.Min((LayerOutline + extra) / valorMaxCapa, 4);
        foreach (var ojo in Ojos)
        {
            ojo.sortingOrder = (LayerOjo + extra) % valorMaxCapa;
            ojo.sortingLayerName = "Agentes" + Mathf.Min((LayerOjo + extra) / valorMaxCapa, 4);
        }
        foreach (var ojoOutline in OjosOutline)
        {
            ojoOutline.sortingOrder = (LayerOjoOutline + extra) % valorMaxCapa;
            ojoOutline.sortingLayerName = "Agentes" + Mathf.Min((LayerOjoOutline + extra) / valorMaxCapa, 4);
        }
        foreach (var pupila in Pupilas)
        {
            pupila.sortingOrder = (LayerPupila + extra) % valorMaxCapa;
            pupila.sortingLayerName = "Agentes" + Mathf.Min((LayerPupila + extra) / valorMaxCapa, 4);
        }
    }
}
