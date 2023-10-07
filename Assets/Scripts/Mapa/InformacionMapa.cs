using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TamanyoMapa
{
    Pequenyo, Mediano, Grande
}

public enum TipoPartida
{
    MVM, JVM
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InfoMapa", order = 1)]
public class InformacionMapa : ScriptableObject
{
    public TipoPartida tipoPartida;
    public TamanyoMapa tamanyo;
    public int numJugadores;
}
