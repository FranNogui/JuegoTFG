using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlimentoSpawner : MonoBehaviour
{
    GameObject[] alimentos;
    [SerializeField] GameObject alimento;
    [SerializeField] int maxAlimentos;
    [SerializeField] float minDelay;
    [SerializeField] float maxDelay;

    int numAlimentos;
    Vector2 minPos = new Vector2 (-25.0f, -25.0f);
    Vector2 maxPos = new Vector2 (25.0f, 25.0f);

    private void Start()
    {
        alimentos = new GameObject[maxAlimentos];
        for (int i = 0; i < maxAlimentos; i++)
        {
            alimentos[i] = GameObject.Instantiate(alimento);
            alimentos[i].GetComponent<AlimentoControlador>().AlimentoSpawner = this;
            alimentos[i].GetComponent<AlimentoControlador>().ID = i;
            alimentos[i].SetActive(false);
            alimentos[i].transform.position = Vector3.zero;
        }

        numAlimentos = 0;
        StartCoroutine(SpawnearAlimento());
    }

    IEnumerator SpawnearAlimento()
    {
        while (true)
        {
            for (int i = 0; i < maxAlimentos; i++)
            {
                if (!alimentos[i].activeSelf)
                {
                    alimentos[i].SetActive(true);
                    alimentos[i].GetComponent<AlimentoControlador>().Randomizar();
                    alimentos[i].transform.position = new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y), 0.0f);
                    numAlimentos++;
                    break;
                }
            }

            float delay = Mathf.Clamp((float) numAlimentos / maxAlimentos, 0.0f, 1.0f) * (maxDelay - minDelay) + minDelay;
            Debug.Log(delay);
            yield return new WaitForSeconds(delay);
        }
    }

    public void DestruirAlimento(int id)
    {
        alimentos[id].SetActive(false);
        numAlimentos--;
    }
}
