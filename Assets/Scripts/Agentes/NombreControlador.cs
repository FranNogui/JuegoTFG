using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NombreControlador : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI nombreLabel;

    public void EstablecerNombre(string nuevoNombre)
    {
        nombreLabel.text = nuevoNombre;
    }

    public string Nombre()
    {
        return nombreLabel.text;
    }

    public void OcultarNick()
    {
        nombreLabel.gameObject.SetActive(false);
    }

    public void MostrarNick()
    {
        nombreLabel.gameObject.SetActive(true);
    }

    public void CambiarEstado()
    {
        nombreLabel.gameObject.SetActive(!nombreLabel.gameObject.activeSelf);
    }
}
