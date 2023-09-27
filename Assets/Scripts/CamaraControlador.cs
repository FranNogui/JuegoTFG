using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraControlador : MonoBehaviour
{
    [SerializeField] float velocidad;
    Camera camara;
    float camaraZ;
    Vector2 posicionInicial, posicionFinal, posCamaraInicial;

    void Start()
    {
        camara  = Camera.main;
        camaraZ = camara.transform.position.z;
        posicionInicial = new Vector2(0, 0);
        posicionFinal   = new Vector2(0, 0);
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print(posCamaraInicial);
            posicionInicial  = Input.mousePosition;
            posicionFinal    = Input.mousePosition;
            posCamaraInicial = camara.transform.position;
        }
        if (Input.GetMouseButton(0))
        {
            posicionFinal = Input.mousePosition;
            Vector2 diferencia = Camera.main.ScreenToViewportPoint(posicionFinal - posicionInicial);
            Vector3 res = posCamaraInicial - diferencia * velocidad;
            res.z = camaraZ;
            camara.transform.position = res;
        }
    }
}
