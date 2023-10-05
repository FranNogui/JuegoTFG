using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjosControlador : MonoBehaviour
{
    [SerializeField] GameObject[] ojos;
    [SerializeField] float maxRadio = 0.2f;

    public void ActualizarPosicion(Vector2 direccion)
    {

        foreach (GameObject ojo in ojos) 
        {
            ojo.transform.localPosition = Vector2.Lerp(ojo.transform.localPosition, direccion * maxRadio, 5.0f * Time.deltaTime);
        }
    }
}
