using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEspectador : MonoBehaviour
{
    [SerializeField] float velocidadNormal = 6.0f;
    [SerializeField] float velocidadSprint = 12.0f;
    float velocidad;

    [SerializeField] float maxZoom   = 30.0f;
    [SerializeField] float minZoom   = 3.0f;
    Camera camara;

    Rigidbody2D cuerpo;

    [SerializeField] GameObject menuOpciones;
    Animator menuOpcionesAnimator;

    public bool canZoom;

    private void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        camara = Camera.main;
        velocidad = velocidadNormal;

        menuOpcionesAnimator = menuOpciones.GetComponent<Animator>();
        canZoom = true;
    }

    void Update()
    {
        Vector2 mov = Vector2.right * Input.GetAxis("Horizontal") + Vector2.up * Input.GetAxis("Vertical");
        if (mov.magnitude > 1.0f) mov.Normalize();

        if (Input.GetKey(KeyCode.LeftShift)) velocidad = velocidadSprint;
        else velocidad = velocidadNormal;

        cuerpo.velocity = mov * velocidad;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && camara.orthographicSize < maxZoom && canZoom)
        {
            camara.orthographicSize++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && camara.orthographicSize > minZoom && canZoom)
        {
            camara.orthographicSize--;
        }
    }
}
