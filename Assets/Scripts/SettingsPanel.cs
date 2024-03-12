using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _devicesDropdown;

    [SerializeField]
    private AudioSource _targetAudioSource;

    [SerializeField]
    private Button _confirmButton;

    public event Action<object> Confirmed;

    private void Start()
    {
        int length = Microphone.devices.Length;
        Debug.Log($"{length} input device(s) detected");

        _devicesDropdown.AddOptions(Microphone.devices.ToList());
    }

    private void OnEnable()
    {
        _confirmButton.onClick.AddListener(ConfirmButton_Clicked);
    }

    private void OnDisable()
    {
        _confirmButton.onClick.RemoveListener(ConfirmButton_Clicked);
    }

    protected virtual void ConfirmButton_Clicked()
    {
        string deviceName = Microphone.devices[_devicesDropdown.value];
        _targetAudioSource.clip = Microphone.Start(deviceName, true, 1, 44100);

        while (!(Microphone.GetPosition(deviceName) > 0)) { }

        _targetAudioSource.Play();

        Confirmed?.Invoke(this);
    }
}
