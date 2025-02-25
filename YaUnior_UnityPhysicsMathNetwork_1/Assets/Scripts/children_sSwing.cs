using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class children_sSwing : MonoBehaviour
{
    [SerializeField] private HingeJoint hingeJoint;
    private float minAngle = -30f; // Минимальный угол
    private float maxAngle = 30f; // Максимальный угол
    [SerializeField] private float speed = 1f; // Скорость раскачивания

    private float angle = 0f; // Текущий угол
    private bool isIncreasing = true; // Флаг для направления раскачивания

    public bool allowed = false;

    private void Start()
    {
        hingeJoint.GetComponent<HingeJoint>();
    }

    private void Update()
    {
        if (allowed)
        {
            if (isIncreasing)
            {
                angle += speed * Time.deltaTime;
                if (angle >= maxAngle)
                {
                    angle = maxAngle;
                    isIncreasing = false;
                }
            }
            else
            {
                angle -= speed * Time.deltaTime;
                if (angle <= minAngle)
                {
                    angle = minAngle;
                    isIncreasing = true;
                }
            }

            JointSpring spring = hingeJoint.spring;
            spring.targetPosition = angle;
            hingeJoint.spring = spring;
        }

    }
}
