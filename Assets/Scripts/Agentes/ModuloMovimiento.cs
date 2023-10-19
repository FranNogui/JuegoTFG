using UnityEngine;

/// <summary>
/// Enumerable para distingir los distintos elementos que pueden controlar a los agentes.
/// </summary>
public enum TipoAgente
{
    Jugador, 
    IAProgramada
}

/// <summary>
/// Clase encargada de decidir que m�dulo mover� al agente.
/// </summary>
public class ModuloMovimiento : MonoBehaviour
{
    [Tooltip("Elemento que mover� al agente.")]
    [SerializeField] TipoAgente tipoAgente;

    IAProgramada iaProgramada;

    void Awake()
    { 
        iaProgramada = new IAProgramada(GetComponent<ControladorAgente>(), transform);
    }

    /// <summary>
    /// M�todo para cambiar el elemento que mueve al agente.
    /// </summary>
    /// <param name="nuevoTipo">Nuevo elemento.</param>
    public void CambiarA(TipoAgente nuevoTipo)
    { tipoAgente = nuevoTipo; }

    /// <summary>
    /// M�todo para obtener el vector resultado de ejecutar un paso de movimiento.
    /// </summary>
    /// <param name="objetos">Lista de objetos que el agente ve a trav�s de los RayCast.</param>
    public Vector2 ActualizarMovimiento(ObjetoDeRayCast[] objetos)
    {
        switch(tipoAgente)
        {
            case TipoAgente.Jugador:
                return Vector2.up * Input.GetAxis("Vertical") + Vector2.right * Input.GetAxis("Horizontal");
            case TipoAgente.IAProgramada:
                return iaProgramada.Resultado(objetos);
            default:
                return Vector2.zero;
        }
    }
}