using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamanyoControlador : MonoBehaviour
{
    [SerializeField] float tamanyoActual;
    [SerializeField] float tamanyoPaso;

    public float TamanyoActual
    {
        get { return tamanyoActual; }
    }

    private void Update()
    {
        this.gameObject.transform.localScale = Vector2.Lerp(this.gameObject.transform.localScale, Vector3.one + Vector3.one * (tamanyoPaso * tamanyoActual), 1.0f * Time.deltaTime);
    }

    public void CambiarTamanyo(float cantidad)
    {
        tamanyoActual += cantidad;
    }
}
