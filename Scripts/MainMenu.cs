using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;  // Importa SceneManagement para cargar la escena

public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button startButton;
    public Button helpButton;
    public Button exitButton;

    private Vector3 originalScale;
    private Color originalColor;

    private void Start()
    {
        // Guardar el tamaño y color originales de los botones
        originalScale = startButton.transform.localScale;
        originalColor = startButton.image.color;
    }

    public void StartGame()
    {
        Debug.Log("Cargando IntroduccionEscena...");
        // Cargar la escena "IntroduccionEscena"
        SceneManager.LoadScene("IntroductionScene");
    }

    public void Help()
    {
        Debug.Log("Mostrar ayuda");
    }

    public void ExitGame()
    {
        Debug.Log("Salir del juego");
    }

    // Cuando el mouse pasa sobre un botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == startButton.gameObject)
        {
            AnimateButton(startButton);
        }
        else if (eventData.pointerEnter == helpButton.gameObject)
        {
            AnimateButton(helpButton);
        }
        else if (eventData.pointerEnter == exitButton.gameObject)
        {
            AnimateButton(exitButton);
        }
    }

    // Cuando el mouse sale de un botón
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter == startButton.gameObject)
        {
            ResetButton(startButton);
        }
        else if (eventData.pointerEnter == helpButton.gameObject)
        {
            ResetButton(helpButton);
        }
        else if (eventData.pointerEnter == exitButton.gameObject)
        {
            ResetButton(exitButton);
        }
    }

    // Método para animar el botón cuando el mouse pasa sobre él
    private void AnimateButton(Button button)
    {
        // Cambiar el tamaño del botón
        button.transform.localScale = originalScale * 1.8f;

        // Cambiar el color del botón
        button.image.color = Color.green;
    }

    // Método para resetear el botón cuando el mouse sale
    private void ResetButton(Button button)
    {
        // Volver al tamaño y color originales
        button.transform.localScale = originalScale;
        button.image.color = originalColor;
    }
}
