using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stoppingDistance = 3f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Transform player;
    [SerializeField] private Transform COM;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (player == null)
        {
            // ������� ������ �� ���� "Player", ���� �� �� ����� �������
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player == null)
            {
                Debug.LogError("����� �� ������. ���������� ��� 'Player' �� ������� ������� ��� ������� ��� �������.");
            }
        }

        Vector3 localPosition = transform.InverseTransformPoint(COM.position);
        rb.centerOfMass = localPosition;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // ������� � ������� ������
        LookAtPlayer();

        // ��������� � ������, �������� ���������
        MoveToPlayer();
    }

    void LookAtPlayer()
    {
        // ������������ ����������� � ������
        Vector3 direction = (player.position - transform.position).normalized;

        // ������� ������� � ������� ������, ��������� ��� X � Z
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.Euler(0, angle, 0);

        // ������ ������������ ����� � ������� ������
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void MoveToPlayer()
    {
        // ������������ ���������� �� ������
        float distance = Vector3.Distance(transform.position, player.position);

        // ���� ���������� ������ ����������� ���������, ��������� � ������
        if (distance > stoppingDistance)
        {
            // ������������ ����������� � ������
            Vector3 direction = (player.position - transform.position).normalized;

            // ��������� ���� ��� �������� ����� � ����������� ������
            Vector3 moveDirection = new Vector3(direction.x, rb.velocity.y, direction.z).normalized * moveSpeed;

            // ������������ �������� �� ��� Y, ����� ���� �� ��������� ����� ��� ����
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        }
        else
        {
            // ���� ��� �� ������ ���������, ������������� �������� �� ���� X � Z
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}
