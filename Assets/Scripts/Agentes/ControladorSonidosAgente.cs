using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSonidosAgente : MonoBehaviour
{
    [SerializeField] AudioClip[] pops;
    AudioSource reproductor;

    private void Start()
    {
        reproductor = GetComponent<AudioSource>();
    }

    public void ReproducirPop()
    {
        reproductor.clip = pops[Random.Range(0, pops.Length)];
        reproductor.Play();
    }
}
