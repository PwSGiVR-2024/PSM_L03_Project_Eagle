using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public float scrollSpeed = 50f; // pr�dko�� przewijania
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Przewijanie tekstu w g�r�
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        // Je�li gracz naci�nie Escape, wracamy do menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(2);
        }
    }
}
