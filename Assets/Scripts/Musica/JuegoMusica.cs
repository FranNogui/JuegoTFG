using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoMusica : MonoBehaviour
{
    [SerializeField] AudioClip[] musicas;
    AudioSource reproductor;

    private void Start()
    {
        reproductor = GetComponent<AudioSource>();
        reproductor.clip = musicas[Random.Range(0, musicas.Length)];
        reproductor.Play();
    }

    private void Update()
    {
        if (!reproductor.isPlaying)
        {
            reproductor.clip = musicas[Random.Range(0, musicas.Length)];
            reproductor.Play();
        }
    }
}
