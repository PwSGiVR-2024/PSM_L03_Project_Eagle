using Unity.VisualScripting;
using UnityEngine;

public class FlagsManager : MonoBehaviour
{
    public bool dialog1 = false;
    [SerializeField] GameObject car;
    private void Update()
    {
        if (dialog1)
        {
            car.SetActive(true);
        }
    }
}
