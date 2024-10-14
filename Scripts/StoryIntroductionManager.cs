using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Importar librer�a para manejo de escenas

public class StoryIntroductionManager : MonoBehaviour
{
    public Image[] introImages; // Im�genes para la introducci�n
    public TextMeshProUGUI narrativeText; // Texto de la narrativa

    // Texto narrativo que aparecer� junto a las im�genes
    private string[] narrativeLines = {
        "Capitulo 1: El Fin de la Paz",
        "En el a�o 3069, el Imperio Mexa se vio envuelto en la devastadora Cuarta Guerra Mundial...",
        "Ethan, un joven y valiente soldado del Imperio Mexa, fue enviado a las l�neas del frente...",
        "Durante su ultima mision, Ethan fue herido gravemente mientras trataba de salvar a sus compa�eros...",
        "Con su cuerpo debilitado, cerr� los ojos, listo para aceptar su destino..."
    };

    public float displayTime = 10f; // Tiempo que cada imagen y texto ser�n mostrados
    private int currentImageIndex = 0; // �ndice de la imagen actual
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
                StartCoroutine(FadeImage(introImages[currentImageIndex], true)); // Transici�n de aparici�n
                ShowImage(currentImageIndex);
            }
            else
            {
                EndIntroduction();  // Cambiar a la siguiente escena cuando la introducci�n termine
            }

            timer = 0f;
        }
    }

    // M�todo para mostrar la imagen y el texto correspondientes
    void ShowImage(int index)
    {
        foreach (Image img in introImages)
        {
            img.gameObject.SetActive(false); // Ocultar todas las im�genes
        }

        introImages[index].gameObject.SetActive(true); // Mostrar la imagen actual
        narrativeText.text = narrativeLines[index]; // Actualizar el texto narrativo
    }

    // M�todo que se llama al finalizar la introducci�n
    void EndIntroduction()
    {
        Debug.Log("Introducci�n terminada");
        // Cambiar a la nueva escena
        SceneManager.LoadScene("Capitulo1_escena1"); // Cambiar a la escena deseada
    }

    // M�todo para hacer una transici�n de aparici�n/desaparici�n
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
