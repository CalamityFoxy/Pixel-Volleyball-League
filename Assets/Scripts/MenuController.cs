using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Fuente de audio para el sonido del clic

    // Método que se llama cuando se hace clic en el botón de salida
    public void OnExitButtonClick()
    {
        audioSource.Play(); // Reproduce el sonido al hacer clic
        StartCoroutine(ExitGameAfterSound()); // Espera a que el sonido termine antes de salir
    }

    // Corutina que espera la duración del sonido antes de salir del juego
    private IEnumerator ExitGameAfterSound()
    {
        yield return new WaitForSeconds(audioSource.clip.length); // Espera la duración del clip de audio
        Application.Quit(); // Cierra el juego

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Esto solo es necesario en el editor
#endif
    }
}

