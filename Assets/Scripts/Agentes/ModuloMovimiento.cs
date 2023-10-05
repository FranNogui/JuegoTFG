using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TipoAgente
{
    Jugador, IA
}

public class ModuloMovimiento : MonoBehaviour
{
    [SerializeField] TipoAgente tipo = TipoAgente.IA;

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
