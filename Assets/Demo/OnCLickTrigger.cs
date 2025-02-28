using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OnCLickTrigger : MonoBehaviour
{
    public Toggle toggle;

    public bool setSiblingIndex;
    public int siblingIndex;
    public GameObject TriggerScreen;
    public GameObject TriggerScreenD2;

    public UnityEvent toggleTrueEvent;
    public UnityEvent toggleFalseEvent;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate { OnToggleClicked(); });
    }
    public void OnToggleClicked()
    {
        if (TriggerScreen)
        {
            TriggerScreen.SetActive(toggle.isOn);
            if (setSiblingIndex)
                TriggerScreen.transform.SetSiblingIndex(siblingIndex);
        }

        if (toggle.isOn)
        {
            toggleTrueEvent?.Invoke();
        }
        if (!toggle.isOn)
        {
            toggleFalseEvent?.Invoke();
        }
    }
}
