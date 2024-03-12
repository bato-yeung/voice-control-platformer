using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField]
    private Button _quitButton;

    private void OnEnable()
    {
        _quitButton.onClick.AddListener(QuitButton_Clicked);
    }

    private void OnDisable()
    {
        _quitButton.onClick.RemoveListener(QuitButton_Clicked);
    }

    protected virtual void QuitButton_Clicked()
    {
        Application.Quit();
    }
}
