using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgenteControlador : MonoBehaviour
{
    const int numRayCasts = 256;

    [SerializeField] float velocidad = 1.0f;
    [SerializeField] float decVelocidadPorTamanyo = 0.001f;
    [SerializeField] float distanciaVision = 5.0f;
    float probabilidadParpadeo = 0.0f;
    float incProbParpadeo = 0.001f;
    float tamanyoProporcion = 1.0f;
    Vector2 mov;
    Vector2 movPrev;

    OjosControlador ojos;
    TamanyoControlador tamanyo;
    Rigidbody2D body;
    ModuloMovimiento modMov;
    Animator animator;

    float[] dirX = new float[numRayCasts];
    float[] dirY = new float[numRayCasts];

    public float Volumen
    {
        get { return tamanyo.TamanyoActual + 1.0f; }
    }

    void Start()
    {
        ojos = GetComponent<OjosControlador>();
        tamanyo = GetComponent<TamanyoControlador>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        modMov = GetComponent<ModuloMovimiento>();
        mov = Vector2.zero;
        movPrev = mov;

        for (int i = 0; i < numRayCasts; i++)
        {
            dirX[i] = Mathf.Cos(i * (2 * Mathf.PI / numRayCasts));
            dirY[i] = Mathf.Sin(i * (2 * Mathf.PI / numRayCasts));
        }
    }

    void Update()
    {
        movPrev = mov;
        mov = modMov.ActualizarMovimiento();
        if (mov.magnitude > 1.0f) { mov.Normalize(); }
        mov = Vector2.Lerp(mov, movPrev, 1.0f * Time.deltaTime);
        ojos.ActualizarPosicion(mov);
        mov *= (velocidad * tamanyoProporcion);
        body.velocity = mov;
        RayCasting();
        Parpadear();
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
            Debug.Log("Atacado");
        }
    }

    public void AumentarTamanyo(float volumen)
    {
        tamanyo.CambiarTamanyo(volumen / transform.localScale.x);
        tamanyoProporcion = (1 / (Mathf.Log(tamanyo.TamanyoActual * decVelocidadPorTamanyo + 1) + 1));
    }

    void RayCasting()
    {
        RaycastHit2D[] hit = new RaycastHit2D[numRayCasts];
        for (int i = 0; i < numRayCasts; i++)
        {
            Vector2 dir = new Vector2(dirX[i], dirY[i]);
            hit[i] = Physics2D.Raycast(transform.position, dir, distanciaVision * transform.localScale.x);
            if (hit[i])
            {
                Debug.DrawRay(transform.position, dir * hit[i].distance, Color.yellow);
                Debug.Log("Hit");
            }
        }
    }
}
