using UnityEngine;

/// <summary>
/// Clase encargada de controlar el movimiento de los ojos.
/// </summary>
public class ControladorOjos : MonoBehaviour
{
    [SerializeField] Transform[] ojos;
    [SerializeField] float maximoRadio = 5.0f;

    /// <summary>
    /// M�todo para actualizar la posici�n actual del ojo de forma interpolada.
    /// </summary>
    /// <param name="direccion">Direcci�n a la que se va a encontrar el ojo.</param>
    public void ActualizarPosicion(Vector2 direccion)
    {
        foreach (var ojo in ojos) 
        { ojo.localPosition = Vector2.Lerp(ojo.localPosition, direccion * maximoRadio, 5.0f * Time.deltaTime); }
    }
}