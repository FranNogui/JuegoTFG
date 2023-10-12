using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoAgente
{
    Jugador, IAProgramada
}

public class ModuloMovimiento : MonoBehaviour
{
    [SerializeField] TipoAgente tipo;
    AgenteControlador agente;

    private void Start()
    {
        agente = GetComponent<AgenteControlador>();
    }

    public void CambiarA(TipoAgente nuevoTipo)
    {
        tipo = nuevoTipo;
    }

    public Vector2 ActualizarMovimiento(RayCastObject[] objetos)
    {
        if (tipo == TipoAgente.Jugador)
        {
            return Vector2.up * Input.GetAxis("Vertical") + Vector2.right * Input.GetAxis("Horizontal");
        }
        else if (tipo == TipoAgente.IAProgramada)
        {
            return IAProgramada(objetos);
        }
        else
        { 
            return Vector2.zero;
        }
    }

    bool esquivandoPared = false;
    Vector2 vectorRandom = Vector2.zero;
    float tiempoRandomActual = 2.0f, maximoTiempo = 2.0f;
    float valorDist = 2.0f;
    float valorTam =  0.5f;
    float dirPared = 1.0f;
    Vector2 IAProgramada(RayCastObject[] objetos)
    {
        RayCastObject mejorAlimento = null;
        RayCastObject peorEnemigo = null;
        foreach (var obj in objetos)
        {
            if (obj.tipo == TipoObjeto.Nada || obj.tipo == TipoObjeto.Borde) continue;
            if (obj.tipo == TipoObjeto.Agente && agente.Volumen * agente.MargenVolumenPorcentual < obj.tamanyo)
            {
                if (peorEnemigo == null || obj.distancia < peorEnemigo.distancia) peorEnemigo = obj;
            }
            if (mejorAlimento == null ||
                mejorAlimento.distancia * valorDist - mejorAlimento.tamanyo * valorTam > obj.distancia * valorDist - obj.tamanyo * valorTam)
            {
                if (obj.tipo == TipoObjeto.Agente && obj.tamanyo * agente.MargenVolumenPorcentual >= agente.Volumen) continue;
                mejorAlimento = obj;
            }
        }

        if (mejorAlimento == null && peorEnemigo == null) 
        {
            if (tiempoRandomActual >= maximoTiempo)
            {
                tiempoRandomActual = 0.0f;
                dirPared *= -1.0f;
                if (!esquivandoPared)
                {
                    vectorRandom = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                }
            }
            else tiempoRandomActual += Time.deltaTime;
            
            esquivandoPared = false;
            for (int i = 0; i < objetos.Length; i++)
            {
                if (objetos[i].tipo == TipoObjeto.Borde && objetos[i].distancia < 5.0f)
                {
                    float dirX1 = Mathf.Cos(i * (2 * Mathf.PI / objetos.Length));
                    float dirY1 = Mathf.Sin(i * (2 * Mathf.PI / objetos.Length));
                    Vector2 v1 = new Vector2(dirX1, dirY1);
                    v1.Normalize();

                    if (Vector2.Angle(v1, vectorRandom) <= (360.0f / objetos.Length))
                    {
                        vectorRandom.Normalize();
                        vectorRandom += new Vector2(dirPared * vectorRandom.y, - dirPared * vectorRandom.x);
                        vectorRandom.Normalize();
                        esquivandoPared = true;
                        break;
                    }      
                }
            }

            Debug.DrawRay(transform.position, vectorRandom.normalized, Color.blue);
            return vectorRandom.normalized;
        }
        else if (peorEnemigo != null)
        {
            if (mejorAlimento != null && mejorAlimento.distancia * 5.0f < peorEnemigo.distancia)
            {
                Vector3 recogida = ((mejorAlimento.posicion - transform.position)).normalized;
                esquivandoPared = false;
                Debug.DrawRay(transform.position, recogida, Color.green);
                vectorRandom = recogida;
                return recogida;
            }

            Vector3 huida;

            if (!esquivandoPared)
            {
                huida = ((peorEnemigo.posicion - transform.position) * -1.0f).normalized;
            }
            else huida = vectorRandom;

            vectorRandom = huida;

            esquivandoPared = false;
            for (int i = 0; i < objetos.Length; i++)
            {
                if (objetos[i].tipo == TipoObjeto.Borde && objetos[i].distancia < 10.0f)
                {
                    float dirX1 = Mathf.Cos(i * (2 * Mathf.PI / objetos.Length));
                    float dirY1 = Mathf.Sin(i * (2 * Mathf.PI / objetos.Length));
                    Vector2 v1 = new Vector2(dirX1, dirY1);
                    v1.Normalize();

                    if (Vector2.Angle(v1, huida) <= (360.0f / objetos.Length))
                    {
                        huida.Normalize();
                        huida += new Vector3(dirPared * huida.y, - dirPared * huida.x);
                        huida.Normalize();
                        vectorRandom = huida;
                        esquivandoPared = true;
                        if (Vector2.Angle(huida, ((peorEnemigo.posicion - transform.position) * -1.0f).normalized) >= 100.0f) dirPared *= -1.0f;
                        break;
                    }            
                }
            }

            Debug.DrawRay(transform.position, huida, Color.red);
            return huida;
        }
        else
        {
            Vector3 recogida = ((mejorAlimento.posicion - transform.position)).normalized;
            esquivandoPared = false;
            Debug.DrawRay(transform.position, recogida, Color.green);
            vectorRandom = recogida;
            return recogida;
        }
    }
}

