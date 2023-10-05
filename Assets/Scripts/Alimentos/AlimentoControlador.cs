using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlimentoControlador : MonoBehaviour
{
    [SerializeField] GameObject alimento;
    [SerializeField] SpriteRenderer render;
    [SerializeField] float volumenMinimo = 0.1f;
    [SerializeField] float volumenMaximo = 2.0f;
    public float volumen;

    AlimentoSpawner alimentoSpawner;
    int id;

    public AlimentoSpawner AlimentoSpawner
    {
        set { alimentoSpawner = value; }
    }

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    private void Start()
    {
        volumen = Random.Range(volumenMinimo, volumenMaximo);
        alimento.transform.localScale = Vector3.one * volumen;
        render.color = ObtenerColorAleatorio();
    }

    public void Randomizar()
    {
        volumen = Random.Range(volumenMinimo, volumenMaximo);
        alimento.transform.localScale = Vector3.one * volumen;
        render.color = ObtenerColorAleatorio();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AgenteControlador ag = collision.gameObject.GetComponent<AgenteControlador>();
        if (ag != null && ag.Volumen >= volumen) 
        {
            ag.AumentarTamanyo(volumen);
            StartCoroutine(Destruir());
        }
    }

    private Color ObtenerColorAleatorio()
    {
        float r = 0, g = 0, b = 0;
        switch(Random.Range(0, 3))
        {
            case 0: 
                r = 1.0f; 
                g = Random.Range(0.0f, 1.0f);
                b = Random.Range(0.0f, 1.0f);
                break;
            case 1: 
                g = 1.0f; 
                r = Random.Range(0.0f, 1.0f);
                b = Random.Range(0.0f, 1.0f);
                break;
            case 2: 
                b = 1.0f;
                r = Random.Range(0.0f, 1.0f);
                g = Random.Range(0.0f, 1.0f);
                break;
        }
        return new Color(r, g, b, 1.0f);
    }

    IEnumerator Destruir()
    {
        yield return 0;
        alimentoSpawner.DestruirAlimento(id);
    }
}
