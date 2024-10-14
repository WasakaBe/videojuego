using System.Collections;
using UnityEngine;

public class IntroAudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // El clip de música de fondo
    private AudioSource audioSource;

    [Range(0f, 1f)]
    public float volume = 0.5f; // Ajusta el volumen desde el Inspector

    public bool useFadeIn = true;  // Opción para activar o desactivar fade-in
    public bool useFadeOut = false;  // Opción para activar o desactivar fade-out
    public float fadeDuration = 2f;  // Duración del fade-in/fade-out

    void Start()
    {
        // Añade un AudioSource al GameObject si no lo tiene ya
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;  // Hace que la música se repita en bucle
        audioSource.playOnAwake = false; // Desactivar playOnAwake si queremos controlarlo manualmente

        audioSource.volume = useFadeIn ? 0 : volume; // Si hay fade-in, empieza en 0

        if (useFadeIn)
        {
            StartCoroutine(FadeInAudio(fadeDuration)); // Iniciar fade-in
        }
        else
        {
            audioSource.Play(); // Empieza la música si no hay fade-in
        }
    }

    // Método para pausar la música
    public void PauseMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();  // Pausa la música si está sonando
        }
    }

    // Método para detener la música con un fade-out opcional
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

        audioSource.Play(); // Iniciar la reproducción de la música

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

        audioSource.Stop(); // Detener la música cuando el fade-out finalice
        audioSource.volume = volume; // Restablecer el volumen para futuras reproducciones
    }
}
