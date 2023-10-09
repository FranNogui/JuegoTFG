using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSonidos : MonoBehaviour
{
    AudioSource reproductor;

    private void Start()
    {
        reproductor = GetComponent<AudioSource>();
    }

    public void ReproducirWhoosh()
    {
        reproductor.Play();
    }
}
