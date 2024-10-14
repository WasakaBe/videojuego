using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Importar librería para manejo de escenas

public class StoryIntroductionManager : MonoBehaviour
{
    public Image[] introImages; // Imágenes para la introducción
    public TextMeshProUGUI narrativeText; // Texto de la narrativa

    // Texto narrativo que aparecerá junto a las imágenes
    private string[] narrativeLines = {
        "Capitulo 1: El Fin de la Paz",
        "En el año 3069, el Imperio Mexa se vio envuelto en la devastadora Cuarta Guerra Mundial...",
        "Ethan, un joven y valiente soldado del Imperio Mexa, fue enviado a las líneas del frente...",
        "Durante su ultima mision, Ethan fue herido gravemente mientras trataba de salvar a sus compañeros...",
        "Con su cuerpo debilitado, cerró los ojos, listo para aceptar su destino..."
    };

    public float displayTime = 10f; // Tiempo que cada imagen y texto serán mostrados
    private int currentImageIndex = 0; // Índice de la imagen actual
    private float timer = 0f; // Temporizador

    // Fondo para el texto
    private GameObject textBackground;
    public Canvas canvas;

    void Start()
    {
        // Mostrar la primera imagen y texto
        ShowImage(currentImageIndex);
    }


    void Update()
    {
        timer += Time.deltaTime;

        // Verificar si es tiempo de cambiar a la siguiente imagen
        if (timer >= displayTime)
        {
            currentImageIndex++;

            if (currentImageIndex < introImages.Length)
            {
                StartCoroutine(FadeImage(introImages[currentImageIndex], true)); // Transición de aparición
                ShowImage(currentImageIndex);
            }
            else
            {
                EndIntroduction();  // Cambiar a la siguiente escena cuando la introducción termine
            }

            timer = 0f;
        }
    }

    // Método para mostrar la imagen y el texto correspondientes
    void ShowImage(int index)
    {
        foreach (Image img in introImages)
        {
            img.gameObject.SetActive(false); // Ocultar todas las imágenes
        }

        introImages[index].gameObject.SetActive(true); // Mostrar la imagen actual
        narrativeText.text = narrativeLines[index]; // Actualizar el texto narrativo
    }

    // Método que se llama al finalizar la introducción
    void EndIntroduction()
    {
        Debug.Log("Introducción terminada");
        // Cambiar a la nueva escena
        SceneManager.LoadScene("Capitulo1_escena1"); // Cambiar a la escena deseada
    }

    // Método para hacer una transición de aparición/desaparición
    private IEnumerator FadeImage(Image image, bool fadeIn)
    {
        float fadeSpeed = 1f;
        Color tempColor = image.color;

        if (fadeIn)
        {
            for (float i = 0; i <= 1; i += Time.deltaTime * fadeSpeed)
            {
                tempColor.a = i; // Incrementar la opacidad
                image.color = tempColor;
                yield return null;
            }
        }
        else
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime * fadeSpeed)
            {
                tempColor.a = i; // Disminuir la opacidad
                image.color = tempColor;
                yield return null;
            }
        }
    }
}
