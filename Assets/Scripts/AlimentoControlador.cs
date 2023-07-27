using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlimentoControlador : MonoBehaviour
{
    [SerializeField] GameObject alimento;
    [SerializeField] float volumenMinimo = 0.1f;
    [SerializeField] float volumenMaximo = 2.0f;
    public float volumen;

    private void Start()
    {
        volumen = Random.Range(volumenMinimo, volumenMaximo);
        alimento.transform.localScale = Vector3.one * volumen;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AgJugador ag = collision.gameObject.GetComponent<AgJugador>();
        if (ag != null && ag.Volumen >= volumen) 
        {
            ag.AumentarTamanyo(volumen);
            StartCoroutine(Destruir());
        }
    }

    IEnumerator Destruir()
    {
        yield return 0;
        Destroy(alimento);
    }
}
