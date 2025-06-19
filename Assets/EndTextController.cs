using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndTextController : MonoBehaviour
{
    public TextMeshProUGUI textField;
    [TextArea(5, 20)] public string fullText;
    public float delay = 0.05f;
    public float startDelay = 2f;
    public float fadeDuration = 3f;
    public float waitAfterFade = 2f;
    public string mainMenuSceneName = "MainMenu"; // <- nazwij swoj¹ scenê menu

    public AudioSource ambientSource;

    void Start()
    {
        if (ambientSource != null)
            ambientSource.Play();

        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        textField.text = "";

        yield return new WaitForSeconds(startDelay);

        foreach (char c in fullText)
        {
            textField.text += c;
            yield return new WaitForSeconds(delay);
        }

        // Fade out ambient
        yield return StartCoroutine(FadeOutAudio(ambientSource, fadeDuration));

        // Poczekaj jeszcze chwilê
        yield return new WaitForSeconds(waitAfterFade);

        // PrzejdŸ do Main Menu
        SceneManager.LoadScene(mainMenuSceneName);
    }

    IEnumerator FadeOutAudio(AudioSource audio, float duration)
    {
        float startVolume = audio.volume;

        while (audio.volume > 0f)
        {
            audio.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audio.Stop();
        audio.volume = startVolume;
    }
}