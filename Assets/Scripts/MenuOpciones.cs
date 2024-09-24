using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource audioSource;  // Fuente de audio para el sonido del clic
    [SerializeField] private GameObject optionsPanel;  // Panel de opciones
    [SerializeField] private GameObject mainMenuPanel; // Panel del menú principal

    // Método que cambia el volumen del juego
    public void CambiarVolumen(float volumen)
    {
        audioMixer.SetFloat("Volumen", volumen);
    }

    // Método que se llama cuando se hace clic en el botón
    public void OnButtonClick()
    {
        audioSource.Play(); // Reproduce el sonido al hacer clic
        StartCoroutine(WaitForSoundToEnd()); // Espera a que el sonido termine antes de cambiar de pantalla
    }

    // Corutina que espera la duración del sonido antes de cambiar a la pantalla de opciones
    IEnumerator WaitForSoundToEnd()
    {
        yield return new WaitForSeconds(audioSource.clip.length); // Espera la duración del clip de audio

        // Cambia de pantalla: desactiva el menú principal y activa las opciones
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
}
