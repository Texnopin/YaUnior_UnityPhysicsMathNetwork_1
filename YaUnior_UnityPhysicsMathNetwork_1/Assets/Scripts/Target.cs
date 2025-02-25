using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void OnCollisionEnter(Collision collision)
    {
        animator.SetTrigger("Hit");
    }
}
