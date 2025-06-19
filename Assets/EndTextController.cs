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
    public string mainMenuSceneName = "MainMenu";
    public AudioSource voiceoverSource;

    public AudioSource ambientSource;

    void Start()
    {
        if (ambientSource != null)
            ambientSource.Play(); // Ambient leci od razu

        if (voiceoverSource != null)
            StartCoroutine(DelayedVoiceStart());

        StartCoroutine(TypeText()); // tekst te� ma 2 sekundy op�nienia
    }
    IEnumerator DelayedVoiceStart()
    {
        yield return new WaitForSeconds(startDelay);  // czyli 2 sekundy
        voiceoverSource.Play();
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

        // Poczekaj jeszcze chwil�
        yield return new WaitForSeconds(waitAfterFade);

        // Przejd� do Main Menu
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