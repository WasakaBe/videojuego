using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioClip backgroundMusic; // El clip de música de fondo
    private AudioSource audioSource;

    void Start()
    {
        // Añade un AudioSource al GameObject si no lo tiene ya
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;  // Hace que la música se repita en bucle
        audioSource.playOnAwake = true; // Empieza la música cuando la escena carga
        audioSource.volume = 0.5f; // Ajusta el volumen
        audioSource.Play(); // Empieza a reproducir la música
    }
}
