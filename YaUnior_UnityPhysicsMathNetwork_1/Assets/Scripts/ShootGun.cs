using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject SheelPrefab;
    [SerializeField] private Transform shellPointSpawn;

    public void ReloadSound()
    {
        audioSource.Play();
    }

    public void ExtractShell()
    {
        Instantiate(SheelPrefab, shellPointSpawn.position, shellPointSpawn.rotation);
    }
}
