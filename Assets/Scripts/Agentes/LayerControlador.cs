using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerControlador : MonoBehaviour
{
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
        Base.sortingOrder = LayerBase + extra;
        Outline.sortingOrder = LayerOutline + extra;
        foreach(var ojo in Ojos) ojo.sortingOrder = LayerOjo + extra;
        foreach(var ojoOutline in OjosOutline) ojoOutline.sortingOrder = LayerOjoOutline + extra;
        foreach(var pupila in Pupilas) pupila.sortingOrder = LayerPupila + extra;
    }
}
