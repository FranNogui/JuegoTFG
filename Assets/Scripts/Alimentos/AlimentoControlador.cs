using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlimentoControlador : MonoBehaviour
{
    [SerializeField] GameObject alimento;
    [SerializeField] SpriteRenderer render;
    [SerializeField] float volumenMinimo = 1.0f;
    [SerializeField] float volumenMaximo = 2.0f;
    [SerializeField] float tamanyoPaso = 1.0f;
    public float volumen;

    bool comido;

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
        alimento.transform.localScale = Vector3.one + Vector3.one * volumen * tamanyoPaso;
        render.color = ObtenerColorAleatorio();
        comido = false;
    }

    public void Randomizar()
    {
        volumen = Random.Range(volumenMinimo, volumenMaximo);
        alimento.transform.localScale = Vector3.one + Vector3.one * volumen * tamanyoPaso;
        render.color = ObtenerColorAleatorio();
        comido = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (comido) return;
        AgenteControlador ag = collision.gameObject.GetComponent<AgenteControlador>();
        if (ag != null && ag.Volumen >= volumen) 
        {
            comido = true;
            ag.AumentarTamanyo(volumen);
            StartCoroutine(Destruir(collision.transform));
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

    IEnumerator Destruir(Transform posAgente)
    {
        float distancia = (transform.position - posAgente.position).magnitude;
        while (true)
        {
            if (transform.localScale.x > 0.1f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 1.0f * Time.deltaTime);
                distancia = distancia - 2.0f * Time.deltaTime;
                float distanciaNueva = (transform.position - posAgente.position).magnitude;
                if (distanciaNueva <= distancia)
                {
                    distancia = distanciaNueva;
                }
                else
                {
                    if (distancia < 0.0f) distancia = 0.0f;
                    Vector2 vec = (transform.position - posAgente.position).normalized * distancia;
                    transform.position = new Vector2(posAgente.position.x + vec.x, posAgente.position.y + vec.y);
                }  
            }
            else
            {
                alimentoSpawner.DestruirAlimento(id);
                break;
            }
            yield return null;
        }
        yield break;
    }
}
