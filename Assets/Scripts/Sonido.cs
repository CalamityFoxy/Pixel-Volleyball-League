using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Necesario para cambiar escenas
using System.Collections; // Necesario para usar IEnumerator

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource; // Arrastra aqu� el AudioSource en el Inspector
    public Button button; // Arrastra el bot�n en el Inspector
    public int sceneIndex; // N�mero de la escena a cargar

    void Start()
    {
        // Agrega un listener para que detecte el click en el bot�n
        button.onClick.AddListener(OnButtonClick);

    }

    void OnButtonClick()
    {
        audioSource.Play(); // Reproduce el sonido
        StartCoroutine(ChangeSceneAfterSound()); // Espera a que el sonido termine antes de cambiar de escena
    }

    IEnumerator ChangeSceneAfterSound()
    {
        // Espera la duraci�n del sonido antes de cambiar de escena
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene(sceneIndex); // Cambia de escena por su �ndice
    }
}

