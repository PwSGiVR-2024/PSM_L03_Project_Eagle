using UnityEngine;

public class DeskController : MonoBehaviour
{
    [SerializeField] Animation animationDrawer;
    public int buttonCounter = 0;
    private void Update()
    {
        if (buttonCounter == 6)
        {
            animationDrawer.Play();
            buttonCounter = 0;
        }
    }
}
