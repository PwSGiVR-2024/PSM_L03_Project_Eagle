using UnityEngine;

public class InspectionManager : MonoBehaviour
{
    public static InspectionManager Instance { get; private set; }

    [SerializeField] private Transform inspectionContainer; // miejsce na �rodku ekranu
    [SerializeField] private GameObject blurPanel; // UI/Canvas z shaderem blur
    [SerializeField] private GameObject inspectionUI; // UI inspekcji (np. tekst: "Wyjd�: R")

    private GameObject inspectedObjectCopy;
    private bool isInspecting = false;
    private PlayerLook playerLook; // referencja do PlayerLook

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (isInspecting && Input.GetKeyDown(KeyCode.R))
        {
            EndInspection();
        }
    }

    public void StartInspection(GameObject objectToInspect)
    {

        if (isInspecting) return;
        // PAUZA gry
        Time.timeScale = 0f;
        isInspecting = true;

        // W��cz blur
        blurPanel.SetActive(true);

        // W��cz UI inspekcji
        inspectionUI.SetActive(true);

        // Skopiuj obiekt do inspekcji i ustaw na �rodek
        inspectedObjectCopy = Instantiate(objectToInspect, inspectionContainer);
        inspectedObjectCopy.transform.localPosition = Vector3.zero;
        inspectedObjectCopy.transform.localRotation = Quaternion.identity;

        // Dodaj komponent obracania
        inspectedObjectCopy.AddComponent<InspectionRotator>();
    }

    public void EndInspection()
    {
        // ODPAUZA gry
        Time.timeScale = 1f;
        isInspecting = false;

        // Wy��cz blur
        blurPanel.SetActive(false);

        // Wy��cz UI inspekcji
        inspectionUI.SetActive(false);

        // Usu� skopiowany obiekt
        if (inspectedObjectCopy != null)
        {
            Destroy(inspectedObjectCopy);
        }
    }

    // To mo�esz wywo�ywa� z systemu interakcji:
    public static void RequestInspection(GameObject obj)
    {
        Instance.StartInspection(obj);
    }
}
