using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform; // Kamera, która œledzi postaæ
    public float moveSpeed = 5f; // Szybkoœæ poruszania
    public float lookSpeedX = 2f; // Szybkoœæ obrotu g³owy w osi X (lewo/prawo)
    public float lookSpeedY = 2f; // Szybkoœæ obrotu g³owy w osi Y (góra/dó³)
    public float upperLookLimit = 80f; // Maksymalny k¹t patrzenia w górê
    public float lowerLookLimit = -80f; // Maksymalny k¹t patrzenia w dó³

    private InputAction moveInput; // Wejœcie z klawiatury/joypada
    private InputAction lookInput;
    private Vector2 moveVector;
    private Vector2 lookVector;
    private float rotationX = 0f; // K¹t obrotu g³owy w osi X
    private float rotationY = 0f; // K¹t obrotu g³owy w osi Y

    private void Awake()
    {
        moveInput = InputSystem.actions.FindAction("Move");
        lookInput = InputSystem.actions.FindAction("Look");
    }

    private void Update()
    {
        // Odczytanie wejœcia do ruchu
        moveVector = moveInput.ReadValue<Vector2>();

        // Odczytanie wejœcia do patrzenia
        lookVector = lookInput.ReadValue<Vector2>();

        // Obracanie kamery w osi Y (lewo/prawo) - obrót kamery
        rotationY += lookVector.x * lookSpeedX; // Obrót w poziomie (lewo/prawo)
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f); // Obracamy postaæ w poziomie

        // Obracanie g³owy w osi X (góra/dó³) - obrót kamery
        rotationX -= lookVector.y * lookSpeedY; // Obrót w pionie (góra/dó³)
        rotationX = Mathf.Clamp(rotationX, lowerLookLimit, upperLookLimit); // Ograniczamy obrót w osi X

        // Ustawienie rotacji kamery w osi X (góra/dó³)
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Ruch postaci w kierunku patrzenia kamery
        MoveCharacter();
    }
    private void MoveCharacter()
    {
        // Kierunek w którym patrzy kamera (wy³¹cznie w poziomie)
        Vector3 forward = cameraTransform.forward;
        forward.y = 0; // Usuwamy komponent Y, by nie ruszaæ postaci¹ w pionie

        Vector3 right = cameraTransform.right;
        right.y = 0; // Usuwamy komponent Y z wektora right

        // Ruch w kierunku kamery - wektory ruchu: do przodu i na boki
        Vector3 direction = forward * moveVector.y + right * moveVector.x;

        // Przemieszczanie postaci
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
}
