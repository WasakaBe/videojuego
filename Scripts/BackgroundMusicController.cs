using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioClip backgroundMusic; // El clip de m�sica de fondo
    private AudioSource audioSource;

    void Start()
    {
        // A�ade un AudioSource al GameObject si no lo tiene ya
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;  // Hace que la m�sica se repita en bucle
        audioSource.playOnAwake = true; // Empieza la m�sica cuando la escena carga
        audioSource.volume = 0.5f; // Ajusta el volumen
        audioSource.Play(); // Empieza a reproducir la m�sica
    }
}
