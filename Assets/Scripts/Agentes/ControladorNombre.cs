using TMPro;
using UnityEngine;

/// <summary>
/// Clase encargada de controlar el label con el nombre del agente.
/// </summary>
public class ControladorNombre : MonoBehaviour
{
    [Tooltip("Label donde aparece el nombre del agente.")]
    [SerializeField] TextMeshProUGUI nombreLabel;

    /// <summary>
    /// M�todo para cambiar el nombre actual del agente.
    /// </summary>
    /// <param name="nuevoNombre">Nombre a colocar.</param>
    public void EstablecerNombre(string nuevoNombre)
    { nombreLabel.text = nuevoNombre; }

    /// <summary>
    /// M�todo para obtener el nombre actual del agente.
    /// </summary>
    public string Nombre()
    {  return nombreLabel.text; }

    /// <summary>
    /// M�todo para ocultar el label con el nombre.
    /// </summary>
    public void OcultarNick()
    { nombreLabel.gameObject.SetActive(false); }

    /// <summary>
    /// M�todo para mostrar el label con el nombre.
    /// </summary>
    public void MostrarNick()
    { nombreLabel.gameObject.SetActive(true); }

    /// <summary>
    /// M�todo para alternar mostrar/ocultar el label con el nombre.
    /// </summary>
    public void CambiarEstado()
    {  nombreLabel.gameObject.SetActive(!nombreLabel.gameObject.activeSelf); }
}