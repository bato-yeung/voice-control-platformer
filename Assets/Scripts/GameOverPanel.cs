using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private Button _hitArea;

    public event Action<object> Clicked;

    private void OnEnable()
    {
        _hitArea.onClick.AddListener(HitArea_Clicked);
    }

    private void OnDisable()
    {
        try
        {
            _hitArea.onClick.AddListener(HitArea_Clicked);
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning(ex);
        }
    }

    protected virtual void HitArea_Clicked()
    {
        Clicked?.Invoke(this);
    }
}
