using UnityEngine;

public class BoxLockController : MonoBehaviour
{
    [SerializeField] private socketController[] sockets;
    [SerializeField] private int[] correctCode;
    [SerializeField] private bool unlockOnce = true;

    private bool isUnlocked = false;

    void Update()
    {
        if (isUnlocked && unlockOnce) return;

        if (IsCodeCorrect())
        {
            Unlock();
        }
    }

    private bool IsCodeCorrect()
    {
        if (sockets.Length != correctCode.Length) return false;

        foreach (socketController socket in sockets)
        {
            int id = socket.socketID;
            if (id < 0 || id >= correctCode.Length) return false;

            if (socket.socketNumber != correctCode[id]) return false;
        }

        return true;
    }

    private void Unlock()
    {
        isUnlocked = true;
        Debug.Log("K³ódka otwarta!");
        // tutaj mo¿esz np. uruchomiæ animacjê, dŸwiêk, aktywowaæ coœ:
        // GetComponent<Animator>().SetTrigger("Open");
        // gameObject.SetActive(false);
    }
}
