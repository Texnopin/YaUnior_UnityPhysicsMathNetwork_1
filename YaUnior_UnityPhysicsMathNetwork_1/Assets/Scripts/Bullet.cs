using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _decalPrefab;
    private void Start()
    {
        // ���������� ���� ����� 2 �������, ���� ��� �� ����������� � ���-�� ������
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.LookRotation(contact.normal);
        Vector3 position = contact.point - contact.normal * 0.1f; 
        GameObject decal = Instantiate(_decalPrefab, position, rotation);
        decal.transform.parent = collision.gameObject.transform; 


        Destroy(gameObject);
    }
}
