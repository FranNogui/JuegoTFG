using UnityEngine;

/// <summary>
/// Clase encargada de controlar el tama�o y volumen del agente.
/// </summary>
public class ControladorTamanyo : MonoBehaviour
{
    [Tooltip("Volumen del agente.")]
    [SerializeField] float volumenActual = 1.0f;

    ControladorAgente controladorAgente;

    void Start()
    { controladorAgente = GetComponent<ControladorAgente>(); }

    private void Update()
    {
        if (controladorAgente.Eliminado) { return; }
        transform.localScale = Vector2.Lerp(transform.localScale, Vector3.one * Mathf.Sqrt(volumenActual), 1.0f * Time.deltaTime);
    }

    /// <summary>
    /// M�todo para cambiar el volumen actual del agente en una cantidad.
    /// </summary>
    /// <param name="cantidad">Cantidad a cambiar en el volumen.</param>
    public void CambiarTamanyo(float cantidad)
    { volumenActual += cantidad; }

    //** Getters y Setters **//

    /// <summary>Volumen actual del agente.</summary>
    public float VolumenActual
    { get { return volumenActual; } }

    //** FIN Getters y Setters **//
}