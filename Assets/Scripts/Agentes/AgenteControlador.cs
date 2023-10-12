using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgenteControlador : MonoBehaviour
{
    const int numRayCasts = 256;
    RayCastObject[] objetos;

    [SerializeField] float velocidad = 1.0f;
    [SerializeField] float decVelocidadPorTamanyo = 0.001f;
    [SerializeField] float distanciaVision = 5.0f;
    [SerializeField] float margenVolumenPorcentual = 1.25f;

    public float MargenVolumenPorcentual
    {
        get { return margenVolumenPorcentual; }
    }

    float probabilidadParpadeo = 0.0f;
    float incProbParpadeo = 0.001f;
    float tamanyoProporcion = 1.0f;
    Vector2 mov;
    Vector2 movPrev;

    bool eliminado;

    OjosControlador ojos;
    TamanyoControlador tamanyo;
    Rigidbody2D body;
    ModuloMovimiento modMov;
    Animator animator;
    ControladorSonidosAgente controladorSonidos;
    Collider2D colision;
    

    float[] dirX = new float[numRayCasts];
    float[] dirY = new float[numRayCasts];

    int id;
    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    ControladorAgentes controladorAgentes;
    public ControladorAgentes ControladorAgentes
    {
        set { controladorAgentes = value; }
    }

    public float Volumen
    {
        get { return tamanyo.TamanyoActual + 1.0f; }
    }

    private void Awake()
    {
        objetos = new RayCastObject[numRayCasts];
        for (int i = 0; i < numRayCasts; i++)
        {
            dirX[i] = Mathf.Cos(i * (2 * Mathf.PI / numRayCasts));
            dirY[i] = Mathf.Sin(i * (2 * Mathf.PI / numRayCasts));
            objetos[i] = new RayCastObject();
            objetos[i].tipo = TipoObjeto.Nada;
        }
    }

    void Start()
    {
        ojos = GetComponent<OjosControlador>();
        tamanyo = GetComponent<TamanyoControlador>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        modMov = GetComponent<ModuloMovimiento>();
        controladorSonidos = GetComponent<ControladorSonidosAgente>();
        colision = GetComponent<Collider2D>();
        mov = Vector2.zero;
        movPrev = mov;
        eliminado = false;

        StartCoroutine(EjecutarRayCast());
    }

    void Update()
    {
        if (eliminado) return;
        mov = modMov.ActualizarMovimiento(objetos);
        if (mov.magnitude > 1.0f) { mov.Normalize(); }
        mov = Vector3.Lerp(mov, movPrev, 0.01f * Time.deltaTime);
        movPrev = mov;
        ojos.ActualizarPosicion(mov);
        mov *= (velocidad * tamanyoProporcion);
        body.velocity = mov;
        Parpadear();
    }

    IEnumerator EjecutarRayCast()
    {
        while (!eliminado)
        {
            yield return 1;
            RayCasting();  
        }
    }

    private void Parpadear()
    {
        if (Random.Range(0.0f, 1.0f) < probabilidadParpadeo)
        {
            probabilidadParpadeo = 0.0f;
            animator.SetTrigger("Parpadeo");
        }
        else
        {
            probabilidadParpadeo += Time.deltaTime * incProbParpadeo;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ColliderInterior")
        {
            AgenteControlador otro = collision.transform.parent.GetComponent<AgenteControlador>();
            if (!otro.eliminado && otro.Volumen * margenVolumenPorcentual <= Volumen)
            {
                AumentarTamanyo(otro.Volumen);
                otro.GetComponent<AgenteControlador>().Eliminar(id);
            }
        }
    }

    private void Eliminar(int idEliminador)
    {
        eliminado = true;
        tamanyo.Eliminado = true;
        colision.enabled = false;
        controladorSonidos.ReproducirPop();
        controladorAgentes.EliminarAgente(id, idEliminador);
    }

    public void AumentarTamanyo(float volumen)
    {
        tamanyo.CambiarTamanyo(volumen);
        tamanyoProporcion = (1 / (Mathf.Log(tamanyo.TamanyoActual * decVelocidadPorTamanyo + 1) + 1));
    }

    public void RandomizarColor()
    {
        float r = 0, g = 0, b = 0;
        switch (Random.Range(0, 3))
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
        transform.Find("Color").GetComponent<SpriteRenderer>().color = new Color(r, g, b);
    }

    void RayCasting()
    {
        RaycastHit2D[] hit = new RaycastHit2D[numRayCasts];
        for (int i = 0; i < numRayCasts; i++)
        {
            Vector2 dir = new Vector2(dirX[i], dirY[i]);
            hit[i] = Physics2D.Raycast(transform.position, dir, distanciaVision * transform.localScale.x / 2.0f);
            if (hit[i])
            {
                //Debug.DrawRay(transform.position, dir * hit[i].distance, Color.yellow);
                GameObject hitObject = hit[i].collider.gameObject;
                if (hitObject.GetComponent<AgenteControlador>() != null)
                {
                    objetos[i].tipo = TipoObjeto.Agente;
                    objetos[i].posicion = hitObject.transform.position;
                    objetos[i].tamanyo = hitObject.GetComponent<AgenteControlador>().Volumen;
                    objetos[i].distancia = hit[i].distance;
                }
                else if (hitObject.GetComponent<AlimentoControlador>() != null)
                {
                    objetos[i].tipo = TipoObjeto.Alimento;
                    objetos[i].posicion = hitObject.transform.position;
                    objetos[i].tamanyo = hitObject.GetComponent<AlimentoControlador>().volumen;
                    objetos[i].distancia = hit[i].distance;
                }
                else
                {
                    objetos[i].tipo = TipoObjeto.Borde;
                    objetos[i].distancia = hit[i].distance;
                }
            }
            else
            {
                objetos[i].tipo = TipoObjeto.Nada;
            }
        }
    }
}
