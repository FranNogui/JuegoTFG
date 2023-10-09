using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoBotones : MonoBehaviour
{
    AudioSource reproductor;

    private void Start()
    {
        reproductor = GetComponent<AudioSource>();
    }

    public void ReproducirBeep()
    {
        reproductor.Play();
    }
}
