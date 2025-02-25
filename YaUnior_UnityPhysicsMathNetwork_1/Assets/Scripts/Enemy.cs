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
            // Ќаходим игрока по тегу "Player", если он не задан вручную
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player == null)
            {
                Debug.LogError("»грок не найден. ”становите тег 'Player' на игровом объекте или задайте его вручную.");
            }
        }

        Vector3 localPosition = transform.InverseTransformPoint(COM.position);
        rb.centerOfMass = localPosition;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // —мотрим в сторону игрока
        LookAtPlayer();

        // ƒвигаемс€ к игроку, сохран€€ дистанцию
        MoveToPlayer();
    }

    void LookAtPlayer()
    {
        // –ассчитываем направление к игроку
        Vector3 direction = (player.position - transform.position).normalized;

        // —оздаем поворот в сторону игрока, игнориру€ оси X и Z
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.Euler(0, angle, 0);

        // ѕлавно поворачиваем врага в сторону игрока
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void MoveToPlayer()
    {
        // –ассчитываем рассто€ние до игрока
        float distance = Vector3.Distance(transform.position, player.position);

        // ≈сли рассто€ние больше минимальной дистанции, двигаемс€ к игроку
        if (distance > stoppingDistance)
        {
            // –ассчитываем направление к игроку
            Vector3 direction = (player.position - transform.position).normalized;

            // ѕримен€ем силу дл€ движени€ врага в направлении игрока
            Vector3 moveDirection = new Vector3(direction.x, rb.velocity.y, direction.z).normalized * moveSpeed;

            // ќграничиваем скорость по оси Y, чтобы враг не ускор€лс€ вверх или вниз
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        }
        else
        {
            // ≈сли уже на нужной дистанции, останавливаем движение по ос€м X и Z
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}
