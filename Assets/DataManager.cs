using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public bool bodyInspected = false;
    public bool weaponFinded = false;
    public bool letterReaded = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
