using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource audioSource;  // Fuente de audio para el sonido del clic
    [SerializeField] private GameObject optionsPanel;  // Panel de opciones
    [SerializeField] private GameObject mainMenuPanel; // Panel del men� principal

    // M�todo que cambia el volumen del juego
    public void CambiarVolumen(float volumen)
    {
        audioMixer.SetFloat("Volumen", volumen);
    }

    // M�todo que se llama cuando se hace clic en el bot�n
    public void OnButtonClick()
    {
        audioSource.Play(); // Reproduce el sonido al hacer clic
        StartCoroutine(WaitForSoundToEnd()); // Espera a que el sonido termine antes de cambiar de pantalla
    }

    // Corutina que espera la duraci�n del sonido antes de cambiar a la pantalla de opciones
    IEnumerator WaitForSoundToEnd()
    {
        yield return new WaitForSeconds(audioSource.clip.length); // Espera la duraci�n del clip de audio

        // Cambia de pantalla: desactiva el men� principal y activa las opciones
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
}
