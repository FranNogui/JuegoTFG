using UnityEngine;

/// <summary>
/// Enumerable con los posible tipos de objetos que puede identifcar un RayCast.
/// </summary>
public enum TipoObjetoRayCast
{
    Agente, 
    Alimento, 
    Borde, 
    Nada
}

/// <summary>
/// Clase para almacenar la informaci�n de un objeto identificado por un RayCast.
/// </summary>
public class ObjetoDeRayCast
{
    public TipoObjetoRayCast tipo;
    public Vector3 posicion;
    public float tamanyo;
    public float distancia;

    public ObjetoDeRayCast()
    {
        tipo      = TipoObjetoRayCast.Nada;
        posicion  = Vector2.zero;
        tamanyo   = 0.0f;
        distancia = 0.0f;
    }  
}