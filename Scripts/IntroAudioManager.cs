using System.Collections;
using UnityEngine;

public class IntroAudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // El clip de m�sica de fondo
    private AudioSource audioSource;

    [Range(0f, 1f)]
    public float volume = 0.5f; // Ajusta el volumen desde el Inspector

    public bool useFadeIn = true;  // Opci�n para activar o desactivar fade-in
    public bool useFadeOut = false;  // Opci�n para activar o desactivar fade-out
    public float fadeDuration = 2f;  // Duraci�n del fade-in/fade-out

    void Start()
    {
        // A�ade un AudioSource al GameObject si no lo tiene ya
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;  // Hace que la m�sica se repita en bucle
        audioSource.playOnAwake = false; // Desactivar playOnAwake si queremos controlarlo manualmente

        audioSource.volume = useFadeIn ? 0 : volume; // Si hay fade-in, empieza en 0

        if (useFadeIn)
        {
            StartCoroutine(FadeInAudio(fadeDuration)); // Iniciar fade-in
        }
        else
        {
            audioSource.Play(); // Empieza la m�sica si no hay fade-in
        }
    }

    // M�todo para pausar la m�sica
    public void PauseMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();  // Pausa la m�sica si est� sonando
        }
    }

    // M�todo para detener la m�sica con un fade-out opcional
    public void StopMusic()
    {
        if (useFadeOut)
        {
            StartCoroutine(FadeOutAudio(fadeDuration));  // Iniciar fade-out
        }
        else
        {
            audioSource.Stop();  // Detener inmediatamente si no hay fade-out
        }
    }

    // Coroutine para hacer el fade-in del audio
    private IEnumerator FadeInAudio(float duration)
    {
        float currentTime = 0;

        audioSource.Play(); // Iniciar la reproducci�n de la m�sica

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, volume, currentTime / duration); // Aumenta el volumen gradualmente
            yield return null;
        }
        audioSource.volume = volume; // Asegurarse de que el volumen final sea el deseado
    }

    // Coroutine para hacer el fade-out del audio
    private IEnumerator FadeOutAudio(float duration)
    {
        float currentTime = 0;
        float startVolume = audioSource.volume; // Guardar el volumen actual

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0, currentTime / duration); // Disminuir el volumen gradualmente
            yield return null;
        }

        audioSource.Stop(); // Detener la m�sica cuando el fade-out finalice
        audioSource.volume = volume; // Restablecer el volumen para futuras reproducciones
    }
}
