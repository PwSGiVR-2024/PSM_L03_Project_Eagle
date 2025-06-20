using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public float scrollSpeed = 50f; // prêdkoœæ przewijania
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Przewijanie tekstu w górê
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        // Jeœli gracz naciœnie Escape, wracamy do menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(2);
        }
    }
}
