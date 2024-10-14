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
        // Guardar el tama�o y color originales de los botones
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

    // Cuando el mouse pasa sobre un bot�n
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

    // Cuando el mouse sale de un bot�n
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

    // M�todo para animar el bot�n cuando el mouse pasa sobre �l
    private void AnimateButton(Button button)
    {
        // Cambiar el tama�o del bot�n
        button.transform.localScale = originalScale * 1.8f;

        // Cambiar el color del bot�n
        button.image.color = Color.green;
    }

    // M�todo para resetear el bot�n cuando el mouse sale
    private void ResetButton(Button button)
    {
        // Volver al tama�o y color originales
        button.transform.localScale = originalScale;
        button.image.color = originalColor;
    }
}
