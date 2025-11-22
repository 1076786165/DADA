using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SwitchToggle : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    public GameObject SwitchOn;

    [SerializeField]
    public GameObject SwitchOff;

    public bool IsOn;

    public Action<bool> OnSwitchCallBack;

    void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SwitchSwith();
        OnSwitchCallBack?.Invoke(IsOn);
        
        AudioManager.I.PlaySound("common_anniuyinxiao");
    }

    void SwitchSwith()
    {
        SetSwitch(!IsOn);
    }

    public void SetSwitch(bool isOn)
    {
        IsOn = isOn;
        SwitchOn.SetActive(isOn);
        SwitchOff.SetActive(!isOn);

    }
    
}
