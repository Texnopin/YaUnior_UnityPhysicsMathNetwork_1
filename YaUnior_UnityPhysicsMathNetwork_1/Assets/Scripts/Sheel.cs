using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Sheel : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Rigidbody rb;
    private bool isUsingAlready;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        
        Vector3 localForward = Vector3.left; 
        Vector3 worldForward = transform.TransformDirection(localForward); 

        rb.AddForce(worldForward * 0.5f, ForceMode.Impulse);
        rb.AddTorque(worldForward * 0.5f, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isUsingAlready)
        {
            audioSource.Play();
            isUsingAlready = true;
        }
    }
}
