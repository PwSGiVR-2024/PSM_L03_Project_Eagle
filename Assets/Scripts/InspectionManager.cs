using UnityEngine;
using static GameStateManager;

public class InspectionManager : MonoBehaviour
{
    public static InspectionManager Instance { get; private set; }

    [Header("Referencje scenowe")]
    [SerializeField] private Transform inspectionContainer; // InspectionArea
    [SerializeField] private GameObject blurPanel; // UI/Canvas z shaderem blur
    [SerializeField] private GameObject inspectionUI; // UI inspekcji (tekst: "Wyjd�: R", tipy, itd.)
    [SerializeField] private Camera inspectionCamera;
    [SerializeField] private InspectionCameraController inspectionCameraController;

    private GameObject inspectedObjectCopy;
    private bool isInspecting = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (isInspecting && Input.GetKeyDown(KeyCode.R) && GameStateManager.Instance.IsInspect)
        {
            EndInspection();
        }
    }

    public void StartInspection(GameObject objectToInspect)
    {
        if (isInspecting) return;
        GameStateManager.Instance.SetState(GameState.Inspect);
        Time.timeScale = 0f;
        isInspecting = true;

        // Pokazujemy blur oraz UI inspekcji
        blurPanel.SetActive(true);
        inspectionUI.SetActive(true);

        // Skopiuj obiekt do inspekcji na �rodek
        inspectedObjectCopy = Instantiate(objectToInspect, inspectionContainer);
        inspectedObjectCopy.transform.localPosition = Vector3.zero;
        inspectedObjectCopy.transform.localRotation = Quaternion.identity;

        CenterModel(inspectedObjectCopy);

        // Resetujemy kamer� inspekcji na start
        if (inspectionCameraController != null)
            inspectionCameraController.ResetCamera();

        // Ustawiamy kamer� inspekcyjn� aktywn� (je�li u�ywasz RenderTexture na RawImage)
        if (inspectionCamera != null)
            inspectionCamera.gameObject.SetActive(true);

    }

    public void EndInspection()
    {
        GameStateManager.Instance.SetState(GameState.Normal);
        Time.timeScale = 1f;
        isInspecting = false;

        blurPanel.SetActive(false);
        inspectionUI.SetActive(false);

        // Niszczymy obiekt inspekcyjny
        if (inspectedObjectCopy != null)
            Destroy(inspectedObjectCopy);

        // Ukryj kamer� inspekcyjn� (je�li u�ywasz RenderTexture na RawImage)
        if (inspectionCamera != null)
            inspectionCamera.gameObject.SetActive(false);

    }

    // Mo�esz wywo�a� to z systemu interakcji
    public static void RequestInspection(GameObject obj)
    {
        Instance.StartInspection(obj);
    }
    void CenterModel(GameObject model)
    {
        // Pobierz wszystkie meshe (MeshRenderer) w prefabie
        var renderers = model.GetComponentsInChildren<MeshRenderer>();

        if (renderers.Length == 0) return;

        // Oblicz bounds ca�ego modelu
        Bounds combinedBounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
            combinedBounds.Encapsulate(renderers[i].bounds);

        // �rodek bounding boxa w world space
        Vector3 center = combinedBounds.center;

        // Przesu� model tak, by �rodek bounds by� w punkcie (0,0,0) kontenera
        // (odwracamy przesuni�cie wzgl�dem rodzica)
        model.transform.position -= (center - inspectionContainer.position);
    }
}
