using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoObjeto
{
    Agente, Alimento, Borde, Nada
}

public class RayCastObject
{
    public RayCastObject()
    {
        tipo = TipoObjeto.Nada;
        posicion = Vector2.zero;
        tamanyo = 0.0f;
        distancia = 0.0f;
    }

    public TipoObjeto tipo;
    public Vector3 posicion;
    public float tamanyo;
    public float distancia;
}
