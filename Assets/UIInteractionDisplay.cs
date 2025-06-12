using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UIInteractionDisplay : MonoBehaviour
{
    public GameObject optionPrefab; // przypisz prefab InteractionOption
    public Transform container;     // przypisz InteractionPanel
    private List<OptionUI> activeOptions = new();
    private string[] hints = { "E", "F", "G", "H" };
    private class OptionUI
    {
        public GameObject root;
        public TMP_Text label;
        public TMP_Text hint;
        public Image progress;
    }

    public void ShowOptions(string[] options, Transform worldTarget)
    {
        ClearOptions();
        int i = 0;
        foreach (string option in options)
        {
            if (string.IsNullOrEmpty(option)) continue;

            GameObject go = Instantiate(optionPrefab, container);
            OptionUI opt = new OptionUI
            {
                root = go,
                label = go.transform.Find("Label").GetComponent<TMP_Text>(),
                hint = go.transform.Find("KeyHintText").GetComponent<TMP_Text>(),
                progress = go.transform.Find("ProgressBar").GetComponent<Image>()
            };
            opt.label.text = option;
            opt.hint.text = hints[i];
            opt.progress.fillAmount = 0f;
            activeOptions.Add(opt);
            i++;
        }

        gameObject.SetActive(true);

        //  Nowa pozycja przed kamer¹
        Camera cam = Camera.main;
        float distance = 1.6f;
        transform.position = cam.transform.position + cam.transform.forward * distance;

        LookAtPlayer();
    }


    public void HideOptions()
    {
        ClearOptions();
        gameObject.SetActive(false);
    }

    public void UpdateHoldProgress(int index, float progress)
    {
        if (index >= 0 && index < activeOptions.Count)
        {
            activeOptions[index].progress.fillAmount = Mathf.Clamp01(progress); 
        }
    }

    private void ClearOptions()
    {
        foreach (var opt in activeOptions)
            Destroy(opt.root);
        activeOptions.Clear();
    }

    private void LookAtPlayer()
    {
        Transform cam = Camera.main.transform;
        transform.LookAt(transform.position + cam.forward);
    }
    void LateUpdate()
    {
        if (!Camera.main || !gameObject.activeSelf) return;

        float distance = 1.6f;
        Vector3 targetPos = Camera.main.transform.position + Camera.main.transform.forward * distance;
        Quaternion targetRot = Quaternion.LookRotation(Camera.main.transform.forward);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
    }

}
