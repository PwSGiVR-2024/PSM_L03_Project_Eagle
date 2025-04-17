using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform; // Kamera, kt�ra �ledzi posta�
    public float moveSpeed = 5f; // Szybko�� poruszania
    public float lookSpeedX = 2f; // Szybko�� obrotu g�owy w osi X (lewo/prawo)
    public float lookSpeedY = 2f; // Szybko�� obrotu g�owy w osi Y (g�ra/d�)
    public float upperLookLimit = 80f; // Maksymalny k�t patrzenia w g�r�
    public float lowerLookLimit = -80f; // Maksymalny k�t patrzenia w d�

    private InputAction moveInput; // Wej�cie z klawiatury/joypada
    private InputAction lookInput;
    private Vector2 moveVector;
    private Vector2 lookVector;
    private float rotationX = 0f; // K�t obrotu g�owy w osi X
    private float rotationY = 0f; // K�t obrotu g�owy w osi Y

    private void Awake()
    {
        moveInput = InputSystem.actions.FindAction("Move");
        lookInput = InputSystem.actions.FindAction("Look");
    }

    private void Update()
    {
        // Odczytanie wej�cia do ruchu
        moveVector = moveInput.ReadValue<Vector2>();

        // Odczytanie wej�cia do patrzenia
        lookVector = lookInput.ReadValue<Vector2>();

        // Obracanie kamery w osi Y (lewo/prawo) - obr�t kamery
        rotationY += lookVector.x * lookSpeedX; // Obr�t w poziomie (lewo/prawo)
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f); // Obracamy posta� w poziomie

        // Obracanie g�owy w osi X (g�ra/d�) - obr�t kamery
        rotationX -= lookVector.y * lookSpeedY; // Obr�t w pionie (g�ra/d�)
        rotationX = Mathf.Clamp(rotationX, lowerLookLimit, upperLookLimit); // Ograniczamy obr�t w osi X

        // Ustawienie rotacji kamery w osi X (g�ra/d�)
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Ruch postaci w kierunku patrzenia kamery
        MoveCharacter();
    }
    private void MoveCharacter()
    {
        // Kierunek w kt�rym patrzy kamera (wy��cznie w poziomie)
        Vector3 forward = cameraTransform.forward;
        forward.y = 0; // Usuwamy komponent Y, by nie rusza� postaci� w pionie

        Vector3 right = cameraTransform.right;
        right.y = 0; // Usuwamy komponent Y z wektora right

        // Ruch w kierunku kamery - wektory ruchu: do przodu i na boki
        Vector3 direction = forward * moveVector.y + right * moveVector.x;

        // Przemieszczanie postaci
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
}
