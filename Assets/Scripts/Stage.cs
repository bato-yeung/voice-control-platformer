using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPoint;
    public GameObject StartPoint { get { return _startPoint; } }

    [SerializeField]
    private Goal _goal;
    public Goal Goal { get { return _goal; } } 
}
