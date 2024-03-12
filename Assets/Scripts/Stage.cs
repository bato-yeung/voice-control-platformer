using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPoint;
    public GameObject StartPoint { get { return _startPoint; } }
}
