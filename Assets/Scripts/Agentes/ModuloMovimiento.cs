using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoAgente
{
    Jugador, IA
}

public class ModuloMovimiento : MonoBehaviour
{
    [SerializeField] TipoAgente tipo;

    public void CambiarA(TipoAgente nuevoTipo)
    {
        tipo = nuevoTipo;
    }

    public Vector2 ActualizarMovimiento()
    {
        if (tipo == TipoAgente.Jugador)
        {
            return Vector2.up * Input.GetAxis("Vertical") + Vector2.right * Input.GetAxis("Horizontal");
        }
        else if (tipo == TipoAgente.IA)
        {
            return Vector2.zero;
        }
        else
        { 
            return Vector2.zero;
        }
    }
}
