using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruct : MonoBehaviour
{
    [SerializeField] private float timeToDestruct;
    void Start()
    {
        Destroy(gameObject, timeToDestruct);
    }
}
